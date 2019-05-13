using System;
using System.Collections.Generic;
using System.Text;

namespace Server.services
{
    class helper
    {
        public static byte[] intToBytes(int n)
        {
            byte[] intBytes = BitConverter.GetBytes(n);
            if (BitConverter.IsLittleEndian)
                Array.Reverse(intBytes);
            return intBytes;
        }

    }
}
