using ST10157545_MunicipalServicesApp;
using ST10157545_MunicipalServicesApp.Models;
using System;
using System.Windows.Forms;

namespace ST10157545_MunicipalServicesApp
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application. Shows the login screen,
        /// then the main menu for whoever logged in. Logging out returns to
        /// the login screen; closing the login screen or the main menu with
        /// the window's close button exits the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            bool keepRunning = true;
            while (keepRunning)
            {
                User user;
                using (LoginForm login = new LoginForm())
                {
                    if (login.ShowDialog() != DialogResult.OK)
                    {
                        break;
                    }
                    user = login.AuthenticatedUser;
                }

                using (MainForm main = new MainForm(user))
                {
                    Application.Run(main);
                    keepRunning = main.LoggedOut;
                }
            }
        }
    }
}
