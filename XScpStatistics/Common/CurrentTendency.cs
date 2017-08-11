using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XScpStatistics.Model;

namespace XScpStatistics.Common
{
    /// <summary>
    /// 
    /// </summary>
    public class CurrentTendency
    {
        /// <summary>
        /// 大小列表
        /// </summary>
        public static List<CurrentTendencyModel> Lt_CurrentBS;

        /// <summary>
        /// 奇偶列表
        /// </summary>
        public static List<CurrentTendencyModel> Lt_CurrentOP;

        static CurrentTendency()
        {
            Lt_CurrentBS = new List<CurrentTendencyModel>();

            for (int i = 0; i <Tendency.Lt_BigSmallNames.Count; i++)
            {
                Lt_CurrentBS.Add(new CurrentTendencyModel() { ID = 0, UnitName = Tendency.Lt_BigSmallNames[i] });//大小列表赋值
            }

            Lt_CurrentOP = new List<CurrentTendencyModel>();
            for (int i = 0; i < Tendency.Lt_OddPairNames.Count; i++)
            {
                Lt_CurrentOP.Add(new CurrentTendencyModel() { ID = 0, UnitName = Tendency.Lt_OddPairNames[i] });//奇偶列表赋值
            }
        }

        /// <summary>
        /// 设置
        /// </summary>
        public static void SetCTendencysValue(TendencyModel tm)
        {
            Lt_CurrentBS[0].Value = tm.Big;
            Lt_CurrentBS[1].Value = tm.BigSmall;
            Lt_CurrentBS[2].Value = tm.SmallBig;
            Lt_CurrentBS[3].Value = tm.Small;

            Lt_CurrentOP[0].Value = tm.Odd;
            Lt_CurrentOP[1].Value = tm.OddPair;
            Lt_CurrentOP[2].Value = tm.PairOdd;
            Lt_CurrentOP[3].Value = tm.Pair;
        }
    }
}
