using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/**************************************************
 * 作者：滕小辉
 * 
 * 创建日期：2015-06-22
 * 
 * 作用描述：获取及保存xml文件信息
 * ***********************************************/
namespace XSCP.Core
{
    public class XmlOperate<T> where T : class
    {
        /// <summary>
        /// 获取xml文件信息
        /// </summary>
        public T GetConfig(string xmlPath)
        {
            return SerializationHelper.DeserialzeXmlFile<T>(xmlPath);
        }

        /// <summary>
        /// 保存xml文件
        /// </summary>
        /// <typeparam name="U"></typeparam>
        /// <param name="u"></param>
        public void SaveConfig<U>(string xmlPath, U u) where U : T
        {
            SerializationHelper.SerialzeXmlFile<U>(xmlPath, u);
        }
    }
}
