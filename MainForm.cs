using ST10157545_MunicipalServicesApp;
using ST10157545_MunicipalServicesApp.Models;
using System;
using System.Windows.Forms;

namespace ST10157545_MunicipalServicesApp
{
    /// <summary>
    /// The application's main menu, shown after login. Presents the three
    /// top-level tasks required by the brief: Report Issues, Local Events and
    /// Announcements, and Service Request Status. What each button does
    /// depends on the logged-in user's role:
    ///   - Citizens can report issues, browse events and track their own requests.
    ///   - Staff and Admins browse events and manage every citizen's requests
    ///     (reporting issues is a citizen action, so it is disabled for them).
    /// The window is resizable (Design Consideration: Responsiveness).
    /// </summary>
    public partial class MainForm : Form
    {
        private readonly User _currentUser;

        /// <summary>True when the user chose Log Out (so Program shows the login screen again) rather than closing the window.</summary>
        public bool LoggedOut { get; private set; }

        public MainForm(User currentUser)
        {
            _currentUser = currentUser;
            InitializeComponent();

            lblSubtitle.Text = "Welcome, " + currentUser.FullName + " (" + currentUser.Role + ")";

            if (currentUser.Role != UserRole.Citizen)
            {
                btnReportIssues.Enabled = false;
                btnReportIssues.BackColor = System.Drawing.SystemColors.Control;
                btnReportIssues.ForeColor = System.Drawing.SystemColors.GrayText;
                btnReportIssues.Text = "Report Issues (citizen accounts only)";
                btnServiceStatus.Text = "Manage Service Requests";
            }
        }

        private void btnReportIssues_Click(object sender, EventArgs e)
        {
            using (ReportIssueForm reportForm = new ReportIssueForm(_currentUser))
            {
                this.Hide();
                reportForm.ShowDialog();
                this.Show();
            }
        }

        private void btnLocalEvents_Click(object sender, EventArgs e)
        {
            using (LocalEventsForm eventsForm = new LocalEventsForm())
            {
                this.Hide();
                eventsForm.ShowDialog();
                this.Show();
            }
        }

        private void btnServiceStatus_Click(object sender, EventArgs e)
        {
            using (Form statusForm = CreateStatusForm())
            {
                this.Hide();
                statusForm.ShowDialog();
                this.Show();
            }
        }

        private Form CreateStatusForm()
        {
            if (_currentUser.Role == UserRole.Citizen)
            {
                return new ServiceRequestStatusForm(_currentUser);
            }

            return new AdminDashboardForm(_currentUser);
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            LoggedOut = true;
            Close();
        }
    }
}
