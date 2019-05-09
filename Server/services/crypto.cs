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
            byte[] saltBytes = Encoding.ASCII.GetBytes(salt);
            var salted = new Rfc2898DeriveBytes(pw, saltBytes, 10000);
            byte[] hashBytes = Encoding.ASCII.GetBytes(hash);

            byte[] generatedHashBytes = salted.GetBytes(20);

            //generera ett nytt hash utifrån det pw och salt som vi har som argument i funktionen
            //jämför det nya hashet med hashet som vi har som argument i funktionen

            if (hashBytes == generatedHashBytes)
            {
                return false;
            }
            else
            {
                return true;
            }
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
