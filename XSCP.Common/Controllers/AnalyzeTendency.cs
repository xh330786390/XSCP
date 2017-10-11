using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using XSCP.Common.Model;
namespace XSCP.Data.Controllers
{
    public class AnalyzeTendency
    {
        #region 一星(定位胆)
        /// <summary>
        /// 获取字段信息
        /// </summary>
        /// <param name="type"></param>
        /// <param name="number"></param>
        /// <returns></returns>
        private FieldInfo getFieldInfo(Type type, int number)
        {
            FieldInfo fieldInfo = type.GetField("Num" + number.ToString(),
                BindingFlags.Public |
                BindingFlags.NonPublic |
                BindingFlags.Instance |
                BindingFlags.Static);
            return fieldInfo;
        }

        /// <summary>
        /// 获取字段值
        /// </summary>
        /// <param name="lm"></param>
        /// <param name="number1"></param>
        /// <param name="number2"></param>
        /// <returns></returns>
        private Value GetValue(LotteryModel lm, int number)
        {
            Value value = new Value();
            Type type = typeof(LotteryModel);
            PropertyInfo fieldInfo = Reflection.GetPropertyInfo(type, "Num" + number.ToString());
            value.v1 = Convert.ToInt16(fieldInfo.GetValue(lm));//获取值
            return value;
        }

        private class Value
        {
            public int v1;
            public int v2;
        }

        /// <summary>
        /// 大
        /// </summary>
        /// <param name="lottery"></param>
        /// <param name="preTendency1"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public int BigNum(LotteryModel lottery, TendencyModel preTendency1, int index)
        {
            Value value = GetValue(lottery, index);
            if (value.v1 > 4)
            {
                return 0;
            }

            int count = 1;
            if (preTendency1 != null)
            {
                count = preTendency1.Big + 1;
            }
            return count;
        }

        /// <summary>
        /// 小
        /// </summary>
        /// <param name="lottery"></param>
        /// <param name="preTendency1"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public int SmallNum(LotteryModel lottery, TendencyModel preTendency1, int index)
        {
            Value value = GetValue(lottery, index);
            if (value.v1 < 5)
            {
                return 0;
            }

            int count = 1;
            if (preTendency1 != null)
            {
                count = preTendency1.Small + 1;
            }
            return count;
        }

        /// <summary>
        /// 012路
        /// </summary>
        /// <param name="lottery"></param>
        /// <param name="preTendency1"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public int RoadNum012(LotteryModel lottery, TendencyModel preTendency1, int index, int num012)
        {
            Value value = GetValue(lottery, index);
            if (value.v1 % 3 == num012)
            {
                return 0;
            }

            int count = 1;
            if (preTendency1 == null) preTendency1 = new TendencyModel();

            if (num012 == 0)
            {
                count = preTendency1.No_0 + 1;
            }
            else if (num012 == 1)
            {
                count = preTendency1.No_1 + 1;
            }
            else if (num012 == 2)
            {
                count = preTendency1.No_2 + 1;
            }
            return count;
        }

        /// <summary>
        /// 奇偶
        /// </summary>
        /// <param name="lottery"></param>
        /// <param name="preTendency1"></param>
        /// <param name="index"></param>
        /// <param name="oddPair"></param>
        /// <returns></returns>
        public int OddPairNum(LotteryModel lottery, TendencyModel preTendency1, int index, int oddPair)
        {
            Value value = GetValue(lottery, index);
            if (value.v1 % 2 == oddPair)
            {
                return 0;
            }

            int count = 1;
            if (preTendency1 != null)
            {
                if (oddPair == 1)
                {
                    count = preTendency1.Odd + 1;
                }
                else if (oddPair == 0)
                {
                    count = preTendency1.Pair + 1;
                }
            }
            return count;
        }

        /// <summary>
        /// 数字
        /// </summary>
        /// <returns></returns>
        public int Digital(List<LotteryModel> lotterys, int index, int number)
        {
            LotteryModel lm;
            int pre = lotterys.Count - 1;
            for (int i = index; i < lotterys.Count; i++)
            {
                lm = lotterys[i];
                int[] array = new int[5];
                for (int j = 1; j <= 5; j++) array[j - 1] = GetValue(lm, j).v1;

                if (array.Contains<int>(number))
                {
                    pre = i;
                    break;
                }
            }
            return pre - index;
        }
        #endregion

        #region 二星
        /// <summary>
        /// 获取字段值
        /// </summary>
        /// <param name="lm"></param>
        /// <param name="number1"></param>
        /// <param name="number2"></param>
        /// <returns></returns>
        private Value GetValue(LotteryModel lm, int number1, int number2)
        {
            Value value = new Value();
            Type type = typeof(LotteryModel);
            PropertyInfo propertyInfo = Reflection.GetPropertyInfo(type, "Num" + number1.ToString());
            value.v1 = Convert.ToInt16(propertyInfo.GetValue(lm));//获取值第一个值

            propertyInfo = Reflection.GetPropertyInfo(type, "Num" + number2.ToString());
            value.v2 = Convert.ToInt16(propertyInfo.GetValue(lm));//获取值第二个值
            return value;
        }

        /// <summary>
        /// 求上次出现大数的 离现在有几次了
        /// </summary>
        /// <param name="curLottery"></param>
        /// <param name="preTendency2"></param>
        /// <param name="number1"></param>
        /// <param name="number2"></param>
        /// <returns></returns>
        public int BigNum(LotteryModel curLottery, Tendency2Model preTendency2, int number1, int number2)
        {
            Value value = GetValue(curLottery, number1, number2);
            if (value.v1 > 4 && value.v2 > 4 && value.v1 != value.v2)
            {
                return 0;
            }

            int count = 1;
            if (preTendency2 != null)
            {
                count = preTendency2.Big + 1;
            }
            return count;
        }

        /// <summary>
        /// 求上次出现大小数的 离现在有几次了
        /// </summary>
        /// <param name="curLottery"></param>
        /// <param name="preTendency2"></param>
        /// <param name="number1"></param>
        /// <param name="number2"></param>
        /// <returns></returns>
        public int BigSmallNum(LotteryModel curLottery, Tendency2Model preTendency2, int number1, int number2)
        {
            Value value = GetValue(curLottery, number1, number2);
            if (value.v1 > 4 && value.v2 < 5 && value.v1 != value.v2)
            {
                return 0;
            }

            int count = 1;
            if (preTendency2 != null)
            {
                count = preTendency2.BigSmall + 1;
            }
            return count;
        }

        /// <summary>
        /// 求上次出现小大数的 离现在有几次了
        /// </summary>
        /// <param name="curLottery"></param>
        /// <param name="preTendency2"></param>
        /// <param name="number1"></param>
        /// <param name="number2"></param>
        /// <returns></returns>
        public int SmallBigNum(LotteryModel curLottery, Tendency2Model preTendency2, int number1, int number2)
        {
            Value value = GetValue(curLottery, number1, number2);
            if (value.v1 < 5 && value.v2 > 4 && value.v1 != value.v2)
            {
                return 0;
            }

            int count = 1;
            if (preTendency2 != null)
            {
                count = preTendency2.SmallBig + 1;
            }
            return count;
        }

        /// <summary>
        /// 求上次出现小数的 离现在有几次了
        /// </summary>
        /// <param name="curLottery"></param>
        /// <param name="preTendency2"></param>
        /// <param name="number1"></param>
        /// <param name="number2"></param>
        /// <returns></returns>
        public int SmallNum(LotteryModel curLottery, Tendency2Model preTendency2, int number1, int number2)
        {
            Value value = GetValue(curLottery, number1, number2);
            if (value.v1 < 5 && value.v2 < 5 && value.v1 != value.v2)
            {
                return 0;
            }

            int count = 1;
            if (preTendency2 != null)
            {
                count = preTendency2.Small + 1;
            }
            return count;
        }

        /// <summary>
        /// 求上次出现奇偶数的 离现在有几次了
        /// </summary>
        /// <param name="curLottery"></param>
        /// <param name="preTendency2"></param>
        /// <param name="number1"></param>
        /// <param name="number2"></param>
        /// <param name="num1"></param>
        /// <param name="num2"></param>
        /// <returns></returns>
        public int OddPairNum(LotteryModel curLottery, Tendency2Model preTendency2, int number1, int number2, int num1, int num2)
        {
            Value value = GetValue(curLottery, number1, number2);
            if (value.v1 % 2 == num1 && value.v2 % 2 == num2 && value.v1 != value.v2)
            {
                return 0;
            }

            int count = 1;
            if (preTendency2 != null)
            {
                if (num1 == 1 && num2 == 1)
                {
                    count = preTendency2.Odd + 1;
                }
                else if (num1 == 0 && num2 == 0)
                {
                    count = preTendency2.Pair + 1;
                }
                else if (num1 == 1 && num2 == 0)
                {
                    count = preTendency2.OddPair + 1;
                }
                else if (num1 == 0 && num2 == 1)
                {
                    count = preTendency2.PairOdd + 1;
                }
            }
            return count;
        }

        /// <summary>
        /// 求上次出现重数的 离现在有几次了
        /// </summary>
        /// <param name="curLottery"></param>
        /// <param name="preTendency2"></param>
        /// <param name="number1"></param>
        /// <param name="number2"></param>
        /// <returns></returns>
        public int DblNum(LotteryModel curLottery, Tendency2Model preTendency2, int number1, int number2)
        {
            Value value = GetValue(curLottery, number1, number2);
            if (value.v1 == value.v2)
            {
                return 0;
            }

            int count = 1;
            if (preTendency2 != null)
            {
                count = preTendency2.Dbl + 1;
            }
            return count;
        }

        #endregion

        #region 三星
        /// <summary>
        /// 三星
        /// </summary>
        /// <returns></returns>
        public int UnitThree(List<LotteryModel> lotterys, int index, int num1, int num2, int num3)
        {
            LotteryModel lm;
            List<int> list;
            int pre = lotterys.Count;
            for (int i = index; i < lotterys.Count; i++)
            {
                lm = lotterys[i];
                list = new List<int>();
                list.Add(GetValue(lm, num1).v1);
                list.Add(GetValue(lm, num2).v1);
                list.Add(GetValue(lm, num3).v1);

                if (list.Distinct().ToList().Count < 3)
                {
                    pre = i;
                    break;
                }
            }
            return pre - index;
        }

        /// <summary>
        /// 三星
        /// </summary>
        /// <returns></returns>
        public int UnitSex(List<LotteryModel> lotterys, int index, int num1, int num2, int num3)
        {
            LotteryModel lm;
            List<int> list;
            int pre = lotterys.Count;
            for (int i = index; i < lotterys.Count; i++)
            {
                lm = lotterys[i];
                list = new List<int>();
                list.Add(GetValue(lm, num1).v1);
                list.Add(GetValue(lm, num2).v1);
                list.Add(GetValue(lm, num3).v1);

                if (list.Distinct().ToList().Count == 3)
                {
                    pre = i;
                    break;
                }
            }
            return pre - index;
        }
        #endregion

        #region 四星
        #endregion

        #region 五星
        /// <summary>
        /// 五星
        /// </summary>
        /// <param name="lotterys"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public int FiveStart(List<LotteryModel> lotterys, int index)
        {
            LotteryModel lm;
            int pre = lotterys.Count - 1;
            List<int> list;
            for (int i = index; i < lotterys.Count; i++)
            {
                lm = lotterys[i];
                list = new List<int>();
                list.Add(lm.Num1);
                list.Add(lm.Num2);
                list.Add(lm.Num3);
                list.Add(lm.Num4);
                list.Add(lm.Num5);
                if (list.Distinct().ToList().Count == 5)
                {
                    pre = i;
                    break;
                }
            }
            return pre - index;
        }
        #endregion

        #region [二星前后包胆]
        public bool ExistBeforeAfterTwo(LotteryModel lottery, int num)
        {
            if (lottery.Num1 != lottery.Num2 && (lottery.Num1 == num || lottery.Num2 == num))
            {
                return true;
            }
            else if (lottery.Num4 != lottery.Num5 && (lottery.Num4 == num || lottery.Num5 == num))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion

        #region [全部数字]
        public bool ExistDigit(LotteryModel lottery, int num)
        {
            return lottery.Lottery.Contains(num.ToString()) ? true : false;
        }
        #endregion
    }
}
