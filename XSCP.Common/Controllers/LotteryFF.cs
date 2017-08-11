using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using   XSCP.Common.Model;

namespace   XSCP.Data.Controllers
{
    public class LotteryFF : Lottery
    {
        /// <summary>
        /// 新增已开奖的号码
        /// </summary>
        /// <param name="lottery"></param>
        public override void AddLottery(DateTime dt, string lottery)
        {
            if (lottery.Contains("-"))
            {
                lottery = lottery.Split('-')[1];
            }
            lottery = lottery.Replace("期", ",").Replace("，", ",");
            string[] array = lottery.Split(',');
            int[] arrayint = new int[array.Length];
            for (int i = 0; i < array.Length; i++)
            {
                try
                {
                    arrayint[i] = Convert.ToInt32(array[i]);
                }
                catch
                {
                }
            }
            if (arrayint.Length == 5)
            {
                this.Lt_Lotterys.Add(new LotteryModel()
                {
                    Num1 = arrayint[0],
                    Num2 = arrayint[1],
                    Num3 = arrayint[2],
                    Num4 = arrayint[3],
                    Num5 = arrayint[4]
                });
            }
            else if (arrayint.Length == 6)
            {
                this.Lt_Lotterys.Add(new LotteryModel()
                {
                    Sno = arrayint[0].ToString().PadLeft(4, '0'),
                    Lottery = lottery.Substring(lottery.IndexOf(',') + 1),
                    Num1 = arrayint[1],
                    Num2 = arrayint[2],
                    Num3 = arrayint[3],
                    Num4 = arrayint[4],
                    Num5 = arrayint[5],
                    Dtime = Common.GetDateTime(dt, arrayint[0])
                });
            }
        }
    }
}
