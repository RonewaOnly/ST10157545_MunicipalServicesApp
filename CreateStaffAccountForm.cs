using ST10157545_MunicipalServicesApp.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ST10157545_MunicipalServicesApp
{
    /** <summary>
    /// Opened from the Admin Dashboard so an Admin can create an account for
    /// a new staff member (or a fellow admin), matching the brief's
    /// requirement that staff accounts are created for them rather than
    /// self-registered the way citizen accounts are.
     </summary>*/
    public partial class CreateStaffAccountForm : Form
    {
        public CreateStaffAccountForm()
        {
            InitializeComponent();
            cmbRole.Items.Add(UserRole.Staff);
            cmbRole.Items.Add(UserRole.Admin);
            cmbRole.SelectedIndex = 0;
        }
        private void btnCreate_Click(object sender, EventArgs e)
        {
            lblError.Text = string.Empty;
            string fullName = txtFullName.Text.Trim();
            string email = txtEmail.Text.Trim();
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text;
            if (string.IsNullOrWhiteSpace(fullName) || string.IsNullOrWhiteSpace(email) ||
                string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                lblError.Text = "Please complete every field.";
                return;
            }
            if (!Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                lblError.Text = "Please enter a valid email address.";
                return;
            }
            if (password.Length < 6)
            {
                lblError.Text = "Temporary password must be at least 6 characters long.";
                return;
            }
            try
            {
                UserRole role = (UserRole)cmbRole.SelectedItem;
                User created = UserRepository.CreateStaffAccount(username, fullName, email, password, role);
                MessageBox.Show(
                    "Account created for " + created.FullName + " (" + created.Role + ").\n\n" +
                    "Username: " + created.Username + "\nTemporary password: " + password,
                    "Account Created",
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
