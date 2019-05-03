using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Server.services
{
    static class crypto
    {
            private static RNGCryptoServiceProvider rngCsp = new RNGCryptoServiceProvider();

        public static string GenerateCookie()
        {
            byte[] cookieBytes = new byte[16];
            rngCsp.GetBytes(cookieBytes);

            StringBuilder builder = new StringBuilder();

            for (int i = 0; i < cookieBytes.Length; i++)
            {
                builder.Append(cookieBytes[i].ToString("x2"));
            }
            rngCsp.Dispose();
            return builder.ToString();
        }
    }
}
}
