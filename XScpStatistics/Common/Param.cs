using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace XScpStatistics.Common
{
    public class Param
    {
        public static string FileBase;//监测的数据目录
        public static string SkinFile;//皮肤文件
        public static string FilePath;//监测的数据文件

        static Param()
        {
            FileBase = System.AppDomain.CurrentDomain.BaseDirectory + @"data\";
            if (!Directory.Exists(FileBase)) Directory.CreateDirectory(FileBase);

            SkinFile = System.AppDomain.CurrentDomain.BaseDirectory + @"\skin\MP10.ssk";
        }
    }
}
