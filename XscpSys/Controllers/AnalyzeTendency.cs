using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using XscpSys.Model;
namespace XscpSys.Controllers
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
            FieldInfo fieldInfo = Reflection.GetFieldInfo(type, "Num" + number1.ToString());
            value.v1 = Convert.ToInt16(fieldInfo.GetValue(lm));//获取值第一个值

            fieldInfo = Reflection.GetFieldInfo(type, "Num" + number2.ToString());
            value.v2 = Convert.ToInt16(fieldInfo.GetValue(lm));//获取值第二个值
            return value;
        }

        #region 大小
        /// <summary>
        /// 求上次出现大数的 离现在有几次了
        /// </summary>
        /// <returns></returns>
        public int BigNum(List<LotteryModel> lotterys, int index, int number1, int number2)
        {
            LotteryModel lm;
            int pre = lotterys.Count;
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
            int pre = lotterys.Count;
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
            int pre = lotterys.Count;
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
            int pre = lotterys.Count;
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
            int pre = lotterys.Count;
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
            int pre = lotterys.Count;
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

        /// <summary>
        /// 求上次出现重数的 离现在有几次了
        /// </summary>
        /// <returns></returns>
        public int CompareLottery(List<LotteryModel> lotterys, int index, int number1, int number2)
        {

            int pre = lotterys.Count;
            if (index < lotterys.Count - 1)
            {
                int[] cur = new int[2];
                int[] next = new int[2];
                Value valueCur = new Value();
                Value valueNext = new Value();
                LotteryModel lm = lotterys[index];
                LotteryModel lmNext = lotterys[index + 1];

                if (number1 == 1)
                {
                    cur[0] = lm.Num1;
                    cur[1] = lm.Num2;

                    next[0] = lmNext.Num1;
                    next[1] = lmNext.Num2;
                }
                else
                {
                    cur[0] = lm.Num4;
                    cur[1] = lm.Num5;

                    next[0] = lmNext.Num4;
                    next[1] = lmNext.Num5;
                }

                if (cur[0] == cur[1]) return 1;

                for (int i = 0; i < cur.Length; i++)
                {
                    for (int j = 0; j < next.Length; j++)
                    {
                        if (cur[i] == next[j]) return 1;
                    }
                }
            }
            return 0;
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
    }
}
