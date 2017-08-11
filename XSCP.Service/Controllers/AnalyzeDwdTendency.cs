using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XSCP.Service.Model;
using System.Reflection;

namespace XSCP.Service.Controllers
{
    public class AnalyzeDwdTendency
    {
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
            FieldInfo fieldInfo = getFieldInfo(type, number);
            value.v1 = Convert.ToInt16(fieldInfo.GetValue(lm));//获取值
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
        public int BigNum(List<LotteryModel> lotterys, int index, int number)
        {
            LotteryModel lm;
            int pre = lotterys.Count - 1;
            for (int i = index; i < lotterys.Count; i++)
            {
                lm = lotterys[i];

                Value value = GetValue(lm, number);
                if (value.v1 > 4)
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
        public int SmallNum(List<LotteryModel> lotterys, int index, int number)
        {
            LotteryModel lm;
            int pre = lotterys.Count - 1;
            for (int i = index; i < lotterys.Count; i++)
            {
                lm = lotterys[i];
                Value value = GetValue(lm, number);
                if (value.v1 < 5)
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
        public int OddPairNum(List<LotteryModel> lotterys, int index, int number, int oddPair)
        {
            LotteryModel lm;
            int pre = lotterys.Count - 1;
            for (int i = index; i < lotterys.Count; i++)
            {
                lm = lotterys[i];

                Value value = GetValue(lm, number);
                if (value.v1 % 2 == oddPair)
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
