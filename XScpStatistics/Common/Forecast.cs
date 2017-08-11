using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XScpStatistics.Model;

namespace XScpStatistics.Common
{
    /// <Summary>
    /// 
    /// </Summary>
    public class Forecast
    {
        /// <Summary>
        /// 预测数据
        /// </Summary>
        public static List<ForecastModel> Lt_Forecasts = new List<ForecastModel>();

        /// <summary>
        /// 存储预测投的次序名称
        /// </summary>
        public static List<string[]> Lt_AllUnitNames = new List<string[]>();

        public static List<AllForecastMode> Lt_AllForecasts = new List<AllForecastMode>();

        /// <Summary>
        /// 预测数据
        /// </Summary>
        public static void InitForecast()
        {
            Lt_Forecasts.Clear();//清空数据
            if (Tendency.Lt_Tendencys.Count > 0)
            {
                CurrentTendency.SetCTendencysValue(Tendency.CurrTendency);
                addForecastData(Tendency.Lt_BigSmallNames, CurrentTendency.Lt_CurrentBS, CurrentTendency.Lt_CurrentOP, Lt_Forecasts);
                addForecastData(Tendency.Lt_OddPairNames, CurrentTendency.Lt_CurrentOP, CurrentTendency.Lt_CurrentBS, Lt_Forecasts);
            }
        }

        /// <Summary>
        /// 添加记录
        /// </Summary>
        /// <param name="fm"></param>
        private static void addForecastData(List<string> lt_names, List<CurrentTendencyModel> lt_ctm1, List<CurrentTendencyModel> lt_ctm2, List<ForecastModel> lt)
        {
            ForecastModel fm;
            CurrentTendencyModel ctm;

            for (int i = 0; i < lt_names.Count; i++)
            {
                ctm = lt_ctm1[i];
                fm = new ForecastModel();
                int sum = ctm.Value * 4;
                for (int j = 0; j < lt_ctm2.Count; j++)
                {
                    sum += lt_ctm2[j].Value;//求和
                }

                int diff = 0;
                for (int j = 0; j < lt_ctm2.Count; j++)
                {
                    diff += Math.Abs(ctm.Value - lt_ctm2[j].Value);//求差
                }

                fm.UnitName = ctm.UnitName;
                fm.Sum = sum;
                fm.Diff = diff;
                fm.Current = ctm.Value;
                lt.Add(fm);
            }

            ///解析
            //    UnitName = "大大",
            //    Sum = fm.Big * 4 + fm.Odd + fm.OddPair + fm.PairOdd + fm.Pair,
            //    Diff = Math.Abs(fm.Big - fm.Odd) + Math.Abs(fm.Big - fm.OddPair) + Math.Abs(fm.Big - fm.PairOdd) + Math.Abs(fm.Big - fm.Pair),
            //    Current = fm.Big
        }

        /// <summary>
        /// 
        /// </summary>
        public static void AddAllUnitNames()
        {
            TendencyModel tm;
            List<ForecastModel> lt;
            Lt_AllUnitNames.Clear();
            for (int i = 0; i < Tendency.Lt_Tendencys.Count; i++)
            {
                tm = Tendency.Lt_Tendencys[i];
                lt = new List<ForecastModel>();

                CurrentTendency.SetCTendencysValue(tm);
                addForecastData(Tendency.Lt_BigSmallNames, CurrentTendency.Lt_CurrentBS, CurrentTendency.Lt_CurrentOP, lt);
                addForecastData(Tendency.Lt_OddPairNames, CurrentTendency.Lt_CurrentOP, CurrentTendency.Lt_CurrentBS, lt);

                var vs = lt.OrderBy(l => l.Diff).ThenBy(l => l.Sum).ToList();
                string[] array = new string[8];
                for (int j = 0; j < vs.Count; j++)
                {
                    array[j] = vs[j].UnitName;
                }
                Lt_AllUnitNames.Add(array);
            }
        }

        public static void AnalyzeAllForecasts()
        {
            string[] allUnitName;
            Lt_AllForecasts.Clear();
            if (Lt_AllUnitNames.Count > 0)
            {
                for (int i = 0; i < Lt_AllUnitNames.Count; i++)
                {
                    allUnitName = Lt_AllUnitNames[i];
                    try
                    {
                        addAllForecasts(allUnitName, i);
                    }
                    catch (Exception er)
                    {
                        System.Windows.Forms.MessageBox.Show(er.ToString());
                    }
                }
            }
        }

        private static void addAllForecasts(string[] unitNames, int index)
        {
            AllForecastMode afm = new AllForecastMode();
            afm.num1 = getTimes(unitNames[0], index);
            afm.num2 = getTimes(unitNames[1], index);
            afm.num3 = getTimes(unitNames[2], index);
            afm.num4 = getTimes(unitNames[3], index);
            afm.num5 = getTimes(unitNames[4], index);
            afm.num6 = getTimes(unitNames[5], index);
            afm.num7 = getTimes(unitNames[6], index);
            afm.num8 = getTimes(unitNames[7], index);
            Lt_AllForecasts.Add(afm);
        }

        /// <summary>
        /// 获取预测类型，离开奖的次数
        /// </summary>
        /// <param name="unitName"></param>
        /// <returns></returns>
        private static int getTimes(string unitName, int index)
        {
            int value = 0;
            bool bl = false;
            TendencyModel tm;
            for (int i = index; i < Tendency.Lt_Tendencys.Count; i++)
            {
                for (int j = i + 1; j < Tendency.Lt_Tendencys.Count; j++)
                {
                    tm = Tendency.Lt_Tendencys[j];
                    if (unitName == "大大" && Tendency.Lt_Tendencys[j].Big == 0 && tm.Dbl != 0)
                    {
                        value = j - i;
                        bl = true;
                    }
                    else if (unitName == "大小" && Tendency.Lt_Tendencys[j].BigSmall == 0 && tm.Dbl != 0)
                    {
                        value = j - i;
                        bl = true;
                    }
                    else if (unitName == "小大" && Tendency.Lt_Tendencys[j].SmallBig == 0 && tm.Dbl != 0)
                    {
                        value = j - i;
                        bl = true;
                    }
                    else if (unitName == "小小" && Tendency.Lt_Tendencys[j].Small == 0 && tm.Dbl != 0)
                    {
                        value = j - i;
                        bl = true;
                    }
                    else if (unitName == "奇奇" && Tendency.Lt_Tendencys[j].Odd == 0 && tm.Dbl != 0)
                    {
                        value = j - i;
                        bl = true;
                    }
                    else if (unitName == "奇偶" && Tendency.Lt_Tendencys[j].OddPair == 0 && tm.Dbl != 0)
                    {
                        value = j - i;
                        bl = true;
                    }
                    else if (unitName == "偶奇" && Tendency.Lt_Tendencys[j].PairOdd == 0 && tm.Dbl != 0)
                    {
                        value = j - i;
                        bl = true;
                    }
                    else if (unitName == "偶偶" && Tendency.Lt_Tendencys[j].Pair == 0 && tm.Dbl != 0)
                    {
                        value = j - i;
                        bl = true;
                    }

                    if (bl) return value;
                    else if (j == Tendency.Lt_Tendencys.Count - 1) return j - i;
                }
            }
            return value;
        }
    }
}
