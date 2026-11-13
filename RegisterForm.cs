using ST10157545_MunicipalServicesApp.Models;
using System;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace ST10157545_MunicipalServicesApp
{
    /// <summary>
    /// Citizen self-registration. Only the Citizen role can be created from
    /// this form - Staff/Admin accounts are seeded demo data or created for
    /// them by an existing Admin from the Admin Dashboard.
    /// </summary>
    public partial class RegisterForm : Form
    {
        public User RegisteredUser { get; private set; }

        public RegisterForm()
        {
            InitializeComponent();
        }

        private void btnCreateAccount_Click(object sender, EventArgs e)
        {
            lblError.Text = string.Empty;

            string fullName = txtFullName.Text.Trim();
            string email = txtEmail.Text.Trim();
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text;
            string confirmPassword = txtConfirmPassword.Text;

            if (string.IsNullOrWhiteSpace(fullName) || string.IsNullOrWhiteSpace(email) ||
                string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                lblError.Text = "Please complete every field before creating your account.";
                return;
            }

            if (!Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                lblError.Text = "Please enter a valid email address.";
                return;
            }

            if (username.Length < 4)
            {
                lblError.Text = "Username must be at least 4 characters long.";
                return;
            }

            if (password.Length < 6)
            {
                lblError.Text = "Password must be at least 6 characters long.";
                return;
            }

            if (password != confirmPassword)
            {
                lblError.Text = "Passwords do not match.";
                return;
            }

            try
            {
                RegisteredUser = UserRepository.RegisterCitizen(username, fullName, email, password);
                MessageBox.Show(
                    "Account created successfully. Welcome, " + fullName + "!",
                    "Registration Complete",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                DialogResult = DialogResult.OK;
                Close();
            }
            catch (InvalidOperationException ex)
            {
                lblError.Text = ex.Message;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
