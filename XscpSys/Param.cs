using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace XscpSys
{
    public class Param
    {
        public static int Count = -1;

        public static string FileBase;//监测的数据目录
        public static string FilePathFF;//监测的分分彩数据文件
        public static string FilePath3D;//监测的3D彩数据文件

        static Param()
        {
            FileBase = System.AppDomain.CurrentDomain.BaseDirectory + @"data\";
            if (!Directory.Exists(FileBase)) Directory.CreateDirectory(FileBase);
        }
    }
}
