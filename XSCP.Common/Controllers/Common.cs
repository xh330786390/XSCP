using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace   XSCP.Data.Controllers
{
    public class Common
    {
        /// <summary>
        /// 获取日期时间
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="num"></param>
        /// <returns></returns>
        public static string GetDateTime(DateTime dt, int num)
        {
            string strDate = "";
            string md = dt.ToString("MM/dd");
            int hour = 8 + num / 60;
            int minute = num % 60;
            if (hour > 23)
            {
                hour -= 24;
                md = dt.AddDays(1).ToString("MM/dd");
            }
            strDate = md + " " + hour.ToString().PadLeft(2, '0') + ":" + minute.ToString().PadLeft(2, '0');
            return strDate;
        }
    }
}
