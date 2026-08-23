using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ST10157545_MunicipalServicesApp
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void btnReportIssues_Click(object sender, EventArgs e)
        {
            // Handle report issues button click
            using(ReportIssueForm reportIssueForm = new ReportIssueForm())
            {
                this.Hide(); // Hide the main form while the report issue form is open
                reportIssueForm.ShowDialog();
                this.Show();
            }
        }
    }
}
