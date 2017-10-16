using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace XSCP.Common.Model
{
    [XmlRoot("Root")]
    public class XsConfig
    {
        /// <summary>
        /// 分分彩
        /// </summary>
        [XmlElementAttribute("node-ffcp")]
        public LotteryType FFCP { get; set; }

        /// <summary>
        /// Cookie 信息
        /// </summary>
        [XmlElementAttribute("cookie", IsNullable = false)]
        public CookieModel[] Cookies { get; set; }
    }

    /// <summary>
    /// Cookie
    /// </summary>
    [XmlRootAttribute("cookie")]
    public class CookieModel
    {
        /// <summary>
        /// Url地址
        /// </summary>
        [XmlAttribute("url")]
        public string Url { get; set; }
        /// <summary>
        /// 域名
        /// </summary>
        [XmlAttribute("domain-name")]
        public string DomainName { get; set; }
        /// <summary>
        /// Cookie名
        /// </summary>
        [XmlAttribute("cookie-name")]
        public string CookieName { get; set; }
        /// <summary>
        /// Cookie值
        /// </summary>
        [XmlAttribute("cookie-value")]
        public string CookieValue { get; set; }

        [XmlAttribute("cookies")]
        public string Cookies { get; set; }
        /// <summary>
        /// 路径
        /// </summary>
        [XmlAttribute("cookie-path")]
        public string CookiePath { get; set; }
        /// <summary>
        /// 交互模式
        /// </summary>
        [XmlAttribute("method")]
        public string Method { get; set; }

        /// <summary>
        /// 交互模式
        /// </summary>
        [XmlAttribute("useragent")]
        public string UserAgent { get; set; }
    }

    public class LotteryType
    {
        [XmlElementAttribute("id")]
        public int Id { get; set; }

        [XmlElementAttribute("num")]
        public int Num { get; set; }
    }
}
