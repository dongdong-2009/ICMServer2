using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace ICMServer
{
    public class Security
    {
        public static string MD5Encode(string input)
        { 
            MD5 md5 = MD5.Create();                             // 建立一個MD5
            byte[] source = Encoding.Default.GetBytes(input);   // 将字串转為Byte[]
            byte[] crypto = md5.ComputeHash(source);            // 進行MD5加密
            return Convert.ToBase64String(crypto);              // 把加密后的字串從Byte[]转為字串
        }
    }
}
