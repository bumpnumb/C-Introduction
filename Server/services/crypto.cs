using Server.modules;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Server.services
{
    static class crypto
    {
        //Instantiate rng service
        private static RNGCryptoServiceProvider rngCsp = new RNGCryptoServiceProvider();

        public static User GenerateSaltHash(string pw)
        {
            //get some bytes to start salt
            byte[] saltBytes = new byte[16];
            //Fill bytes
            rngCsp.GetBytes(saltBytes);

            //Mathematical
            var salted = new Rfc2898DeriveBytes(pw, saltBytes, 10000);

            //Take 20 bytes of the salted pw for hash
            byte[] hashBytes = salted.GetBytes(20);

            //Create a user for easy data transfer
            User tempUser = new User();
            tempUser.Salt = Convert.ToBase64String(saltBytes);
            tempUser.Hash = Convert.ToBase64String(hashBytes);
            return tempUser;
        }

        public static bool AuthenticateLogin(string pw, string hash, string salt)
        {
            //opposite process:
            //get saltbytes from string salt
            byte[] saltBytes = Convert.FromBase64String(salt);

            //Mathematically generate same hash from pw and stored salt
            var salted = new Rfc2898DeriveBytes(pw, saltBytes, 10000);
            //take 20 bytes of it
            byte[] generatedHashBytes = salted.GetBytes(20);

            //stored hash is in string for so convert bytes to string
            string generatedHash = Convert.ToBase64String(generatedHashBytes);

            //compare
            if (hash == generatedHash)
                return true;
            return false;
        }
    }
}
