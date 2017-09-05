using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XSCP.Common;
using XSCP.Common.Model;

namespace SyncData
{
    internal class Export
    {
        public void SqlLiteToMysql()
        {
            List<string> lt_date = XscpSqlliteBLL.QueryDateList().OrderBy(l => l).ToList();
            if (lt_date != null && lt_date.Count > 0)
            {
                //Task.Factory.StartNew(() => SaveLottery(lt_date));

                /////新增一星走势
                //Task.Factory.StartNew(() => SaveTendency1(Tendency1Enum.TenThousand, lt_date));
                //Task.Factory.StartNew(() => SaveTendency1(Tendency1Enum.Thousand, lt_date));
                //Task.Factory.StartNew(() => SaveTendency1(Tendency1Enum.Hundred, lt_date));
                //Task.Factory.StartNew(() => SaveTendency1(Tendency1Enum.Ten, lt_date));
                //Task.Factory.StartNew(() => SaveTendency1(Tendency1Enum.One, lt_date));

                /////新增二星走势
                //Task.Factory.StartNew(() => SaveTendency2(Tendency2Enum.Before, lt_date));
                //Task.Factory.StartNew(() => SaveTendency2(Tendency2Enum.After, lt_date));

                SaveLottery(lt_date);

                SaveTendency1(Tendency1Enum.TenThousand, lt_date);
                SaveTendency1(Tendency1Enum.Thousand, lt_date);
                SaveTendency1(Tendency1Enum.Hundred, lt_date);
                SaveTendency1(Tendency1Enum.Ten, lt_date);
                SaveTendency1(Tendency1Enum.One, lt_date);

                SaveTendency2(Tendency2Enum.Before, lt_date);
                SaveTendency2(Tendency2Enum.After, lt_date);
            }
        }

        private void SaveLottery(List<string> lt_date)
        {
            lt_date.ForEach(date =>
            {
                var lottery = XscpSqlliteBLL.QueryLottery(date, 1380);
                try
                {
                    MysqlHelper.SaveLotteryData(lottery);
                }
                catch (Exception er)
                {
                }
                Console.WriteLine("完成奖号：" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "【" + date + "】");
            });
        }

        private void SaveTendency2(Tendency2Enum type, List<string> lt_date)
        {
            lt_date.ForEach(date =>
            {
                var lottery = XscpSqlliteBLL.QueryTendency2(type, date, 1380);
                lottery = lottery.OrderBy(l => l.Sno).ToList();
                MysqlHelper.SaveTendency2(type, lottery);
                Console.WriteLine("完成二星：" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "【" + date + "】");
            });
        }

        private void SaveTendency1(Tendency1Enum type, List<string> lt_date)
        {
            lt_date.ForEach(date =>
            {
                var lottery = XscpSqlliteBLL.QueryTendency1(type, date, 1380);
                lottery = lottery.OrderBy(l => l.Sno).ToList();
                MysqlHelper.SaveTendency1(type, lottery);
                Console.WriteLine("完成一星：" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "【" + date + "】");
            });
        }
    }
}

