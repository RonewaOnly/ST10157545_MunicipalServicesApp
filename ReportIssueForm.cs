using ST10157545_MunicipalServicesApp.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace ST10157545_MunicipalServicesApp
{
    /// <summary>
    /// Handles the "Report Issues" workflow: capturing the location, category,
    /// description and optional attachments for a citizen-reported issue, and
    /// giving the user real-time feedback on how complete their report is.
    ///
    /// The engagement feature (progress bar + encouraging label) implements the
    /// "real-time status feedback" user engagement strategy chosen and justified
    /// in the accompanying research document (Task 1).
    /// </summary>
    public partial class ReportIssueForm : Form
    {
        private readonly List<string> _attachmentPaths = new List<string>();
        private readonly User _currentUser;

        public ReportIssueForm(User currentUser)
        {
            _currentUser = currentUser;
            InitializeComponent();
            progressEngagement.Minimum = 0;
            progressEngagement.Maximum = 100;
            UpdateEngagementProgress();
        }

        /// <summary>
        /// Fired whenever a tracked input control changes. Recalculates the
        /// completeness of the report and updates the engagement feature.
        /// </summary>
        private void OnFormFieldChanged(object sender, EventArgs e)
        {
            UpdateEngagementProgress();
        }

        /// <summary>
        /// Recalculates how complete the current report is (0-100%) and updates
        /// the progress bar and the accompanying encouraging message.
        /// </summary>
        private void UpdateEngagementProgress()
        {
            int completedSections = 0;
            const int totalSections = 4;

            if (!string.IsNullOrWhiteSpace(txtLocation.Text))
            {
                completedSections++;
            }

            if (cmbCategory.SelectedIndex != -1)
            {
                completedSections++;
            }

            if (!string.IsNullOrWhiteSpace(rtbDescription.Text))
            {
                completedSections++;
            }

            if (_attachmentPaths.Count > 0)
            {
                completedSections++;
            }

            int percentage = (completedSections * 100) / totalSections;
            progressEngagement.Value = percentage;

            switch (completedSections)
            {
                case 0:
                    lblEngagement.Text = "Let's get this issue reported - fill in the fields below.";
                    break;
                case 1:
                    lblEngagement.Text = "Good start! Now select a category for the issue.";
                    break;
                case 2:
                    lblEngagement.Text = "Nice work - add a description so the municipality knows what's wrong.";
                    break;
                case 3:
                    lblEngagement.Text = "Almost there! Attach a photo if you have one, or submit now.";
                    break;
                case 4:
                    lblEngagement.Text = "Report is complete - thank you for helping improve your community!";
                    break;
            }
        }

        private void btnAttach_Click(object sender, EventArgs e)
        {
            if (openFileDialogAttach.ShowDialog() == DialogResult.OK)
            {
                foreach (string fileName in openFileDialogAttach.FileNames)
                {
                    if (!_attachmentPaths.Contains(fileName))
                    {
                        _attachmentPaths.Add(fileName);
                        lstAttachments.Items.Add(Path.GetFileName(fileName));
                    }
                }

                UpdateEngagementProgress();
            }
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            List<string> missingFields = new List<string>();

            if (string.IsNullOrWhiteSpace(txtLocation.Text))
            {
                missingFields.Add("Location");
            }

            if (cmbCategory.SelectedIndex == -1)
            {
                missingFields.Add("Category");
            }

            if (string.IsNullOrWhiteSpace(rtbDescription.Text))
            {
                missingFields.Add("Description");
            }

            if (missingFields.Count > 0)
            {
                MessageBox.Show(
                    "Please complete the following field(s) before submitting:\n\n- " +
                    string.Join("\n- ", missingFields),
                    "Incomplete Report",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            Issue newIssue = new Issue(
                txtLocation.Text.Trim(),
                cmbCategory.SelectedItem.ToString(),
                rtbDescription.Text.Trim(),
                new List<string>(_attachmentPaths));

            newIssue.CreatedByUsername = _currentUser.Username;
            IssueRepository.Add(newIssue);

            MessageBox.Show(
                "Thank you! Your issue has been reported successfully.\n\n" +
                "Ticket number: " + newIssue.TicketReference + " (priority: " + newIssue.Priority + ")\n" +
                "Track its progress any time under Service Request Status.",
                "Report Submitted",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);

            ResetForm();
        }

        /// <summary>
        /// Clears all input fields so the user can report another issue without
        /// needing to reopen the form.
        /// </summary>
        private void ResetForm()
        {
            txtLocation.SelectedIndex = -1;
            cmbCategory.SelectedIndex = -1;
            rtbDescription.Clear();
            lstAttachments.Items.Clear();
            _attachmentPaths.Clear();
            UpdateEngagementProgress();
            txtLocation.Focus();
        }

        private void btnBackToMain_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
