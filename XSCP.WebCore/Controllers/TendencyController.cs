using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using XSCP.Common;
using XSCP.Common.Model;

namespace XSCP.WebCore.Controllers
{
    public class TendencyController : Controller
    {
        //
        // GET: /Trendency/
        public ActionResult Main()
        {
            return View();
        }

        public ActionResult Load()
        {
            string strResult = null;
            string date = "20171017";
            int num = 30;
            //strResult = Road2Start012(date, num);
            //strResult = Road1Start012(date, num);
            strResult = Road2StartDxjo(date, num);
            return Content(strResult);
        }

        /// <summary>
        /// 二星大小奇偶
        /// </summary>
        /// <param name="date"></param>
        /// <param name="num"></param>
        /// <returns></returns>
        private string Road2StartDxjo(string date, int num)
        {
            StringBuilder sb = new StringBuilder();

            //@1.标题
            sb.Append("<tbody>");
            sb.Append("<tr>");
            sb.Append("<td width='30' rowspan='2' class='z_bg_03'>序号</td>");
            sb.Append("<td width='120' rowspan='2' class='z_bg_03'> 期号 </td>");
            sb.Append("<td width='45' rowspan='2' class='z_bg_03'>奖号</td>");
            sb.Append("<td width='15' rowspan='2' class='z_bg_03'></td>");
            sb.Append("<td colspan='9' class='z_bg_03'>前二</td>");
            sb.Append("<td width='15'  rowspan='2' class='z_bg_03'></td>");
            sb.Append("<td colspan='9' class='z_bg_03'>后二</td>");
            sb.Append("<td width='50' rowspan='2' class='z_bg_03'>时间</td>");
            sb.Append("</tr>");

            sb.Append("<tr>");
            sb.Append("<td class='z_bg_03'>大大</td>");
            sb.Append("<td class='z_bg_03'>小小</td>");
            sb.Append("<td class='z_bg_03'>大小</td>");
            sb.Append("<td class='z_bg_03'>小大</td>");
            sb.Append("<td class='z_bg_03'>奇奇</td>");
            sb.Append("<td class='z_bg_03'>偶偶</td>");
            sb.Append("<td class='z_bg_03'>奇偶</td>");
            sb.Append("<td class='z_bg_03'>偶奇</td>");
            sb.Append("<td class='z_bg_03'>对子</td>");
            sb.Append("<td class='z_bg_03'>大大</td>");
            sb.Append("<td class='z_bg_03'>小小</td>");
            sb.Append("<td class='z_bg_03'>大小</td>");
            sb.Append("<td class='z_bg_03'>小大</td>");
            sb.Append("<td class='z_bg_03'>奇奇</td>");
            sb.Append("<td class='z_bg_03'>偶偶</td>");
            sb.Append("<td class='z_bg_03'>奇偶</td>");
            sb.Append("<td class='z_bg_03'>偶奇</td>");
            sb.Append("<td class='z_bg_03'>对子</td>");
            sb.Append("</tr>");
            sb.Append("</tbody>");

            var lt_BeforeData = XscpMysqlBLL.QueryTendency2(Tendency2Enum.Before, date, num);
            var maxBeforeData = XscpMysqlBLL.QueryMaxTendency2(Tendency2Enum.Before, date);

            var lt_AfterData = XscpMysqlBLL.QueryTendency2(Tendency2Enum.After, date, num);
            var maxAfterData = XscpMysqlBLL.QueryMaxTendency2(Tendency2Enum.After, date);

            var maxWan = XscpMysqlBLL.QueryMaxTendency1(Tendency1Enum.TenThousand, date);
            var maxQian = XscpMysqlBLL.QueryMaxTendency1(Tendency1Enum.Thousand, date);
            var maxShi = XscpMysqlBLL.QueryMaxTendency1(Tendency1Enum.Ten, date);
            var maxGe = XscpMysqlBLL.QueryMaxTendency1(Tendency1Enum.One, date);

            //var curWan = XscpMysqlBLL.QueryTendency1(Tendency1Enum.TenThousand, date, 1).FirstOrDefault();
            //var curQian = XscpMysqlBLL.QueryTendency1(Tendency1Enum.TenThousand, date, 1).FirstOrDefault();

            //var curBeforeDwd = GetTendencyDwdValue(curWan, curQian);

            var curShi = XscpMysqlBLL.QueryTendency1(Tendency1Enum.TenThousand, date, 1);
            var curGe = XscpMysqlBLL.QueryTendency1(Tendency1Enum.TenThousand, date, 1);

            sb.Append("<tbody  id='pagedata'>");
            sb.Append(" <tr>");
            sb.Append("<td class='z_bg_tendency' colspan='3'>最大遗漏</td>");
            sb.Append("<td class='z_bg_03'></td>");
            sb.Append("<td class='z_bg_tendency'>" + maxBeforeData.Big + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + maxBeforeData.Small + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + maxBeforeData.BigSmall + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + maxBeforeData.SmallBig + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + maxBeforeData.Odd + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + maxBeforeData.Pair + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + maxBeforeData.OddPair + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + maxBeforeData.PairOdd + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + maxBeforeData.Dbl + "</td>");

            sb.Append("<td class='z_bg_03'>" + "" + "</td>");

            sb.Append("<td class='z_bg_tendency'>" + maxAfterData.Big + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + maxAfterData.Small + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + maxAfterData.BigSmall + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + maxAfterData.SmallBig + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + maxAfterData.Odd + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + maxAfterData.Pair + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + maxAfterData.OddPair + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + maxAfterData.PairOdd + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + maxAfterData.Dbl + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + "" + "</td>");
            sb.Append(" </tr>");

            for (int i = 0; i < lt_BeforeData.Count; i++)
            {
                var beforeData = lt_BeforeData[i];
                var afterData = lt_AfterData[i];
                //string lottery = beforeData.Lottery.Replace(",", "");
                string lottery = beforeData.Lottery;

                sb.Append(" <tr>");
                sb.Append("<td class='z_bg_tendency'>" + (i + 1).ToString() + "</td>");
                sb.Append("<td class='z_bg_tendency'>" + beforeData.Ymd + "-" + beforeData.Sno + "</td>");
                sb.Append("<td class='z_bg_tendency'>" + "<span style='color:Red;font-weight:bold;'>" + lottery.Substring(0, 3) + "</span>" + lottery.Substring(3, 3) + "<span style='color:Red;font-weight:bold;'>" + lottery.Substring(6) + "</span></td>");
                sb.Append("<td class='z_bg_03'>" + "" + "</td>");
                sb.Append(getBgcolor2Start(beforeData.Big));
                sb.Append(getBgcolor2Start(beforeData.Small));
                sb.Append(getBgcolor2Start(beforeData.BigSmall));
                sb.Append(getBgcolor2Start(beforeData.SmallBig));
                sb.Append(getBgcolor2Start(beforeData.Odd));
                sb.Append(getBgcolor2Start(beforeData.Pair));
                sb.Append(getBgcolor2Start(beforeData.OddPair));
                sb.Append(getBgcolor2Start(beforeData.PairOdd));
                sb.Append(getBgcolor2Start(beforeData.Dbl));

                sb.Append("<td class='z_bg_03'>" + "" + "</td>");

                sb.Append(getBgcolor2Start(afterData.Big));
                sb.Append(getBgcolor2Start(afterData.Small));
                sb.Append(getBgcolor2Start(afterData.BigSmall));
                sb.Append(getBgcolor2Start(afterData.SmallBig));
                sb.Append(getBgcolor2Start(afterData.Odd));
                sb.Append(getBgcolor2Start(afterData.Pair));
                sb.Append(getBgcolor2Start(afterData.OddPair));
                sb.Append(getBgcolor2Start(afterData.PairOdd));
                sb.Append(getBgcolor2Start(afterData.Dbl));
                sb.Append("<td class='z_bg_tendency'>" + beforeData.Dtime.Substring(6) + "</td>");
                sb.Append(" </tr>");
            }

            sb.Append(" <tr>");
            sb.Append("<td class='z_bg_tendency' colspan='3'>最大遗漏</td>");
            sb.Append("<td class='z_bg_03'></td>");
            sb.Append("<td class='z_bg_tendency'>" + maxBeforeData.Big + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + maxBeforeData.Small + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + maxBeforeData.BigSmall + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + maxBeforeData.SmallBig + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + maxBeforeData.Odd + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + maxBeforeData.Pair + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + maxBeforeData.OddPair + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + maxBeforeData.PairOdd + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + maxBeforeData.Dbl + "</td>");

            sb.Append("<td class='z_bg_03'>" + "" + "</td>");

            sb.Append("<td class='z_bg_tendency'>" + maxAfterData.Big + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + maxAfterData.Small + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + maxAfterData.BigSmall + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + maxAfterData.SmallBig + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + maxAfterData.Odd + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + maxAfterData.Pair + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + maxAfterData.OddPair + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + maxAfterData.PairOdd + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + maxAfterData.Dbl + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + "" + "</td>");
            sb.Append(" </tr>");
            sb.Append("</tbody>");
            return sb.ToString();
        }

        /// <summary>
        /// 二星012路
        /// </summary>
        /// <param name="date"></param>
        /// <param name="num"></param>
        /// <returns></returns>
        private string Road2Start012(string date, int num)
        {
            StringBuilder sb = new StringBuilder();

            //@1.标题
            sb.Append("<tbody>");
            sb.Append("<tr>");
            sb.Append("<td width='30' rowspan='2' class='z_bg_03'>序号</td>");
            sb.Append("<td width='120' rowspan='2' class='z_bg_03'> 期号 </td>");
            sb.Append("<td width='45' rowspan='2' class='z_bg_03'>奖号</td>");
            sb.Append("<td width='45' rowspan='2' class='z_bg_03'>类型</td>");
            sb.Append("<td colspan='9' class='z_bg_03'>012路</td>");
            sb.Append("<td width='50' rowspan='2' class='z_bg_03'>时间</td>");
            sb.Append("</tr>");

            sb.Append("<tr>");
            sb.Append("<td class='z_bg_03'>00</td>");
            sb.Append("<td class='z_bg_03'>01</td>");
            sb.Append("<td class='z_bg_03'>02</td>");
            sb.Append("<td class='z_bg_03'>10</td>");
            sb.Append("<td class='z_bg_03'>11</td>");
            sb.Append("<td class='z_bg_03'>12</td>");
            sb.Append("<td class='z_bg_03'>20</td>");
            sb.Append("<td class='z_bg_03'>21</td>");
            sb.Append("<td class='z_bg_03'>22</td>");
            sb.Append("</tr>");
            sb.Append("</tbody>");

            var lt_data = XscpMysqlBLL.QueryTendency2(Tendency2Enum.After, date, num);
            var maxData = XscpMysqlBLL.QueryMaxTendency2(Tendency2Enum.After, date);
            sb.Append("<tbody  id='pagedata'>");

            sb.Append(" <tr>");
            sb.Append("<td class='z_bg_tendency' colspan='3'>最大遗漏</td>");
            sb.Append("<td class='z_bg_tendency'>0</td>");
            sb.Append("<td class='z_bg_tendency'>" + maxData.No_00 + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + maxData.No_01 + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + maxData.No_02 + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + maxData.No_10 + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + maxData.No_11 + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + maxData.No_12 + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + maxData.No_20 + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + maxData.No_21 + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + maxData.No_22 + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + "" + "</td>");
            sb.Append(" </tr>");

            for (int i = 0; i < lt_data.Count; i++)
            {
                var data = lt_data[i];
                sb.Append(" <tr>");
                sb.Append("<td class='z_bg_tendency'>" + (i + 1).ToString() + "</td>");
                sb.Append("<td class='z_bg_tendency'>" + data.Ymd + "-" + data.Sno + "</td>");
                sb.Append("<td class='z_bg_tendency'>" + data.Lottery.Replace(",", "") + "</td>");
                sb.Append("<td class='z_bg_tendency'>" + "012" + "</td>");
                sb.Append("<td class='z_bg_tendency'>" + data.No_00 + "</td>");
                sb.Append("<td class='z_bg_tendency'>" + data.No_01 + "</td>");
                sb.Append("<td class='z_bg_tendency'>" + data.No_02 + "</td>");
                sb.Append("<td class='z_bg_tendency'>" + data.No_10 + "</td>");
                sb.Append("<td class='z_bg_tendency'>" + data.No_11 + "</td>");
                sb.Append("<td class='z_bg_tendency'>" + data.No_12 + "</td>");
                sb.Append("<td class='z_bg_tendency'>" + data.No_20 + "</td>");
                sb.Append("<td class='z_bg_tendency'>" + data.No_21 + "</td>");
                sb.Append("<td class='z_bg_tendency'>" + data.No_22 + "</td>");
                sb.Append("<td class='z_bg_tendency'>" + data.Dtime.Substring(6) + "</td>");
                sb.Append(" </tr>");
            }

            sb.Append(" <tr>");
            sb.Append("<td class='z_bg_tendency' colspan='3'>最大遗漏</td>");
            sb.Append("<td class='z_bg_tendency'>0</td>");
            sb.Append("<td class='z_bg_tendency'>" + maxData.No_00 + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + maxData.No_01 + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + maxData.No_02 + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + maxData.No_10 + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + maxData.No_11 + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + maxData.No_12 + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + maxData.No_20 + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + maxData.No_21 + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + maxData.No_22 + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + "" + "</td>");
            sb.Append(" </tr>");
            sb.Append("</tbody>");
            return sb.ToString();
        }

        /// <summary>
        /// 1星012路
        /// </summary>
        /// <param name="date"></param>
        /// <param name="num"></param>
        /// <returns></returns>
        private string Road1Start012(string date, int num)
        {
            StringBuilder sb = new StringBuilder();

            //@1.标题
            sb.Append("<tbody id='tbodyssc'>");
            sb.Append("<tr>");
            sb.Append("<td width='30' rowspan='3' class='z_bg_03'>序号</td>");
            sb.Append("<td width='120' rowspan='3' class='z_bg_03'> 期号 </td>");
            sb.Append("<td width='45' rowspan='3' class='z_bg_03'>奖号</td>");
            sb.Append("<td width='45' rowspan='3' class='z_bg_03'>类型</td>");
            sb.Append("<td colspan='15' class='z_bg_03'>012路</td>");
            sb.Append("<td width='50' rowspan='3' class='z_bg_03'>时间</td>");
            sb.Append("</tr>");

            sb.Append("<tr>");
            sb.Append("<td colspan='3' class='z_bg_03'>万</td>");
            sb.Append("<td colspan='3' class='z_bg_03'>千</td>");
            sb.Append("<td colspan='3' class='z_bg_03'>百</td>");
            sb.Append("<td colspan='3' class='z_bg_03'>十</td>");
            sb.Append("<td colspan='3' class='z_bg_03'>个</td>");
            sb.Append("</tr>");

            sb.Append("<tr>");
            sb.Append("<td class='z_bg_03'>0</td>");
            sb.Append("<td class='z_bg_03'>1</td>");
            sb.Append("<td class='z_bg_03'>2</td>");
            sb.Append("<td class='z_bg_03'>0</td>");
            sb.Append("<td class='z_bg_03'>1</td>");
            sb.Append("<td class='z_bg_03'>2</td>");
            sb.Append("<td class='z_bg_03'>0</td>");
            sb.Append("<td class='z_bg_03'>1</td>");
            sb.Append("<td class='z_bg_03'>2</td>");
            sb.Append("<td class='z_bg_03'>0</td>");
            sb.Append("<td class='z_bg_03'>1</td>");
            sb.Append("<td class='z_bg_03'>2</td>");
            sb.Append("<td class='z_bg_03'>0</td>");
            sb.Append("<td class='z_bg_03'>1</td>");
            sb.Append("<td class='z_bg_03'>2</td>");
            sb.Append("</tr>");
            sb.Append("</tbody>");

            var maxWan = XscpMysqlBLL.QueryMaxTendency1(Tendency1Enum.TenThousand, date);
            var maxQian = XscpMysqlBLL.QueryMaxTendency1(Tendency1Enum.Thousand, date);
            var maxBai = XscpMysqlBLL.QueryMaxTendency1(Tendency1Enum.Hundred, date);
            var maxShi = XscpMysqlBLL.QueryMaxTendency1(Tendency1Enum.Ten, date);
            var maxGe = XscpMysqlBLL.QueryMaxTendency1(Tendency1Enum.One, date);

            var lt_wan = XscpMysqlBLL.QueryTendency1(Tendency1Enum.TenThousand, date, num);
            var lt_qian = XscpMysqlBLL.QueryTendency1(Tendency1Enum.Thousand, date, num);
            var lt_bai = XscpMysqlBLL.QueryTendency1(Tendency1Enum.Hundred, date, num);
            var lt_shi = XscpMysqlBLL.QueryTendency1(Tendency1Enum.Ten, date, num);
            var lt_ge = XscpMysqlBLL.QueryTendency1(Tendency1Enum.One, date, num);

            sb.Append("<tbody>");

            sb.Append(" <tr>");
            sb.Append("<td class='z_bg_tendency' colspan='3'>最大遗漏</td>");
            sb.Append("<td class='z_bg_tendency'>0</td>");
            sb.Append("<td class='z_bg_tendency'>" + maxWan.No_0 + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + maxWan.No_1 + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + maxWan.No_2 + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + maxQian.No_0 + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + maxQian.No_1 + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + maxQian.No_2 + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + maxBai.No_0 + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + maxBai.No_1 + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + maxBai.No_2 + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + maxShi.No_0 + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + maxShi.No_1 + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + maxShi.No_2 + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + maxGe.No_0 + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + maxGe.No_1 + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + maxGe.No_2 + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + "" + "</td>");
            sb.Append(" </tr>");


            for (int i = 0; i < lt_wan.Count; i++)
            {
                var wan = lt_wan[i];
                var qian = lt_qian[i];
                var bai = lt_bai[i];
                var shi = lt_shi[i];
                var ge = lt_ge[i];

                sb.Append(" <tr>");
                sb.Append("<td class='z_bg_tendency'>" + (i + 1).ToString() + "</td>");
                sb.Append("<td class='z_bg_tendency'>" + wan.Ymd + "-" + wan.Sno + "</td>");
                sb.Append("<td class='z_bg_tendency'>" + wan.Lottery.Replace(",", "") + "</td>");
                sb.Append("<td class='z_bg_tendency'>" + "012" + "</td>");

                sb.Append("<td class='z_bg_tendency'>" + wan.No_0 + "</td>");
                sb.Append("<td class='z_bg_tendency'>" + wan.No_1 + "</td>");
                sb.Append("<td class='z_bg_tendency'>" + wan.No_2 + "</td>");

                sb.Append("<td class='z_bg_tendency'>" + qian.No_0 + "</td>");
                sb.Append("<td class='z_bg_tendency'>" + qian.No_1 + "</td>");
                sb.Append("<td class='z_bg_tendency'>" + qian.No_2 + "</td>");

                sb.Append("<td class='z_bg_tendency'>" + bai.No_0 + "</td>");
                sb.Append("<td class='z_bg_tendency'>" + bai.No_1 + "</td>");
                sb.Append("<td class='z_bg_tendency'>" + bai.No_2 + "</td>");

                sb.Append("<td class='z_bg_tendency'>" + shi.No_0 + "</td>");
                sb.Append("<td class='z_bg_tendency'>" + shi.No_1 + "</td>");
                sb.Append("<td class='z_bg_tendency'>" + shi.No_2 + "</td>");

                sb.Append("<td class='z_bg_tendency'>" + ge.No_0 + "</td>");
                sb.Append("<td class='z_bg_tendency'>" + ge.No_1 + "</td>");
                sb.Append("<td class='z_bg_tendency'>" + ge.No_2 + "</td>");

                sb.Append("<td class='z_bg_tendency'>" + wan.Dtime.Substring(6) + "</td>");
                sb.Append(" </tr>");
            }

            sb.Append(" <tr>");
            sb.Append("<td class='z_bg_tendency' colspan='3'>最大遗漏</td>");
            sb.Append("<td class='z_bg_tendency'>0</td>");
            sb.Append("<td class='z_bg_tendency'>" + maxWan.No_0 + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + maxWan.No_1 + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + maxWan.No_2 + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + maxQian.No_0 + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + maxQian.No_1 + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + maxQian.No_2 + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + maxBai.No_0 + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + maxBai.No_1 + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + maxBai.No_2 + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + maxShi.No_0 + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + maxShi.No_1 + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + maxShi.No_2 + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + maxGe.No_0 + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + maxGe.No_1 + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + maxGe.No_2 + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + "" + "</td>");
            sb.Append(" </tr>");

            sb.Append("</tbody>");

            return sb.ToString();
        }

        #region 一星(定位胆)走势
        /// <summary>
        /// 万、千、百、十、个走势
        /// </summary>
        /// <param name="type"></param>
        /// <param name="date"></param>
        /// <param name="num"></param>
        /// <returns></returns>
        public JsonResult PostDigitTrendecy(int type, string date, int num)
        {
            date = date.Replace("-", "");
            Tendency1Enum tendency1Enum = (Tendency1Enum)type;
            var list = XscpMysqlBLL.QueryTendency1(tendency1Enum, date, num);
            return Json(list, JsonRequestBehavior.AllowGet);
        }


        private TendencyDwdModel GetTendencyDwdValue(TendencyModel tm1, TendencyModel tm2)
        {
            if (tm1 == null || tm2 == null) return null;
            TendencyDwdModel tmResult = new TendencyDwdModel();
            tmResult.Lottery = tm1.Lottery;
            tmResult.Big = tm1.Big + "|" + tm2.Big;
            tmResult.Small = tm1.Small + "|" + tm2.Small;
            tmResult.BigSmall = tm1.Big + "|" + tm2.Small;
            tmResult.SmallBig = tm1.Small + "|" + tm2.Big;
            tmResult.Odd = tm1.Odd + "|" + tm2.Odd;
            tmResult.Pair = tm1.Pair + "|" + tm2.Pair;
            tmResult.OddPair = tm1.Odd + "|" + tm2.Pair;
            tmResult.PairOdd = tm1.Pair + "|" + tm2.Odd;
            tmResult.Dbl = "-";
            tmResult.Dtime = tm1.Dtime;
            return tmResult;
        }

        #region 走势背景色
        /// <summary>
        /// 二星背景色
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        private string getBgcolor2Start(int val)
        {
            var strResult = "<td align='center'";
            if (val == 0)
            {
                strResult = strResult + "bgcolor='#B0C4DE'  style='font-size:16px;font-weight:bold;'";
            }
            else if (val > 35)
            {
                strResult = strResult + "bgcolor='#9932CC'";
            }
            else if (val > 25)
            {
                strResult = strResult + "bgcolor='#FF0000'";
            }
            else if (val > 15)
            {
                strResult = strResult + "bgcolor='#FFD700'";
            }
            else if (val > 6)
            {
                strResult = strResult + "bgcolor='#00FF00'";
            }
            strResult = strResult + ">" + val + "</td>";
            return strResult;
        }

        /// <summary>
        /// 一星背景色
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        private string getBgcolor1Start(int val)
        {
            var strResult = "<td align='center'";
            if (val == 0)
            {
                strResult = strResult + "bgcolor='#B0C4DE'  style='font-size:16px;font-weight:bold;'";
            }
            else if (val > 15)
            {
                strResult = strResult + "bgcolor='#9932CC'";
            }
            else if (val > 12)
            {
                strResult = strResult + "bgcolor='#FF0000'";
            }
            else if (val > 7)
            {
                strResult = strResult + "bgcolor='#FFD700'";
            }
            else if (val > 3)
            {
                strResult = strResult + "bgcolor='#00FF00'";
            }
            strResult = strResult + ">" + val + "</td>";
            return strResult;
        }
        #endregion
        #endregion
    }
}
