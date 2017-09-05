using System;
using System.Collections.Generic;
using System.Data.SQLite;
using Dapper;
using System.IO;
using System.Linq;
using XSCP.Common.Model;
using MySteel.Common.Helper;
using MySql.Data.MySqlClient;

namespace XSCP.Common
{
    public class MysqlHelper
    {
        private static string mysqlConnectString = AppSettingsHelper.GetStringValue("MysqlConnectString");


        /// <summary>
        /// Google Cookie存储路径
        /// </summary>
        public static string CookiePath = null;

        /// <summary>
        /// 开奖记录
        /// </summary>
        public static string LottoryTbName = "Lottery_" + DateTime.Now.Year;

        /// <summary>
        /// 一星走势
        /// </summary>
        public static string TendencyDigit1TbName = "TendencyDigit1_" + DateTime.Now.Year;   //万
        public static string TendencyDigit2TbName = "TendencyDigit2_" + DateTime.Now.Year;   //千
        public static string TendencyDigit3TbName = "TendencyDigit3_" + DateTime.Now.Year;   //百
        public static string TendencyDigit4TbName = "TendencyDigit4_" + DateTime.Now.Year;   //十
        public static string TendencyDigit5TbName = "TendencyDigit5_" + DateTime.Now.Year;   //个

        /// <summary>
        /// 二星走势
        /// </summary>
        public static string TendencyBefore2TbName = "TendencyBefore2_" + DateTime.Now.Year;
        public static string TendencyAfter2TbName = "TendencyAfter2_" + DateTime.Now.Year;

        static MysqlHelper()
        {
            //@2.建表
            CreateLotteryTable(LottoryTbName);

            ///创建一星趋势表
            CreateTendency1Table(TendencyDigit1TbName);   //万
            CreateTendency1Table(TendencyDigit2TbName);   //千
            CreateTendency1Table(TendencyDigit3TbName);   //百
            CreateTendency1Table(TendencyDigit4TbName);   //十
            CreateTendency1Table(TendencyDigit5TbName);   //个

            ///创建二星趋势表
            CreateTendency2Table(TendencyBefore2TbName);
            CreateTendency2Table(TendencyAfter2TbName);
        }

        /// <summary>
        /// 获取二星走势表名
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private static string getTendency1Table(Tendency1Enum type)
        {
            return "TendencyDigit" + (int)type + "_" + DateTime.Now.Year;
        }

        /// <summary>
        /// 获取二星走势表名
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private static string getTendency2Table(Tendency2Enum type)
        {
            string tableName = null;
            if (type == Tendency2Enum.Before)
                tableName = TendencyBefore2TbName;
            else
                tableName = TendencyAfter2TbName;
            return tableName;
        }

        #region [开奖号码]
        /// <summary>
        /// 创建连接
        /// </summary>
        /// <returns></returns>
        public static MySqlConnection CreateConnection()
        {
            var connection = new MySqlConnection(mysqlConnectString);
            //connection.Open();
            return connection;
        }

        /// <summary>
        /// 创建奖号数据表
        /// </summary>
        /// <param name="conn"></param>
        public static void CreateLotteryTable(string tableName)
        {
            string sql = string.Format("SELECT count(1) FROM information_schema.TABLES WHERE table_name ='{0}'", tableName);

            using (MySqlConnection conn = CreateConnection())
            {

                int count = conn.Query<int>(sql).FirstOrDefault();
                if (count == 0)
                {
                    sql = string.Format("CREATE TABLE {0} ( " +
                                   @"ID      int  auto_increment not null primary key,     " +
                                   @"Ymd     CHAR( 8 )      NOT NULL,	     " +
                                   @"Sno     CHAR( 4 )      NOT NULL,	     " +
                                   @"Lottery CHAR( 9 )      NOT NULL,	     " +
                                   @"Num1    int             NOT NULL,	     " +
                                   @"Num2    int             NOT NULL,	     " +
                                   @"Num3    int             NOT NULL,	     " +
                                   @"Num4    int             NOT NULL,	     " +
                                   @"Num5    int             NOT NULL,	     " +
                                   @"Dtime   CHAR( 12 )      NOT NULL 	     )", tableName);
                    conn.Execute(sql);
                }
            }
        }

        /// <summary>
        /// 新增开奖数据
        /// </summary>
        /// <param name="lotterys"></param>
        public static void SaveLotteryData(List<LotteryModel> lotterys)
        {
            using (MySqlConnection conn = CreateConnection())
            {
                conn.Open();
                MySqlTransaction trans = conn.BeginTransaction();

                string sqlCount = string.Format("SELECT count(1) FROM {0} where Ymd = @Ymd and Sno=@Sno", LottoryTbName);

                try
                {
                    for (int i = lotterys.Count - 1; i >= 0; i--)
                    {
                        LotteryModel lm = lotterys[i];
                        int count = conn.Query<int>(sqlCount, lm).FirstOrDefault();
                        if (count == 0)
                        {
                            string sql = string.Format("insert into {0}(Ymd,Sno,Lottery,Num1,Num2,Num3,Num4,Num5,Dtime) VALUES( @Ymd,@Sno,@Lottery,@Num1,@Num2,@Num3,@Num4,@Num5,@Dtime)", LottoryTbName);
                            conn.Execute(sql, lm, trans);
                        }
                    }
                    trans.Commit();
                }
                catch (Exception)
                {
                    trans.Rollback();
                }
            }
        }

        /// <summary>
        /// 通过日期查找开奖号码
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static List<LotteryModel> QueryLottery(string date)
        {
            using (MySqlConnection conn = CreateConnection())
            {
                string sql = string.Format("select * from {0} where Ymd = '{1}' order by Sno desc", LottoryTbName, date);
                var lt = conn.Query<LotteryModel>(sql).ToList();
                return lt;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="date"></param>
        /// <param name="sno"></param>
        /// <returns></returns>
        public static LotteryModel QueryLottery(string date, string sno)
        {
            using (MySqlConnection conn = CreateConnection())
            {
                string sql = string.Format("select * from {0} where Ymd = '{1}' and sno ='{2}'", LottoryTbName, date, sno);
                return conn.Query<LotteryModel>(sql).FirstOrDefault();
            }
        }

        /// <summary>
        /// 通过日期查找最近开奖期数
        /// </summary>
        /// <param name="date">日期</param>
        /// <param name="topNum">最近期数</param>
        /// <returns></returns>
        public static List<LotteryModel> QueryLottery(string date, int topNum)
        {
            using (MySqlConnection conn = CreateConnection())
            {
                string sql = string.Format("select * from {0} where Ymd = '{1}' order by Sno desc limit 0,{2}", LottoryTbName, date, topNum);
                var lt = conn.Query<LotteryModel>(sql).ToList();
                return lt;
            }
        }

        /// <summary>
        /// 检测该日期下缺失的期数
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static List<int> CheckLottery(string date)
        {
            using (MySqlConnection conn = CreateConnection())
            {
                string sql = string.Format("select Sno from {0} where Ymd = '{1}'", LottoryTbName, date);
                return conn.Query<int>(sql).ToList();
            }
        }
        #endregion

        #region [一星趋势]
        /// <summary>
        /// 创建一星趋势表
        /// </summary>
        /// <param name="tableName"></param>
        public static void CreateTendency1Table(string tableName)
        {
            string sql = string.Format("SELECT count(1) FROM information_schema.TABLES WHERE table_name ='{0}'", tableName);

            using (MySqlConnection conn = CreateConnection())
            {
                int count = conn.Query<int>(sql).FirstOrDefault();
                if (count == 0)
                {
                    sql = string.Format("CREATE TABLE {0} ( " +
                                   @"ID      int  auto_increment not null primary key,     " +
                                   @"Ymd     CHAR( 8 )      NOT NULL,	     " +
                                   @"Sno     CHAR( 4 )      NOT NULL,	     " +
                                   @"Lottery CHAR( 9 )      NOT NULL,	     " +
                                   @"Big    INT            NOT NULL,	     " +
                                   @"Small    INT            NOT NULL,	     " +
                                   @"Odd    INT            NOT NULL,	     " +
                                   @"Pair    INT            NOT NULL,	     " +
                                   @"Dtime   CHAR( 12 )      NOT NULL 	     )", tableName);
                    conn.Execute(sql);
                }
            }
        }

        /// <summary>
        /// 新增一星走势数据
        /// </summary>
        /// <param name="type"></param>
        /// <param name="tendencys"></param>
        public static void SaveTendency1(Tendency1Enum type, List<TendencyModel> tendencys)
        {
            string tableName = getTendency1Table(type);
            using (MySqlConnection conn = CreateConnection())
            {
                conn.Open();
                MySqlTransaction trans = conn.BeginTransaction();

                string sqlCount = string.Format("SELECT count(1) FROM {0} where Ymd = @Ymd and Sno=@Sno", tableName);

                try
                {
                    string sql = null;
                    for (int i = 0; i < tendencys.Count; i++)
                    {
                        TendencyModel lm = tendencys[i];
                        int count = conn.Query<int>(sqlCount, lm).FirstOrDefault();
                        if (count == 0)
                        {
                            ///新增
                            sql = string.Format("insert into {0}(Ymd      ," +
                                                                        @"Sno      ," +
                                                                        @"Lottery  ," +
                                                                        @"Big      ," +
                                                                        @"Small    ," +
                                                                        @"Odd      ," +
                                                                        @"Pair     ," +
                                                                        @"Dtime     )" +
                                                        @" VALUES (" +
                                                                        @"@Ymd      ," +
                                                                        @"@Sno      ," +
                                                                        @"@Lottery  ," +
                                                                        @"@Big      ," +
                                                                        @"@Small    ," +
                                                                        @"@Odd      ," +
                                                                        @"@Pair     ," +
                                                                        @"@Dtime     " +
                                                        @")", tableName);
                        }
                        else
                        {
                            ///修改
                            sql = string.Format("Update {0} set Big       =@Big      ," +
                                                                "Small    =@Small    ," +
                                                                "Odd      =@Odd      ," +
                                                                "Pair     =@Pair     " +
                                                                "where Ymd = @Ymd and Sno=@Sno   ", tableName);
                        }
                        conn.Execute(sql, lm, trans);
                    }
                    trans.Commit();
                }
                catch (Exception)
                {
                    trans.Rollback();
                }
            }
        }

        /// <summary>
        /// 查找一星走势
        /// </summary>
        /// <param name="type"></param>
        /// <param name="date"></param>
        /// <param name="topNum"></param>
        /// <returns></returns>
        public static List<TendencyModel> QueryTendency1(Tendency1Enum type, string date, int topNum)
        {
            string tableName = getTendency1Table(type);
            using (MySqlConnection conn = CreateConnection())
            {
                string sql = string.Format("select * from {0} where Ymd = '{1}' order by Sno desc limit 0,{2}", tableName, date, topNum);
                return conn.Query<TendencyModel>(sql).ToList();
            }
        }

        /// <summary>
        ///  查找指定期号
        /// </summary>
        /// <param name="type"></param>
        /// <param name="date"></param>
        /// <param name="sno"></param>
        /// <returns></returns>
        public static Tendency2Model QueryTendency1(Tendency1Enum type, string date, string sno)
        {
            string tableName = getTendency1Table(type);
            using (MySqlConnection conn = CreateConnection())
            {
                string sql = string.Format("select * from {0} where Ymd = '{1}' and sno='{2}'", tableName, date, sno);
                return conn.Query<Tendency2Model>(sql).FirstOrDefault();
            }
        }

        /// <summary>
        /// 获取一星指定日期最大走势
        /// </summary>
        /// <param name="type"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        public static TendencyModel QueryMaxTendency1(Tendency1Enum type, string date)
        {
            string tableName = getTendency1Table(type);
            using (MySqlConnection conn = CreateConnection())
            {
                string sql = string.Format("select  max(Big) Big,    " +
                                                    "max(Small) Small,       " +
                                                    "max(Odd) Odd,           " +
                                                    "max(Pair) Pair      " +
                                                    "from {0} where Ymd = '{1}'", tableName, date);
                return conn.Query<TendencyModel>(sql).FirstOrDefault();
            }
        }

        #endregion

        #region [二星趋势]
        /// <summary>
        /// 创建二星趋势表
        /// </summary>
        /// <param name="tableName"></param>
        public static void CreateTendency2Table(string tableName)
        {
            string sql = string.Format("SELECT count(1) FROM information_schema.TABLES WHERE table_name ='{0}'", tableName);

            using (MySqlConnection conn = CreateConnection())
            {
                int count = conn.Query<int>(sql).FirstOrDefault();
                if (count == 0)
                {
                    sql = string.Format("CREATE TABLE {0} ( " +
                                   @"ID      int  auto_increment not null primary key,     " +
                                   @"Ymd     CHAR( 8 )      NOT NULL,	     " +
                                   @"Sno     CHAR( 4 )      NOT NULL,	     " +
                                   @"Lottery CHAR( 9 )      NOT NULL,	     " +
                                   @"Big    INT            NOT NULL,	     " +
                                   @"Small    INT            NOT NULL,	     " +
                                   @"BigSmall    INT            NOT NULL,	     " +
                                   @"SmallBig    INT            NOT NULL,	     " +
                                   @"Odd    INT            NOT NULL,	     " +
                                   @"Pair    INT            NOT NULL,	     " +
                                   @"OddPair    INT            NOT NULL,	     " +
                                   @"PairOdd    INT            NOT NULL,	     " +
                                   @"Dbl    INT            NOT NULL,	     " +
                                   @"Dtime   CHAR( 12 )      NOT NULL 	     )", tableName);
                    conn.Execute(sql);
                }
            }
        }

        /// <summary>
        /// 新增二星走势数据
        /// </summary>
        /// <param name="lotterys"></param>
        public static void SaveTendency2(Tendency2Enum type, List<Tendency2Model> lotterys)
        {
            string tableName = getTendency2Table(type);
            using (MySqlConnection conn = CreateConnection())
            {
                conn.Open();
                MySqlTransaction trans = conn.BeginTransaction();

                string sqlCount = string.Format("SELECT count(1) FROM {0} where Ymd = @Ymd and Sno=@Sno", tableName);

                try
                {
                    string sql = null;
                    for (int i = 0; i < lotterys.Count; i++)
                    {
                        Tendency2Model lm = lotterys[i];
                        int count = conn.Query<int>(sqlCount, lm).FirstOrDefault();
                        if (count == 0)
                        {
                            ///新增
                            sql = string.Format("insert into {0}(Ymd      ," +
                                                                        @"Sno      ," +
                                                                        @"Lottery  ," +
                                                                        @"Big      ," +
                                                                        @"Small    ," +
                                                                        @"BigSmall ," +
                                                                        @"SmallBig ," +
                                                                        @"Odd      ," +
                                                                        @"Pair     ," +
                                                                        @"OddPair  ," +
                                                                        @"PairOdd  ," +
                                                                        @"Dbl      ," +
                                                                        @"Dtime     )" +
                                                        @" VALUES (" +
                                                                        @"@Ymd      ," +
                                                                        @"@Sno      ," +
                                                                        @"@Lottery  ," +
                                                                        @"@Big      ," +
                                                                        @"@Small    ," +
                                                                        @"@BigSmall ," +
                                                                        @"@SmallBig ," +
                                                                        @"@Odd      ," +
                                                                        @"@Pair     ," +
                                                                        @"@OddPair  ," +
                                                                        @"@PairOdd  ," +
                                                                        @"@Dbl      ," +
                                                                        @"@Dtime     " +
                                                        @")", tableName);

                        }
                        else
                        {
                            ///修改
                            sql = string.Format("Update {0} set Big       =@Big      ," +
                                                                "Small    =@Small    ," +
                                                                "BigSmall =@BigSmall ," +
                                                                "SmallBig =@SmallBig ," +
                                                                "Odd      =@Odd      ," +
                                                                "Pair     =@Pair     ," +
                                                                "OddPair  =@OddPair  ," +
                                                                "PairOdd  =@PairOdd  ," +
                                                                "Dbl      =@Dbl       " +
                                                                "where Ymd = @Ymd and Sno=@Sno   ", tableName);
                        }
                        conn.Execute(sql, lm, trans);
                    }
                    trans.Commit();
                }
                catch (Exception)
                {
                    trans.Rollback();
                }
            }
        }

        /// <summary>
        ///  查找指定期号
        /// </summary>
        /// <param name="type"></param>
        /// <param name="date"></param>
        /// <param name="sno"></param>
        /// <returns></returns>
        public static Tendency2Model QueryTendency2(Tendency2Enum type, string date, string sno)
        {
            string tableName = getTendency2Table(type);
            using (MySqlConnection conn = CreateConnection())
            {
                string sql = string.Format("select * from {0} where Ymd = '{1}' and sno='{2}'", tableName, date, sno);
                return conn.Query<Tendency2Model>(sql).FirstOrDefault();
            }
        }

        /// <summary>
        /// 通过日期查找二星走势
        /// </summary>
        /// <param name="type"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        public static List<Tendency2Model> QueryTendency2(Tendency2Enum type, string date)
        {
            string tableName = getTendency2Table(type);
            using (MySqlConnection conn = CreateConnection())
            {
                string sql = string.Format("select * from {0} where Ymd = '{1}' order by Sno desc", tableName, date);
                var lt = conn.Query<Tendency2Model>(sql).ToList();
                return lt;
            }
        }

        /// <summary>
        /// 通过日期查找最近走势图
        /// </summary>
        /// <param name="date">日期</param>
        /// <param name="topNum">最近期数</param>
        /// <returns></returns>
        public static List<Tendency2Model> QueryTendency2(Tendency2Enum type, string date, int topNum)
        {
            string tableName = getTendency2Table(type);
            using (MySqlConnection conn = CreateConnection())
            {
                string sql = string.Format("select * from {0} where Ymd = '{1}' order by Sno desc limit 0,{2}", tableName, date, topNum);
                var lt = conn.Query<Tendency2Model>(sql).ToList();
                return lt;
            }
        }

        /// <summary>
        /// 通过日期区间查找走势图
        /// </summary>
        /// <param name="date">日期</param>
        /// <param name="topNum">最近期数</param>
        /// <returns></returns>
        public static List<Tendency2Model> QueryTendency2Range(Tendency2Enum type, string startDate, string endDate)
        {
            string tableName = getTendency2Table(type);
            using (MySqlConnection conn = CreateConnection())
            {
                string sql = string.Format("select * from {0} where  Ymd  BETWEEN {1}  AND {2} order by ymd,Sno desc", tableName, startDate, endDate);
                var lt = conn.Query<Tendency2Model>(sql).ToList();
                return lt;
            }
        }

        /// <summary>
        /// 获取二星指定日期最大走势
        /// </summary>
        /// <param name="type"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        public static Tendency2Model QueryMaxTendency2(Tendency2Enum type, string date)
        {
            string tableName = getTendency2Table(type);
            using (MySqlConnection conn = CreateConnection())
            {
                string sql = string.Format("select  max(Big) Big,    " +
                                                    "max(Small) Small,       " +
                                                    "max(BigSmall) BigSmall, " +
                                                    "max(SmallBig) SmallBig, " +
                                                    "max(Odd) Odd,           " +
                                                    "max(Pair) Pair,         " +
                                                    "max(OddPair) OddPair,   " +
                                                    "max(PairOdd) PairOdd,   " +
                                                    "max(Dbl) Dbl            " +
                                                    " from {0} where Ymd = '{1}'", tableName, date);

                return conn.Query<Tendency2Model>(sql).FirstOrDefault();
            }
        }
        #endregion
    }
}