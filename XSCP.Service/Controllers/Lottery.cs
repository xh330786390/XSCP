using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XSCP.Service.Model;

namespace XSCP.Service.Controllers
{
    public abstract class Lottery
    {
        /// <summary>
        /// 所有已开过奖的号码记录
        /// </summary>
        public List<LotteryModel> Lt_Lotterys = new List<LotteryModel>();

        /// <summary>
        /// 新增已开奖的号码
        /// </summary>
        /// <param name="lottery"></param>
        public abstract void AddLottery(DateTime dt, string lottery);

        /// <summary>
        /// 获取已开过的奖
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public LotteryModel GetLottery(int index)
        {
            if (this.Lt_Lotterys.Count - 1 > index)
                return this.Lt_Lotterys[index];
            return null;
        }

        /// <summary>
        /// 清除所有记录
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public void ClearLotterys()
        {
            this.Lt_Lotterys.Clear();
        }
    }
}
