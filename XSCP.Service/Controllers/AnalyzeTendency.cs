using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XSCP.Service.Model;
using System.Reflection;
using XSCP.Service.Common;

namespace XSCP.Service.Controllers
{
    public class AnalyzeTendency
    {
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
            FieldInfo fieldInfo = Reflection.GetFieldInfo(type, "Num" + number1.ToString());
            value.v1 = Convert.ToInt16(fieldInfo.GetValue(lm));//获取值第一个值

            fieldInfo = Reflection.GetFieldInfo(type, "Num" + number2.ToString());
            value.v2 = Convert.ToInt16(fieldInfo.GetValue(lm));//获取值第二个值
            return value;
        }

        private class Value
        {
            public int v1;
            public int v2;
        }

        #region 大小
        /// <summary>
        /// 求上次出现大数的 离现在有几次了
        /// </summary>
        /// <returns></returns>
        public int BigNum(List<LotteryModel> lotterys, int index, int number1, int number2)
        {
            LotteryModel lm;
            int pre = lotterys.Count ;
            for (int i = index; i < lotterys.Count; i++)
            {
                lm = lotterys[i];

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
        public int BigSmallNum(List<LotteryModel> lotterys, int index, int number1, int number2)
        {
            LotteryModel lm;
            int pre = lotterys.Count ;
            for (int i = index; i < lotterys.Count; i++)
            {
                lm = lotterys[i];

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
        public int SmallBigNum(List<LotteryModel> lotterys, int index, int number1, int number2)
        {
            LotteryModel lm;
            int pre = lotterys.Count ;
            for (int i = index; i < lotterys.Count; i++)
            {
                lm = lotterys[i];
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
        public int SmallNum(List<LotteryModel> lotterys, int index, int number1, int number2)
        {
            LotteryModel lm;
            int pre = lotterys.Count ;
            for (int i = index; i < lotterys.Count; i++)
            {
                lm = lotterys[i];
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
        public int OddPairNum(List<LotteryModel> lotterys, int index, int number1, int number2, int num1, int num2)
        {
            LotteryModel lm;
            int pre = lotterys.Count ;
            for (int i = index; i < lotterys.Count; i++)
            {
                lm = lotterys[i];

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
        public int DblNum(List<LotteryModel> lotterys, int index, int number1, int number2)
        {
            LotteryModel lm;
            int pre = lotterys.Count ;
            for (int i = index; i < lotterys.Count; i++)
            {
                lm = lotterys[i];

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
    }
}
