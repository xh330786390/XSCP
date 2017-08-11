using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XSCP.Service.Model;


namespace XSCP.Service.Controllers
{
    public class XscpControllers
    {
        /// <summary>
        /// 获取彩票
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        public List<Model.LotteryModel> GetLotteryModels(string result)
        {
            List<Model.LotteryModel> lt = new List<LotteryModel>();
            Model.LotteryModel lm = null;
            string[] arr1 = result.Split('\n');
            for (int i = arr1.Length - 1; i >= 0; i--)
            {
                string[] arr2 = arr1[i].Split(',');
                lm.Dtime = GetDateTime(DateTime.Now, int.Parse(arr2[0]));

                lm = new LotteryModel();
                lm.Ymd = DateTime.Now.ToString("yyyyMMdd");
                lm.Sno = arr2[0];
                lm.Num1 = int.Parse(arr2[1]);
                lm.Num2 = int.Parse(arr2[2]);
                lm.Num3 = int.Parse(arr2[3]);
                if (arr2.Length > 4)
                {
                    lm.Num4 = int.Parse(arr2[4]);
                    lm.Num5 = int.Parse(arr2[5]);
                }

                //lm.ID = ff.MaxId() + 1;

                lt.Add(lm);
            }
            return lt;

        }

        public static string GetXscpData(string result)
        {
            int index = result.IndexOf("<div id=\"ewinnumber\">");
            result = result.Substring(index);
            index = result.IndexOf("</div>");
            result = result.Substring(0, index);
            result = result.Replace("<div id=\"ewinnumber\">\n", "").Replace("<dl class=\"num_dl01 num_dl02\"><dt>", "").Replace("</dd></dl>", "").Replace("</dd></dl>", "").Replace("&#26399;</dt><dd>", ",").Replace("	    ", "");
            index = result.LastIndexOf('\n');
            result = result.Substring(0, index);
            return result;
        }

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
