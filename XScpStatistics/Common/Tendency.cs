using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XScpStatistics.Model;

namespace XScpStatistics.Common
{
    /// <summary>
    /// 记录有多少次未中奖了(趋势记录)
    /// </summary>
    public class Tendency
    {
        /// <summary>
        /// 大小组合名称
        /// </summary>
        public static List<string> Lt_BigSmallNames = new List<string> { "大大", "大小", "小大", "小小", };

        /// <summary>
        /// 奇偶组合名称
        /// </summary>
        public static List<string> Lt_OddPairNames = new List<string> { "奇奇", "奇偶", "偶奇", "偶偶" };

        /// <summary>
        /// 记录有多少次未中奖了(趋势记录)
        /// </summary>
        public static List<TendencyModel> Lt_Tendencys = new List<TendencyModel>();

        public static List<TendencyModel> Lt_TendencyOther = new List<TendencyModel>();

        /// <summary>
        /// 当前走势
        /// </summary>
        public static TendencyModel CurrTendency
        {
            get
            {
                if (Lt_Tendencys.Count > 0)
                {
                    return GetTendency(Lt_Tendencys.Count - 1);
                }
                else
                {
                    return new TendencyModel();
                }
            }
        }

        /// <summary>
        /// 新增趋势记录
        /// </summary>
        /// <param name="lottery"></param>
        public static void AddTendency(TendencyModel tm)
        {
            Lt_Tendencys.Add(tm);
        }

        /// <summary>
        /// 获取趋势记录
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public static TendencyModel GetTendency(int index)
        {
            if (Lt_Tendencys.Count > index)
                return Lt_Tendencys[index];
            return null;
        }

        /// <summary>
        /// 清除趋势记录
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public static void ClearTendencys()
        {
            Lt_Tendencys.Clear();
        }
    }
}
