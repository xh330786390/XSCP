using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XSCP.Common;
using XSCP.Common.Model;

namespace XSCP.Forecast
{
    public class LotteryHelper
    {
        /// <summary>
        /// 更新彩票
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="ltData"></param>
        /// <returns></returns>
        public static bool Update(DateTime dt, List<string> ltData)
        {
            try
            {
                ///新增开奖号码
                List<LotteryModel> lt_LotteryModel = XscpBLL.SaveLottery(dt, ltData);

                ///新增一星走势
                XscpBLL.SaveTendency1(Tendency1Enum.TenThousand, lt_LotteryModel);
                XscpBLL.SaveTendency1(Tendency1Enum.Thousand, lt_LotteryModel);
                XscpBLL.SaveTendency1(Tendency1Enum.Hundred, lt_LotteryModel);
                XscpBLL.SaveTendency1(Tendency1Enum.Ten, lt_LotteryModel);
                XscpBLL.SaveTendency1(Tendency1Enum.One, lt_LotteryModel);

                ///新增二星走势
                XscpBLL.SaveTendency2(Tendency2Enum.Before, lt_LotteryModel);
                XscpBLL.SaveTendency2(Tendency2Enum.After, lt_LotteryModel);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
