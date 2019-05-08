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
            u.Salt = strBuilder(saltBytes);
            u.Hash = strBuilder(hashBytes);
            return u;
        }

        public static bool AuthenticateLogin(string pw, string salt, string hash)
        {

            //gör om salt till saltbytes
            //gör om hash till hashbytes2
            //
            //var salted = new Rfc2898DeriveBytes(pw, saltBytes, 10000);

            //byte[] hashBytes = salted.GetBytes(20);

            //jämför hashBytes med hashbytes2

            return true;
        }


        private static string strBuilder(byte[] arr)
        {
            StringBuilder Builder = new StringBuilder();
            for (int i = 0; i < arr.Length; i++)
            {
                Builder.Append(arr[i].ToString("x2"));
            }
            rngCsp.Dispose();

            return Builder.ToString();
        }

    }
}

