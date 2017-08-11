using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XScpStatistics.Model;
using System.Reflection;

namespace XScpStatistics.Common
{
    public class AnalyzeTendency
    {
        #region 旧方法
        #region 大小
        /// <summary>
        /// 求上次出现大数的 离现在有几次了
        /// </summary>
        /// <returns></returns>
        public int BigNum(int index, EnumStyle es)
        {
            LotteryModel lm;
            int pre = Lottery.Lt_Lotterys.Count - 1;
            for (int i = index; i < Lottery.Lt_Lotterys.Count; i++)
            {
                lm = Lottery.Lt_Lotterys[i];

                if (es == EnumStyle.后二 && lm.num4 > 4 && lm.num5 > 4 && lm.num4 != lm.num5)
                {
                    pre = i;
                    break;
                }
                else if (es == EnumStyle.前二 && lm.num1 > 4 && lm.num2 > 4 && lm.num1 != lm.num2)
                {
                    pre = i;
                    break;
                }
            }
            return pre - index;
        }

        /// <summary>
        /// 求上次出现大小数的 离现在有几次了
        /// </summary>
        /// <returns></returns>
        public int BigSmallNum(int index, EnumStyle es)
        {
            LotteryModel lm;
            int pre = Lottery.Lt_Lotterys.Count - 1;
            for (int i = index; i < Lottery.Lt_Lotterys.Count; i++)
            {
                lm = Lottery.Lt_Lotterys[i];
                if (es == EnumStyle.后二 && lm.num4 > 4 && lm.num5 < 5 && lm.num4 != lm.num5)
                {
                    pre = i;
                    break;
                }
                else if (es == EnumStyle.前二 && lm.num1 > 4 && lm.num2 < 5 && lm.num1 != lm.num2)
                {
                    pre = i;
                    break;
                }
            }
            return pre - index;
        }

        /// <summary>
        /// 求上次出现小大数的 离现在有几次了
        /// </summary>
        /// <returns></returns>
        public int SmallBigNum(int index, EnumStyle es)
        {
            LotteryModel lm;
            int pre = Lottery.Lt_Lotterys.Count - 1;
            for (int i = index; i < Lottery.Lt_Lotterys.Count; i++)
            {
                lm = Lottery.Lt_Lotterys[i];
                if (es == EnumStyle.后二 && lm.num4 < 5 && lm.num5 > 4 && lm.num4 != lm.num5)
                {
                    pre = i;
                    break;
                }
                else if (es == EnumStyle.前二 && lm.num1 < 5 && lm.num2 > 4 && lm.num1 != lm.num2)
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
        public int SmallNum(int index, EnumStyle es)
        {
            LotteryModel lm;
            int pre = Lottery.Lt_Lotterys.Count - 1;
            for (int i = index; i < Lottery.Lt_Lotterys.Count; i++)
            {
                lm = Lottery.Lt_Lotterys[i];
                if (es == EnumStyle.后二 && lm.num4 < 5 && lm.num5 < 5 && lm.num4 != lm.num5)
                {
                    pre = i;
                    break;
                }
                else if (es == EnumStyle.前二 && lm.num1 < 5 && lm.num2 < 5 && lm.num1 != lm.num2)
                {
                    pre = i;
                    break;
                }
            }
            return pre - index;
        }
        #endregion

        #region 奇偶
        /// <summary>
        /// 求上次出现奇偶数的 离现在有几次了
        /// </summary>
        /// <returns></returns>
        public int OddPairNum(int index, int number1, int number2, EnumStyle es)
        {
            LotteryModel lm;
            int pre = Lottery.Lt_Lotterys.Count - 1;
            for (int i = index; i < Lottery.Lt_Lotterys.Count; i++)
            {
                lm = Lottery.Lt_Lotterys[i];

                if (es == EnumStyle.后二 && lm.num4 % 2 == number1 && lm.num5 % 2 == number2 && lm.num4 != lm.num5)
                {
                    pre = i;
                    break;
                }
                else if (es == EnumStyle.前二 && lm.num1 % 2 == number1 && lm.num2 % 2 == number2 && lm.num1 != lm.num2)
                {
                    pre = i;
                    break;
                }
            }
            return pre - index;
        }
        #endregion

        #region 重数
        /// <summary>
        /// 求上次出现重数的 离现在有几次了
        /// </summary>
        /// <returns></returns>
        public int DblNum(int index, EnumStyle es)
        {
            LotteryModel lm;
            int pre = Lottery.Lt_Lotterys.Count - 1;
            for (int i = index; i < Lottery.Lt_Lotterys.Count; i++)
            {
                lm = Lottery.Lt_Lotterys[i];

                if (es == EnumStyle.后二 && lm.num4 == lm.num5)
                {
                    pre = i;
                    break;
                }
                else if (es == EnumStyle.前二 && lm.num1 == lm.num2)
                {
                    pre = i;
                    break;
                }
            }
            return pre - index;
        }
        #endregion
        #endregion

        #region 新方法
        /// <summary>
        /// 获取字段信息
        /// </summary>
        /// <param name="type"></param>
        /// <param name="number"></param>
        /// <returns></returns>
        private FieldInfo getFieldInfo(Type type, int number)
        {
            FieldInfo fieldInfo = type.GetField("num" + number.ToString(),
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
        private Value GetValue(LotteryModel lm, int number1, int number2)
        {
            Value value = new Value();
            Type type = typeof(LotteryModel);
            FieldInfo fieldInfo = getFieldInfo(type, number1);
            value.v1 = Convert.ToInt16(fieldInfo.GetValue(lm));//获取值第一个值

            fieldInfo = getFieldInfo(type, number2);
            value.v2 = Convert.ToInt16(fieldInfo.GetValue(lm));//获取值第二个值
            return value;
        }

        class Value
        {
            public int v1;
            public int v2;
        }
        #region 大小
        /// <summary>
        /// 求上次出现大数的 离现在有几次了
        /// </summary>
        /// <returns></returns>
        public int BigNum(int index, int number1, int number2)
        {
            LotteryModel lm;
            int pre = Lottery.Lt_Lotterys.Count - 1;
            for (int i = index; i < Lottery.Lt_Lotterys.Count; i++)
            {
                lm = Lottery.Lt_Lotterys[i];

                Value value = GetValue(lm, number1, number2);
                if (value.v1 > 4 && value.v2 > 4 && value.v1 != value.v2)
                {
                    pre = i;
                    break;
                }
            }
            return pre - index;
        }

        /// <summary>
        /// 求上次出现大小数的 离现在有几次了
        /// </summary>
        /// <returns></returns>
        public int BigSmallNum(int index, int number1, int number2)
        {
            LotteryModel lm;
            int pre = Lottery.Lt_Lotterys.Count - 1;
            for (int i = index; i < Lottery.Lt_Lotterys.Count; i++)
            {
                lm = Lottery.Lt_Lotterys[i];

                Value value = GetValue(lm, number1, number2);
                if (value.v1 > 4 && value.v2 < 5 && value.v1 != value.v2)
                {
                    pre = i;
                    break;
                }
            }
            return pre - index;
        }

        /// <summary>
        /// 求上次出现小大数的 离现在有几次了
        /// </summary>
        /// <returns></returns>
        public int SmallBigNum(int index, int number1, int number2)
        {
            LotteryModel lm;
            int pre = Lottery.Lt_Lotterys.Count - 1;
            for (int i = index; i < Lottery.Lt_Lotterys.Count; i++)
            {
                lm = Lottery.Lt_Lotterys[i];
                Value value = GetValue(lm, number1, number2);
                if (value.v1 < 5 && value.v2 > 4 && value.v1 != value.v2)
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
        public int SmallNum(int index, int number1, int number2)
        {
            LotteryModel lm;
            int pre = Lottery.Lt_Lotterys.Count - 1;
            for (int i = index; i < Lottery.Lt_Lotterys.Count; i++)
            {
                lm = Lottery.Lt_Lotterys[i];
                Value value = GetValue(lm, number1, number2);
                if (value.v1 < 5 && value.v2 < 5 && value.v1 != value.v2)
                {
                    pre = i;
                    break;
                }
            }
            return pre - index;
        }
        #endregion

        #region 奇偶
        /// <summary>
        /// 求上次出现奇偶数的 离现在有几次了
        /// </summary>
        /// <returns></returns>
        public int OddPairNum(int index, int number1, int number2, int num1, int num2)
        {
            LotteryModel lm;
            int pre = Lottery.Lt_Lotterys.Count - 1;
            for (int i = index; i < Lottery.Lt_Lotterys.Count; i++)
            {
                lm = Lottery.Lt_Lotterys[i];

                Value value = GetValue(lm, number1, number2);
                if (value.v1 % 2 == num1 && value.v2 % 2 == num2 && value.v1 != value.v2)
                {
                    pre = i;
                    break;
                }
            }
            return pre - index;
        }
        #endregion

        #region 重数
        /// <summary>
        /// 求上次出现重数的 离现在有几次了
        /// </summary>
        /// <returns></returns>
        public int DblNum(int index, int number1, int number2)
        {
            LotteryModel lm;
            int pre = Lottery.Lt_Lotterys.Count - 1;
            for (int i = index; i < Lottery.Lt_Lotterys.Count; i++)
            {
                lm = Lottery.Lt_Lotterys[i];

                Value value = GetValue(lm, number1, number2);
                if (value.v1 == value.v2)
                {
                    pre = i;
                    break;
                }
            }
            return pre - index;
        }
        #endregion
        #endregion
    }
}
