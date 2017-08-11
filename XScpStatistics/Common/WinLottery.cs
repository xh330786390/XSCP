using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XScpStatistics.Model;

namespace XScpStatistics.Common
{
    public class WinLottery
    {
        public static List<WinLotteryModel> Lt_WinLotterys = new List<WinLotteryModel>();//统计组合的开奖次数

        public static void CalculatorWinLottery()
        {
            Lt_WinLotterys.Clear();
            TendencyModel curfm;
            TendencyModel nextfm;

            for (int i = Tendency.Lt_Tendencys.Count - 1; i >= 1; i--)
            {
                curfm = Tendency.Lt_Tendencys[i];
                if (i - 1 > 0)
                {
                    nextfm = Tendency.Lt_Tendencys[i - 1];
                    addStatisticsValue(nextfm, curfm);
                }
            }
        }

        /// <summary>
        /// 给统计表添加数据
        /// </summary>
        /// <param name="curFm"></param>
        /// <param name="nextFm"></param>
        private static void addStatisticsValue(TendencyModel curfm, TendencyModel nextfm)
        {
            WinLotteryModel sm = new WinLotteryModel();
            if (nextfm.Dbl == 0)
            {
                sm.BigSmallValue = curfm.Dbl;
                sm.UnitName = "重重";
            }
            else if (nextfm.Big == 0)
            {
                sm.BigSmallValue = curfm.Big;
                sm.UnitName = "大大";
            }
            else if (nextfm.BigSmall == 0)
            {
                sm.BigSmallValue = curfm.BigSmall;
                sm.UnitName = "大小";
            }
            else if (nextfm.SmallBig == 0)
            {
                sm.BigSmallValue = curfm.SmallBig;
                sm.UnitName = "小大";
            }
            else if (nextfm.Small == 0)
            {
                sm.BigSmallValue = curfm.Small;
                sm.UnitName = "小小";
            }

            if (nextfm.Dbl == 0)
            {
                sm.BigSmallValue = curfm.Dbl;
                sm.UnitName += "重重";
            }
            else if (nextfm.Odd == 0)
            {
                sm.OddPairValue = curfm.Odd;
                sm.UnitName += "奇奇";
            }
            else if (nextfm.OddPair == 0)
            {
                sm.OddPairValue = curfm.OddPair;
                sm.UnitName += "奇偶";
            }
            else if (nextfm.PairOdd == 0)
            {
                sm.OddPairValue = curfm.PairOdd;
                sm.UnitName += "偶奇";
            }
            else if (nextfm.Pair == 0)
            {
                sm.OddPairValue = curfm.Pair;
                sm.UnitName += "偶偶";
            }

            //var vs = Lt_WinLotterys.Where(lt => lt.UnitName == sm.UnitName && lt.BigSmallValue == sm.BigSmallValue && lt.OddPairValue == sm.OddPairValue).ToList();
            Lt_WinLotterys.Add(sm);

            //if (vs.Count == 0)
            //{
            //    sm.Sum = 1;
            //    Lt_WinLotterys.Add(sm);
            //}
            //else
            //{
            //    vs.ForEach(l => l.Sum += 1);
            //}
        }
    }
}
