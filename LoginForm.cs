using ST10157545_MunicipalServicesApp.Models;
using ST10157545_MunicipalServicesApp;
using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace ST10157545_MunicipalServicesApp
{
    /// <summary>
    /// The application's entry screen. Every user - citizen, staff or admin -
    /// authenticates here against the in-memory UserRepository before the
    /// main menu is shown. A citizen who has not registered yet can open
    /// RegisterForm from this screen; staff/admin accounts are either one of
    /// the seeded demo accounts or were created for them by an admin.
    /// </summary>
    public partial class LoginForm : Form
    {
        public User AuthenticatedUser { get; private set; }

        public LoginForm()
        {
            InitializeComponent();
        }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnLogin_Click(sender, e);
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            lblError.Text = string.Empty;

            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text;

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                lblError.Text = "Please enter both a username and a password.";
                return;
            }

            User user = UserRepository.Authenticate(username, password);
            if (user == null)
            {
                lblError.Text = "Incorrect username or password. Please try again.";
                txtPassword.Clear();
                txtPassword.Focus();
                return;
            }

            AuthenticatedUser = user;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            using (RegisterForm registerForm = new RegisterForm())
            {
                if (registerForm.ShowDialog() == DialogResult.OK && registerForm.RegisteredUser != null)
                {
                    AuthenticatedUser = registerForm.RegisteredUser;
                    DialogResult = DialogResult.OK;
                    Close();
                }
            }
        }
    }
}
