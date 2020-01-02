using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace OA.Common
{
    public class ContextCode
    {
        public static string MD5Encryption(string pwd)
        {
            MD5 m5 = MD5.Create();
            byte[] buffer = m5.ComputeHash(Encoding.Default.GetBytes(pwd));
            StringBuilder sbStr = new StringBuilder();
            foreach (byte item in buffer)
            {
                sbStr.Append(item.ToString("X2"));
            }
            return sbStr.ToString();
        }
    }
}
