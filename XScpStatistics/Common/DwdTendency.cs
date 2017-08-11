using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XScpStatistics.Model;

namespace XScpStatistics.Common
{
    public class DwdTendency
    {
        /// <summary>
        /// 定位胆
        /// </summary>
        public static List<DwdTendencyModel> Lt_Dwd = new List<DwdTendencyModel>();

        /// <summary>
        /// 当前走势
        /// </summary>
        public static DwdTendencyModel GetCurrentDwdTendency(int digitNum)
        {
            DwdTendencyModel dtm = null;
            var vs = Lt_Dwd.Where(l => l.type == digitNum).ToList();
            if (vs.Count > 0)
            {
                dtm = vs[vs.Count - 1];
            }
            return dtm;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static int GetCount()
        {
            var vs = Lt_Dwd.Where(l => l.type == 1).ToList();
            return vs.Count;
        }

        /// <summary>
        /// 新增趋势记录
        /// </summary>
        /// <param name="lottery"></param>
        public static void AddDwdTendency(DwdTendencyModel tm)
        {
            Lt_Dwd.Add(tm);
        }

        /// <summary>
        /// 清除趋势记录
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public static void ClearDwdTendencys()
        {
            Lt_Dwd.Clear();
        }

        #region 大
        /// <summary>
        /// 求上次出现大数的 离现在有几次了
        /// </summary>
        /// <returns></returns>
        public int BigNum(int index, int digitNum)
        {
            LotteryModel lm;
            int pre = Lottery.Lt_Lotterys.Count - 1;
            for (int i = index; i < Lottery.Lt_Lotterys.Count; i++)
            {
                lm = Lottery.Lt_Lotterys[i];
                if (digitNum == 1 && lm.num1 > 4)
                {
                    pre = i;
                    break;
                }
                else if (digitNum == 2 && lm.num2 > 4)
                {
                    pre = i;
                    break;
                }
                else if (digitNum == 3 && lm.num3 > 4)
                {
                    pre = i;
                    break;
                }
                else if (digitNum == 4 && lm.num4 > 4)
                {
                    pre = i;
                    break;
                }
                else if (digitNum == 5 && lm.num5 > 4)
                {
                    pre = i;
                    break;
                }
            }
            return pre - index;
        }



        /// <summary>
        /// 求上次出现小数的 离现在有几次了
        /// </summary>
        /// <returns></returns>
        public int SmallNum(int index, int digitNum)
        {
            LotteryModel lm;
            int pre = Lottery.Lt_Lotterys.Count - 1;
            for (int i = index; i < Lottery.Lt_Lotterys.Count; i++)
            {
                lm = Lottery.Lt_Lotterys[i];
                if (digitNum == 1 && lm.num1 < 5)
                {
                    pre = i;
                    break;
                }
                else if (digitNum == 2 && lm.num2 < 5)
                {
                    pre = i;
                    break;
                }
                else if (digitNum == 3 && lm.num3 < 5)
                {
                    pre = i;
                    break;
                }
                else if (digitNum == 4 && lm.num4 < 5)
                {
                    pre = i;
                    break;
                }
                else if (digitNum == 5 && lm.num5 < 5)
                {
                    pre = i;
                    break;
                }
            }
            return pre - index;
        }

        /// <summary>
        /// 求上次出现奇或偶数的 离现在有几次了
        /// </summary>
        /// <returns></returns>
        public int OddPairNum(int index, int oddPair, int digitNum)
        {
            LotteryModel lm;
            int pre = Lottery.Lt_Lotterys.Count - 1;
            for (int i = index; i < Lottery.Lt_Lotterys.Count; i++)
            {
                lm = Lottery.Lt_Lotterys[i];
                if (digitNum == 1 && lm.num1 % 2 == oddPair)
                {
                    pre = i;
                    break;
                }
                else if (digitNum == 2 && lm.num2 % 2 == oddPair)
                {
                    pre = i;
                    break;
                }
                else if (digitNum == 3 && lm.num3 % 2 == oddPair)
                {
                    pre = i;
                    break;
                }
                else if (digitNum == 4 && lm.num4 % 2 == oddPair)
                {
                    pre = i;
                    break;
                }
                else if (digitNum == 5 && lm.num5 % 2 == oddPair)
                {
                    pre = i;
                    break;
                }
            }
            return pre - index;
        }
        #endregion
    }
}
