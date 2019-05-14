using Server.modules;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Server.services
{
    static class crypto
    {
        private static RNGCryptoServiceProvider rngCsp = new RNGCryptoServiceProvider();

        public static User GenerateSaltHash(string pw)
        {
            byte[] saltBytes = new byte[16];
            rngCsp.GetBytes(saltBytes);
            var salted = new Rfc2898DeriveBytes(pw, saltBytes, 10000);
            byte[] hashBytes = salted.GetBytes(20);

            User u = new User();
            //u.Salt = strBuilder(saltBytes);
            u.Salt = Convert.ToBase64String(saltBytes);
            u.Hash = Convert.ToBase64String(hashBytes);
            return u;
        }

        public static bool AuthenticateLogin(string pw, string hash, string salt)
        {
            byte[] saltBytes = Convert.FromBase64String(salt);

            var salted = new Rfc2898DeriveBytes(pw, saltBytes, 10000);
            byte[] generatedHashBytes = salted.GetBytes(20);
            string generatedHash = Convert.ToBase64String(generatedHashBytes);

            if (hash == generatedHash)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
