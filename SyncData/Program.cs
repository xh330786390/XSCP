using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XSCP.Common;

namespace SyncData
{
    class Program
    {
        static void Main(string[] args)
        {
            MysqlHelper.ClearRepeatData();
            Console.WriteLine("完成");
            Console.Read();
            new Export().SqlLiteToMysql();
            Console.Read();
        }
    }
}
