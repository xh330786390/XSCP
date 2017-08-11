using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace XSCP.Core
{
    public class Base64UTIL
    {
        //字符串编码：
        public static String EncodString(String data)
        {
            byte[] bytes = Encoding.Default.GetBytes(data);
            return Convert.ToBase64String(bytes);
        }

        //字符串解码：
        public static String DeEncodString(String data)
        {
            byte[] outputb = Convert.FromBase64String(data);
            return Encoding.Default.GetString(outputb);
        }

        /// <summary>
        /// 文件转换成字符串 (编码)
        /// </summary>
        /// <param name="file">需编码的文件路径</param>
        /// <returns></returns>
        public static string File2String(string filePath)
        {
            FileStream fs = File.OpenRead(filePath);
            byte[] bytes = new byte[fs.Length];
            int i = fs.Read(bytes, 0, bytes.Length);
            fs.Flush();
            fs.Close();
            return Convert.ToBase64String(bytes);
        }

        /// <summary>
        /// 字符串转换成文件 (解码)
        /// </summary>
        /// <param name="data">解码的字符串</param>
        /// <param name="filePath">文件路径</param>
        public static void String2File(String data, string filePath)
        {
            FileStream fs = null;
            try
            {
                fs = File.Create(filePath);
                byte[] bytes = Convert.FromBase64String(data);
                for (int i = 0; i < bytes.Length; i++)
                {
                    fs.WriteByte(bytes[i]);
                }

                fs.Flush();
                fs.Close();

            } catch (Exception)
            {
                fs.Flush();
                fs.Close();
                if (File.Exists(filePath))
                    File.Delete(filePath);
            }
        }
    }
}
