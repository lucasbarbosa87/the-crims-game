using Org.BouncyCastle.Crypto.Generators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crims.Core.Helpers
{
    public interface IPasswordHelper
    {
        public string GenerateSalt();
        public string HashPassword(string password, string salt);
        public bool VerifyPassword(string inputPassword, string password);
    }

    public class PasswordHelper : IPasswordHelper
    {
        public string GenerateSalt()
        {
            return BCrypt.Net.BCrypt.GenerateSalt();
        }

        public string HashPassword(string password, string salt)
        {
            return BCrypt.Net.BCrypt.HashPassword(password, salt);
        }

        public bool VerifyPassword(string inputPassword, string password)
        {
            return BCrypt.Net.BCrypt.Verify(inputPassword, password);
        }
    }
}
