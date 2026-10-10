using System;
using System.Windows.Forms;

namespace ST10157545_MunicipalServicesApp
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void BtnReportIssues_Click(object sender, EventArgs e)
        {
            ReportIssueForm reportIssueForm = new ReportIssueForm(this);
            reportIssueForm.Show();
            this.Hide();
        }
    }
}
