using ST10157545_MunicipalServicesApp.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace ST10157545_MunicipalServicesApp
{
    /// <summary>
    /// Citizen view of "Service Request Status". Lists the requests the
    /// logged-in citizen has submitted and lets them track any one of them by
    /// its unique ticket number. The ticket lookup goes through the
    /// Binary Search Tree held by IssueRepository rather than scanning the
    /// whole list, and a citizen can only open tickets that they created.
    /// </summary>
    public partial class ServiceRequestStatusForm : Form
    {
        private readonly User _currentUser;

        public ServiceRequestStatusForm(User currentUser)
        {
            _currentUser = currentUser;
            InitializeComponent();
            ShowAllMine();
        }

        private void ShowAllMine()
        {
            PopulateList(IssueRepository.ByCitizen(_currentUser.Username));
            txtDetails.Text = "Select a request above to see its full details and any response from the municipality.";
        }

        private void PopulateList(List<Issue> issues)
        {
            lvRequests.Items.Clear();
            foreach (Issue issue in issues)
            {
                ListViewItem item = new ListViewItem(issue.TicketReference);
                item.SubItems.Add(issue.DateReported.ToString("dd MMM yyyy HH:mm"));
                item.SubItems.Add(issue.Category);
                item.SubItems.Add(issue.Location);
                item.SubItems.Add(issue.Status.ToString());
                item.Tag = issue;
                lvRequests.Items.Add(item);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string digits = txtTicketSearch.Text.Trim().ToUpperInvariant().Replace("SR-", string.Empty);
            int ticketNumber;
            if (!int.TryParse(digits, out ticketNumber))
            {
                MessageBox.Show("Please enter a valid ticket number, for example 1001 or SR-01001.",
                    "Invalid Ticket Number", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Issue found;
            bool exists = IssueRepository.TryFindByTicketNumber(ticketNumber, out found);
            bool ownedByUser = exists && string.Equals(found.CreatedByUsername, _currentUser.Username, StringComparison.OrdinalIgnoreCase);

            if (!ownedByUser)
            {
                // Same message for "doesn't exist" and "not yours" so ticket numbers can't be probed.
                MessageBox.Show("No request with that ticket number was found on your account.",
                    "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            PopulateList(new List<Issue> { found });
            lvRequests.Items[0].Selected = true;
        }

        private void btnShowAll_Click(object sender, EventArgs e)
        {
            txtTicketSearch.Clear();
            ShowAllMine();
        }

        private void lvRequests_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvRequests.SelectedItems.Count == 0) return;

            Issue issue = (Issue)lvRequests.SelectedItems[0].Tag;
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Ticket: " + issue.TicketReference + "    Priority: " + issue.Priority + "    Status: " + issue.Status);
            sb.AppendLine("Reported: " + issue.DateReported.ToString("dd MMM yyyy HH:mm") + " at " + issue.Location);
            sb.AppendLine();
            sb.AppendLine(issue.Description);
            sb.AppendLine();

            if (string.IsNullOrWhiteSpace(issue.StaffResponse))
            {
                sb.AppendLine("Municipal response: none yet - we will update this ticket as soon as it is reviewed.");
            }
            else
            {
                sb.AppendLine("Municipal response from " + issue.RespondedByUsername + " (" +
                    issue.DateResponded.Value.ToString("dd MMM yyyy HH:mm") + "):");
                sb.AppendLine(issue.StaffResponse);
            }

            txtDetails.Text = sb.ToString().Replace("\r\n", "\n").Replace("\n", Environment.NewLine);
        }

        private void btnBackToMain_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
