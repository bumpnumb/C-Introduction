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

        public static bool AuthenticateLogin(string pw, string hash, string salt)
        {
            //byte[] saltBytes = System.Text.Encoding.UTF8.GetBytes(salt);
            byte[] saltBytes = Encoding.ASCII.GetBytes(salt);

            //byte[] hashBytes = System.Text.Encoding.UTF8.GetBytes(hash);
            byte[] hashBytes = Encoding.ASCII.GetBytes(hash);
                int byteLength = Buffer.ByteLength(hashBytes);

            var salted = new Rfc2898DeriveBytes(pw, saltBytes, 10000);
            byte[] generatedHashBytes = salted.GetBytes(byteLength);

            //byte[] passwordAndSaltBytes = System.Text.Encoding.UTF8.GetBytes(pw + salt);
            //byte[] hashBytes = new System.Security.Cryptography.SHA256Managed().ComputeHash(passwordAndSaltBytes);
            //string hashString = Convert.ToBase64String(hashBytes);

            //får ej rätt bytes här
            if (hashBytes == generatedHashBytes)    //hashBytes == generatedHashBytes
            {
                return true;
            }
            else
            {
                return false;
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
