using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Text.RegularExpressions;

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

        public static List<string> GetHtml(this string strResult)
        {
            //http://regexr.com/
            //http://tools.jb51.net/regex/create_reg
            List<string> list = new List<string>();
            string pattern = @"<table class=[\s\S]*<\/table>";
            var match = Regex.Match(strResult, pattern);
            if (!string.IsNullOrEmpty(match.Value))
            {
                string tr_pattern = @"<tr>[\s\S]*?<\/tr>";
                var matchs = Regex.Matches(match.Value, tr_pattern);
                foreach (Match item in matchs)
                {
                    string strItem = item.Value;

                    string key_parttern = @"<td [\S]*<\/td>";
                    var match_key = Regex.Match(strItem, key_parttern);
                    if (!string.IsNullOrEmpty(match_key.Value))
                    {
                        string key = match_key.Value.Replace("<td class=\"first\">", "").Replace("&#26399;</td>", "");

                        string value_parttern = @"<td>[\s\S]*<\/td>";
                        var match_value = Regex.Match(strItem, value_parttern);
                        string value = match_value.Value.Replace("<td>", "").Replace("</td>", "").Replace("\n", "").Replace("\t", "");

                        list.Add(key + "," + value);
                    }
                }
            }

            return list;
        }
    }
}
