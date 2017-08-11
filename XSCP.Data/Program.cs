using System;
using System.Collections.Generic;
using XSCP.Common.Model;
using XSCP.Common;
using System.Linq;
using XSCP.Common.Extend;
namespace XSCP.Data
{
    public class Program
    {
        private static DateTime gettime2()
        {
            

            return DateTime.Now;
        }


        public static void Main(string[] args)
        {
            DateTime t1 = gettime2();

            DateTime dt = DateTime.Now;
            List<int> lllll = XscpBLL.CheckLottery(dt.ToString("yyyyMMdd"));
            int max = lllll.Max(l => l);
            Console.WriteLine("max：" + max);
            int min = lllll.Min(l => l);
            Console.WriteLine("min：" + min);

            for (int i = min; i <= max; i++)
            {
                if (!lllll.Contains(i))
                {
                    Console.WriteLine("缺失：" + i.ToString().PadLeft(4, '0'));
                }
            }

            List<string> lt = new List<string>();

            lt.Add("15,3,0,0,5,2");
            lt.Add("14,3,4,0,2,4");
            lt.Add("13,9,1,3,5,8");
            lt.Add("12,3,7,1,1,7");


            //lt.Add("11,1,3,2,3,1");
            //lt.Add("10,8,4,0,0,0");
            //lt.Add("9,1,7,3,3,6");
            //lt.Add("8,2,7,9,9,8");
            //lt.Add("7,3,0,0,5,2");
            //lt.Add("6,3,4,0,2,4");
            //lt.Add("5,2,7,9,9,8");
            //lt.Add("4,3,0,0,5,2");
            //lt.Add("3,3,4,0,2,4");
            //lt.Add("2,9,1,3,5,8");
            //lt.Add("1,3,7,1,1,7");

            //lt.Add("1380,3,1,0,7,2");
            //lt.Add("1379,7,8,9,4,9");
            //lt.Add("1378,2,6,8,0,2");
            //lt.Add("1377,4,4,8,7,3");
            //lt.Add("1376,4,5,7,3,3");
            //lt.Add("1375,0,3,7,2,7");
            //lt.Add("1374,0,0,9,6,4");
            //lt.Add("1373,1,3,2,3,1");
            //lt.Add("1372,8,4,0,0,0");
            //lt.Add("1371,1,7,3,3,6");
            //lt.Add("1370,2,7,9,9,8");
            //lt.Add("1369,3,0,0,5,2");
            //lt.Add("1368,3,4,0,2,4");
            //lt.Add("1367,9,1,3,5,8");
            //lt.Add("1366,3,7,1,1,7");
            //lt.Add("1365,1,8,8,2,6");
            //lt.Add("1364,4,2,2,3,1");
            //lt.Add("1363,7,8,2,7,8");
            //lt.Add("1362,2,3,5,4,8");
            //lt.Add("1361,9,7,0,1,5");
            //lt.Add("1360,9,4,6,6,5");
            //lt.Add("1359,9,1,5,9,7");
            //lt.Add("1358,9,5,6,4,9");
            //lt.Add("1357,9,8,6,4,3");
            //lt.Add("1356,0,9,3,9,9");


            ///新增开奖号码
            List<LotteryModel> lt_LotteryModel = XscpBLL.SaveLottery(dt, lt);
            ///新增前二走势
            XscpBLL.SaveTendency2(Tendency2Enum.Before, lt_LotteryModel);
            ///新增后二走势
            XscpBLL.SaveTendency2(Tendency2Enum.After, lt_LotteryModel);
            Console.Read();
            //Console.WriteLine(dt.ToXscpDateTime(1020));
            //Console.Read();

            //List<LotteryModel> ltm = SQLiteHelper.QueryLottery("20170522");
            //ltm = SQLiteHelper.QueryLottery("20170522", 0);
            //ltm = SQLiteHelper.QueryLottery("20170522", 1);
            //ltm = SQLiteHelper.QueryLottery("20170522", 2);
            //ltm = SQLiteHelper.QueryLottery("20170522", 3);


            //SQLiteHelper.SaveLotteryData(lt);


            //using (SQLiteConnection conn = connectToDatabase(@"E:\sqlite\test7.db3"))
            //{
            //CreateLotteryTable(conn);
            //conn.Execute("insert into highscores(name,score) values(@name,@score)", new Info() { name = "zxj", score = 1000 });
            //conn.Execute("update highscores set score = @score where name=@name", new { name = "txh", score = 85 });
            //var it = conn.Query<Info>("select * from  highscores").ToList();

            //查看表是否存在
            //var it = conn.Query<int>("SELECT COUNT(*) FROM sqlite_master where type='table' and name='highscores'");


            //}

            //DbProviderFactory fact = DbProviderFactories.GetFactory("System.Data.SQLite");
            //fact.CreateConnection();
            //createTable(connectToDatabase(@"E:\sqlite\test8.db3"));
        }
    }

    public class Info
    {
        public string name { get; set; }
        public int score { get; set; }
    }
}
