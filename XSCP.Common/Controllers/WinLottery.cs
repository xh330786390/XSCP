using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using   XSCP.Common.Model;
using System.Reflection;

namespace   XSCP.Data.Controllers
{
    public class WinLottery
    {
        #region 中奖统计
        public List<WinLotteryModel> Lt_WinLotterys = new List<WinLotteryModel>();
        public void CalculatorWinLottery(List<Tendency2Model> lt, string fieldName, string unitName)
        {
            Lt_WinLotterys.Clear();
            Tendency2Model curfm;
            Tendency2Model nextfm;
            Type type = typeof(Tendency2Model);
            int value = -1;
            WinLotteryModel sm = null;
            for (int i = lt.Count - 1; i >= 1; i--)
            {
                curfm = lt[i];

                PropertyInfo propertyInfo = Reflection.GetPropertyInfo(type, fieldName);
                value = Convert.ToInt16(propertyInfo.GetValue(curfm, null));
                if (value == 0)
                {
                    if (i - 1 >= 0)
                    {
                        nextfm = lt[i - 1];
                        sm = new WinLotteryModel();
                        sm.KjLong = Convert.ToInt16(propertyInfo.GetValue(nextfm, null));

                        sm.UnitName = unitName;
                        Lt_WinLotterys.Add(sm);
                    }
                }
            }
        }

        public void CalculatorWinLottery(List<Tendency2Model> lt, List<TendencyType> lt_type, string enName)
        {
            Lt_WinLotterys.Clear();
            Tendency2Model curfm;
            Tendency2Model nextfm;
            Type type = typeof(Tendency2Model);
            int value = -1;
            WinLotteryModel sm = null;
            int start = 0;
            int end = 0;
            if (enName == "AllOddPair")
            {
                start = 1;
                end = 5;
            }
            else if (enName == "AllBigSmall")
            {
                start = 5;
                end = 9;
            }

            for (int k = start; k < end; k++)
            {
                for (int i = lt.Count - 1; i >= 1; i--)
                {
                    curfm = lt[i];
                    PropertyInfo propertyInfo = Reflection.GetPropertyInfo(type, lt_type[k].EnName);
                    value = Convert.ToInt16(propertyInfo.GetValue(curfm, null));
                    if (value == 0)
                    {
                        if (i - 1 >= 0)
                        {
                            nextfm = lt[i - 1];
                            sm = new WinLotteryModel();
                            sm.KjLong = Convert.ToInt16(propertyInfo.GetValue(nextfm, null));

                            sm.UnitName = lt_type[k].ChName;
                            Lt_WinLotterys.Add(sm);
                        }
                    }
                }
            }
        }

        public List<WinLotteryModel> CalculatorWinLottery(List<Tendency2Model> lt)
        {
            Tendency2Model curfm;
            Tendency2Model nextfm;

            for (int i = lt.Count - 1; i >= 1; i--)
            {
                curfm = lt[i];
                if (i - 1 >= 0)
                {
                    nextfm = lt[i - 1];
                    return addStatisticsValue(curfm, nextfm);
                }
            }
            return null;
        }

        public void CalculatorCompareWinLottery(List<CompareTwoModel> lt)
        {
            Lt_WinLotterys.Clear();

            CompareTwoModel curfm;
            CompareTwoModel nextfm;
            int value = -1;
            WinLotteryModel sm = null;


            for (int i = lt.Count - 1; i >= 1; i--)
            {
                curfm = lt[i];
                value = curfm.TendencyValue;
                if (value == 0)
                {
                    if (i - 1 >= 0)
                    {
                        nextfm = lt[i - 1];
                        sm = new WinLotteryModel();
                        sm.KjLong = nextfm.TendencyValue;

                        Lt_WinLotterys.Add(sm);
                    }
                }
            }
        }

        /// <summary>
        /// 给统计表添加数据
        /// </summary>
        /// <param name="curFm"></param>
        /// <param name="nextFm"></param>
        private List<WinLotteryModel> addStatisticsValue(Tendency2Model curfm, Tendency2Model nextfm)
        {
            List<WinLotteryModel> lt = new List<WinLotteryModel>();
            WinLotteryModel wm = new WinLotteryModel();
            if (curfm.Dbl == 0)
            {
                wm.KjLong = nextfm.Dbl;
                wm.UnitName = "Dbl";
            }
            else if (curfm.Big == 0)
            {
                wm.KjLong = nextfm.Big;
                wm.UnitName = "Big";
            }
            else if (curfm.BigSmall == 0)
            {
                wm.KjLong = nextfm.BigSmall;
                wm.UnitName = "BigSmall";
            }
            else if (curfm.SmallBig == 0)
            {
                wm.KjLong = nextfm.SmallBig;
                wm.UnitName = "SmallBig";
            }
            else if (curfm.Small == 0)
            {
                wm.KjLong = nextfm.Small;
                wm.UnitName = "Small";
            }
            lt.Add(wm);

            wm = new WinLotteryModel();
            if (curfm.Dbl == 0)
            {
                wm.KjLong = nextfm.Dbl;
                wm.UnitName = "Dbl";
            }
            else if (curfm.Odd == 0)
            {
                wm.KjLong = nextfm.Odd;
                wm.UnitName = "Odd";
            }
            else if (curfm.OddPair == 0)
            {
                wm.KjLong = nextfm.OddPair;
                wm.UnitName = "OddPair";
            }
            else if (curfm.PairOdd == 0)
            {
                wm.KjLong = nextfm.PairOdd;
                wm.UnitName = "PairOdd";
            }
            else if (curfm.Pair == 0)
            {
                wm.KjLong = nextfm.Pair;
                wm.UnitName = "Pair";
            }
            lt.Add(wm);
            return lt;
        }
        #endregion

        #region 组合中奖统计
        public List<WinLotteryModel> Lt_UnitWinLotterys = new List<WinLotteryModel>();
        public void CalculatorUnitWinLottery(List<TendencyUnitModel> lt, string fieldName, string unitName)
        {
            TendencyUnitModel curfm;
            TendencyUnitModel nextfm;
            Type type = typeof(TendencyUnitModel);
            int value = -1;
            WinLotteryModel sm = null;
            for (int i = 0; i < lt.Count; i++)
            {
                curfm = lt[i];

                PropertyInfo propertyInfo = Reflection.GetPropertyInfo(type, fieldName);
                value = Convert.ToInt16(propertyInfo.GetValue(curfm, null));
                if (value == 0)
                {
                    if (i + 1 < lt.Count)
                    {
                        nextfm = lt[i + 1];
                        sm = new WinLotteryModel();
                        sm.KjLong = Convert.ToInt16(propertyInfo.GetValue(nextfm, null));
                        sm.UnitName = unitName;
                        Lt_UnitWinLotterys.Add(sm);
                    }
                }
            }
        }
        #endregion
    }

    public class WinLottery<T>
    {
        #region 中奖统计
        public List<WinLotteryModel> Lt_WinLotterys = new List<WinLotteryModel>();

        public void CalculatorWinLottery(List<T> lt, string fieldName, string unitName)
        {
            Lt_WinLotterys.Clear();
            T curfm;
            T nextfm;
            Type type = typeof(T);
            int value = -1;
            WinLotteryModel sm = null;
            for (int i = lt.Count - 1; i >= 1; i--)
            {
                curfm = lt[i];

                PropertyInfo propertyInfo = Reflection.GetPropertyInfo(type, fieldName);
                value = Convert.ToInt16(propertyInfo.GetValue(curfm, null));
                if (value == 0)
                {
                    if (i - 1 >= 0)
                    {
                        nextfm = lt[i - 1];
                        sm = new WinLotteryModel();
                        sm.KjLong = Convert.ToInt16(propertyInfo.GetValue(nextfm, null));

                        sm.UnitName = unitName;
                        Lt_WinLotterys.Add(sm);
                    }
                }
            }
        }

        public void CalculatorWinLottery(List<T> lt, List<TendencyType> lt_type)
        {
            Lt_WinLotterys.Clear();
            T curfm;
            T nextfm;
            Type type = typeof(T);
            int value = -1;
            WinLotteryModel sm = null;
            for (int k = 0; k < lt_type.Count - 1; k++)
            {
                for (int i = lt.Count - 1; i >= 1; i--)
                {
                    curfm = lt[i];
                    PropertyInfo propertyInfo = Reflection.GetPropertyInfo(type, lt_type[k].EnName);
                    value = Convert.ToInt16(propertyInfo.GetValue(curfm, null));
                    if (value == 0)
                    {
                        if (i - 1 >= 0)
                        {
                            nextfm = lt[i - 1];
                            sm = new WinLotteryModel();
                            sm.KjLong = Convert.ToInt16(propertyInfo.GetValue(nextfm, null));

                            sm.UnitName = lt_type[k].ChName;
                            Lt_WinLotterys.Add(sm);
                        }
                    }
                }
            }
        }
        #endregion

        #region 组合中奖统计
        public List<WinLotteryModel> Lt_UnitWinLotterys = new List<WinLotteryModel>();
        public void CalculatorUnitWinLottery(List<TendencyUnitModel> lt, string fieldName, string unitName)
        {
            TendencyUnitModel curfm;
            TendencyUnitModel nextfm;
            Type type = typeof(TendencyUnitModel);
            int value = -1;
            WinLotteryModel sm = null;
            for (int i = 0; i < lt.Count; i++)
            {
                curfm = lt[i];

                PropertyInfo propertyInfo = Reflection.GetPropertyInfo(type, fieldName);
                value = Convert.ToInt16(propertyInfo.GetValue(curfm, null));
                if (value == 0)
                {
                    if (i + 1 < lt.Count)
                    {
                        nextfm = lt[i + 1];
                        sm = new WinLotteryModel();
                        sm.KjLong = Convert.ToInt16(propertyInfo.GetValue(nextfm, null));
                        sm.UnitName = unitName;
                        Lt_UnitWinLotterys.Add(sm);
                    }
                }
            }
        }
        #endregion
    }
}
