using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace XscpSys
{
    public class WebHelper
    {
        static List<string> ltDomain = new List<string>();
        private string _url = null;
        private CookieContainer cookie = null;
        private CookieCollection coo = null;

        static WebHelper()
        {
            ltDomain.Add("www.goxs.co");
            ltDomain.Add("www.xs6868.com");
            ltDomain.Add("www.xs8cp.com");
            ltDomain.Add("www.xs8cp.info");
            ltDomain.Add("www.run88.co");
            ltDomain.Add("run88.co");
        }

        //public void SetCookies(string cookieValue, string jsessionid, string domain)
        //{
        //    _domain = domain;
        //    if (!string.IsNullOrEmpty(cookieValue))
        //    {
        //        coo.Add(new Cookie("incap_ses_625_1064974", cookieValue, "/", domain));
        //    }
        //    //coo.Add(new Cookie("incap_ses_625_1064974", "3UTJRZooBG9j2AySTnKsCLHrH1kAAAAAsXzOACNtwcbGbQCcXJR9AQ==", "/", ".run88.co"));
        //    //coo.Add(new Cookie("visid_incap_1064974", "goRn+hCzTOquKnjw6nnz53jJH1kAAAAAQUIPAAAAAACw9lqE5BTVdOKCp9AN9p7P", "/", ".run88.co"));
        //    //coo.Add(new Cookie("dypoint", "0", "/", "www.run88.co"));
        //    //coo.Add(new Cookie("modes", "3", "/", "www.run88.co"));
        //    coo.Add(new Cookie("JSESSIONID", cookieValue, "/", domain));
        //    cookie.Add(coo);
        //}

        public void Initcoo(string cookieValue, string seesion)
        {
            cookie = new CookieContainer();
            coo = new CookieCollection();

            string domain = null;
            ltDomain.ForEach(l =>
            {
                if (this._url.Contains(l))
                    domain = l;
            });
            //coo.Add(new Cookie("incap_ses_625_1064974", "wQn1ST8lv2/97zSSTnKsCL4OIFkAAAAAK+O94tu7AdVKL+DjTJ47hg==", "/", ".run88.co"));
            //coo.Add(new Cookie("visid_incap_1064974", "goRn+hCzTOquKnjw6nnz53jJH1kAAAAAQUIPAAAAAACw9lqE5BTVdOKCp9AN9p7P", "/", ".run88.co"));
            //coo.Add(new Cookie("dypoint", "0", "/", "www.run88.co"));
            //coo.Add(new Cookie("modes", "3", "/", "www.run88.co"));

            if (!string.IsNullOrEmpty(cookieValue))
            {
                coo.Add(new Cookie("incap_ses_432_1064974", cookieValue, "/", domain));
            }
            coo.Add(new Cookie("JSESSIONID", "	seesion", "/", domain));
            cookie.Add(coo);
        }

        //url为请求的网址，param参数为需要查询的条件（服务端接收的参数，没有则为null）
        //返回该次请求的响应
        public string Get(string url, string cookieValue, string seesion, Dictionary<String, String> param)
        {
            this._url = url;
            Initcoo(cookieValue, seesion);
            //string referer = "http://" + _domain + "/ssccs.shtml";
            if (param != null) //有参数的情况下，拼接url
            {
                url = url + "?";
                foreach (var item in param)
                {
                    url = url + item.Key + "=" + item.Value + "&";
                }
                url = url.Substring(0, url.Length - 1);
            }
            HttpWebRequest httpReq = WebRequest.Create(url) as HttpWebRequest;//创建请求
            httpReq.Method = "GET"; //请求方法为GET
            httpReq.CookieContainer = cookie;
            httpReq.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8";
            httpReq.Headers.Add("Accept-Encoding", "gzip, deflate");
            httpReq.Headers.Add("Accept-Language", "zh-CN,zh;q=0.8");
            httpReq.UserAgent = "Mozilla/5.0 (Windows NT 6.1) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/33.0.1750.146 BIDUBrowser/6.x Safari/537.36";// UserAgent;// "Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 5.1; Trident/4.0; GTB6.5; .NET CLR 2.0.50727)";
            httpReq.KeepAlive = true;

            HttpWebResponse res; //定义返回的response
            try
            {
                res = (HttpWebResponse)httpReq.GetResponse(); //此处发送了请求并获得响应
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