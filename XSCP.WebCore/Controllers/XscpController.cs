using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using XSCP.Common;
using XSCP.Common.Model;
using XSCP.Common.Extend;
using XSCP.Forecast;
using System.Text.RegularExpressions;

namespace XSCP.WebCore.Controllers
{
    public class XscpController : Controller
    {
        //
        // GET: /Xscp/
        public ActionResult Index()
        {
            var lt_lotterys = XscpBLL.QueryLottery(DateTime.Now.AddDays(-2).ToString("yyyyMMdd"), 5);
            ViewBag.Lotterys = lt_lotterys;
            return View();
        }

        /// <summary>
        /// 加载数据
        /// </summary>
        /// <param name="num"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        public ActionResult LoadData(int num, string date)
        {
            return Content("date=" + date + "  num=" + num);
        }

        //public JsonResult PostLoadData(int num, string date)
        //{

        //    var students = new List<Student>
        //    {
        //        new Student(){ID  =1,Name = "张三",Age =20, Birthday = DateTime.Now},
        //        new Student(){ID  =2,Name = "李四",Age =20, Birthday = DateTime.Now}
        //    };

        //    var result = new JsonResult();
        //    result.Data = students;
        //    result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
        //    return result;
        //}

        /// <summary>
        /// 获取最大奖号
        /// </summary>
        /// <param name="type"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        public JsonResult PostLoadMaxData(int type, string date)
        {
            Tendency2Model maxLotery = new Tendency2Model();
            if (type == 1)
            {
                maxLotery = XscpBLL.QueryMaxTendency2(Common.Model.Tendency2Enum.Before, date);
                maxLotery.Sno = "前大";
                maxLotery.Dtime = "-";
            }
            else
            {
                maxLotery = XscpBLL.QueryMaxTendency2(Common.Model.Tendency2Enum.After, date);
                maxLotery.Sno = "后大";
                maxLotery.Dtime = "-";
            }

            return Json(maxLotery, JsonRequestBehavior.AllowGet);
        }

        public JsonResult PostDwdData(int type, string date)
        {
            TendencyDwdModel tmDwd = new TendencyDwdModel();

            if (type == 1)
            {
                var lt_TenThousand = XscpBLL.QueryTendency1(Tendency1Enum.TenThousand, date, 1);  //万位
                var lt_Thousand = XscpBLL.QueryTendency1(Tendency1Enum.Thousand, date, 1);        //千位
                if (lt_TenThousand != null && lt_TenThousand.Count > 0)
                {
                    tmDwd = GetTendencyDwdValue(lt_TenThousand[0], lt_Thousand[0]);
                }
                tmDwd.Sno = "前定";


            }
            else
            {
                var lt_Ten = XscpBLL.QueryTendency1(Tendency1Enum.Ten, date, 1); //十位
                var lt_One = XscpBLL.QueryTendency1(Tendency1Enum.One, date, 1); //个位
                if (lt_Ten != null && lt_Ten.Count > 0)
                {
                    tmDwd = GetTendencyDwdValue(lt_Ten[0], lt_One[0]);
                }
                tmDwd.Sno = "后定";
            }

            return Json(tmDwd, JsonRequestBehavior.AllowGet);
        }

        public JsonResult PostLoadData(int type, int num, string date)
        {
            List<Tendency2Model> lt_lotterys = new List<Tendency2Model>();
            if (type == 1)
            {
                lt_lotterys = XscpBLL.QueryTendency2(Common.Model.Tendency2Enum.Before, date, num);
            }
            else
            {
                lt_lotterys = XscpBLL.QueryTendency2(Common.Model.Tendency2Enum.After, date, num);
            }
            return Json(lt_lotterys, JsonRequestBehavior.AllowGet);
        }

        public static TendencyDwdModel GetTendencyDwdValue(TendencyModel tm1, TendencyModel tm2)
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
            tmResult.Dtime = tm1.Dtime;
            return tmResult;
        }

        public ActionResult HightCharts()
        {
            ViewBag.Name = "hah";
            return View();
        }

        public JsonResult GetDwdData(int type, string date, int num = 1380)
        {
            //List<TendencyModel> lt_tm = new List<TendencyModel>();
            //Random random = new Random();
            //for (int i = 1; i < 1381; i++)
            //{
            //    lt_tm.Add(new TendencyModel()
            //    {
            //        Sno = i.ToString().PadLeft(4, '0'),
            //        Big = random.Next(0, 25)
            //    });
            //}

            List<Tendency2Model> lt_lotterys = new List<Tendency2Model>();
            if (type == 1)
            {
                lt_lotterys = XscpBLL.QueryTendency2(Common.Model.Tendency2Enum.Before, date, num);
            }
            else
            {
                lt_lotterys = XscpBLL.QueryTendency2(Common.Model.Tendency2Enum.After, date, num);
            }
            lt_lotterys = lt_lotterys.OrderBy(l => l.Sno).ToList();
            return Json(lt_lotterys, JsonRequestBehavior.AllowGet);
        }
    }

    /// <summary>
    /// 定位胆
    /// </summary>
    public class TendencyDwdModel
    {
        /// <summary>
        /// 年月日
        /// </summary>
        public string Ymd { get; set; }
        /// <summary>
        /// 开奖期数
        /// </summary>
        public string Sno { get; set; }
        /// <summary>
        /// 开奖期数
        /// </summary>
        public string Lottery { get; set; }
        /// <summary>
        /// 大数
        /// </summary>
        public string Big { get; set; }
        /// <summary>
        /// 小数
        /// </summary>
        public string Small { get; set; }
        /// <summary>
        /// 奇数
        /// </summary>
        public string Odd { get; set; }
        /// <summary>
        /// 偶数
        /// </summary>
        public string Pair { get; set; }

        /// <summary>
        /// 大小数
        /// </summary>
        public string BigSmall { get; set; }
        /// <summary>
        /// 小大数
        /// </summary>
        public string SmallBig { get; set; }
        /// <summary>
        /// 奇偶数
        /// </summary>
        public string OddPair { get; set; }
        /// <summary>
        /// 偶奇数
        /// </summary>
        public string PairOdd { get; set; }
        /// <summary>
        /// 开奖时间
        /// </summary>
        public string Dtime { get; set; }
    }
}
