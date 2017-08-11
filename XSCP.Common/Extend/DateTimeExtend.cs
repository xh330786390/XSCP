using System;

namespace XSCP.Common.Extend
{
    public static class DateTimeExtend
    {
        /// <summary>
        /// 根据期数获取时间
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="sno"></param>
        /// <returns></returns>
        public static string ToXscpDateTime(this DateTime dt, int sno)
        {
            string md = dt.ToString("MM/dd");
            int hour = 8 + sno / 60;
            int minute = sno % 60;
            if (hour > 23)
            {
                hour -= 24;
                md = dt.AddDays(1).ToString("MM/dd");
            }
            return md + " " + hour.ToString().PadLeft(2, '0') + ":" + minute.ToString().PadLeft(2, '0');
        }

        /// <summary>
        /// 时间转换
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static DateTime ToXscpDateTime(this DateTime dt)
        {
            if (dt.Hour < 8 && dt.Hour >= 0)
            {
                dt = dt.AddDays(-1);
            }
            return dt;
        }
    }
}
