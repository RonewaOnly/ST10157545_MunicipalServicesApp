using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ST10157545_MunicipalServicesApp.Models
{
    public static class PasswordHelper
    {
        public static string GenerateSalt()
        {
            byte[] saltBytes = new byte[16];
            using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(saltBytes);
            }
            return Convert.ToBase64String(saltBytes);
        }
        public static string Hash(string password, string salt)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] combined = Encoding.UTF8.GetBytes(salt + password);
                byte[] hashBytes = sha256.ComputeHash(combined);
                return Convert.ToBase64String(hashBytes);
            }
        }
        public static bool Verify(string password, string salt, string expectedHash)
        {
            string actualHash = Hash(password, salt);
            return string.Equals(actualHash, expectedHash, StringComparison.Ordinal);
        }
    }
}
