using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Runtime.InteropServices;
using System.Text;
using System.Net;
using System.Collections.Generic;

namespace XscpSys
{
    /**/
    ///<summary>
    /// 获取Cookie的方法类。
    ///</summary>
    public class Cookies
    {
        [DllImport("wininet.dll", SetLastError = true)]
        public static extern bool InternetGetCookie(string url, string cookieName, StringBuilder cookieData, ref int size);

        public static CookieContainer GetUriCookieContainer(Uri uri)
        {
            CookieContainer cookies = null;
            //定义Cookie数据的大小。
            int datasize = 256;
            StringBuilder cookieData = new StringBuilder(datasize);

            if (!InternetGetCookie(uri.ToString(), null, cookieData, ref datasize))
            {
                if (datasize < 0) return null;
                // 确信有足够大的空间来容纳Cookie数据。
                cookieData = new StringBuilder(datasize);
                if (!InternetGetCookie(uri.ToString(), null, cookieData, ref datasize)) return null;
            }

            if (cookieData.Length > 0)
            {
                cookies = new CookieContainer();
                cookies.SetCookies(uri, cookieData.ToString().Replace(';', ','));
            }
            return cookies;
        }

        public static void PrintCookies(CookieContainer cookies, Uri uri)
        {
            CookieCollection cc = cookies.GetCookies(uri);

            foreach (var cook in cc)
            {
                System.Windows.Forms.MessageBox.Show(cook.ToString());
            }
        }
    }

    //public class Test
    //{
    //    static void Main(string[] args)
    //    {
    //        string url = @"http://www.kaixin001.com/";
    //        Uri uri = new Uri(url);
    //        CookieContainer cookies = Cookies.GetUriCookieContainer(uri);
    //        Cookies.PrintCookies(cookies, uri);
    //        Console.ReadKey();
    //    }
    //}
}