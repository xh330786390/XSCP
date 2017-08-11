using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace XSCP.Service.Model
{
    [Serializable, XmlRoot("config")]
    public class Config
    {
        /// <summary>
        /// 配置信息
        /// </summary>
        [XmlElement("items")]
        public Items Items;

        /// <summary>
        /// 所有已保存的用户
        /// </summary>
        [XmlElement("users")]
        public Users Users;

        /// <summary>
        /// 最后登录的用户
        /// </summary>
        [XmlElement("last-login")]
        public User LastLogin;
    }

    /// <summary>
    /// 系统配置信息
    /// </summary>
    public class Items
    {
        /// <summary>
        ///链接方式
        /// </summary>
        [XmlElement("connection")]
        public string ConnectionString;

        /// <summary>
        /// 数据源
        /// </summary>
        [XmlElement("datasource")]
        public string DataSource;

        /// <summary>
        /// cookie1
        /// </summary>
        [XmlElement("cookie1")]
        public string Cookie1;

        /// <summary>
        /// cookie2
        /// </summary>
        [XmlElement("cookie2")]
        public string Cookie2;

    }

    /// <summary>
    /// 用户列表
    /// </summary>
    public class Users
    {
        /// <summary>
        /// 用户列表
        /// </summary>
        [XmlElement("user")]
        public List<User> UserList;
    }

    /// <summary>
    /// 用户信息
    /// </summary>
    public class User
    {
        /// <summary>
        /// 用户名
        /// </summary>
        [XmlElement("name")]
        public string Name;

        /// <summary>
        /// 登入密码
        /// </summary>
        [XmlElement("password")]
        public string Password;

        /// <summary>
        /// 保存用户
        /// </summary>
        [XmlElement("save")]
        public bool IsSave;

        /// <summary>
        /// 自动登录
        /// </summary>
        [XmlElement("auto-login")]
        public bool AutoLogin;
    }
}
