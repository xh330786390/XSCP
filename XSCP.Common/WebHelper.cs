using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using XSCP.Common.Model;

namespace XSCP.Common
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
        public static string UserAgent = "";

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
            httpReq.UserAgent = UserAgent;// "Mozilla/5.0 (Windows NT 6.1) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/33.0.1750.146 BIDUBrowser/6.x Safari/537.36";// UserAgent;// "Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 5.1; Trident/4.0; GTB6.5; .NET CLR 2.0.50727)";
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

        /// <summary>
        /// Post方法
        /// </summary>
        /// <param name="Url"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string Post(string Url, string data)
        {
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(Url);
            httpWebRequest.CookieContainer = Cookie;
            httpWebRequest.Method = "POST";
            httpWebRequest.Timeout = 20000;
            httpWebRequest.ContentType = "application/x-www-form-urlencoded; charset=UTF-8";
            httpWebRequest.Accept = "application/json, text/javascript, */*; q=0.01";
            httpWebRequest.Headers.Add("Accept-Encoding", "gzip, deflate");
            httpWebRequest.Headers.Add("Accept-Language", "zh-CN,zh;q=0.8");
            httpWebRequest.UserAgent = UserAgent;//"Mozilla/5.0 (Windows NT 6.1) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/33.0.1750.146 BIDUBrowser/6.x Safari/537.36";

            byte[] btBodys = Encoding.UTF8.GetBytes(data);
            httpWebRequest.ContentLength = btBodys.Length;

            httpWebRequest.GetRequestStream().Write(btBodys, 0, btBodys.Length);

            HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            //string head = httpWebResponse.Headers["Content-Encoding"];//查看返回数据的 压缩格式
            using (Stream stream = httpWebResponse.GetResponseStream())
            {
                byte[] bytes = CommonHelper.Ungzip(stream);
                return Encoding.UTF8.GetString(bytes);
            }
        }

        /// <summary>
        /// 设置Cookie
        /// </summary>
        /// <param name="lt_cookie"></param>
        public static CookieContainer GetCookies(CookieModel cookieMode)
        {
            CookieContainer cookieContainer = new CookieContainer();
            CookieCollection cookieCollection = new CookieCollection();

            if (cookieMode != null)
            {
                string strCookies = cookieMode.Cookies.Trim();
                var lt_cookies = strCookies.Split(';').ToList();
                lt_cookies.ForEach(item =>
                {
                    int index = item.IndexOf('=');
                    string cookieName = item.Substring(0, index).Trim();
                    string cookieValue = item.Substring(index + 1).Trim();
                    try
                    {
                        cookieCollection.Add(new Cookie(cookieName, cookieValue, cookieMode.CookiePath, cookieMode.DomainName));
                    }
                    catch (Exception er) { }
                });
            }

            cookieContainer.Add(cookieCollection);
            return cookieContainer;
        }

        /// <summary>
        /// 获取通信内容
        /// </summary>
        /// <param name="config"></param>
        /// <returns></returns>
        public static ProtocolInfo GetProtocolInfo(XsConfig config, bool monitor = false)
        {
            ProtocolInfo pinfo = null;
            CookieModel cmodel = null;
            if (config.Cookies.Length > 0)
            {
                pinfo = new ProtocolInfo();

                cmodel = config.Cookies[0];
                pinfo.Url = cmodel.Url;

                string strParam = GetParam(config);
                if (cmodel.Method.ToUpper() == "POST")
                {
                    pinfo.Method = ProtocolMethod.Post;
                    if (monitor)
                    {
                        pinfo.Url += "/LotteryService.aspx";
                        pinfo.Data = "flag=balance";
                    }
                    else
                    {
                        pinfo.Url += "/UserService.aspx";
                        pinfo.Data = "flag=UIWinOpenNumberBean&" + strParam;
                    }
                }
                else
                {
                    pinfo.Method = ProtocolMethod.Get;
                    pinfo.Url += "/page/WORecord.shtml?" + strParam;
                }

                UserAgent = cmodel.UserAgent ;
            }
            return pinfo;
        }

        /// <summary>
        /// 获取参数
        /// </summary>
        /// <param name="config"></param>
        /// <returns></returns>
        private static string GetParam(XsConfig config)
        {
            Dictionary<string, int> param = new Dictionary<string, int>();
            param["id"] = config.FFCP.Id;
            param["num"] = config.FFCP.Num;

            string data = string.Empty;
            if (param != null) //有参数的情况下，拼接url
            {
                foreach (var item in param)
                {
                    data = data + item.Key + "=" + item.Value + "&";
                }
                data = data.Substring(0, data.Length - 1);
            }
            return data;
        }
    }

    /// <summary>
    /// 通信内容
    /// </summary>
    public class ProtocolInfo
    {
        /// <summary>
        /// 交互方式
        /// </summary>
        public ProtocolMethod Method { get; set; }
        /// <summary>
        /// 域名地址
        /// </summary>
        public string Url { get; set; }
        /// <summary>
        /// 数据（只限 Post方法）
        /// </summary>
        public string Data { get; set; }
    }

    public enum ProtocolMethod
    {
        /// <summary>
        /// Get方式
        /// </summary>
        Get = 0,
        /// <summary>
        /// Post方式
        /// </summary>
        Post = 1
    }
}