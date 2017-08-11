using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XScpStatistics.Model;

namespace XScpStatistics.Common
{
    /// <summary>
    /// 存储所有开过奖的号码
    /// </summary>
    public class Lottery
    {
        /// <summary>
        /// 所有已开过奖的号码记录
        /// </summary>
        public static List<LotteryModel> Lt_Lotterys = new List<LotteryModel>();

        /// <summary>
        /// 新增已开奖的号码
        /// </summary>
        /// <param name="lottery"></param>
        public static void AddLottery(string lottery)
        {
            if (lottery.Contains("-"))
            {
                lottery = lottery.Split('-')[1];
            }
            string[] array = lottery.Replace("期", ",").Replace("，", ",").Split(',');
            int[] arrayint = new int[array.Length];
            for (int i = 0; i < array.Length; i++)
            {
                arrayint[i] = Convert.ToInt32(array[i]);
            }
            if (arrayint.Length == 3)
            {
                Lt_Lotterys.Add(new LotteryModel()
                {
                    num1 = arrayint[0],
                    num2 = arrayint[1],
                    num3 = arrayint[2],
                });
            }
            else if (arrayint.Length == 4)
            {
                Lt_Lotterys.Add(new LotteryModel()
                {
                    sno = arrayint[0],
                    num1 = arrayint[1],
                    num2 = arrayint[2],
                    num3 = arrayint[3],
                });
            }
            else if (arrayint.Length == 5)
            {
                Lt_Lotterys.Add(new LotteryModel()
                {
                    num1 = arrayint[0],
                    num2 = arrayint[1],
                    num3 = arrayint[2],
                    num4 = arrayint[3],
                    num5 = arrayint[4]
                });
            }
            else if (arrayint.Length == 6)
            {
                Lt_Lotterys.Add(new LotteryModel()
                {
                    sno = arrayint[0],
                    num1 = arrayint[1],
                    num2 = arrayint[2],
                    num3 = arrayint[3],
                    num4 = arrayint[4],
                    num5 = arrayint[5]
                });
            }

        }

        /// <summary>
        /// 获取已开过的奖
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public static LotteryModel GetLottery(int index)
        {
            if (Lt_Lotterys.Count - 1 > index)
                return Lt_Lotterys[index];
            return null;
        }

        /// <summary>
        /// 清除所有记录
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public static void ClearLotterys()
        {
            Lt_Lotterys.Clear();
        }
    }
}
