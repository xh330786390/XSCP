using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XscpSys.Model;

namespace XscpSys.Controllers
{
    public class XsMath
    {
        /// <summary>
        /// 获取定位胆最大走势值
        /// </summary>
        /// <param name="Lt_Tendencys"></param>
        /// <returns></returns>
        public static Tendency1Model GetMaxTendency(List<Tendency1Model> Lt_Tendencys)
        {
            Tendency1Model tm = new Tendency1Model();
            tm.Num0 = Lt_Tendencys.Max(l => l.Num0);
            tm.Num1 = Lt_Tendencys.Max(l => l.Num1);
            tm.Num2 = Lt_Tendencys.Max(l => l.Num2);
            tm.Num3 = Lt_Tendencys.Max(l => l.Num3);
            tm.Num4 = Lt_Tendencys.Max(l => l.Num4);
            tm.Num5 = Lt_Tendencys.Max(l => l.Num5);
            tm.Num6 = Lt_Tendencys.Max(l => l.Num6);
            tm.Num7 = Lt_Tendencys.Max(l => l.Num7);
            tm.Num8 = Lt_Tendencys.Max(l => l.Num8);
            tm.Num9 = Lt_Tendencys.Max(l => l.Num9);
            return tm;
        }

        /// <summary>
        /// 获取定位胆最大走势值
        /// </summary>
        /// <param name="Lt_Tendencys"></param>
        /// <returns></returns>
        public static Tendency1Model GetMaxDwdTendency(List<Tendency1Model> Lt_Tendencys)
        {
            Tendency1Model tm = new Tendency1Model();
            tm.Big = Lt_Tendencys.Max(l => l.Big);
            tm.Small = Lt_Tendencys.Max(l => l.Small);
            tm.Odd = Lt_Tendencys.Max(l => l.Odd);
            tm.Pair = Lt_Tendencys.Max(l => l.Pair);
            return tm;
        }

        /// <summary>
        /// 获取前二、后二最大走势值
        /// </summary>
        /// <returns></returns>
        public static Tendency2Model GetMaxTendency(List<Tendency2Model> Lt_Tendencys)
        {
            Tendency2Model tm = new Tendency2Model();
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
        /// 获取前二、后二最大走势值
        /// </summary>
        /// <returns></returns>
        public static TendencyAllModel GetMaxTendency(List<TendencyAllModel> Lt_Tendencys)
        {
            TendencyAllModel tm = new TendencyAllModel();
            tm.ThreeBeforeStart = Lt_Tendencys.Max(l => l.ThreeBeforeStart);//前组三
            tm.SexBeforeStart = Lt_Tendencys.Max(l => l.SexBeforeStart);//前组六

            tm.ThreeAfterStart = Lt_Tendencys.Max(l => l.ThreeAfterStart);//后组三
            tm.SexAfterStart = Lt_Tendencys.Max(l => l.SexAfterStart);//后组六
            tm.FiveStart = Lt_Tendencys.Max(l => l.FiveStart);//五星
            return tm;
        }

    }
}
