using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XSCP.Service.Model;

namespace XSCP.Service.Controllers
{
    public class Lottery3D : Lottery
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
            string[] array = lottery.Replace("期", ",").Replace("，", ",").Split(',');
            int[] arrayint = new int[array.Length];
            for (int i = 0; i < array.Length; i++)
            {
                arrayint[i] = Convert.ToInt32(array[i]);
            }
            if (arrayint.Length == 3)
            {
                this.Lt_Lotterys.Add(new LotteryModel()
                {
                    Num1 = arrayint[0],
                    Num2 = arrayint[1],
                    Num3 = arrayint[2]
                });
            }
            else if (arrayint.Length == 4)
            {
                this.Lt_Lotterys.Add(new LotteryModel()
                {
                    Sno = arrayint[0].ToString().PadLeft(4, '0'),
                    Num1 = arrayint[1],
                    Num2 = arrayint[2],
                    Num3 = arrayint[3],
                    Dtime = XscpControllers.GetDateTime(dt, arrayint[0])
                });
            }
        }
    }
}
