﻿using System;
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

        /// <summary>
        /// 加载走势数据
        /// </summary>
        /// <param name="type"></param>
        /// <param name="date"></param>
        /// <param name="num"></param>
        /// <returns></returns>
        public ActionResult LoadTendencyData(string type, string date, int num = 10)
        {
            string strResult = null;
            date = date.Replace("-", "");
            switch (type)
            {
                //万
                case "x1wan":
                    strResult = Dwd1Start(Tendency1Enum.TenThousand, date, num);
                    break;
                //千
                case "x1qian":
                    strResult = Dwd1Start(Tendency1Enum.Thousand, date, num);
                    break;
                //百
                case "x1bai":
                    strResult = Dwd1Start(Tendency1Enum.Hundred, date, num);
                    break;
                //十
                case "x1shi":
                    strResult = Dwd1Start(Tendency1Enum.Ten, date, num);
                    break;
                //个
                case "x1ge":
                    strResult = Dwd1Start(Tendency1Enum.One, date, num);
                    break;
                //大中小
                case "x1dzx":
                    strResult = Road1StartDzx(date, num);
                    break;
                //大小奇偶
                case "x1dxjo":
                    strResult = Road1StartDxjo(date, num);
                    break;
                //012路
                case "x1012":
                    strResult = Road1Start012(date, num);
                    break;
                //二星012路
                case "x2012":
                    strResult = Road2Start012(date, num);
                    break;
                //二星大小奇偶
                case "x2sdxds":
                    strResult = Road2StartDxjo(date, num);
                    break;
                default:
                    break;
            }
            return Content(strResult);
        }

        /// <summary>
        /// 获取当前开奖号码
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public JsonResult LoadOpenCode(string date)
        {
            date = date.Replace("-", "");
            LotteryModel lm = null;
            var lt_lottery = XscpMysqlBLL.QueryLottery(date, 1);
            if (lt_lottery != null && lt_lottery.Count > 0)
            {
                lm = lt_lottery[0];
            }
            return Json(lm, JsonRequestBehavior.AllowGet);
        }

        #region [二星]
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
            sb.Append("<td colspan='10' class='z_bg_03'>前二</td>");
            sb.Append("<td width='15'  rowspan='2' class='z_bg_03'></td>");
            sb.Append("<td colspan='10' class='z_bg_03'>后二</td>");
            sb.Append("<td width='50' rowspan='2' class='z_bg_03'>时间</td>");
            sb.Append("</tr>");

            sb.Append("<tr>");
            sb.Append("<td class='z_bg_03'>大大</td>");
            sb.Append("<td class='z_bg_03'>小小</td>");
            sb.Append("<td class='z_bg_03'>大小</td>");
            sb.Append("<td class='z_bg_03'>小大</td>");
            sb.Append("<td width='10' class='z_bg_03'></td>");
            sb.Append("<td class='z_bg_03'>奇奇</td>");
            sb.Append("<td class='z_bg_03'>偶偶</td>");
            sb.Append("<td class='z_bg_03'>奇偶</td>");
            sb.Append("<td class='z_bg_03'>偶奇</td>");
            sb.Append("<td class='z_bg_03'>对子</td>");
            sb.Append("<td class='z_bg_03'>大大</td>");
            sb.Append("<td class='z_bg_03'>小小</td>");
            sb.Append("<td class='z_bg_03'>大小</td>");
            sb.Append("<td class='z_bg_03'>小大</td>");
            sb.Append("<td width='10' class='z_bg_03'></td>");
            sb.Append("<td class='z_bg_03'>奇奇</td>");
            sb.Append("<td class='z_bg_03'>偶偶</td>");
            sb.Append("<td class='z_bg_03'>奇偶</td>");
            sb.Append("<td class='z_bg_03'>偶奇</td>");
            sb.Append("<td class='z_bg_03'>对子</td>");
            sb.Append("</tr>");
            sb.Append("</tbody>");

            //二星遗漏
            var lt_BeforeData = XscpMysqlBLL.QueryTendency2(Tendency2Enum.Before, date, num);
            var maxBeforeData = XscpMysqlBLL.QueryMaxTendency2(Tendency2Enum.Before, date);

            var lt_AfterData = XscpMysqlBLL.QueryTendency2(Tendency2Enum.After, date, num);
            var maxAfterData = XscpMysqlBLL.QueryMaxTendency2(Tendency2Enum.After, date);

            //定位胆最大遗漏
            var maxWan = XscpMysqlBLL.QueryMaxTendency1(Tendency1Enum.TenThousand, date);
            var maxQian = XscpMysqlBLL.QueryMaxTendency1(Tendency1Enum.Thousand, date);
            var maxShi = XscpMysqlBLL.QueryMaxTendency1(Tendency1Enum.Ten, date);
            var maxGe = XscpMysqlBLL.QueryMaxTendency1(Tendency1Enum.One, date);

            var berforeMaxDwd = GetTendencyDwdValue(maxWan, maxQian);
            var afterMaxDwd = GetTendencyDwdValue(maxShi, maxGe);

            //当前定位胆遗漏
            var curWan = XscpMysqlBLL.QueryTendency1(Tendency1Enum.TenThousand, date, 1);
            var curQian = XscpMysqlBLL.QueryTendency1(Tendency1Enum.Thousand, date, 1);
            var curShi = XscpMysqlBLL.QueryTendency1(Tendency1Enum.Ten, date, 1);
            var curGe = XscpMysqlBLL.QueryTendency1(Tendency1Enum.One, date, 1);

            //前二
            TendencyDwdModel berforDwd = new TendencyDwdModel();
            if (curWan != null && curWan.Count > 0 && curQian != null && curQian.Count > 0)
            {
                berforDwd = GetTendencyDwdValue(curWan[0], curQian[0]);
            }

            //后二
            TendencyDwdModel afterDwd = new TendencyDwdModel();
            if (curShi != null && curShi.Count > 0 && curGe != null && curGe.Count > 0)
            {
                afterDwd = GetTendencyDwdValue(curShi[0], curGe[0]);
            }

            sb.Append("<tbody  id='pagedata'>");

            //二星最大遗漏
            sb.Append(" <tr>");
            sb.Append("<td class='z_bg_tendency' colspan='3'>二星最大遗漏</td>");
            sb.Append("<td class='z_bg_03'></td>");
            sb.Append("<td class='z_bg_tendency'>" + maxBeforeData.Big + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + maxBeforeData.Small + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + maxBeforeData.BigSmall + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + maxBeforeData.SmallBig + "</td>");
            sb.Append("<td class='z_bg_03'></td>");
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
            sb.Append("<td class='z_bg_03'></td>");
            sb.Append("<td class='z_bg_tendency'>" + maxAfterData.Odd + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + maxAfterData.Pair + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + maxAfterData.OddPair + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + maxAfterData.PairOdd + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + maxAfterData.Dbl + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + "-" + "</td>");
            sb.Append(" </tr>");

            //定位胆最大遗漏
            sb.Append(" <tr>");
            sb.Append("<td class='z_bg_tendency' colspan='3'>定位胆最大遗漏</td>");
            sb.Append("<td class='z_bg_03'></td>");
            sb.Append("<td class='z_bg_tendency'>" + berforeMaxDwd.Big + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + berforeMaxDwd.Small + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + berforeMaxDwd.BigSmall + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + berforeMaxDwd.SmallBig + "</td>");
            sb.Append("<td class='z_bg_03'></td>");
            sb.Append("<td class='z_bg_tendency'>" + berforeMaxDwd.Odd + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + berforeMaxDwd.Pair + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + berforeMaxDwd.OddPair + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + berforeMaxDwd.PairOdd + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + berforeMaxDwd.Dbl + "</td>");
            sb.Append("<td class='z_bg_03'>" + "" + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + afterMaxDwd.Big + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + afterMaxDwd.Small + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + afterMaxDwd.BigSmall + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + afterMaxDwd.SmallBig + "</td>");
            sb.Append("<td class='z_bg_03'></td>");
            sb.Append("<td class='z_bg_tendency'>" + afterMaxDwd.Odd + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + afterMaxDwd.Pair + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + afterMaxDwd.OddPair + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + afterMaxDwd.PairOdd + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + afterMaxDwd.Dbl + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + "-" + "</td>");
            sb.Append(" </tr>");

            //当前定位胆遗漏
            sb.Append(" <tr>");
            sb.Append("<td class='z_bg_tendency' colspan='3'>当前定位胆遗漏</td>");
            sb.Append("<td class='z_bg_03'></td>");
            sb.Append(getBgcolor1Dwd(berforDwd.Big));
            sb.Append(getBgcolor1Dwd(berforDwd.Small));
            sb.Append(getBgcolor1Dwd(berforDwd.BigSmall));
            sb.Append(getBgcolor1Dwd(berforDwd.SmallBig));
            sb.Append("<td class='z_bg_03'></td>");
            sb.Append(getBgcolor1Dwd(berforDwd.Odd));
            sb.Append(getBgcolor1Dwd(berforDwd.Pair));
            sb.Append(getBgcolor1Dwd(berforDwd.OddPair));
            sb.Append(getBgcolor1Dwd(berforDwd.PairOdd));
            sb.Append(getBgcolor1Dwd(berforDwd.Dbl));
            sb.Append("<td class='z_bg_03'></td>");
            sb.Append(getBgcolor1Dwd(afterDwd.Big));
            sb.Append(getBgcolor1Dwd(afterDwd.Small));
            sb.Append(getBgcolor1Dwd(afterDwd.BigSmall));
            sb.Append(getBgcolor1Dwd(afterDwd.SmallBig));
            sb.Append("<td class='z_bg_03'></td>");
            sb.Append(getBgcolor1Dwd(afterDwd.Odd));
            sb.Append(getBgcolor1Dwd(afterDwd.Pair));
            sb.Append(getBgcolor1Dwd(afterDwd.OddPair));
            sb.Append(getBgcolor1Dwd(afterDwd.PairOdd));
            sb.Append(getBgcolor1Dwd(afterDwd.Dbl));
            sb.Append("<td class='z_bg_tendency'>-</td>");
            sb.Append(" </tr>");

            for (int i = 0; i < lt_BeforeData.Count; i++)
            {
                var beforeData = lt_BeforeData[i];
                var afterData = lt_AfterData[i];
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
                sb.Append("<td class='z_bg_03'></td>");
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
                sb.Append("<td class='z_bg_03'></td>");
                sb.Append(getBgcolor2Start(afterData.Odd));
                sb.Append(getBgcolor2Start(afterData.Pair));
                sb.Append(getBgcolor2Start(afterData.OddPair));
                sb.Append(getBgcolor2Start(afterData.PairOdd));
                sb.Append(getBgcolor2Start(afterData.Dbl));
                sb.Append("<td class='z_bg_tendency'>" + beforeData.Dtime.Substring(6) + "</td>");
                sb.Append(" </tr>");
            }

            //当前定位胆遗漏
            sb.Append(" <tr>");
            sb.Append("<td class='z_bg_tendency' colspan='3'>当前定位胆遗漏</td>");
            sb.Append("<td class='z_bg_03'></td>");
            sb.Append(getBgcolor1Dwd(berforDwd.Big));
            sb.Append(getBgcolor1Dwd(berforDwd.Small));
            sb.Append(getBgcolor1Dwd(berforDwd.BigSmall));
            sb.Append(getBgcolor1Dwd(berforDwd.SmallBig));
            sb.Append("<td class='z_bg_03'></td>");
            sb.Append(getBgcolor1Dwd(berforDwd.Odd));
            sb.Append(getBgcolor1Dwd(berforDwd.Pair));
            sb.Append(getBgcolor1Dwd(berforDwd.OddPair));
            sb.Append(getBgcolor1Dwd(berforDwd.PairOdd));
            sb.Append(getBgcolor1Dwd(berforDwd.Dbl));
            sb.Append("<td class='z_bg_03'></td>");
            sb.Append(getBgcolor1Dwd(afterDwd.Big));
            sb.Append(getBgcolor1Dwd(afterDwd.Small));
            sb.Append(getBgcolor1Dwd(afterDwd.BigSmall));
            sb.Append(getBgcolor1Dwd(afterDwd.SmallBig));
            sb.Append("<td class='z_bg_03'></td>");
            sb.Append(getBgcolor1Dwd(afterDwd.Odd));
            sb.Append(getBgcolor1Dwd(afterDwd.Pair));
            sb.Append(getBgcolor1Dwd(afterDwd.OddPair));
            sb.Append(getBgcolor1Dwd(afterDwd.PairOdd));
            sb.Append(getBgcolor1Dwd(afterDwd.Dbl));
            sb.Append("<td class='z_bg_tendency'>-</td>");
            sb.Append(" </tr>");

            //定位胆最大遗漏
            sb.Append(" <tr>");
            sb.Append("<td class='z_bg_tendency' colspan='3'>定位胆最大遗漏</td>");
            sb.Append("<td class='z_bg_03'></td>");
            sb.Append("<td class='z_bg_tendency'>" + berforeMaxDwd.Big + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + berforeMaxDwd.Small + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + berforeMaxDwd.BigSmall + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + berforeMaxDwd.SmallBig + "</td>");
            sb.Append("<td class='z_bg_03'></td>");
            sb.Append("<td class='z_bg_tendency'>" + berforeMaxDwd.Odd + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + berforeMaxDwd.Pair + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + berforeMaxDwd.OddPair + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + berforeMaxDwd.PairOdd + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + berforeMaxDwd.Dbl + "</td>");
            sb.Append("<td class='z_bg_03'>" + "" + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + afterMaxDwd.Big + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + afterMaxDwd.Small + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + afterMaxDwd.BigSmall + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + afterMaxDwd.SmallBig + "</td>");
            sb.Append("<td class='z_bg_03'></td>");
            sb.Append("<td class='z_bg_tendency'>" + afterMaxDwd.Odd + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + afterMaxDwd.Pair + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + afterMaxDwd.OddPair + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + afterMaxDwd.PairOdd + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + afterMaxDwd.Dbl + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + "-" + "</td>");
            sb.Append(" </tr>");

            //二星最大遗漏
            sb.Append(" <tr>");
            sb.Append("<td class='z_bg_tendency' colspan='3'>二星最大遗漏</td>");
            sb.Append("<td class='z_bg_03'></td>");
            sb.Append("<td class='z_bg_tendency'>" + maxBeforeData.Big + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + maxBeforeData.Small + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + maxBeforeData.BigSmall + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + maxBeforeData.SmallBig + "</td>");
            sb.Append("<td class='z_bg_03'></td>");
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
            sb.Append("<td class='z_bg_03'></td>");
            sb.Append("<td class='z_bg_tendency'>" + maxAfterData.Odd + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + maxAfterData.Pair + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + maxAfterData.OddPair + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + maxAfterData.PairOdd + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + maxAfterData.Dbl + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + "-" + "</td>");
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
            sb.Append("<td width='15' rowspan='2' class='z_bg_03'></td>");
            sb.Append("<td colspan='11' class='z_bg_03'>前二</td>");
            sb.Append("<td width='15'  rowspan='2' class='z_bg_03'></td>");
            sb.Append("<td colspan='11' class='z_bg_03'>后二</td>");
            sb.Append("<td width='50' rowspan='2' class='z_bg_03'>时间</td>");
            sb.Append("</tr>");

            sb.Append("<tr>");
            sb.Append("<td class='z_bg_03'>00</td>");
            sb.Append("<td class='z_bg_03'>01</td>");
            sb.Append("<td class='z_bg_03'>02</td>");
            sb.Append("<td width='10' class='z_bg_03'></td>");
            sb.Append("<td class='z_bg_03'>10</td>");
            sb.Append("<td class='z_bg_03'>11</td>");
            sb.Append("<td class='z_bg_03'>12</td>");
            sb.Append("<td width='10' class='z_bg_03'></td>");
            sb.Append("<td class='z_bg_03'>20</td>");
            sb.Append("<td class='z_bg_03'>21</td>");
            sb.Append("<td class='z_bg_03'>22</td>");
            sb.Append("<td class='z_bg_03'>00</td>");
            sb.Append("<td class='z_bg_03'>01</td>");
            sb.Append("<td class='z_bg_03'>02</td>");
            sb.Append("<td width='10' class='z_bg_03'></td>");
            sb.Append("<td class='z_bg_03'>10</td>");
            sb.Append("<td class='z_bg_03'>11</td>");
            sb.Append("<td class='z_bg_03'>12</td>");
            sb.Append("<td width='10' class='z_bg_03'></td>");
            sb.Append("<td class='z_bg_03'>20</td>");
            sb.Append("<td class='z_bg_03'>21</td>");
            sb.Append("<td class='z_bg_03'>22</td>");
            sb.Append("</tr>");
            sb.Append("</tbody>");

            var lt_BeforeData = XscpMysqlBLL.QueryTendency2(Tendency2Enum.Before, date, num);
            var maxBeforeData = XscpMysqlBLL.QueryMaxTendency2(Tendency2Enum.Before, date);

            var lt_AfterData = XscpMysqlBLL.QueryTendency2(Tendency2Enum.After, date, num);
            var maxAfterData = XscpMysqlBLL.QueryMaxTendency2(Tendency2Enum.After, date);

            //定位胆最大遗漏
            var maxWan = XscpMysqlBLL.QueryMaxTendency1(Tendency1Enum.TenThousand, date);
            var maxQian = XscpMysqlBLL.QueryMaxTendency1(Tendency1Enum.Thousand, date);
            var maxShi = XscpMysqlBLL.QueryMaxTendency1(Tendency1Enum.Ten, date);
            var maxGe = XscpMysqlBLL.QueryMaxTendency1(Tendency1Enum.One, date);

            var berforeMaxDwd = GetTendencyDwdValue(maxWan, maxQian);
            var afterMaxDwd = GetTendencyDwdValue(maxShi, maxGe);

            //当前定位胆遗漏
            var curWan = XscpMysqlBLL.QueryTendency1(Tendency1Enum.TenThousand, date, 1);
            var curQian = XscpMysqlBLL.QueryTendency1(Tendency1Enum.Thousand, date, 1);
            var curShi = XscpMysqlBLL.QueryTendency1(Tendency1Enum.Ten, date, 1);
            var curGe = XscpMysqlBLL.QueryTendency1(Tendency1Enum.One, date, 1);

            //前二
            TendencyDwdModel berforDwd = new TendencyDwdModel();
            if (curWan != null && curWan.Count > 0 && curQian != null && curQian.Count > 0)
            {
                berforDwd = GetTendencyDwdValue(curWan[0], curQian[0]);
            }

            //后二
            TendencyDwdModel afterDwd = new TendencyDwdModel();
            if (curShi != null && curShi.Count > 0 && curGe != null && curGe.Count > 0)
            {
                afterDwd = GetTendencyDwdValue(curShi[0], curGe[0]);
            }

            sb.Append("<tbody  id='pagedata'>");

            //二星最大遗漏
            sb.Append(" <tr>");
            sb.Append("<td class='z_bg_tendency' colspan='3'>二星最大遗漏</td>");
            sb.Append("<td class='z_bg_03'></td>");
            sb.Append("<td class='z_bg_tendency'>" + maxBeforeData.No_00 + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + maxBeforeData.No_01 + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + maxBeforeData.No_02 + "</td>");
            sb.Append("<td class='z_bg_03'>" + "" + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + maxBeforeData.No_10 + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + maxBeforeData.No_11 + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + maxBeforeData.No_12 + "</td>");
            sb.Append("<td class='z_bg_03'>" + "" + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + maxBeforeData.No_20 + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + maxBeforeData.No_21 + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + maxBeforeData.No_22 + "</td>");
            sb.Append("<td class='z_bg_03'>" + "" + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + maxAfterData.No_00 + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + maxAfterData.No_01 + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + maxAfterData.No_02 + "</td>");
            sb.Append("<td class='z_bg_03'>" + "" + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + maxAfterData.No_10 + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + maxAfterData.No_11 + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + maxAfterData.No_12 + "</td>");
            sb.Append("<td class='z_bg_03'>" + "" + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + maxAfterData.No_20 + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + maxAfterData.No_21 + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + maxAfterData.No_22 + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + "-" + "</td>");
            sb.Append(" </tr>");

            //定位胆最大遗漏
            sb.Append(" <tr>");
            sb.Append("<td class='z_bg_tendency' colspan='3'>定位胆最大遗漏</td>");
            sb.Append("<td class='z_bg_03'></td>");
            sb.Append("<td class='z_bg_tendency'>" + berforeMaxDwd.No_00 + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + berforeMaxDwd.No_01 + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + berforeMaxDwd.No_02 + "</td>");
            sb.Append("<td class='z_bg_03'>" + "" + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + berforeMaxDwd.No_10 + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + berforeMaxDwd.No_11 + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + berforeMaxDwd.No_12 + "</td>");
            sb.Append("<td class='z_bg_03'>" + "" + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + berforeMaxDwd.No_20 + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + berforeMaxDwd.No_21 + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + berforeMaxDwd.No_22 + "</td>");
            sb.Append("<td class='z_bg_03'>" + "" + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + afterMaxDwd.No_00 + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + afterMaxDwd.No_01 + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + afterMaxDwd.No_02 + "</td>");
            sb.Append("<td class='z_bg_03'>" + "" + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + afterMaxDwd.No_10 + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + afterMaxDwd.No_11 + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + afterMaxDwd.No_12 + "</td>");
            sb.Append("<td class='z_bg_03'>" + "" + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + afterMaxDwd.No_20 + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + afterMaxDwd.No_21 + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + afterMaxDwd.No_22 + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + "-" + "</td>");
            sb.Append(" </tr>");

            //当前定位胆遗漏
            sb.Append(" <tr>");
            sb.Append("<td class='z_bg_tendency' colspan='3'>当前定位胆遗漏</td>");
            sb.Append("<td class='z_bg_03'></td>");
            sb.Append(getBgcolor1Dwd(berforDwd.No_00));
            sb.Append(getBgcolor1Dwd(berforDwd.No_01));
            sb.Append(getBgcolor1Dwd(berforDwd.No_02));
            sb.Append("<td class='z_bg_03'>" + "" + "</td>");
            sb.Append(getBgcolor1Dwd(berforDwd.No_10));
            sb.Append(getBgcolor1Dwd(berforDwd.No_11));
            sb.Append(getBgcolor1Dwd(berforDwd.No_12));
            sb.Append("<td class='z_bg_03'>" + "" + "</td>");
            sb.Append(getBgcolor1Dwd(berforDwd.No_20));
            sb.Append(getBgcolor1Dwd(berforDwd.No_21));
            sb.Append(getBgcolor1Dwd(berforDwd.No_22));
            sb.Append("<td class='z_bg_03'>" + "" + "</td>");
            sb.Append(getBgcolor1Dwd(afterDwd.No_00));
            sb.Append(getBgcolor1Dwd(afterDwd.No_01));
            sb.Append(getBgcolor1Dwd(afterDwd.No_02));
            sb.Append("<td class='z_bg_03'>" + "" + "</td>");
            sb.Append(getBgcolor1Dwd(afterDwd.No_10));
            sb.Append(getBgcolor1Dwd(afterDwd.No_11));
            sb.Append(getBgcolor1Dwd(afterDwd.No_12));
            sb.Append("<td class='z_bg_03'>" + "" + "</td>");
            sb.Append(getBgcolor1Dwd(afterDwd.No_20));
            sb.Append(getBgcolor1Dwd(afterDwd.No_21));
            sb.Append(getBgcolor1Dwd(afterDwd.No_22));
            sb.Append("<td class='z_bg_tendency'>" + "-" + "</td>");
            sb.Append(" </tr>");

            for (int i = 0; i < lt_BeforeData.Count; i++)
            {
                var beforeData = lt_BeforeData[i];
                var afterData = lt_AfterData[i];
                string lottery = beforeData.Lottery;

                sb.Append(" <tr>");
                sb.Append("<td class='z_bg_tendency'>" + (i + 1).ToString() + "</td>");
                sb.Append("<td class='z_bg_tendency'>" + beforeData.Ymd + "-" + beforeData.Sno + "</td>");
                sb.Append("<td class='z_bg_tendency'>" + "<span style='color:Red;font-weight:bold;'>" + lottery.Substring(0, 3) + "</span>" + lottery.Substring(3, 3) + "<span style='color:Red;font-weight:bold;'>" + lottery.Substring(6) + "</span></td>");
                sb.Append("<td class='z_bg_03'>" + "" + "</td>");
                sb.Append(getBgcolor2Start(beforeData.No_00));
                sb.Append(getBgcolor2Start(beforeData.No_01));
                sb.Append(getBgcolor2Start(beforeData.No_02));
                sb.Append("<td class='z_bg_03'>" + "" + "</td>");
                sb.Append(getBgcolor2Start(beforeData.No_10));
                sb.Append(getBgcolor2Start(beforeData.No_11));
                sb.Append(getBgcolor2Start(beforeData.No_12));
                sb.Append("<td class='z_bg_03'>" + "" + "</td>");
                sb.Append(getBgcolor2Start(beforeData.No_20));
                sb.Append(getBgcolor2Start(beforeData.No_21));
                sb.Append(getBgcolor2Start(beforeData.No_22));
                sb.Append("<td class='z_bg_03'>" + "" + "</td>");
                sb.Append(getBgcolor2Start(afterData.No_00));
                sb.Append(getBgcolor2Start(afterData.No_01));
                sb.Append(getBgcolor2Start(afterData.No_02));
                sb.Append("<td class='z_bg_03'>" + "" + "</td>");
                sb.Append(getBgcolor2Start(afterData.No_10));
                sb.Append(getBgcolor2Start(afterData.No_11));
                sb.Append(getBgcolor2Start(afterData.No_12));
                sb.Append("<td class='z_bg_03'>" + "" + "</td>");
                sb.Append(getBgcolor2Start(afterData.No_20));
                sb.Append(getBgcolor2Start(afterData.No_21));
                sb.Append(getBgcolor2Start(afterData.No_22));
                sb.Append("<td class='z_bg_tendency'>" + beforeData.Dtime.Substring(6) + "</td>");
                sb.Append(" </tr>");
            }


            //当前定位胆遗漏
            sb.Append(" <tr>");
            sb.Append("<td class='z_bg_tendency' colspan='3'>当前定位胆遗漏</td>");
            sb.Append("<td class='z_bg_03'></td>");
            sb.Append(getBgcolor1Dwd(berforDwd.No_00));
            sb.Append(getBgcolor1Dwd(berforDwd.No_01));
            sb.Append(getBgcolor1Dwd(berforDwd.No_02));
            sb.Append("<td class='z_bg_03'>" + "" + "</td>");
            sb.Append(getBgcolor1Dwd(berforDwd.No_10));
            sb.Append(getBgcolor1Dwd(berforDwd.No_11));
            sb.Append(getBgcolor1Dwd(berforDwd.No_12));
            sb.Append("<td class='z_bg_03'>" + "" + "</td>");
            sb.Append(getBgcolor1Dwd(berforDwd.No_20));
            sb.Append(getBgcolor1Dwd(berforDwd.No_21));
            sb.Append(getBgcolor1Dwd(berforDwd.No_22));
            sb.Append("<td class='z_bg_03'>" + "" + "</td>");
            sb.Append(getBgcolor1Dwd(afterDwd.No_00));
            sb.Append(getBgcolor1Dwd(afterDwd.No_01));
            sb.Append(getBgcolor1Dwd(afterDwd.No_02));
            sb.Append("<td class='z_bg_03'>" + "" + "</td>");
            sb.Append(getBgcolor1Dwd(afterDwd.No_10));
            sb.Append(getBgcolor1Dwd(afterDwd.No_11));
            sb.Append(getBgcolor1Dwd(afterDwd.No_12));
            sb.Append("<td class='z_bg_03'>" + "" + "</td>");
            sb.Append(getBgcolor1Dwd(afterDwd.No_20));
            sb.Append(getBgcolor1Dwd(afterDwd.No_21));
            sb.Append(getBgcolor1Dwd(afterDwd.No_22));
            sb.Append("<td class='z_bg_tendency'>" + "-" + "</td>");
            sb.Append(" </tr>");

            //定位胆最大遗漏
            sb.Append(" <tr>");
            sb.Append("<td class='z_bg_tendency' colspan='3'>定位胆最大遗漏</td>");
            sb.Append("<td class='z_bg_03'></td>");
            sb.Append("<td class='z_bg_tendency'>" + berforeMaxDwd.No_00 + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + berforeMaxDwd.No_01 + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + berforeMaxDwd.No_02 + "</td>");
            sb.Append("<td class='z_bg_03'>" + "" + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + berforeMaxDwd.No_10 + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + berforeMaxDwd.No_11 + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + berforeMaxDwd.No_12 + "</td>");
            sb.Append("<td class='z_bg_03'>" + "" + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + berforeMaxDwd.No_20 + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + berforeMaxDwd.No_21 + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + berforeMaxDwd.No_22 + "</td>");
            sb.Append("<td class='z_bg_03'>" + "" + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + afterMaxDwd.No_00 + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + afterMaxDwd.No_01 + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + afterMaxDwd.No_02 + "</td>");
            sb.Append("<td class='z_bg_03'>" + "" + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + afterMaxDwd.No_10 + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + afterMaxDwd.No_11 + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + afterMaxDwd.No_12 + "</td>");
            sb.Append("<td class='z_bg_03'>" + "" + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + afterMaxDwd.No_20 + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + afterMaxDwd.No_21 + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + afterMaxDwd.No_22 + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + "-" + "</td>");
            sb.Append(" </tr>");

            //二星最大遗漏
            sb.Append(" <tr>");
            sb.Append("<td class='z_bg_tendency' colspan='3'>二星最大遗漏</td>");
            sb.Append("<td class='z_bg_03'></td>");
            sb.Append("<td class='z_bg_tendency'>" + maxBeforeData.No_00 + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + maxBeforeData.No_01 + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + maxBeforeData.No_02 + "</td>");
            sb.Append("<td class='z_bg_03'>" + "" + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + maxBeforeData.No_10 + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + maxBeforeData.No_11 + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + maxBeforeData.No_12 + "</td>");
            sb.Append("<td class='z_bg_03'>" + "" + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + maxBeforeData.No_20 + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + maxBeforeData.No_21 + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + maxBeforeData.No_22 + "</td>");
            sb.Append("<td class='z_bg_03'>" + "" + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + maxAfterData.No_00 + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + maxAfterData.No_01 + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + maxAfterData.No_02 + "</td>");
            sb.Append("<td class='z_bg_03'>" + "" + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + maxAfterData.No_10 + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + maxAfterData.No_11 + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + maxAfterData.No_12 + "</td>");
            sb.Append("<td class='z_bg_03'>" + "" + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + maxAfterData.No_20 + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + maxAfterData.No_21 + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + maxAfterData.No_22 + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + "-" + "</td>");
            sb.Append(" </tr>");
            sb.Append("</tbody>");
            return sb.ToString();
        }
        #endregion

        #region [一星]
        /// <summary>
        /// 一星定位胆
        /// </summary>
        /// <param name="date"></param>
        /// <param name="num"></param>
        /// <returns></returns>
        private string Dwd1Start(Tendency1Enum type, string date, int num)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<tbody id='tbodyssc'>");
            sb.Append("<tr>");
            sb.Append("<td width='30' rowspan='2' class='z_bg_03'>序号</td>");
            sb.Append("<td width='120' rowspan='2' class='z_bg_03'> 期号 </td>");
            sb.Append("<td width='45' rowspan='2' class='z_bg_03'>奖号</td>");
            sb.Append("<td width='45' rowspan='2' class='z_bg_03'>类型</td>");
            sb.Append("<td colspan='2' class='z_bg_03'>大小</td>");
            sb.Append("<td colspan='2' class='z_bg_03'>奇偶</td>");
            sb.Append("<td colspan='3' class='z_bg_03'>大中小</td>");
            sb.Append("<td colspan='2' class='z_bg_03'>质合</td>");
            sb.Append("<td colspan='3' class='z_bg_03'>012路</td>");
            //sb.Append("<td colspan='10' class='z_bg_03'>0～9</td>");
            sb.Append("<td width='50' rowspan='2' class='z_bg_03'>时间</td>");
            sb.Append("</tr>");

            sb.Append("<tr>");
            sb.Append("<td class='z_bg_03'>大</td>");
            sb.Append("<td class='z_bg_03'>小</td>");
            sb.Append("<td class='z_bg_03'>奇</td>");
            sb.Append("<td class='z_bg_03'>偶</td>");
            sb.Append("<td class='z_bg_03'>大</td>");
            sb.Append("<td class='z_bg_03'>中</td>");
            sb.Append("<td class='z_bg_03'>小</td>");
            sb.Append("<td class='z_bg_03'>质</td>");
            sb.Append("<td class='z_bg_03'>合</td>");
            sb.Append("<td class='z_bg_03'>0</td>");
            sb.Append("<td class='z_bg_03'>1</td>");
            sb.Append("<td class='z_bg_03'>2</td>");
            //sb.Append("<td class='z_bg_03'>0</td>");
            //sb.Append("<td class='z_bg_03'>1</td>");
            //sb.Append("<td class='z_bg_03'>2</td>");
            //sb.Append("<td class='z_bg_03'>3</td>");
            //sb.Append("<td class='z_bg_03'>4</td>");
            //sb.Append("<td class='z_bg_03'>5</td>");
            //sb.Append("<td class='z_bg_03'>6</td>");
            //sb.Append("<td class='z_bg_03'>7</td>");
            //sb.Append("<td class='z_bg_03'>8</td>");
            //sb.Append("<td class='z_bg_03'>9</td>");
            sb.Append("</tr>");
            sb.Append("</tbody>");

            var lt_data = XscpMysqlBLL.QueryTendency1(type, date, num);
            var maxData = XscpMysqlBLL.QueryMaxTendency1(type, date);
            sb.Append("<tbody>");

            sb.Append(" <tr>");
            sb.Append("<td class='z_bg_tendency' colspan='3'>最大遗漏</td>");
            sb.Append("<td class='z_bg_tendency'>0</td>");
            sb.Append("<td class='z_bg_tendency'>" + maxData.Big + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + maxData.Small + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + maxData.Odd + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + maxData.Pair + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + maxData.Big_1 + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + maxData.Mid_1 + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + maxData.Small_1 + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + maxData.Prime + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + maxData.Composite + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + maxData.No_0 + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + maxData.No_1 + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + maxData.No_2 + "</td>");
            //sb.Append("<td class='z_bg_tendency'>" + "" + "</td>");
            //sb.Append("<td class='z_bg_tendency'>" + "" + "</td>");
            //sb.Append("<td class='z_bg_tendency'>" + "" + "</td>");
            //sb.Append("<td class='z_bg_tendency'>" + "" + "</td>");
            //sb.Append("<td class='z_bg_tendency'>" + "" + "</td>");
            //sb.Append("<td class='z_bg_tendency'>" + "" + "</td>");
            //sb.Append("<td class='z_bg_tendency'>" + "" + "</td>");
            //sb.Append("<td class='z_bg_tendency'>" + "" + "</td>");
            //sb.Append("<td class='z_bg_tendency'>" + "" + "</td>");
            //sb.Append("<td class='z_bg_tendency'>" + "" + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + "" + "</td>");
            sb.Append(" </tr>");


            for (int i = 0; i < lt_data.Count; i++)
            {
                var data = lt_data[i];
                var lottery = data.Lottery;
                sb.Append(" <tr>");
                sb.Append("<td class='z_bg_tendency'>" + (i + 1).ToString() + "</td>");
                sb.Append("<td class='z_bg_tendency'>" + data.Ymd + "-" + data.Sno + "</td>");
                sb.Append(dwdFormateLottery(type, lottery));
                sb.Append("<td class='z_bg_tendency'>" + "" + "</td>");
                sb.Append(getBgcolor1Start(data.Big));
                sb.Append(getBgcolor1Start(data.Small));
                sb.Append(getBgcolor1Start(data.Odd));
                sb.Append(getBgcolor1Start(data.Pair));
                sb.Append(getBgcolor1Start(data.Big_1));
                sb.Append(getBgcolor1Start(data.Mid_1));
                sb.Append(getBgcolor1Start(data.Small_1));
                sb.Append(getBgcolor1Start(data.Prime));
                sb.Append(getBgcolor1Start(data.Composite));
                sb.Append(getBgcolor1Start(data.No_0));
                sb.Append(getBgcolor1Start(data.No_1));
                sb.Append(getBgcolor1Start(data.No_2));

                //sb.Append("<td class='z_bg_tendency'>" + "" + "</td>");
                //sb.Append("<td class='z_bg_tendency'>" + "" + "</td>");
                //sb.Append("<td class='z_bg_tendency'>" + "" + "</td>");
                //sb.Append("<td class='z_bg_tendency'>" + "" + "</td>");
                //sb.Append("<td class='z_bg_tendency'>" + "" + "</td>");
                //sb.Append("<td class='z_bg_tendency'>" + "" + "</td>");
                //sb.Append("<td class='z_bg_tendency'>" + "" + "</td>");
                //sb.Append("<td class='z_bg_tendency'>" + "" + "</td>");
                //sb.Append("<td class='z_bg_tendency'>" + "" + "</td>");
                //sb.Append("<td class='z_bg_tendency'>" + "" + "</td>");
                sb.Append("<td class='z_bg_tendency'>" + data.Dtime.Substring(6) + "</td>");
                sb.Append(" </tr>");
            }

            sb.Append(" <tr>");
            sb.Append("<td class='z_bg_tendency' colspan='3'>最大遗漏</td>");
            sb.Append("<td class='z_bg_tendency'>0</td>");
            sb.Append("<td class='z_bg_tendency'>" + maxData.Big + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + maxData.Small + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + maxData.Odd + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + maxData.Pair + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + maxData.Big_1 + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + maxData.Mid_1 + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + maxData.Small_1 + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + maxData.Prime + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + maxData.Composite + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + maxData.No_0 + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + maxData.No_1 + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + maxData.No_2 + "</td>");
            //sb.Append("<td class='z_bg_tendency'>" + "" + "</td>");
            //sb.Append("<td class='z_bg_tendency'>" + "" + "</td>");
            //sb.Append("<td class='z_bg_tendency'>" + "" + "</td>");
            //sb.Append("<td class='z_bg_tendency'>" + "" + "</td>");
            //sb.Append("<td class='z_bg_tendency'>" + "" + "</td>");
            //sb.Append("<td class='z_bg_tendency'>" + "" + "</td>");
            //sb.Append("<td class='z_bg_tendency'>" + "" + "</td>");
            //sb.Append("<td class='z_bg_tendency'>" + "" + "</td>");
            //sb.Append("<td class='z_bg_tendency'>" + "" + "</td>");
            //sb.Append("<td class='z_bg_tendency'>" + "" + "</td>");
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
            sb.Append("<td width='15' rowspan='3' class='z_bg_03'></td>");
            sb.Append("<td colspan='19' class='z_bg_03'>012路</td>");
            sb.Append("<td width='50' rowspan='3' class='z_bg_03'>时间</td>");
            sb.Append("</tr>");

            sb.Append("<tr>");
            sb.Append("<td colspan='4' class='z_bg_03'>万</td>");
            sb.Append("<td colspan='4' class='z_bg_03'>千</td>");
            sb.Append("<td colspan='4' class='z_bg_03'>百</td>");
            sb.Append("<td colspan='4' class='z_bg_03'>十</td>");
            sb.Append("<td colspan='3' class='z_bg_03'>个</td>");
            sb.Append("</tr>");

            sb.Append("<tr>");
            sb.Append("<td class='z_bg_03'>0</td>");
            sb.Append("<td class='z_bg_03'>1</td>");
            sb.Append("<td class='z_bg_03'>2</td>");
            sb.Append("<td width='10' class='z_bg_03'></td>");
            sb.Append("<td class='z_bg_03'>0</td>");
            sb.Append("<td class='z_bg_03'>1</td>");
            sb.Append("<td class='z_bg_03'>2</td>");
            sb.Append("<td width='10' class='z_bg_03'></td>");
            sb.Append("<td class='z_bg_03'>0</td>");
            sb.Append("<td class='z_bg_03'>1</td>");
            sb.Append("<td class='z_bg_03'>2</td>");
            sb.Append("<td width='10' class='z_bg_03'></td>");
            sb.Append("<td class='z_bg_03'>0</td>");
            sb.Append("<td class='z_bg_03'>1</td>");
            sb.Append("<td class='z_bg_03'>2</td>");
            sb.Append("<td width='10' class='z_bg_03'></td>");
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
            sb.Append("<td class='z_bg_03'></td>");
            sb.Append("<td class='z_bg_tendency'>" + maxWan.No_0 + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + maxWan.No_1 + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + maxWan.No_2 + "</td>");
            sb.Append("<td class='z_bg_03'></td>");
            sb.Append("<td class='z_bg_tendency'>" + maxQian.No_0 + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + maxQian.No_1 + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + maxQian.No_2 + "</td>");
            sb.Append("<td class='z_bg_03'></td>");
            sb.Append("<td class='z_bg_tendency'>" + maxBai.No_0 + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + maxBai.No_1 + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + maxBai.No_2 + "</td>");
            sb.Append("<td class='z_bg_03'></td>");
            sb.Append("<td class='z_bg_tendency'>" + maxShi.No_0 + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + maxShi.No_1 + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + maxShi.No_2 + "</td>");
            sb.Append("<td class='z_bg_03'></td>");
            sb.Append("<td class='z_bg_tendency'>" + maxGe.No_0 + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + maxGe.No_1 + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + maxGe.No_2 + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + "-" + "</td>");
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
                sb.Append("<td class='z_bg_tendency'>" + wan.Lottery + "</td>");
                sb.Append("<td class='z_bg_03'></td>");

                sb.Append(getBgcolor1Start(wan.No_0));
                sb.Append(getBgcolor1Start(wan.No_1));
                sb.Append(getBgcolor1Start(wan.No_2));
                sb.Append("<td class='z_bg_03'></td>");
                sb.Append(getBgcolor1Start(qian.No_0));
                sb.Append(getBgcolor1Start(qian.No_1));
                sb.Append(getBgcolor1Start(qian.No_2));
                sb.Append("<td class='z_bg_03'></td>");
                sb.Append(getBgcolor1Start(bai.No_0));
                sb.Append(getBgcolor1Start(bai.No_1));
                sb.Append(getBgcolor1Start(bai.No_2));
                sb.Append("<td class='z_bg_03'></td>");
                sb.Append(getBgcolor1Start(shi.No_0));
                sb.Append(getBgcolor1Start(shi.No_1));
                sb.Append(getBgcolor1Start(shi.No_2));
                sb.Append("<td class='z_bg_03'></td>");
                sb.Append(getBgcolor1Start(ge.No_0));
                sb.Append(getBgcolor1Start(ge.No_1));
                sb.Append(getBgcolor1Start(ge.No_2));
                sb.Append("<td class='z_bg_tendency'>" + wan.Dtime.Substring(6) + "</td>");
                sb.Append(" </tr>");
            }

            sb.Append(" <tr>");
            sb.Append("<td class='z_bg_tendency' colspan='3'>最大遗漏</td>");
            sb.Append("<td class='z_bg_03'></td>");
            sb.Append("<td class='z_bg_tendency'>" + maxWan.No_0 + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + maxWan.No_1 + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + maxWan.No_2 + "</td>");
            sb.Append("<td class='z_bg_03'></td>");
            sb.Append("<td class='z_bg_tendency'>" + maxQian.No_0 + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + maxQian.No_1 + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + maxQian.No_2 + "</td>");
            sb.Append("<td class='z_bg_03'></td>");
            sb.Append("<td class='z_bg_tendency'>" + maxBai.No_0 + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + maxBai.No_1 + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + maxBai.No_2 + "</td>");
            sb.Append("<td class='z_bg_03'></td>");
            sb.Append("<td class='z_bg_tendency'>" + maxShi.No_0 + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + maxShi.No_1 + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + maxShi.No_2 + "</td>");
            sb.Append("<td class='z_bg_03'></td>");
            sb.Append("<td class='z_bg_tendency'>" + maxGe.No_0 + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + maxGe.No_1 + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + maxGe.No_2 + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + "-" + "</td>");
            sb.Append(" </tr>");


            sb.Append("</tbody>");

            return sb.ToString();
        }

        /// <summary>
        /// 1星大中小
        /// </summary>
        /// <param name="date"></param>
        /// <param name="num"></param>
        /// <returns></returns>
        private string Road1StartDzx(string date, int num)
        {
            StringBuilder sb = new StringBuilder();

            //@1.标题
            sb.Append("<tbody id='tbodyssc'>");
            sb.Append("<tr>");
            sb.Append("<td width='30' rowspan='3' class='z_bg_03'>序号</td>");
            sb.Append("<td width='120' rowspan='3' class='z_bg_03'> 期号 </td>");
            sb.Append("<td width='45' rowspan='3' class='z_bg_03'>奖号</td>");
            sb.Append("<td width='45' rowspan='3' class='z_bg_03'>类型</td>");
            sb.Append("<td colspan='15' class='z_bg_03'>大中小</td>");
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
            sb.Append("<td class='z_bg_03'>大</td>");
            sb.Append("<td class='z_bg_03'>中</td>");
            sb.Append("<td class='z_bg_03'>小</td>");
            sb.Append("<td class='z_bg_03'>大</td>");
            sb.Append("<td class='z_bg_03'>中</td>");
            sb.Append("<td class='z_bg_03'>小</td>");
            sb.Append("<td class='z_bg_03'>大</td>");
            sb.Append("<td class='z_bg_03'>中</td>");
            sb.Append("<td class='z_bg_03'>小</td>");
            sb.Append("<td class='z_bg_03'>大</td>");
            sb.Append("<td class='z_bg_03'>中</td>");
            sb.Append("<td class='z_bg_03'>小</td>");
            sb.Append("<td class='z_bg_03'>大</td>");
            sb.Append("<td class='z_bg_03'>中</td>");
            sb.Append("<td class='z_bg_03'>小</td>");
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
            sb.Append("<td class='z_bg_tendency'>" + maxWan.Big_1 + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + maxWan.Mid_1 + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + maxWan.Small_1 + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + maxQian.Big_1 + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + maxQian.Mid_1 + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + maxQian.Small_1 + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + maxBai.Big_1 + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + maxBai.Mid_1 + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + maxBai.Small_1 + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + maxShi.Big_1 + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + maxShi.Mid_1 + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + maxShi.Small_1 + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + maxGe.Big_1 + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + maxGe.Mid_1 + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + maxGe.Small_1 + "</td>");
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
                sb.Append("<td class='z_bg_tendency'>" + wan.Lottery + "</td>");
                sb.Append("<td class='z_bg_tendency'>" + "" + "</td>");

                sb.Append(getBgcolor1Start(wan.Big_1));
                sb.Append(getBgcolor1Start(wan.Mid_1));
                sb.Append(getBgcolor1Start(wan.Small_1));
                sb.Append(getBgcolor1Start(qian.Big_1));
                sb.Append(getBgcolor1Start(qian.Mid_1));
                sb.Append(getBgcolor1Start(qian.Small_1));
                sb.Append(getBgcolor1Start(bai.Big_1));
                sb.Append(getBgcolor1Start(bai.Mid_1));
                sb.Append(getBgcolor1Start(bai.Small_1));
                sb.Append(getBgcolor1Start(shi.Big_1));
                sb.Append(getBgcolor1Start(shi.Mid_1));
                sb.Append(getBgcolor1Start(shi.Small_1));
                sb.Append(getBgcolor1Start(ge.Big_1));
                sb.Append(getBgcolor1Start(ge.Mid_1));
                sb.Append(getBgcolor1Start(ge.Small_1));
                sb.Append("<td class='z_bg_tendency'>" + wan.Dtime.Substring(6) + "</td>");
                sb.Append(" </tr>");
            }

            sb.Append(" <tr>");
            sb.Append("<td class='z_bg_tendency' colspan='3'>最大遗漏</td>");
            sb.Append("<td class='z_bg_tendency'>0</td>");
            sb.Append("<td class='z_bg_tendency'>" + maxWan.Big_1 + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + maxWan.Mid_1 + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + maxWan.Small_1 + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + maxQian.Big_1 + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + maxQian.Mid_1 + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + maxQian.Small_1 + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + maxBai.Big_1 + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + maxBai.Mid_1 + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + maxBai.Small_1 + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + maxShi.Big_1 + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + maxShi.Mid_1 + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + maxShi.Small_1 + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + maxGe.Big_1 + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + maxGe.Mid_1 + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + maxGe.Small_1 + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + "" + "</td>");
            sb.Append(" </tr>");
            sb.Append("</tbody>");

            return sb.ToString();
        }

        /// <summary>
        /// 1星大小奇偶
        /// </summary>
        /// <param name="date"></param>
        /// <param name="num"></param>
        /// <returns></returns>
        private string Road1StartDxjo(string date, int num)
        {
            StringBuilder sb = new StringBuilder();

            //@1.标题
            sb.Append("<tbody id='tbodyssc'>");
            sb.Append("<tr>");
            sb.Append("<td width='30' rowspan='3' class='z_bg_03'>序号</td>");
            sb.Append("<td width='120' rowspan='3' class='z_bg_03'> 期号 </td>");
            sb.Append("<td width='45' rowspan='3' class='z_bg_03'>奖号</td>");
            sb.Append("<td width='45' rowspan='3' class='z_bg_03'>类型</td>");
            sb.Append("<td colspan='10' class='z_bg_03'>大小</td>");
            sb.Append("<td colspan='10' class='z_bg_03'>奇偶</td>");
            sb.Append("<td width='50' rowspan='3' class='z_bg_03'>时间</td>");
            sb.Append("</tr>");

            sb.Append("<tr>");
            sb.Append("<td colspan='2' class='z_bg_03'>万</td>");
            sb.Append("<td colspan='2' class='z_bg_03'>千</td>");
            sb.Append("<td colspan='2' class='z_bg_03'>百</td>");
            sb.Append("<td colspan='2' class='z_bg_03'>十</td>");
            sb.Append("<td colspan='2' class='z_bg_03'>个</td>");
            sb.Append("<td colspan='2' class='z_bg_03'>万</td>");
            sb.Append("<td colspan='2' class='z_bg_03'>千</td>");
            sb.Append("<td colspan='2' class='z_bg_03'>百</td>");
            sb.Append("<td colspan='2' class='z_bg_03'>十</td>");
            sb.Append("<td colspan='2' class='z_bg_03'>个</td>");
            sb.Append("</tr>");

            sb.Append("<tr>");
            sb.Append("<td class='z_bg_03'>大</td>");
            sb.Append("<td class='z_bg_03'>小</td>");
            sb.Append("<td class='z_bg_03'>大</td>");
            sb.Append("<td class='z_bg_03'>小</td>");
            sb.Append("<td class='z_bg_03'>大</td>");
            sb.Append("<td class='z_bg_03'>小</td>");
            sb.Append("<td class='z_bg_03'>大</td>");
            sb.Append("<td class='z_bg_03'>小</td>");
            sb.Append("<td class='z_bg_03'>大</td>");
            sb.Append("<td class='z_bg_03'>小</td>");

            sb.Append("<td class='z_bg_03'>奇</td>");
            sb.Append("<td class='z_bg_03'>偶</td>");
            sb.Append("<td class='z_bg_03'>奇</td>");
            sb.Append("<td class='z_bg_03'>偶</td>");
            sb.Append("<td class='z_bg_03'>奇</td>");
            sb.Append("<td class='z_bg_03'>偶</td>");
            sb.Append("<td class='z_bg_03'>奇</td>");
            sb.Append("<td class='z_bg_03'>偶</td>");
            sb.Append("<td class='z_bg_03'>奇</td>");
            sb.Append("<td class='z_bg_03'>偶</td>");
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
            sb.Append("<td class='z_bg_tendency'>" + maxWan.Big + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + maxWan.Small + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + maxQian.Big + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + maxQian.Small + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + maxBai.Big + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + maxBai.Small + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + maxShi.Big + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + maxShi.Small + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + maxGe.Big + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + maxGe.Small + "</td>");

            sb.Append("<td class='z_bg_tendency'>" + maxWan.Odd + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + maxWan.Pair + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + maxQian.Odd + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + maxQian.Pair + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + maxBai.Odd + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + maxBai.Pair + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + maxShi.Odd + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + maxShi.Pair + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + maxGe.Odd + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + maxGe.Pair + "</td>");
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
                sb.Append("<td class='z_bg_tendency'>" + wan.Lottery + "</td>");
                sb.Append("<td class='z_bg_tendency'>" + "" + "</td>");

                sb.Append(getBgcolor1Start(wan.Big));
                sb.Append(getBgcolor1Start(wan.Small));
                sb.Append(getBgcolor1Start(qian.Big));
                sb.Append(getBgcolor1Start(qian.Small));
                sb.Append(getBgcolor1Start(bai.Big));
                sb.Append(getBgcolor1Start(bai.Small));
                sb.Append(getBgcolor1Start(shi.Big));
                sb.Append(getBgcolor1Start(shi.Small));
                sb.Append(getBgcolor1Start(ge.Big));
                sb.Append(getBgcolor1Start(ge.Small));
                sb.Append(getBgcolor1Start(wan.Odd));
                sb.Append(getBgcolor1Start(wan.Pair));
                sb.Append(getBgcolor1Start(qian.Odd));
                sb.Append(getBgcolor1Start(qian.Pair));
                sb.Append(getBgcolor1Start(bai.Odd));
                sb.Append(getBgcolor1Start(bai.Pair));
                sb.Append(getBgcolor1Start(shi.Odd));
                sb.Append(getBgcolor1Start(shi.Pair));
                sb.Append(getBgcolor1Start(ge.Odd));
                sb.Append(getBgcolor1Start(ge.Pair));

                sb.Append("<td class='z_bg_tendency'>" + wan.Dtime.Substring(6) + "</td>");
                sb.Append(" </tr>");
            }

            sb.Append(" <tr>");
            sb.Append("<td class='z_bg_tendency' colspan='3'>最大遗漏</td>");
            sb.Append("<td class='z_bg_tendency'>0</td>");
            sb.Append("<td class='z_bg_tendency'>" + maxWan.Big + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + maxWan.Small + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + maxQian.Big + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + maxQian.Small + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + maxBai.Big + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + maxBai.Small + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + maxShi.Big + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + maxShi.Small + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + maxGe.Big + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + maxGe.Small + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + maxWan.Odd + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + maxWan.Pair + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + maxQian.Odd + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + maxQian.Pair + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + maxBai.Odd + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + maxBai.Pair + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + maxShi.Odd + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + maxShi.Pair + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + maxGe.Odd + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + maxGe.Pair + "</td>");
            sb.Append("<td class='z_bg_tendency'>" + "" + "</td>");
            sb.Append(" </tr>");
            sb.Append("</tbody>");

            return sb.ToString();
        }

        #endregion

        #region 内部调用私有方法
        /// <summary>
        /// 定位胆对奖号格式化
        /// </summary>
        /// <param name="type"></param>
        /// <param name="lottery"></param>
        /// <returns></returns>
        private string dwdFormateLottery(Tendency1Enum type, string lottery)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<td class='z_bg_tendency'>");
            switch (type)
            {
                case Tendency1Enum.TenThousand:
                    sb.Append("<span style='color:Red;font-weight:bold;'>");
                    sb.Append(lottery.Substring(0, 1));
                    sb.Append("</span>");
                    sb.Append(lottery.Substring(1));
                    break;
                case Tendency1Enum.Thousand:
                    sb.Append(lottery.Substring(0, 2));
                    sb.Append("<span style='color:Red;font-weight:bold;'>");
                    sb.Append(lottery.Substring(2, 1));
                    sb.Append("</span>");
                    sb.Append(lottery.Substring(3));
                    break;
                case Tendency1Enum.Hundred:
                    sb.Append(lottery.Substring(0, 4));
                    sb.Append("<span style='color:Red;font-weight:bold;'>");
                    sb.Append(lottery.Substring(4, 1));
                    sb.Append("</span>");
                    sb.Append(lottery.Substring(5));
                    break;
                case Tendency1Enum.Ten:
                    sb.Append(lottery.Substring(0, 6));
                    sb.Append("<span style='color:Red;font-weight:bold;'>");
                    sb.Append(lottery.Substring(6, 1));
                    sb.Append("</span>");
                    sb.Append(lottery.Substring(7));
                    break;
                case Tendency1Enum.One:
                    sb.Append(lottery.Substring(0, 8));
                    sb.Append("<span style='color:Red;font-weight:bold;'>");
                    sb.Append(lottery.Substring(8, 1));
                    sb.Append("</span>");
                    break;
            }
            sb.Append("</td>>");
            return sb.ToString();
        }

        /// <summary>
        /// 组合二星定位胆
        /// </summary>
        /// <param name="tm1"></param>
        /// <param name="tm2"></param>
        /// <returns></returns>
        private TendencyDwdModel GetTendencyDwdValue(TendencyModel tm1, TendencyModel tm2)
        {
            if (tm1 == null || tm2 == null) return new TendencyDwdModel();

            TendencyDwdModel tmResult = new TendencyDwdModel();
            tmResult.Lottery = tm1.Lottery;
            //@大小
            tmResult.Big = tm1.Big + "|" + tm2.Big;
            tmResult.Small = tm1.Small + "|" + tm2.Small;
            tmResult.BigSmall = tm1.Big + "|" + tm2.Small;
            tmResult.SmallBig = tm1.Small + "|" + tm2.Big;

            //奇偶
            tmResult.Odd = tm1.Odd + "|" + tm2.Odd;
            tmResult.Pair = tm1.Pair + "|" + tm2.Pair;
            tmResult.OddPair = tm1.Odd + "|" + tm2.Pair;
            tmResult.PairOdd = tm1.Pair + "|" + tm2.Odd;

            //012路
            tmResult.No_00 = tm1.No_0 + "|" + tm2.No_0;
            tmResult.No_01 = tm1.No_0 + "|" + tm2.No_1;
            tmResult.No_02 = tm1.No_0 + "|" + tm2.No_2;
            tmResult.No_10 = tm1.No_1 + "|" + tm2.No_0;
            tmResult.No_11 = tm1.No_1 + "|" + tm2.No_1;
            tmResult.No_12 = tm1.No_1 + "|" + tm2.No_2;
            tmResult.No_20 = tm1.No_2 + "|" + tm2.No_0;
            tmResult.No_21 = tm1.No_2 + "|" + tm2.No_1;
            tmResult.No_22 = tm1.No_2 + "|" + tm2.No_2;

            //大中小
            tmResult.Big1Big1 = tm1.Big_1 + "|" + tm2.Big_1;
            tmResult.Big1Mid1 = tm1.Big_1 + "|" + tm2.Mid_1;
            tmResult.Big1Small1 = tm1.Big_1 + "|" + tm2.Small_1;
            tmResult.Mid1Big1 = tm1.Mid_1 + "|" + tm2.Big_1;
            tmResult.Mid1Mid1 = tm1.Mid_1 + "|" + tm2.Mid_1;
            tmResult.Mid1Small1 = tm1.Mid_1 + "|" + tm2.Small_1;
            tmResult.Small1Big1 = tm1.Small_1 + "|" + tm2.Big_1;
            tmResult.Small1Mid1 = tm1.Small_1 + "|" + tm2.Mid_1;
            tmResult.Small1Small1 = tm1.Small_1 + "|" + tm2.Small_1;

            tmResult.Dbl = "-";
            tmResult.Dtime = tm1.Dtime;
            return tmResult;
        }

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

        /// <summary>
        /// 二星当前定位胆开奖样式
        /// </summary>
        /// <param name="strDwd"></param>
        /// <returns></returns>
        private string getBgcolor1Dwd(string strDwd)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<td align='center'");

            if (strDwd == "0|0")
            {
                sb.Append("bgcolor='#B0C4DE'  style='font-size:16px;font-weight:bold;'");
            }
            else
            {
                sb.Append(" class='z_bg_tendency'");
            }
            sb.Append(">" + strDwd + "</td>");
            sb.Append(">");
            return sb.ToString();
        }
        #endregion
    }
}



