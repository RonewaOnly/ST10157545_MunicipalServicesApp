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
           using(ReportIssueForm reportForm = new ReportIssueForm(this))
            {
                this.Hide();
                reportForm.ShowDialog();
                this.Show();
            }
        }

        private void btnLocalEvents_Click(object sender, EventArgs e)
        {
            using(LocalEventsForm eventsForm = new LocalEventsForm())
            {
                this.Hide();
                eventsForm.ShowDialog();
                this.Show();
            }
        }
    }
}
