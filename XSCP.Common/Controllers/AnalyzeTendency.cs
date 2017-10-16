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
        /// <summary>
        /// 0-9：质数
        /// </summary>
        private static List<int> Lt_Prime = new List<int>() { 1, 2, 3, 5, 7 };
        /// 0-9：合数
        private static List<int> Lt_Composite = new List<int>() { 0, 4, 6, 8, 9 };

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
        /// <param name="digit">位数：1 万 2 千 3 百 4 十 5 个</param>
        /// <returns></returns>
        public int BigNum(LotteryModel lottery, TendencyModel preTendency1, int digit)
        {
            Value value = GetValue(lottery, digit);
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
        /// <param name="digit">位数：1 万 2 千 3 百 4 十 5 个</param>
        /// <returns></returns>
        public int SmallNum(LotteryModel lottery, TendencyModel preTendency1, int digit)
        {
            Value value = GetValue(lottery, digit);
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
        /// <param name="digit">位数：1 万 2 千 3 百 4 十 5 个</param>
        /// <returns></returns>
        public int RoadNum012(LotteryModel lottery, TendencyModel preTendency1, int digit, int num012)
        {
            Value value = GetValue(lottery, digit);
            if (value.v1 % 3 == num012)
            {
                return 0;
            }

            int count = 1;
            if (preTendency1 != null)
            {
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
            }
            return count;
        }

        /// <summary>
        /// 奇偶
        /// </summary>
        /// <param name="lottery"></param>
        /// <param name="preTendency1"></param>
        /// <param name="digit">位数：1 万 2 千 3 百 4 十 5 个</param>
        /// <param name="oddPair"></param>
        /// <returns></returns>
        public int OddPairNum(LotteryModel lottery, TendencyModel preTendency1, int digit, int oddPair)
        {
            Value value = GetValue(lottery, digit);
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
        /// 质数
        /// </summary>
        /// <param name="lottery"></param>
        /// <param name="preTendency1"></param>
        /// <param name="digit">位数：1 万 2 千 3 百 4 十 5 个</param>
        /// <returns></returns>
        public int Prime(LotteryModel lottery, TendencyModel preTendency1, int digit)
        {
            Value value = GetValue(lottery, digit);
            if (Lt_Prime.Contains(value.v1))
            {
                return 0;
            }

            int count = 1;
            if (preTendency1 != null)
            {
                count = preTendency1.Prime + 1;
            }
            return count;
        }

        /// <summary>
        /// 合数
        /// </summary>
        /// <param name="lottery"></param>
        /// <param name="preTendency1"></param>
        /// <param name="digit">位数：1 万 2 千 3 百 4 十 5 个</param>
        /// <returns></returns>
        public int Composite(LotteryModel lottery, TendencyModel preTendency1, int digit)
        {
            Value value = GetValue(lottery, digit);
            if (Lt_Composite.Contains(value.v1))
            {
                return 0;
            }

            int count = 1;
            if (preTendency1 != null)
            {
                count = preTendency1.Composite + 1;
            }
            return count;
        }

        /// <summary>
        /// 大
        /// </summary>
        /// <param name="lottery"></param>
        /// <param name="preTendency1"></param>
        /// <param name="digit">位数：1 万 2 千 3 百 4 十 5 个</param>
        /// <returns></returns>
        public int Big_1(LotteryModel lottery, TendencyModel preTendency1, int digit)
        {
            Value value = GetValue(lottery, digit);
            if (value.v1 > 6)
            {
                return 0;
            }

            int count = 1;
            if (preTendency1 != null)
            {
                count = preTendency1.Big_1 + 1;
            }
            return count;
        }

        /// <summary>
        /// 中
        /// </summary>
        /// <param name="lottery"></param>
        /// <param name="preTendency1"></param>
        /// <param name="digit">位数：1 万 2 千 3 百 4 十 5 个</param>
        /// <returns></returns>
        public int Mid_1(LotteryModel lottery, TendencyModel preTendency1, int digit)
        {
            Value value = GetValue(lottery, digit);
            if (value.v1 > 2 && value.v1 < 7)
            {
                return 0;
            }

            int count = 1;
            if (preTendency1 != null)
            {
                count = preTendency1.Mid_1 + 1;
            }
            return count;
        }

        /// <summary>
        /// 小
        /// </summary>
        /// <param name="lottery"></param>
        /// <param name="preTendency1"></param>
        /// <param name="digit">位数：1 万 2 千 3 百 4 十 5 个</param>
        /// <returns></returns>
        public int Small_1(LotteryModel lottery, TendencyModel preTendency1, int digit)
        {
            Value value = GetValue(lottery, digit);
            if (value.v1 < 3)
            {
                return 0;
            }

            int count = 1;
            if (preTendency1 != null)
            {
                count = preTendency1.Small_1 + 1;
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

        #region 大中小
        /// <summary>
        ///大大
        /// </summary>
        /// <param name="curLottery"></param>
        /// <param name="preTendency2"></param>
        /// <param name="number1">1 万位、4 十位</param>
        /// <param name="number2">2 千万、5 个位</param>
        /// <returns></returns>
        public int Big1Big1(LotteryModel curLottery, Tendency2Model preTendency2, int number1, int number2)
        {
            Value value = GetValue(curLottery, number1, number2);
            if (value.v1 > 6 && value.v2 > 6 && value.v1 != value.v2)
            {
                return 0;
            }

            int count = 1;
            if (preTendency2 != null)
            {
                count = preTendency2.Big1Big1 + 1;
            }
            return count;
        }

        /// <summary>
        ///大中
        /// </summary>
        /// <param name="curLottery"></param>
        /// <param name="preTendency2"></param>
        /// <param name="number1">1 万位、4 十位</param>
        /// <param name="number2">2 千万、5 个位</param>
        /// <returns></returns>
        public int Big1Mid1(LotteryModel curLottery, Tendency2Model preTendency2, int number1, int number2)
        {
            Value value = GetValue(curLottery, number1, number2);
            if (value.v1 > 6 && value.v2 > 2 && value.v2 < 7 && value.v1 != value.v2)
            {
                return 0;
            }

            int count = 1;
            if (preTendency2 != null)
            {
                count = preTendency2.Big1Mid1 + 1;
            }
            return count;
        }

        /// <summary>
        ///大小
        /// </summary>
        /// <param name="curLottery"></param>
        /// <param name="preTendency2"></param>
        /// <param name="number1">1 万位、4 十位</param>
        /// <param name="number2">2 千万、5 个位</param>
        /// <returns></returns>
        public int Big1Small1(LotteryModel curLottery, Tendency2Model preTendency2, int number1, int number2)
        {
            Value value = GetValue(curLottery, number1, number2);
            if (value.v1 > 6 && value.v2 < 3 && value.v1 != value.v2)
            {
                return 0;
            }

            int count = 1;
            if (preTendency2 != null)
            {
                count = preTendency2.Big1Small1 + 1;
            }
            return count;
        }

        /// <summary>
        ///中大
        /// </summary>
        /// <param name="curLottery"></param>
        /// <param name="preTendency2"></param>
        /// <param name="number1">1 万位、4 十位</param>
        /// <param name="number2">2 千万、5 个位</param>
        /// <returns></returns>
        public int Mid1Big1(LotteryModel curLottery, Tendency2Model preTendency2, int number1, int number2)
        {
            Value value = GetValue(curLottery, number1, number2);
            if (value.v1 > 2 && value.v1 < 7 && value.v2 > 6 && value.v1 != value.v2)
            {
                return 0;
            }

            int count = 1;
            if (preTendency2 != null)
            {
                count = preTendency2.Mid1Big1 + 1;
            }
            return count;
        }

        /// <summary>
        ///中中
        /// </summary>
        /// <param name="curLottery"></param>
        /// <param name="preTendency2"></param>
        /// <param name="number1">1 万位、4 十位</param>
        /// <param name="number2">2 千万、5 个位</param>
        /// <returns></returns>
        public int Mid1Mid1(LotteryModel curLottery, Tendency2Model preTendency2, int number1, int number2)
        {
            Value value = GetValue(curLottery, number1, number2);
            if (value.v1 > 2 && value.v1 < 7 && value.v2 > 2 && value.v2 < 7 && value.v1 != value.v2)
            {
                return 0;
            }

            int count = 1;
            if (preTendency2 != null)
            {
                count = preTendency2.Mid1Mid1 + 1;
            }
            return count;
        }

        /// <summary>
        ///中小
        /// </summary>
        /// <param name="curLottery"></param>
        /// <param name="preTendency2"></param>
        /// <param name="number1">1 万位、4 十位</param>
        /// <param name="number2">2 千万、5 个位</param>
        /// <returns></returns>
        public int Mid1Small1(LotteryModel curLottery, Tendency2Model preTendency2, int number1, int number2)
        {
            Value value = GetValue(curLottery, number1, number2);
            if (value.v1 > 2 && value.v1 < 7 && value.v2 < 3 && value.v1 != value.v2)
            {
                return 0;
            }

            int count = 1;
            if (preTendency2 != null)
            {
                count = preTendency2.Mid1Small1 + 1;
            }
            return count;
        }

        /// <summary>
        ///小大
        /// </summary>
        /// <param name="curLottery"></param>
        /// <param name="preTendency2"></param>
        /// <param name="number1">1 万位、4 十位</param>
        /// <param name="number2">2 千万、5 个位</param>
        /// <returns></returns>
        public int Small1Big1(LotteryModel curLottery, Tendency2Model preTendency2, int number1, int number2)
        {
            Value value = GetValue(curLottery, number1, number2);
            if (value.v1 < 3 && value.v2 > 6 && value.v1 != value.v2)
            {
                return 0;
            }

            int count = 1;
            if (preTendency2 != null)
            {
                count = preTendency2.Small1Big1 + 1;
            }
            return count;
        }

        /// <summary>
        ///小中
        /// </summary>
        /// <param name="curLottery"></param>
        /// <param name="preTendency2"></param>
        /// <param name="number1">1 万位、4 十位</param>
        /// <param name="number2">2 千万、5 个位</param>
        /// <returns></returns>
        public int Small1Mid1(LotteryModel curLottery, Tendency2Model preTendency2, int number1, int number2)
        {
            Value value = GetValue(curLottery, number1, number2);
            if (value.v1 < 3 && value.v2 > 2 && value.v2 < 7 && value.v1 != value.v2)
            {
                return 0;
            }

            int count = 1;
            if (preTendency2 != null)
            {
                count = preTendency2.Small1Mid1 + 1;
            }
            return count;
        }

        /// <summary>
        ///小小
        /// </summary>
        /// <param name="curLottery"></param>
        /// <param name="preTendency2"></param>
        /// <param name="number1">1 万位、4 十位</param>
        /// <param name="number2">2 千万、5 个位</param>
        /// <returns></returns>
        public int Small1Small1(LotteryModel curLottery, Tendency2Model preTendency2, int number1, int number2)
        {
            Value value = GetValue(curLottery, number1, number2);
            if (value.v1 < 3 && value.v2 < 3)
            {
                return 0;
            }

            int count = 1;
            if (preTendency2 != null)
            {
                count = preTendency2.Small1Small1 + 1;
            }
            return count;
        }
        #endregion

        #region 012路
        public int RoadNum012(LotteryModel curLottery, Tendency2Model preTendency2, int number1, int number2, int num012_1, int num012_2)
        {
            Value value = GetValue(curLottery, number1, number2);
            if (value.v1 % 3 == num012_1 && value.v2 % 3 == num012_2)
            {
                return 0;
            }

            int count = 1;
            if (preTendency2 != null)
            {
                if (num012_1 == 0 && num012_2 == 0)
                {
                    count = preTendency2.No_00 + 1;
                }
                else if (num012_1 == 0 && num012_2 == 1)
                {
                    count = preTendency2.No_01 + 1;
                }
                else if (num012_1 == 0 && num012_2 == 2)
                {
                    count = preTendency2.No_02 + 1;
                }
                else if (num012_1 == 1 && num012_2 == 0)
                {
                    count = preTendency2.No_10 + 1;
                }
                else if (num012_1 == 1 && num012_2 == 1)
                {
                    count = preTendency2.No_11 + 1;
                }
                else if (num012_1 == 1 && num012_2 == 2)
                {
                    count = preTendency2.No_12 + 1;
                }
                else if (num012_1 == 2 && num012_2 == 0)
                {
                    count = preTendency2.No_20 + 1;
                }
                else if (num012_1 == 2 && num012_2 == 1)
                {
                    count = preTendency2.No_21 + 1;
                }
                else if (num012_1 == 2 && num012_2 == 2)
                {
                    count = preTendency2.No_22 + 1;
                }
            }
            return count;
        }
        #endregion

        #region 质合

        /// </summary>
        //private static List<int> Lt_Prime = new List<int>() { 1, 2, 3, 5, 7 };
        ///// 0-9：合数
        //private static List<int> Lt_Composite = new List<int>() { 0, 4, 6, 8, 9 };
        /// <summary>
        /// 质质数
        /// </summary>
        /// <param name="curLottery"></param>
        /// <param name="preTendency2"></param>
        /// <param name="number1"></param>
        /// <param name="number2"></param>
        /// <returns></returns>
        public int PrimePrime(LotteryModel curLottery, Tendency2Model preTendency2, int number1, int number2)
        {
            Value value = GetValue(curLottery, number1, number2);
            if (Lt_Prime.Contains(value.v1) && Lt_Prime.Contains(value.v2))
            {
                return 0;
            }

            int count = 1;
            if (preTendency2 != null)
            {
                count = preTendency2.PrimePrime + 1;
            }
            return count;
        }

        /// <summary>
        /// 质合数
        /// </summary>
        /// <param name="curLottery"></param>
        /// <param name="preTendency2"></param>
        /// <param name="number1"></param>
        /// <param name="number2"></param>
        /// <returns></returns>
        public int PrimeComposite(LotteryModel curLottery, Tendency2Model preTendency2, int number1, int number2)
        {
            Value value = GetValue(curLottery, number1, number2);
            if (Lt_Prime.Contains(value.v1) && Lt_Composite.Contains(value.v2))
            {
                return 0;
            }

            int count = 1;
            if (preTendency2 != null)
            {
                count = preTendency2.PrimeComposite + 1;
            }
            return count;
        }

        /// <summary>
        /// 合质数
        /// </summary>
        /// <param name="curLottery"></param>
        /// <param name="preTendency2"></param>
        /// <param name="number1"></param>
        /// <param name="number2"></param>
        /// <returns></returns>
        public int CompositePrime(LotteryModel curLottery, Tendency2Model preTendency2, int number1, int number2)
        {
            Value value = GetValue(curLottery, number1, number2);
            if (Lt_Composite.Contains(value.v1) && Lt_Prime.Contains(value.v2))
            {
                return 0;
            }

            int count = 1;
            if (preTendency2 != null)
            {
                count = preTendency2.CompositePrime + 1;
            }
            return count;
        }

        /// <summary>
        /// 合合数
        /// </summary>
        /// <param name="curLottery"></param>
        /// <param name="preTendency2"></param>
        /// <param name="number1"></param>
        /// <param name="number2"></param>
        /// <returns></returns>
        public int CompositeComposite(LotteryModel curLottery, Tendency2Model preTendency2, int number1, int number2)
        {
            Value value = GetValue(curLottery, number1, number2);
            if (Lt_Composite.Contains(value.v1) && Lt_Composite.Contains(value.v2))
            {
                return 0;
            }

            int count = 1;
            if (preTendency2 != null)
            {
                count = preTendency2.CompositeComposite + 1;
            }
            return count;
        }
        #endregion

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
