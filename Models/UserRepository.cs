using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ST10157545_MunicipalServicesApp.Models
{
    public static class UserRepository
    {
        private static readonly List<User> _users = new List<User>();
        static UserRepository()
        {
            SeedDemoAccounts();
        }
        public static IReadOnlyList<User> Users
        {
            get { return _users; }
        }
        private static void SeedDemoAccounts()
        {
            _users.Add(new User("admin", "Thandi Mokoena (Demo Admin)", "admin@demo.municipality.gov.za", UserRole.Admin, "Admin@123"));
            _users.Add(new User("staff", "Sipho Dlamini (Demo Staff)", "staff@demo.municipality.gov.za", UserRole.Staff, "Staff@123"));
            _users.Add(new User("citizen", "Lindiwe Khumalo (Demo Citizen)", "citizen@demo.example.com", UserRole.Citizen, "Citizen@123"));
        }
        public static bool UsernameExists(string username)
        {
            return _users.Any(u => string.Equals(u.Username, username, StringComparison.OrdinalIgnoreCase));
        }
        /// <summary>
        /// Citizen self-registration. Staff/Admin accounts are not created
        /// through this path - they are seeded as demo data or created by an
        /// existing Admin via <see cref="CreateStaffAccount"/>.
        /// </summary>
        public static User RegisterCitizen(string username, string fullName, string email, string password)
        {
            if (UsernameExists(username))
            {
                throw new InvalidOperationException("That username is already taken. Please choose another one.");
            }
            User newUser = new User(username, fullName, email, UserRole.Citizen, password);
            _users.Add(newUser);
            return newUser;
        }
        /// <summary>
        /// Lets a logged-in Admin create an account for a new staff member or
        /// fellow admin, matching the brief's requirement that "accounts can
        /// be created for" municipal staff rather than self-registered.
        /// </summary>
        public static User CreateStaffAccount(string username, string fullName, string email, string password, UserRole role)
        {
            if (role == UserRole.Citizen)
            {
                throw new ArgumentException("Use RegisterCitizen for citizen accounts.");
            }
            if (UsernameExists(username))
            {
                throw new InvalidOperationException("That username is already taken. Please choose another one.");
            }
            User newUser = new User(username, fullName, email, role, password);
            _users.Add(newUser);
            return newUser;
        }
        public static User Authenticate(string username, string password)
        {
            User candidate = _users.FirstOrDefault(u => string.Equals(u.Username, username, StringComparison.OrdinalIgnoreCase));
            if (candidate != null && candidate.CheckPassword(password))
            {
                return candidate;
            }
            return null;
        }
    }
}
