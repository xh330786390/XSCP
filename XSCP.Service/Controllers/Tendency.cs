using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XSCP.Service.Model;

namespace XSCP.Service.Controllers
{
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
        public List<TendencyModel> Lt_Tendencys = new List<TendencyModel>();

        /// <summary>
        /// 获取最大走势值
        /// </summary>
        /// <returns></returns>
        public TendencyModel GetMaxTendency()
        {
            TendencyModel tm = new TendencyModel();
            tm.Big = Lt_Tendencys.Max(l => l.Big);//大
            tm.Small = Lt_Tendencys.Max(l => l.Small);//小
            tm.BigSmall = Lt_Tendencys.Max(l => l.BigSmall);//大小
            tm.SmallBig = Lt_Tendencys.Max(l => l.SmallBig);//小大

            tm.Odd = Lt_Tendencys.Max(l => l.Odd);//奇
            tm.Pair = Lt_Tendencys.Max(l => l.Pair);//偶
            tm.OddPair = Lt_Tendencys.Max(l => l.OddPair);//奇偶
            tm.PairOdd = Lt_Tendencys.Max(l => l.PairOdd);//偶奇
            tm.Dbl = Lt_Tendencys.Max(l => l.Dbl);//重
            return tm;
        }

        /// <summary>
        /// 获取最大走势值
        /// </summary>
        /// <returns></returns>
        public static TendencyModel GetMaxTendency(List<TendencyModel> Lt_Tendencys)
        {
            TendencyModel tm = new TendencyModel();
            tm.Big = Lt_Tendencys.Max(l => l.Big);//大
            tm.Small = Lt_Tendencys.Max(l => l.Small);//小
            tm.BigSmall = Lt_Tendencys.Max(l => l.BigSmall);//大小
            tm.SmallBig = Lt_Tendencys.Max(l => l.SmallBig);//小大

            tm.Odd = Lt_Tendencys.Max(l => l.Odd);//奇
            tm.Pair = Lt_Tendencys.Max(l => l.Pair);//偶
            tm.OddPair = Lt_Tendencys.Max(l => l.OddPair);//奇偶
            tm.PairOdd = Lt_Tendencys.Max(l => l.PairOdd);//偶奇
            tm.Dbl = Lt_Tendencys.Max(l => l.Dbl);//重
            return tm;
        }

        /// <summary>
        /// 当前走势
        /// </summary>
        public TendencyModel CurrTendency
        {
            get
            {
                if (this.Lt_Tendencys.Count > 0)
                {
                    return GetTendency(this.Lt_Tendencys.Count - 1);
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
        public void AddTendency(TendencyModel tm)
        {
            this.Lt_Tendencys.Add(tm);
        }

        /// <summary>
        /// 获取趋势记录
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public TendencyModel GetTendency(int index)
        {
            if (this.Lt_Tendencys.Count > index)
                return this.Lt_Tendencys[index];
            return null;
        }

        /// <summary>
        /// 清除趋势记录
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public void ClearTendencys()
        {
            this.Lt_Tendencys.Clear();
        }
    }
}
