using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XSCP.Service.Model;
using XSCP.Core;

namespace XSCP.Service
{
    /**************************************************
        * 作者：滕小辉
        * 
        * 创建日期：2015-06-22
        * 
        * 作用描述：系统参数
        * ***********************************************/
    public static class SysConfig
    {
        #region 只读系统参数

        //应用程序基本路径	
        public static readonly string AppBasePath = DirectoryUtility.GetInstallDirectory();

        //配置文件路径
        public static readonly string ConfigPath = AppBasePath + @"\config.xml"; // ConfigurationManager.AppSettings["config"].ToString();

        static SysConfig()
        {
            SysConfig.Config = new XmlOperate<Config>().GetConfig(SysConfig.ConfigPath);
        }

        public static void ReloadSysConfig()
        {
            SysConfig.Config = new XmlOperate<Config>().GetConfig(SysConfig.ConfigPath);
        }
        #endregion

        #region 可读写的系统参数
        /// <summary>
        /// 配置信息
        /// </summary>
        private static Config _config;

        public static Config Config
        {
            get { return _config; }
            set { _config = value; }
        }
        #endregion
    }
}
