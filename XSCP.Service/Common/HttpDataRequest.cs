using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Xml;

namespace XSCP.Service.Common
{
    public class HttpDataRequest
    {
        public static CookieContainer cookie = new CookieContainer();
        public static CookieCollection coo = new CookieCollection();

        private static readonly string UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/44.0.2403.125 Safari/537.36";
        private static readonly string ContentType = "application/x-www-form-urlencoded; charset=UTF-8";

        static HttpDataRequest()
        {
            coo.Add(new Cookie("__nxquid", SysConfig.Config.Items.Cookie1, "", "xscp.co"));
            coo.Add(new Cookie("__nxqsid", SysConfig.Config.Items.Cookie2, "", "xscp.co"));
            cookie.Add(coo);
        }

        public static void SetCookieValues(string value1,string value2)
        {
            coo["__nxquid"].Value = value1;
            coo["__nxqsid"].Value = value2;
        }

        /// <summary>
        /// 获取彩票数据
        /// </summary>
        /// <param name="strPostUrl"></param>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string GetData(string referer, RequestMethod method, string url)
        {
            HttpWebRequest httpReq = (HttpWebRequest)HttpWebRequest.Create(URL.Main + url);
            System.Net.ServicePointManager.Expect100Continue = false;
            httpReq.Method = method == RequestMethod.Post ? "POST" : "GET";

            httpReq.Referer = URL.Main + referer;
            httpReq.CookieContainer = cookie;
            httpReq.ContentType = ContentType;

            httpReq.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8";
            httpReq.Headers.Add("Accept-Encoding", "gzip, deflate");
            httpReq.Headers.Add("Accept-Language", "zh-CN,zh;q=0.8");
            httpReq.UserAgent = "Mozilla/5.0 (Windows NT 6.1) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/50.0.2657.3 Safari/537.36";// UserAgent;// "Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 5.1; Trident/4.0; GTB6.5; .NET CLR 2.0.50727)";
            httpReq.KeepAlive = true;

            HttpWebResponse httpWebResponse = (HttpWebResponse)httpReq.GetResponse();
            HttpStatusCode httpStatusCode = httpWebResponse.StatusCode;

            using (System.IO.Stream stream = httpWebResponse.GetResponseStream())
            {
                using (StreamReader responseReader = new StreamReader(stream, Encoding.GetEncoding("iso-8859-1")))
                {
                    String retStr = responseReader.ReadToEnd();
                    return retStr;
                }
            }
        }

        public static string LotteryService(string referer, RequestMethod method, string url)
        {
            HttpWebRequest httpReq = (HttpWebRequest)HttpWebRequest.Create(URL.Main + url);
            System.Net.ServicePointManager.Expect100Continue = false;
            httpReq.Method = method == RequestMethod.Post ? "POST" : "GET";
            httpReq.Headers.Add("Connection", "Keep-Alive");
            httpReq.KeepAlive = false;
            //httpReq.Headers.Add("connection", "keep-alive");

            httpReq.ContentLength = 12;
            httpReq.Referer = URL.Main + referer;
            httpReq.CookieContainer = cookie;
            httpReq.ContentType = ContentType;

            httpReq.Accept = "*/*";
            httpReq.Headers.Add("Accept-Encoding", "gzip, deflate");
            httpReq.Headers.Add("Accept-Language", "zh-CN,zh;q=0.8");
            httpReq.UserAgent = "Mozilla/5.0 (Windows NT 6.1) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/50.0.2657.3 Safari/537.36";// UserAgent;// "Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 5.1; Trident/4.0; GTB6.5; .NET CLR 2.0.50727)";


            HttpWebResponse httpWebResponse = (HttpWebResponse)httpReq.GetResponse();
            HttpStatusCode httpStatusCode = httpWebResponse.StatusCode;

            using (System.IO.Stream stream = httpWebResponse.GetResponseStream())
            {
                using (StreamReader responseReader = new StreamReader(stream, Encoding.GetEncoding("utf-8")))
                {
                    String retStr = responseReader.ReadToEnd();
                    return retStr;
                }
            }
        }

        /// <summary>
        /// 登陆
        /// <param name="strPostUrl"></param>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string Login(string strPostUrl, string url)
        {
            string strResault = "";
            try
            {
                HttpWebRequest httpReq = (HttpWebRequest)WebRequest.Create(strPostUrl);
                System.Net.ServicePointManager.Expect100Continue = false;
                httpReq.Method = "POST";

                httpReq.CookieContainer = cookie;
                httpReq.Referer = strPostUrl;
                httpReq.ContentType = ContentType;

                httpReq.Accept = "*/*";
                httpReq.Headers.Add("Accept-Encoding", "gzip, deflate");
                httpReq.Headers.Add("Accept-Language", "zh-CN,zh;q=0.8");
                httpReq.UserAgent = UserAgent;
                //httpReq.Headers.Add("Cache-Control", "no-cache");

                httpReq.KeepAlive = true;
                byte[] bsInData = UTF8Encoding.UTF8.GetBytes(url);
                httpReq.ContentLength = bsInData.Length;
                using (System.IO.Stream stream = httpReq.GetRequestStream())
                {
                    stream.Write(bsInData, 0, bsInData.Length);
                }

                HttpWebResponse httpRes = null;
                httpRes = (HttpWebResponse)httpReq.GetResponse();
                using (Stream outStream = httpRes.GetResponseStream())
                {
                    using (StreamReader sr = new StreamReader(outStream, System.Text.Encoding.GetEncoding("utf-8")))
                    {
                        strResault = sr.ReadToEnd();
                    }
                }
                return strResault;
            }
            catch (Exception ex)
            {
                return "TimeOut:" + ex.Message;
            }
        }
    }
}
