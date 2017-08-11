using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using System.IO;

namespace XSCP.Core {

    public class SerializerUtility {

        /// <summary>
        /// conver string to object
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="str"></param>
        /// <returns></returns>
        public static T Deserialze<T>(string str)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(str);

            XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));

            using (MemoryStream stream = new MemoryStream(bytes))
            {
                var obj = xmlSerializer.Deserialize((Stream)stream);
                return (T)obj;
            }
        }

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
        /// conver object to  string
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filename"></param>
        /// <param name="t"></param>
        public static void Serialze<T>(string filename, T t)
        {
            Stream stream = (Stream)File.Open(filename, FileMode.Create, FileAccess.ReadWrite);
            XmlSerializerNamespaces xmlSpace = new XmlSerializerNamespaces();
            xmlSpace.Add("", "");

            XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
            xmlSerializer.Serialize(stream, t, xmlSpace);
            stream.Close();
        }
    }
}