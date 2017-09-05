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
            new Export().SqlLiteToMysql();
            Console.Read();
        }
    }
}
