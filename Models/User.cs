using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ST10157545_MunicipalServicesApp.Models
{

    /**
     * <summary>
     *      AA registered account. No database is used anywhere in this project, so 
     *      every User instance lives only in the in-memory List &It;User&gt; owned by
     *      UserRepository for the lifetime of the running application
     * </summary>
     * 
     * **/
    public class User
    {
        public Guid Id { get; private set; }
        public string Username { get; set; }

        public string FullName { get; set; }
        public string Email { get; set; }

        public UserRole Role { get; set; }

        public string PasswordSalt { get; set; }

        public string PasswordHash { get; set; }

        public DateTime DateRegistered { get; private set; }

        public User(string username, string fullname, string email, UserRole role, string password)
        {
            Id = Guid.NewGuid();
            Username = username;
            FullName = fullname;
            Email = email;
            Role = role;
            DateRegistered = DateTime.Now;

            PasswordSalt = PasswordHelper.GenerateSalt();
            PasswordHash = PasswordHelper.Hash(password, PasswordSalt);
        }

        public bool CheckPassword(string password)
        {
            return PasswordHelper.Verify(password, PasswordSalt, PasswordHash);
        }
    }
}
