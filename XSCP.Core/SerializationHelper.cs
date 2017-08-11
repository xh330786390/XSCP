using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Serialization;

/**************************************************
 * 作者：滕小辉
 * 
 * 创建日期：2015-06-21
 * 
 * 作用描述：序列及反序列操作
 * ***********************************************/

namespace XSCP.Core
{
    public class SerializationHelper
    {
        /// <summary>
        /// 反序列化，将文件内容转化成实例
        /// </summary>
        /// <typeparam name="T">序列化对象</typeparam>
        /// <param name="fileName">文件路径(xml)</param>
        /// <returns>序列化对象</returns>
        public static T DeserialzeXmlFile<T>(string fileName)
        {
            FileStream fs = null;
            try
            {
                fs = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                return (T)serializer.Deserialize(fs);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (fs != null)
                    fs.Close();
            }
        }

        /// <summary>
        /// 序列化(把对象转换成文件)
        /// </summary>
        /// <typeparam name="T">序列化对象</typeparam>
        /// <param name="fileName">文件路径(xml)</param>
        /// <param name="t"></param>
        public static void SerialzeXmlFile<T>(string fileName, T t)
        {
            FileStream fs = null;
            try
            {
                fs = new FileStream(fileName, FileMode.Create, FileAccess.Write, FileShare.ReadWrite);
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                serializer.Serialize(fs, t);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (fs != null) fs.Close();
            }
        }

        /// <summary>
        /// 反序列化，将字符串转化成实例
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="strXml"></param>
        /// <returns></returns>
        public static T DeserialzeXmlString<T>(string strXml)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(strXml);

            XmlSerializer serializer = new XmlSerializer(typeof(T));
            using (MemoryStream stream = new MemoryStream(bytes))
            {
                return (T)serializer.Deserialize((Stream)stream);
            }
        }
    }
}
