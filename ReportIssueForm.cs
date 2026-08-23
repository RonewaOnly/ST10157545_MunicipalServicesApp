using ST10157545_MunicipalServicesApp.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ST10157545_MunicipalServicesApp
{
    public partial class ReportIssueForm : Form
    {
        private readonly List<string> _attachementPaths = new List<string>();
        public ReportIssueForm()
        {
            InitializeComponent();
            progressEngagement.Minimum = 0;
            progressEngagement.Maximum = 100;
            UpdateEngagementProgress();

        }

        private void OnFormFieldChanged(object sender, EventArgs e)
        {
            UpdateEngagementProgress();
        }

        private void UpdateEngagementProgress()
        {
            int completedSections = 0;
            const int totalSections = 4;

            if (!string.IsNullOrWhiteSpace(txtLocation.Text))
            {
                completedSections++;
            }

            if(cmbCategory.SelectedIndex != -1)
            {
                completedSections++;
            }

            if (!string.IsNullOrWhiteSpace(rtbDescription.Text))
            {
                completedSections++;
            }

            if (_attachementPaths.Count > 0) 
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
                    lblEngagement.Text = "Nice work - add a description so the municaipality knows what's wrong.";
                    break;
                case 3:
                    lblEngagement.Text = "Almost there! Attach a photo if you one , or submit now.";
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
                    if (!_attachementPaths.Contains(fileName))
                    {
                        _attachementPaths.Add(fileName);
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
            if(missingFields.Count > 0)
            {
                MessageBox.Show(
                    "Please complete the following field(s) before submiting: \n\n -"+
                    string.Join(", ", missingFields),
                    "Incomplete Report",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                    
                    );
                return;
            }

            Issue newIssue = new Issue(
                txtLocation.Text.Trim(),
                cmbCategory.SelectedItem.ToString(), 
                rtbDescription.Text.Trim(),
                new List<string>(_attachementPaths)
                );

            IssueRepository.Add(newIssue);

            MessageBox.Show(
                "Thank you! Your issue has been reported successfully. \n\n"+
                "Reference number: "+ newIssue.Id.ToString().Substring(0,8).ToUpper() + "\n"+
                "Total issues reported this session: " + IssueRepository.Count,
                "Report Submitted",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
                );
            ResetForm();
        }

        private void ResetForm()
        {
            txtLocation.Clear();
            cmbCategory.SelectedIndex = -1;
            rtbDescription.Clear();
            lstAttachments.Items.Clear();
            _attachementPaths.Clear();
            UpdateEngagementProgress();
            txtLocation.Focus();
        }

        private void btnBackToMain_Click( object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
