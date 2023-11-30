using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Mapdoon.Common.HashFunctions
{
    public class MD5Hash
    {
        public static string ComputeMD5(string s)
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] hashValue = md5.ComputeHash(Encoding.UTF8.GetBytes(s));
                return Convert.ToHexString(hashValue);
            }
        }
    }
}
