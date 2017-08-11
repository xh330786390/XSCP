using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using XSCP.Common.Model;

namespace XSCP.Forecast
{
    public class WebHelper
    {
        /// <summary>
        /// 是否连接成功
        /// </summary>
        public static bool Connected = false;
        /// <summary>
        /// Cookie
        /// </summary>
        public static CookieContainer Cookie = null;
        /// <summary>
        /// Url
        /// </summary>
        public static string Url = null;

        //url为请求的网址，param参数为需要查询的条件（服务端接收的参数，没有则为null）
        //返回该次请求的响应
        public static string Get(string url)
        {
            if (string.IsNullOrEmpty(url)) return null;
            if (Cookie == null) return null;

            HttpWebRequest httpReq = WebRequest.Create(url) as HttpWebRequest;//创建请求
            httpReq.Method = "GET"; //请求方法为GET
            httpReq.CookieContainer = Cookie;
            httpReq.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8";
            httpReq.Headers.Add("Accept-Encoding", "gzip, deflate");
            httpReq.Headers.Add("Accept-Language", "zh-CN,zh;q=0.8");
            httpReq.UserAgent = "Mozilla/5.0 (Windows NT 6.1) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/33.0.1750.146 BIDUBrowser/6.x Safari/537.36";// UserAgent;// "Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 5.1; Trident/4.0; GTB6.5; .NET CLR 2.0.50727)";
            httpReq.KeepAlive = true;

            HttpWebResponse res = null; //定义返回的response
            try
            {
                //此处发送了请求并获得响应
                res = (HttpWebResponse)httpReq.GetResponse();
            }
            catch (WebException ex)
            {
                res = (HttpWebResponse)ex.Response;
            }
            StreamReader sr = new StreamReader(res.GetResponseStream(), Encoding.UTF8);
            string content = sr.ReadToEnd(); //响应转化为String字符串
            return content;
        }
    }
}