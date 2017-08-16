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

        public JsonResult GetPieData(int type, string date, int num = 1380)
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
            List<PieData> lt_pie = new List<PieData>();

            PieData big_pie0 = getSomePie(lt_lotterys, 0);
            lt_pie.Add(big_pie0);
            PieData big_pie1 = getSomePie(lt_lotterys, 1);
            lt_pie.Add(big_pie1);
            PieData big_pie2 = getSomePie(lt_lotterys, 2);
            lt_pie.Add(big_pie2);
            PieData big_pie3 = getSomePie(lt_lotterys, 3);
            lt_pie.Add(big_pie3);
            PieData big_pie4 = getSomePie(lt_lotterys, 4);
            lt_pie.Add(big_pie4);

            return Json(lt_pie, JsonRequestBehavior.AllowGet);
        }

        public PieData getSomePie(List<Tendency2Model> lt_lotterys, int type = 0)
        {
            string strType = null;
            switch (type)
            {
                case 0:
                    strType = "大大";
                    break;
                case 1:
                    strType = "小小";
                    break;
                case 2:
                    strType = "大小";
                    break;
                case 3:
                    strType = "小大";
                    break;
                case 4:
                    strType = "重重";
                    break;
            }

            PieData pie = new PieData() { Name = strType };
            List<Tendency2Model> lt_Result = new List<Tendency2Model>();

            List<Tendency2Model> lt_0 = null;

            switch (type)
            {
                case 0:
                    lt_0 = lt_lotterys.Where(l => l.Big == 0).ToList();
                    break;
                case 1:
                    lt_0 = lt_lotterys.Where(l => l.Small == 0).ToList();
                    break;
                case 2:
                    lt_0 = lt_lotterys.Where(l => l.BigSmall == 0).ToList();
                    break;
                case 3:
                    lt_0 = lt_lotterys.Where(l => l.SmallBig == 0).ToList();
                    break;
                case 4:
                    lt_0 = lt_lotterys.Where(l => l.Dbl == 0).ToList();
                    break;
            }


            List<string> lt_sno = new List<string>();
            lt_0.ForEach(item =>
            {
                if (int.Parse(item.Sno) > 0)
                {
                    lt_sno.Add((int.Parse(item.Sno) - 1).ToString().PadLeft(4, '0'));
                }
            });

            lt_sno.ForEach(item =>
            {
                var ll = lt_lotterys.Where(l => l.Sno == item).FirstOrDefault();
                if (ll != null)
                {
                    lt_Result.Add(ll);
                }
            });

            List<PieDataCount> lt_count = null;

            switch (type)
            {
                case 0:
                    lt_count = lt_Result.GroupBy(l => l.Big).Select(l => (new PieDataCount()
                    {
                        Num = l.Key,
                        Count = l.Count()
                    })).ToList();
                    break;
                case 1:
                    lt_count = lt_Result.GroupBy(l => l.Small).Select(l => (new PieDataCount()
                   {
                       Num = l.Key,
                       Count = l.Count()
                   })).ToList();
                    break;
                case 2:
                    lt_count = lt_Result.GroupBy(l => l.BigSmall).Select(l => (new PieDataCount()
                     {
                         Num = l.Key,
                         Count = l.Count()
                     })).ToList();
                    break;
                case 3:
                    lt_count = lt_Result.GroupBy(l => l.SmallBig).Select(l => (new PieDataCount()
                     {
                         Num = l.Key,
                         Count = l.Count()
                     })).ToList();
                    break;
                case 4:
                    lt_count = lt_Result.GroupBy(l => l.Dbl).Select(l => (new PieDataCount()
                    {
                        Num = l.Key,
                        Count = l.Count()
                    })).ToList();
                    break;
            }

            string per = ((lt_0.Count / (double)lt_lotterys.Count) * 100).ToString("f2") + "%";
            pie.Name = pie.Name + "(max:" + lt_count.Max(l => l.Num) + "   p.c:" + lt_0.Count + "-" + per + ")";
            pie.List = lt_count.OrderBy(l => l.Num).ToList();
            return pie;
        }
    }

    public class PieData
    {
        public string Name { get; set; }
        public List<PieDataCount> List { get; set; }
    }

    public class PieDataCount
    {
        public int Num { get; set; }
        public int Count { get; set; }
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
