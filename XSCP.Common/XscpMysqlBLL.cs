using System.Collections.Generic;
using XSCP.Common.Model;
using XSCP.Common.Extend;
using System;
using XSCP.Data.Controllers;

namespace XSCP.Common
{
    /// <summary>
    /// @1.新增奖号
    /// @2.连续上次期数
    ///     1).连续期数，趋势则同步连续
    ///     2).不连续期数，趋势则从新开始
    /// @3.检测缺失期数
    /// </summary>
    public class XscpMysqlBLL
    {
        /// <summary>
        /// 更新彩票
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="ltData"></param>
        /// <returns></returns>
        public static bool Update(DateTime dt, List<string> ltData)
        {
            try
            {
                ///新增开奖号码
                List<LotteryModel> lt_LotteryModel = XscpMysqlBLL.SaveLottery(dt, ltData);

                ///新增一星走势
                XscpMysqlBLL.SaveTendency1(Tendency1Enum.TenThousand, lt_LotteryModel);
                XscpMysqlBLL.SaveTendency1(Tendency1Enum.Thousand, lt_LotteryModel);
                XscpMysqlBLL.SaveTendency1(Tendency1Enum.Hundred, lt_LotteryModel);
                XscpMysqlBLL.SaveTendency1(Tendency1Enum.Ten, lt_LotteryModel);
                XscpMysqlBLL.SaveTendency1(Tendency1Enum.One, lt_LotteryModel);

                ///前二、后二 包胆走势
                XscpMysqlBLL.SaveTendencyDigit1(lt_LotteryModel);

                ///所有数字走势
                XscpMysqlBLL.SaveTendencyAllDigit(lt_LotteryModel);

                ///新增二星走势
                XscpMysqlBLL.SaveTendency2(Tendency2Enum.Before, lt_LotteryModel);
                XscpMysqlBLL.SaveTendency2(Tendency2Enum.After, lt_LotteryModel);
                return true;
            }
            catch (Exception er)
            {
                return false;
            }
        }

        #region [开奖号码]
        /// <summary>
        /// 保存奖号
        /// </summary>
        /// <param name="lottery"></param>
        public static List<LotteryModel> SaveLottery(DateTime dt, List<string> ltData)
        {
            if (ltData == null || ltData.Count == 0) return null;

            List<string> lottery = new List<string>();
            foreach (string item in ltData)
            {
                lottery.Add(item.Replace("期", ","));
            }

            List<LotteryModel> lt_lotterys = new List<LotteryModel>();

            lottery.ForEach(l =>
            {
                LotteryModel lotteryModel = getLottery(dt, l);
                lt_lotterys.Add(lotteryModel);

                int sno = int.Parse(lotteryModel.Sno);
                if (sno == 1)
                {
                    //上一日期号
                    dt = dt.AddDays(-1);
                }
            });

            try
            {
                MysqlHelper.SaveLotteryData(lt_lotterys);
            }
            catch (Exception er)
            {
                return null;
            }

            return lt_lotterys;
        }

        private static LotteryModel getLottery(DateTime dt, string lottery)
        {
            string[] array = lottery.Split(',');
            int[] arrayint = new int[array.Length];
            for (int i = 0; i < array.Length; i++)
            {
                try
                {
                    arrayint[i] = int.Parse(array[i]);
                }
                catch { }
            }

            LotteryModel lotteryModel = new LotteryModel();
            lotteryModel.Sno = arrayint[0].ToString().PadLeft(4, '0');
            lotteryModel.Ymd = dt.ToString("yyyyMMdd");
            lotteryModel.Lottery = lottery.Substring(lottery.IndexOf(',') + 1);
            lotteryModel.Num1 = arrayint[1];
            lotteryModel.Num2 = arrayint[2];
            lotteryModel.Num3 = arrayint[3];
            lotteryModel.Num4 = arrayint[4];
            lotteryModel.Num5 = arrayint[5];
            lotteryModel.Dtime = dt.ToXscpDateTime(arrayint[0]);
            return lotteryModel;
        }

        /// <summary>
        ///  查询最近期数
        /// </summary>
        /// <param name="date">日期</param>
        /// <param name="topNum">最近期数</param>
        /// <returns></returns>
        public static List<LotteryModel> QueryLottery(string date, int topNum)
        {
            return MysqlHelper.QueryLottery(date, topNum);
        }

        /// <summary>
        /// 检测该日期下缺失的期数
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static List<int> CheckLottery(string date)
        {
            return MysqlHelper.CheckLottery(date);
        }
        #endregion

        #region [一星趋势]
        /// <summary>
        /// 新增一星走势数据
        /// </summary>
        /// <param name="type"></param>
        /// <param name="tendencys"></param>
        public static void SaveTendency1(Tendency1Enum type, List<LotteryModel> ltData)
        {
            int index = (int)type;

            LotteryModel minData = ltData[ltData.Count - 1];

            //上一期趋势
            TendencyModel preTendency1 = null;

            //本次上一次开奖趋势，除非最后一期
            int prePperiod = int.Parse(minData.Sno) - 1;
            if (prePperiod > 0)
            {
                preTendency1 = MysqlHelper.QueryTendency1(type, minData.Ymd, prePperiod.ToString().PadLeft(4, '0'));
            }

            AnalyzeTendency At = new AnalyzeTendency();
            List<TendencyModel> ltTendency = new List<TendencyModel>();
            for (int i = ltData.Count - 1; i >= 0; i--)
            {
                LotteryModel lm = ltData[i];
                TendencyModel tm = new TendencyModel();
                tm.Ymd = lm.Ymd;
                tm.Lottery = lm.Lottery;
                tm.Sno = lm.Sno;

                tm.Big = At.BigNum(lm, preTendency1, index);//大大
                tm.Small = At.SmallNum(lm, preTendency1, index);//小小

                tm.Odd = At.OddPairNum(lm, preTendency1, index, 1);//奇奇
                tm.Pair = At.OddPairNum(lm, preTendency1, index, 0);//偶偶

                tm.Dtime = lm.Dtime;

                ltTendency.Add(tm);

                prePperiod = int.Parse(lm.Sno);
                if (prePperiod == 1380)
                {
                    preTendency1 = null;
                }
                else
                {
                    preTendency1 = tm;
                }
            }

            MysqlHelper.SaveTendency1(type, ltTendency);
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
            return MysqlHelper.QueryTendency1(type, date, topNum);
        }

        /// <summary>
        /// 获取一星指定日期最大走势
        /// </summary>
        /// <param name="type"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        public static TendencyModel QueryMaxTendency1(Tendency1Enum type, string date)
        {
            return MysqlHelper.QueryMaxTendency1(type, date);
        }
        #endregion

        #region [二星趋势]
        /// <summary>
        /// 添加二星趋势
        /// </summary>
        /// <param name="type"></param>
        /// <param name="dt"></param>
        /// <param name="ltData"></param>
        public static void SaveTendency2(Tendency2Enum type, List<LotteryModel> ltData)
        {
            int index1, index2 = 0;
            if (type == Tendency2Enum.Before)
            {
                index1 = 1;
                index2 = 2;
            }
            else
            {
                index1 = 4;
                index2 = 5;
            }

            LotteryModel minData = ltData[ltData.Count - 1];

            //上一期趋势
            Tendency2Model preTendency2 = null;

            //本次上一次开奖趋势，除非最后一期
            int prePperiod = int.Parse(minData.Sno) - 1;
            if (prePperiod > 0)
            {
                preTendency2 = MysqlHelper.QueryTendency2(type, minData.Ymd, prePperiod.ToString().PadLeft(4, '0'));
            }

            AnalyzeTendency At = new AnalyzeTendency();
            List<Tendency2Model> ltTendency2 = new List<Tendency2Model>();
            for (int i = ltData.Count - 1; i >= 0; i--)
            {
                LotteryModel lm = ltData[i];
                Tendency2Model tm = new Tendency2Model();
                tm.Ymd = lm.Ymd;
                tm.Lottery = lm.Lottery;
                tm.Sno = lm.Sno;

                tm.Big = At.BigNum(lm, preTendency2, index1, index2);//大大
                tm.Small = At.SmallNum(lm, preTendency2, index1, index2);//小小
                tm.BigSmall = At.BigSmallNum(lm, preTendency2, index1, index2);//大小
                tm.SmallBig = At.SmallBigNum(lm, preTendency2, index1, index2);//小大

                tm.Odd = At.OddPairNum(lm, preTendency2, index1, index2, 1, 1);//奇奇
                tm.Pair = At.OddPairNum(lm, preTendency2, index1, index2, 0, 0);//偶偶
                tm.OddPair = At.OddPairNum(lm, preTendency2, index1, index2, 1, 0);//奇偶
                tm.PairOdd = At.OddPairNum(lm, preTendency2, index1, index2, 0, 1);//偶奇

                tm.Dbl = At.DblNum(lm, preTendency2, index1, index2);//重数
                tm.Dtime = lm.Dtime;

                ltTendency2.Add(tm);

                prePperiod = int.Parse(lm.Sno);
                if (prePperiod == 1380)
                {
                    preTendency2 = null;
                }
                else
                {
                    preTendency2 = tm;
                }
            }

            MysqlHelper.SaveTendency2(type, ltTendency2);
        }

        /// <summary>
        /// 通过日期查找最近走势图
        /// </summary>
        /// <param name="date">日期</param>
        /// <param name="topNum">最近期数</param>
        /// <returns></returns>
        public static List<Tendency2Model> QueryTendency2(Tendency2Enum type, string date, int topNum)
        {
            return MysqlHelper.QueryTendency2(type, date, topNum);
        }

        /// <summary>
        /// 通过日期区间查找走势图
        /// </summary>
        /// <param name="type"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public static List<Tendency2Model> QueryTendency2Range(Tendency2Enum type, string startDate, string endDate)
        {
            return MysqlHelper.QueryTendency2Range(type, startDate, endDate);
        }

        /// <summary>
        /// 获取二星指定日期最大走势
        /// </summary>
        /// <param name="type"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        public static Tendency2Model QueryMaxTendency2(Tendency2Enum type, string date)
        {
            return MysqlHelper.QueryMaxTendency2(type, date);
        }

        #endregion

        #region [前二、后二 包胆走势]
        public static void SaveTendencyDigit1(List<LotteryModel> ltData)
        {
            LotteryModel minData = ltData[ltData.Count - 1];

            //上一期趋势
            Tendency1Model preTendency1 = null;

            //本次上一次开奖趋势，除非最后一期
            int prePperiod = int.Parse(minData.Sno) - 1;
            if (prePperiod > 0)
            {
                preTendency1 = MysqlHelper.QueryTendencyDigit1(minData.Ymd, prePperiod.ToString().PadLeft(4, '0'));
            }

            if (preTendency1 == null) preTendency1 = new Tendency1Model();
            AnalyzeTendency At = new AnalyzeTendency();
            List<Tendency1Model> ltTendency = new List<Tendency1Model>();
            for (int i = ltData.Count - 1; i >= 0; i--)
            {
                LotteryModel lm = ltData[i];
                Tendency1Model tm = new Tendency1Model();
                tm.Ymd = lm.Ymd;
                tm.Lottery = lm.Lottery;
                tm.Sno = lm.Sno;

                if (tm.Sno == "0186")
                {
                    Console.WriteLine("");
                }

                tm.Num0 = At.ExistBeforeAfterTwo(lm, 0) ? 0 : preTendency1.Num0 + 1;
                tm.Num1 = At.ExistBeforeAfterTwo(lm, 1) ? 0 : preTendency1.Num1 + 1;
                tm.Num2 = At.ExistBeforeAfterTwo(lm, 2) ? 0 : preTendency1.Num2 + 1;
                tm.Num3 = At.ExistBeforeAfterTwo(lm, 3) ? 0 : preTendency1.Num3 + 1;
                tm.Num4 = At.ExistBeforeAfterTwo(lm, 4) ? 0 : preTendency1.Num4 + 1;
                tm.Num5 = At.ExistBeforeAfterTwo(lm, 5) ? 0 : preTendency1.Num5 + 1;
                tm.Num6 = At.ExistBeforeAfterTwo(lm, 6) ? 0 : preTendency1.Num6 + 1;
                tm.Num7 = At.ExistBeforeAfterTwo(lm, 7) ? 0 : preTendency1.Num7 + 1;
                tm.Num8 = At.ExistBeforeAfterTwo(lm, 8) ? 0 : preTendency1.Num8 + 1;
                tm.Num9 = At.ExistBeforeAfterTwo(lm, 9) ? 0 : preTendency1.Num9 + 1;

                tm.Dtime = lm.Dtime;
                ltTendency.Add(tm);

                prePperiod = int.Parse(lm.Sno);
                if (prePperiod == 1380)
                {
                    preTendency1 = new Tendency1Model();
                }
                else
                {
                    preTendency1 = tm;
                }
            }

            MysqlHelper.SaveTendencyDigit1(ltTendency);
        }

        public static List<Tendency1Model> QueryTendencyDigit1(string date, int topNum)
        {
            return MysqlHelper.QueryTendencyDigit1(date, topNum);
        }

        public static Tendency1Model QueryMaxTendencyDigit1(string startDate, string endDate)
        {
            return MysqlHelper.QueryMaxTendencyDigit1(startDate, endDate);
        }
        #endregion

        #region [所有数字]

        public static void SaveTendencyAllDigit(List<LotteryModel> ltData)
        {
            LotteryModel minData = ltData[ltData.Count - 1];

            //上一期趋势
            Tendency1Model preTendency1 = null;

            //本次上一次开奖趋势，除非最后一期
            int prePperiod = int.Parse(minData.Sno) - 1;
            if (prePperiod > 0)
            {
                preTendency1 = MysqlHelper.QueryAllTendencyDigit1(minData.Ymd, prePperiod.ToString().PadLeft(4, '0'));
            }

            if (preTendency1 == null) preTendency1 = new Tendency1Model();
            AnalyzeTendency At = new AnalyzeTendency();
            List<Tendency1Model> ltTendency = new List<Tendency1Model>();
            for (int i = ltData.Count - 1; i >= 0; i--)
            {
                LotteryModel lm = ltData[i];
                Tendency1Model tm = new Tendency1Model();
                tm.Ymd = lm.Ymd;
                tm.Lottery = lm.Lottery;
                tm.Sno = lm.Sno;

                tm.Num0 = At.ExistDigit(lm, 0) ? 0 : preTendency1.Num0 + 1;
                tm.Num1 = At.ExistDigit(lm, 1) ? 0 : preTendency1.Num1 + 1;
                tm.Num2 = At.ExistDigit(lm, 2) ? 0 : preTendency1.Num2 + 1;
                tm.Num3 = At.ExistDigit(lm, 3) ? 0 : preTendency1.Num3 + 1;
                tm.Num4 = At.ExistDigit(lm, 4) ? 0 : preTendency1.Num4 + 1;
                tm.Num5 = At.ExistDigit(lm, 5) ? 0 : preTendency1.Num5 + 1;
                tm.Num6 = At.ExistDigit(lm, 6) ? 0 : preTendency1.Num6 + 1;
                tm.Num7 = At.ExistDigit(lm, 7) ? 0 : preTendency1.Num7 + 1;
                tm.Num8 = At.ExistDigit(lm, 8) ? 0 : preTendency1.Num8 + 1;
                tm.Num9 = At.ExistDigit(lm, 9) ? 0 : preTendency1.Num9 + 1;

                tm.Dtime = lm.Dtime;
                ltTendency.Add(tm);

                prePperiod = int.Parse(lm.Sno);
                if (prePperiod == 1380)
                {
                    preTendency1 = new Tendency1Model();
                }
                else
                {
                    preTendency1 = tm;
                }
            }

            MysqlHelper.SaveAllTendencyDigit1(ltTendency);
        }

        public static List<Tendency1Model> QueryAllTendencyDigit1(string date, int topNum)
        {
            return MysqlHelper.QueryAllTendencyDigit1(date, topNum);
        }

        public static Tendency1Model QueryAllMaxTendencyDigit1(string startDate, string endDate)
        {
            return MysqlHelper.QueryAllMaxTendencyDigit1(startDate, endDate);
        }
        #endregion
    }
}
