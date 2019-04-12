using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Security.Cryptography;

namespace Server.services
{
    class crypto
    {
        private static RNGCryptoServiceProvider rngCsp = new RNGCryptoServiceProvider();
        // Main method.
        public static string GenerateSalt()
        {
            byte[] saltBytes = new byte[16];
            rngCsp.GetBytes(saltBytes);

            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < saltBytes.Length; i++)
            {
                builder.Append(saltBytes[i].ToString("x2"));
            }
            rngCsp.Dispose();
            return builder.ToString();
        }

    }
}
