using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XSCP.Common
{
    public class CommonHelper
    {
        /// <summary>
        /// Gzip 解压
        /// </summary>
        /// <param name="streamSource"></param>
        /// <returns></returns>
        public static byte[] Ungzip(Stream streamSource)
        {
            using (GZipStream stream = new GZipStream(streamSource, CompressionMode.Decompress))
            {
                using (MemoryStream mStream = new MemoryStream())
                {
                    int data;
                    while ((data = stream.ReadByte()) != -1)
                    {
                        mStream.WriteByte((byte)data);
                    }
                    return mStream.ToArray();
                }
            }
        }
    }
}
