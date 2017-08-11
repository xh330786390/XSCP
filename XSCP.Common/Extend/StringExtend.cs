using System.Collections.Generic;
using System.Linq;

namespace XSCP.Common.Extend
{
    public static class StringExtend
    {
        /// <summary>
        /// 彩票转换
        /// </summary>
        /// <param name="str"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public static List<string> TransLottery(this string result)
        {
            try
            {
                List<string> ltData = null;
                int index = result.IndexOf("<div id=\"ewinnumber\">");
                if (index < 0)
                {
                    ltData = result.Replace("期", ",").Split('\n').ToList();
                    if (ltData[0].Split(',').Length == 6) return ltData;
                }
                result = result.Substring(index);
                index = result.IndexOf("</div>");
                result = result.Substring(0, index);
                result = result.Replace("<div id=\"ewinnumber\">\n", "").Replace("<dl class=\"num_dl01 num_dl02\"><dt>", "").Replace("</dd></dl>", "").Replace("</dd></dl>", "").Replace("&#26399;</dt><dd>", ",").Replace("	    ", "");
                index = result.LastIndexOf('\n');
                result = result.Substring(0, index);
                if (!string.IsNullOrEmpty(result))
                {
                    ltData = result.Split('\n').ToList();
                    if (ltData[0].Split(',').Length == 6) return ltData;
                }
            }
            catch { }
            return null;
        }
    }
}
