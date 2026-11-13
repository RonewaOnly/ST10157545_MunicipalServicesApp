using ST10157545_MunicipalServicesApp.Models;
using ST10157545_MunicipalServicesApp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace ST10157545_MunicipalServicesApp
{
    /// <summary>
    /// Staff / Admin view of "Service Request Status". Lets municipal staff
    /// browse every request in three different orders (each served by a
    /// different tree), change a request's status, reply to the citizen,
    /// see the heap-driven urgent queue, and use the ward graph for
    /// traversal and route planning. Only Admins see the button for creating
    /// further staff accounts.
    /// </summary>
    public partial class AdminDashboardForm : Form
    {
        private const string SortNewest = "Newest first (AVL tree)";
        private const string SortPriority = "Highest priority first (Red-Black tree)";
        private const string SortTicket = "Ticket number (Binary Search Tree)";
        private const string AllStatuses = "All statuses";

        private readonly User _currentUser;

        public AdminDashboardForm(User currentUser)
        {
            _currentUser = currentUser;
            InitializeComponent();

            btnCreateAccount.Visible = currentUser.Role == UserRole.Admin;

            cmbSort.Items.AddRange(new object[] { SortNewest, SortPriority, SortTicket });
            cmbSort.SelectedIndex = 0;

            cmbStatusFilter.Items.Add(AllStatuses);
            foreach (RequestStatus status in Enum.GetValues(typeof(RequestStatus)))
            {
                cmbStatusFilter.Items.Add(status);
                cmbNewStatus.Items.Add(status);
            }
            cmbStatusFilter.SelectedIndex = 0;

            cmbWard.Items.AddRange(LocationGraph.Wards);
            cmbWard.SelectedIndex = 0;

            RefreshRequests();
        }

        private void FilterChanged(object sender, EventArgs e)
        {
            if (cmbSort.SelectedIndex < 0 || cmbStatusFilter.SelectedIndex < 0) return;
            RefreshRequests();
        }

        private void RefreshRequests()
        {
            Issue previouslySelected = lvRequests.SelectedItems.Count > 0 ? (Issue)lvRequests.SelectedItems[0].Tag : null;

            IEnumerable<Issue> source;
            switch (cmbSort.SelectedItem as string)
            {
                case SortPriority:
                    source = IssueRepository.AllByPriorityDescending();
                    break;
                case SortTicket:
                    source = IssueRepository.AllByTicketNumber();
                    break;
                default:
                    source = IssueRepository.AllByDateDescending();
                    break;
            }

            if (cmbStatusFilter.SelectedItem is RequestStatus)
            {
                RequestStatus wanted = (RequestStatus)cmbStatusFilter.SelectedItem;
                source = source.Where(i => i.Status == wanted);
            }

            lvRequests.BeginUpdate();
            lvRequests.Items.Clear();
            foreach (Issue issue in source)
            {
                ListViewItem item = new ListViewItem(issue.TicketReference);
                item.SubItems.Add(issue.DateReported.ToString("dd MMM yyyy HH:mm"));
                item.SubItems.Add(issue.Category);
                item.SubItems.Add(issue.Location);
                item.SubItems.Add(issue.Priority.ToString());
                item.SubItems.Add(issue.Status.ToString());
                item.SubItems.Add(string.IsNullOrEmpty(issue.CreatedByUsername) ? "(unknown)" : issue.CreatedByUsername);
                item.Tag = issue;
                if (issue == previouslySelected) item.Selected = true;
                lvRequests.Items.Add(item);
            }
            lvRequests.EndUpdate();

            if (lvRequests.SelectedItems.Count == 0)
            {
                txtDetails.Text = "Select a request to view its details, change its status or respond to the citizen.";
            }
        }

        private void lvRequests_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvRequests.SelectedItems.Count == 0) return;

            Issue issue = (Issue)lvRequests.SelectedItems[0].Tag;
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(issue.TicketReference + " | " + issue.Category + " | " + issue.Location + " | Priority: " + issue.Priority);
            sb.AppendLine("Reported by " + (issue.CreatedByUsername ?? "(unknown)") + " on " + issue.DateReported.ToString("dd MMM yyyy HH:mm"));
            sb.AppendLine(issue.Description);
            if (!string.IsNullOrWhiteSpace(issue.StaffResponse))
            {
                sb.AppendLine("Last response by " + issue.RespondedByUsername + ": " + issue.StaffResponse);
            }
            txtDetails.Text = sb.ToString();

            cmbNewStatus.SelectedItem = issue.Status;
            txtResponse.Text = issue.StaffResponse ?? string.Empty;
        }

        private void btnSaveUpdate_Click(object sender, EventArgs e)
        {
            if (lvRequests.SelectedItems.Count == 0)
            {
                MessageBox.Show("Please select a request first.", "No Request Selected",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (!(cmbNewStatus.SelectedItem is RequestStatus))
            {
                MessageBox.Show("Please choose a status.", "Status Required",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Issue issue = (Issue)lvRequests.SelectedItems[0].Tag;
            IssueRepository.UpdateStatus(issue, (RequestStatus)cmbNewStatus.SelectedItem);

            string response = txtResponse.Text.Trim();
            if (response.Length > 0 && response != (issue.StaffResponse ?? string.Empty))
            {
                IssueRepository.RecordResponse(issue, response, _currentUser.Username);
            }

            RefreshRequests();
            MessageBox.Show("Request " + issue.TicketReference + " has been updated. The citizen will see the new status and response.",
                "Update Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void tabs_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabs.SelectedTab == tabUrgent)
            {
                RefreshUrgentTab();
            }
        }

        private void RefreshUrgentTab()
        {
            lstUrgent.Items.Clear();
            List<Issue> urgent = IssueRepository.TopUrgentOpenRequests(8);
            if (urgent.Count == 0)
            {
                lstUrgent.Items.Add("No open requests - nothing urgent right now.");
            }
            foreach (Issue issue in urgent)
            {
                lstUrgent.Items.Add(string.Format("{0}  [{1}]  {2} - {3}  ({4})",
                    issue.TicketReference, issue.Priority, issue.Category, issue.Location, issue.Status));
            }

            Dictionary<string, int> counts = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
            foreach (Issue issue in IssueRepository.Issues)
            {
                int current;
                counts.TryGetValue(issue.Category, out current);
                counts[issue.Category] = current + 1;
            }

            StringBuilder sb = new StringBuilder();
            foreach (string line in CategoryTree.PreOrderHierarchy())
            {
                int count;
                counts.TryGetValue(line.Trim(), out count);
                bool isLeaf = Array.IndexOf(CategoryTree.LeafCategories, line.Trim()) >= 0;
                sb.AppendLine(isLeaf ? line + "  (" + count + ")" : line);
            }
            txtCategoryTree.Text = sb.ToString().Replace("\r\n", "\n").Replace("\n", Environment.NewLine);
        }

        private void btnBfs_Click(object sender, EventArgs e)
        {
            string start = (string)cmbWard.SelectedItem;
            txtGraphOutput.Text = "Breadth-first traversal from " + start + ":" + Environment.NewLine +
                FormatOrder(LocationGraph.BreadthFirstSearch(start));
        }

        private void btnDfs_Click(object sender, EventArgs e)
        {
            string start = (string)cmbWard.SelectedItem;
            txtGraphOutput.Text = "Depth-first traversal from " + start + ":" + Environment.NewLine +
                FormatOrder(LocationGraph.DepthFirstSearch(start));
        }

        private static string FormatOrder(List<string> wards)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < wards.Count; i++)
            {
                sb.AppendLine((i + 1) + ". " + wards[i]);
            }
            return sb.ToString();
        }

        private void btnNearby_Click(object sender, EventArgs e)
        {
            string start = (string)cmbWard.SelectedItem;
            List<string> wards = LocationGraph.WardsWithinHops(start, 2);
            wards.Insert(0, start);

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Open requests in " + start + " and wards within 2 hops:");
            sb.AppendLine();

            int total = 0;
            foreach (string ward in wards)
            {
                List<Issue> open = IssueRepository.Issues
                    .Where(i => string.Equals(i.Location, ward, StringComparison.OrdinalIgnoreCase)
                                && i.Status != RequestStatus.Resolved && i.Status != RequestStatus.Closed)
                    .ToList();

                if (open.Count == 0) continue;
                sb.AppendLine(ward + " (" + open.Count + ")");
                foreach (Issue issue in open)
                {
                    sb.AppendLine("    " + issue.TicketReference + "  [" + issue.Priority + "]  " + issue.Category);
                }
                total += open.Count;
            }

            if (total == 0)
            {
                sb.AppendLine("None - no open requests in this part of the network.");
            }

            txtGraphOutput.Text = sb.ToString();
        }

        private void btnMst_Click(object sender, EventArgs e)
        {
            List<string> required = LocationGraph.Wards
                .Where(w => IssueRepository.WardsWithOpenRequests().Contains(w))
                .ToList();

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Wards with open requests: " + required.Count);
            foreach (string ward in required) sb.AppendLine("  - " + ward);
            sb.AppendLine();

            MstResult mst = LocationGraph.MinimumSpanningTree(required);
            if (mst.Edges.Count == 0)
            {
                sb.AppendLine("A route needs at least two wards with open requests.");
            }
            else
            {
                sb.AppendLine("Cheapest set of road links connecting them (Prim's algorithm):");
                foreach (GraphEdge edge in mst.Edges)
                {
                    sb.AppendLine("  " + edge.From + "  <->  " + edge.To + "   " + edge.DistanceKm + " km");
                }
                sb.AppendLine();
                sb.AppendLine("Total distance: " + mst.TotalDistanceKm + " km");
            }

            txtGraphOutput.Text = sb.ToString();
        }

        private void btnCreateAccount_Click(object sender, EventArgs e)
        {
            using (CreateStaffAccountForm form = new CreateStaffAccountForm())
            {
                form.ShowDialog(this);
            }
        }

        private void btnBackToMain_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
