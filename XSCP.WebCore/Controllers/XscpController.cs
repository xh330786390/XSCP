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
            //var lt_lotterys = XscpMysqlBLL.QueryLottery(DateTime.Now.AddDays(-2).ToString("yyyyMMdd"), 5);
            //ViewBag.Lotterys = lt_lotterys;
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
                maxLotery = XscpMysqlBLL.QueryMaxTendency2(Common.Model.Tendency2Enum.Before, date);
                maxLotery.Sno = "前大";
                maxLotery.Dtime = "-";
            }
            else
            {
                maxLotery = XscpMysqlBLL.QueryMaxTendency2(Common.Model.Tendency2Enum.After, date);
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
                var lt_TenThousand = XscpMysqlBLL.QueryTendency1(Tendency1Enum.TenThousand, date, 1);  //万位
                var lt_Thousand = XscpMysqlBLL.QueryTendency1(Tendency1Enum.Thousand, date, 1);        //千位
                if (lt_TenThousand != null && lt_TenThousand.Count > 0)
                {
                    tmDwd = GetTendencyDwdValue(lt_TenThousand[0], lt_Thousand[0]);
                }
                tmDwd.Sno = "前定";


            }
            else
            {
                var lt_Ten = XscpMysqlBLL.QueryTendency1(Tendency1Enum.Ten, date, 1); //十位
                var lt_One = XscpMysqlBLL.QueryTendency1(Tendency1Enum.One, date, 1); //个位
                if (lt_Ten != null && lt_Ten.Count > 0)
                {
                    tmDwd = GetTendencyDwdValue(lt_Ten[0], lt_One[0]);
                }
                tmDwd.Sno = "后定";
            }

            return Json(tmDwd, JsonRequestBehavior.AllowGet);
        }

        public JsonResult PostMaxDwdData(int type, string date)
        {
            TendencyDwdModel tmDwd = new TendencyDwdModel();

            if (type == 1)
            {
                var lt_TenThousand = XscpMysqlBLL.QueryMaxTendency1(Tendency1Enum.TenThousand, date);  //万位
                var lt_Thousand = XscpMysqlBLL.QueryMaxTendency1(Tendency1Enum.Thousand, date);        //千位
                if (lt_TenThousand != null)
                {
                    tmDwd = GetTendencyDwdValue(lt_TenThousand, lt_Thousand);
                }
                tmDwd.Sno = "定大";


            }
            else
            {
                var lt_Ten = XscpMysqlBLL.QueryMaxTendency1(Tendency1Enum.Ten, date); //十位
                var lt_One = XscpMysqlBLL.QueryMaxTendency1(Tendency1Enum.One, date); //个位
                if (lt_Ten != null)
                {
                    tmDwd = GetTendencyDwdValue(lt_Ten, lt_One);
                }
                tmDwd.Sno = "定大  ";
            }

            return Json(tmDwd, JsonRequestBehavior.AllowGet);
        }

        public JsonResult PostLoadData(int type, int num, string date)
        {
            List<Tendency2Model> lt_lotterys = new List<Tendency2Model>();
            if (type == 1)
            {
                lt_lotterys = XscpMysqlBLL.QueryTendency2(Common.Model.Tendency2Enum.Before, date, num);
            }
            else
            {
                lt_lotterys = XscpMysqlBLL.QueryTendency2(Common.Model.Tendency2Enum.After, date, num);
            }
            return Json(lt_lotterys, JsonRequestBehavior.AllowGet);
        }

        public JsonResult PostLoadDigitData(int num, string date)
        {
            List<Tendency1Model> lt_lotterys = new List<Tendency1Model>();
            lt_lotterys = XscpMysqlBLL.QueryTendencyDigit1(date, num);
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
            tmResult.Dbl = "-";
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
                lt_lotterys = XscpMysqlBLL.QueryTendency2(Common.Model.Tendency2Enum.Before, date, num);
            }
            else
            {
                lt_lotterys = XscpMysqlBLL.QueryTendency2(Common.Model.Tendency2Enum.After, date, num);
            }

            List<PieData> lt_pie = new List<PieData>();
            lt_pie.Add(getSomePie(lt_lotterys, EnumLotteryPatter.Big));
            lt_pie.Add(getSomePie(lt_lotterys, EnumLotteryPatter.Small));
            lt_pie.Add(getSomePie(lt_lotterys, EnumLotteryPatter.BigSmall));
            lt_pie.Add(getSomePie(lt_lotterys, EnumLotteryPatter.SmallBig));
            lt_pie.Add(getSomePie(lt_lotterys, EnumLotteryPatter.Dbl));
            lt_pie.Add(getSomePie(lt_lotterys, EnumLotteryPatter.Odd));
            lt_pie.Add(getSomePie(lt_lotterys, EnumLotteryPatter.Pair));
            lt_pie.Add(getSomePie(lt_lotterys, EnumLotteryPatter.OddPair));
            lt_pie.Add(getSomePie(lt_lotterys, EnumLotteryPatter.PairOdd));
            return Json(lt_pie, JsonRequestBehavior.AllowGet);
        }

        private PieData getSomePie(List<Tendency2Model> lt_lotterys, EnumLotteryPatter type = 0)
        {
            string strType = null;
            switch (type)
            {
                case EnumLotteryPatter.Big:
                    strType = "大大";
                    break;
                case EnumLotteryPatter.Small:
                    strType = "小小";
                    break;
                case EnumLotteryPatter.BigSmall:
                    strType = "大小";
                    break;
                case EnumLotteryPatter.SmallBig:
                    strType = "小大";
                    break;
                case EnumLotteryPatter.Dbl:
                    strType = "重重";
                    break;
                case EnumLotteryPatter.Odd:
                    strType = "奇奇";
                    break;
                case EnumLotteryPatter.Pair:
                    strType = "偶偶";
                    break;
                case EnumLotteryPatter.OddPair:
                    strType = "奇偶";
                    break;
                case EnumLotteryPatter.PairOdd:
                    strType = "偶奇";
                    break;
            }

            PieData pie = new PieData() { Name = strType };
            List<Tendency2Model> lt_Result = new List<Tendency2Model>();

            List<Tendency2Model> lt_0 = null;

            switch (type)
            {
                case EnumLotteryPatter.Big:
                    lt_0 = lt_lotterys.Where(l => l.Big == 0).ToList();
                    break;
                case EnumLotteryPatter.Small:
                    lt_0 = lt_lotterys.Where(l => l.Small == 0).ToList();
                    break;
                case EnumLotteryPatter.BigSmall:
                    lt_0 = lt_lotterys.Where(l => l.BigSmall == 0).ToList();
                    break;
                case EnumLotteryPatter.SmallBig:
                    lt_0 = lt_lotterys.Where(l => l.SmallBig == 0).ToList();
                    break;
                case EnumLotteryPatter.Dbl:
                    lt_0 = lt_lotterys.Where(l => l.Dbl == 0).ToList();
                    break;
                case EnumLotteryPatter.Odd:
                    lt_0 = lt_lotterys.Where(l => l.Odd == 0).ToList();
                    break;
                case EnumLotteryPatter.Pair:
                    lt_0 = lt_lotterys.Where(l => l.Pair == 0).ToList();
                    break;
                case EnumLotteryPatter.OddPair:
                    lt_0 = lt_lotterys.Where(l => l.OddPair == 0).ToList();
                    break;
                case EnumLotteryPatter.PairOdd:
                    lt_0 = lt_lotterys.Where(l => l.PairOdd == 0).ToList();
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
                case EnumLotteryPatter.Big:
                    lt_count = lt_Result.GroupBy(l => l.Big).Select(l => (new PieDataCount() { Num = l.Key.ToString(), Count = l.Count() })).ToList();
                    break;
                case EnumLotteryPatter.Small:
                    lt_count = lt_Result.GroupBy(l => l.Small).Select(l => (new PieDataCount() { Num = l.Key.ToString(), Count = l.Count() })).ToList();
                    break;
                case EnumLotteryPatter.BigSmall:
                    lt_count = lt_Result.GroupBy(l => l.BigSmall).Select(l => (new PieDataCount() { Num = l.Key.ToString(), Count = l.Count() })).ToList();
                    break;
                case EnumLotteryPatter.SmallBig:
                    lt_count = lt_Result.GroupBy(l => l.SmallBig).Select(l => (new PieDataCount() { Num = l.Key.ToString(), Count = l.Count() })).ToList();
                    break;
                case EnumLotteryPatter.Dbl:
                    lt_count = lt_Result.GroupBy(l => l.Dbl).Select(l => (new PieDataCount() { Num = l.Key.ToString(), Count = l.Count() })).ToList();
                    break;
                case EnumLotteryPatter.Odd:
                    lt_count = lt_Result.GroupBy(l => l.Odd).Select(l => (new PieDataCount() { Num = l.Key.ToString(), Count = l.Count() })).ToList();
                    break;
                case EnumLotteryPatter.Pair:
                    lt_count = lt_Result.GroupBy(l => l.Pair).Select(l => (new PieDataCount() { Num = l.Key.ToString(), Count = l.Count() })).ToList();
                    break;
                case EnumLotteryPatter.OddPair:
                    lt_count = lt_Result.GroupBy(l => l.OddPair).Select(l => (new PieDataCount() { Num = l.Key.ToString(), Count = l.Count() })).ToList();
                    break;
                case EnumLotteryPatter.PairOdd:
                    lt_count = lt_Result.GroupBy(l => l.PairOdd).Select(l => (new PieDataCount() { Num = l.Key.ToString(), Count = l.Count() })).ToList();
                    break;
            }

            string per = ((lt_0.Count / (double)lt_lotterys.Count) * 100).ToString("f2") + "%";
            pie.Name = pie.Name + "(max:" + lt_count.Max(l => l.Num) + ")";

            pie.SubName = "p.c:" + lt_0.Count + "-" + per;
            pie.List = lt_count.OrderBy(l => l.Num).ToList();
            return pie;
        }


        public JsonResult GetPieRangeData(int type, string startDate = "20170801", string endDate = "20170831")
        {
            List<Tendency2Model> lt_lotterys = new List<Tendency2Model>();
            if (type == 1)
            {
                lt_lotterys = XscpMysqlBLL.QueryTendency2Range(Common.Model.Tendency2Enum.Before, startDate, endDate);
            }
            else
            {
                lt_lotterys = XscpMysqlBLL.QueryTendency2Range(Common.Model.Tendency2Enum.After, startDate, endDate);
            }
            List<PieData> lt_pie = new List<PieData>();
            if (lt_lotterys != null && lt_lotterys.Count > 0)
            {
                lt_pie.Add(getSomeRangePie(lt_lotterys, EnumLotteryPatter.BigSmall));
                lt_pie.Add(getSomeRangePie(lt_lotterys, EnumLotteryPatter.SmallBig));
                lt_pie.Add(getSomeRangePie(lt_lotterys, EnumLotteryPatter.OddPair));
                lt_pie.Add(getSomeRangePie(lt_lotterys, EnumLotteryPatter.PairOdd));
                lt_pie.Add(getSomeRangePie(lt_lotterys, EnumLotteryPatter.Big));
                lt_pie.Add(getSomeRangePie(lt_lotterys, EnumLotteryPatter.Small));
                lt_pie.Add(getSomeRangePie(lt_lotterys, EnumLotteryPatter.Odd));
                lt_pie.Add(getSomeRangePie(lt_lotterys, EnumLotteryPatter.Pair));
                lt_pie.Add(getSomeRangePie(lt_lotterys, EnumLotteryPatter.Dbl));
            }

            return Json(lt_pie, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 线长分布
        /// </summary>
        /// <param name="lt_lotterys"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        private PieData getSomeRangePie(List<Tendency2Model> lt_lotterys, EnumLotteryPatter type = 0)
        {
            string strType = null;
            switch (type)
            {
                case EnumLotteryPatter.Big:
                    strType = "大大";
                    break;
                case EnumLotteryPatter.Small:
                    strType = "小小";
                    break;
                case EnumLotteryPatter.BigSmall:
                    strType = "大小";
                    break;
                case EnumLotteryPatter.SmallBig:
                    strType = "小大";
                    break;
                case EnumLotteryPatter.Dbl:
                    strType = "重重";
                    break;
                case EnumLotteryPatter.Odd:
                    strType = "奇奇";
                    break;
                case EnumLotteryPatter.Pair:
                    strType = "偶偶";
                    break;
                case EnumLotteryPatter.OddPair:
                    strType = "奇偶";
                    break;
                case EnumLotteryPatter.PairOdd:
                    strType = "偶奇";
                    break;
            }

            PieData pie = new PieData() { Name = strType };


            List<Tendency2Model> lt_0 = null;

            switch (type)
            {
                case EnumLotteryPatter.Big:
                    lt_0 = lt_lotterys.Where(l => l.Big == 0).ToList();
                    break;
                case EnumLotteryPatter.Small:
                    lt_0 = lt_lotterys.Where(l => l.Small == 0).ToList();
                    break;
                case EnumLotteryPatter.BigSmall:
                    lt_0 = lt_lotterys.Where(l => l.BigSmall == 0).ToList();
                    break;
                case EnumLotteryPatter.SmallBig:
                    lt_0 = lt_lotterys.Where(l => l.SmallBig == 0).ToList();
                    break;
                case EnumLotteryPatter.Dbl:
                    lt_0 = lt_lotterys.Where(l => l.Dbl == 0).ToList();
                    break;
                case EnumLotteryPatter.Odd:
                    lt_0 = lt_lotterys.Where(l => l.Odd == 0).ToList();
                    break;
                case EnumLotteryPatter.Pair:
                    lt_0 = lt_lotterys.Where(l => l.Pair == 0).ToList();
                    break;
                case EnumLotteryPatter.OddPair:
                    lt_0 = lt_lotterys.Where(l => l.OddPair == 0).ToList();
                    break;
                case EnumLotteryPatter.PairOdd:
                    lt_0 = lt_lotterys.Where(l => l.PairOdd == 0).ToList();
                    break;
            }

            List<Tendency2Model> lt_sno = new List<Tendency2Model>();
            lt_0.ForEach(item =>
            {
                if (int.Parse(item.Sno) > 0)
                {
                    lt_sno.Add(new Tendency2Model() { Ymd = item.Ymd, Sno = (int.Parse(item.Sno) - 1).ToString().PadLeft(4, '0') });
                }
            });

            //效率高
            List<Tendency2Model> lt_Result = (from a in lt_lotterys
                                              join b in lt_sno
                                              on new { a.Ymd, a.Sno } equals new { b.Ymd, b.Sno }
                                              select a).ToList();
            ///效率低
            //lt_sno.ForEach(item =>
            //{
            //    var ll = lt_lotterys.Where(l => l.Ymd == item.Ymd && l.Sno == item.Sno).FirstOrDefault();
            //    if (ll != null)
            //    {
            //        lt_Result.Add(ll);
            //    }
            //});

            List<PieDataCount> lt_count = null;

            switch (type)
            {
                case EnumLotteryPatter.Big:
                    lt_count = lt_Result.GroupBy(l => l.Big).Select(l => (new PieDataCount() { Num = l.Key.ToString(), Count = l.Count() })).ToList();
                    break;
                case EnumLotteryPatter.Small:
                    lt_count = lt_Result.GroupBy(l => l.Small).Select(l => (new PieDataCount() { Num = l.Key.ToString(), Count = l.Count() })).ToList();
                    break;
                case EnumLotteryPatter.BigSmall:
                    lt_count = lt_Result.GroupBy(l => l.BigSmall).Select(l => (new PieDataCount() { Num = l.Key.ToString(), Count = l.Count() })).ToList();
                    break;
                case EnumLotteryPatter.SmallBig:
                    lt_count = lt_Result.GroupBy(l => l.SmallBig).Select(l => (new PieDataCount() { Num = l.Key.ToString(), Count = l.Count() })).ToList();
                    break;
                case EnumLotteryPatter.Dbl:
                    lt_count = lt_Result.GroupBy(l => l.Dbl).Select(l => (new PieDataCount() { Num = l.Key.ToString(), Count = l.Count() })).ToList();
                    break;
                case EnumLotteryPatter.Odd:
                    lt_count = lt_Result.GroupBy(l => l.Odd).Select(l => (new PieDataCount() { Num = l.Key.ToString(), Count = l.Count() })).ToList();
                    break;
                case EnumLotteryPatter.Pair:
                    lt_count = lt_Result.GroupBy(l => l.Pair).Select(l => (new PieDataCount() { Num = l.Key.ToString(), Count = l.Count() })).ToList();
                    break;
                case EnumLotteryPatter.OddPair:
                    lt_count = lt_Result.GroupBy(l => l.OddPair).Select(l => (new PieDataCount() { Num = l.Key.ToString(), Count = l.Count() })).ToList();
                    break;
                case EnumLotteryPatter.PairOdd:
                    lt_count = lt_Result.GroupBy(l => l.PairOdd).Select(l => (new PieDataCount() { Num = l.Key.ToString(), Count = l.Count() })).ToList();
                    break;
            }

            lt_count.ForEach(item =>
            {
                item.Num = item.Num.PadLeft(2, '0') + ":" + item.Count.ToString();
            });

            string per = ((lt_0.Count / (double)lt_lotterys.Count) * 100).ToString("f2") + "%";
            pie.Name = pie.Name + "(max:" + lt_count.Max(l => l.Num) + ")";

            pie.SubName = "p.c: " + lt_0.Count + "/" + lt_lotterys.Count + " = " + per;
            pie.List = lt_count.OrderBy(l => l.Num).ToList();
            return pie;
        }

        public JsonResult GetPieRangeTimeData(int type, string startDate = "20170501", string endDate = "20170701")
        {
            List<Tendency2Model> lt_lotterys = new List<Tendency2Model>();
            if (type == 1)
            {
                lt_lotterys = XscpMysqlBLL.QueryTendency2Range(Common.Model.Tendency2Enum.Before, startDate, endDate);
            }
            else
            {
                lt_lotterys = XscpMysqlBLL.QueryTendency2Range(Common.Model.Tendency2Enum.After, startDate, endDate);
            }

            List<PieData> lt_pie = new List<PieData>();
            lt_pie.Add(getSomeRangeTimePie(lt_lotterys, EnumLotteryPatter.Big));
            lt_pie.Add(getSomeRangeTimePie(lt_lotterys, EnumLotteryPatter.Small));
            lt_pie.Add(getSomeRangeTimePie(lt_lotterys, EnumLotteryPatter.BigSmall));
            lt_pie.Add(getSomeRangeTimePie(lt_lotterys, EnumLotteryPatter.SmallBig));
            lt_pie.Add(getSomeRangeTimePie(lt_lotterys, EnumLotteryPatter.Dbl));
            lt_pie.Add(getSomeRangeTimePie(lt_lotterys, EnumLotteryPatter.Odd));
            lt_pie.Add(getSomeRangeTimePie(lt_lotterys, EnumLotteryPatter.Pair));
            lt_pie.Add(getSomeRangeTimePie(lt_lotterys, EnumLotteryPatter.OddPair));
            lt_pie.Add(getSomeRangeTimePie(lt_lotterys, EnumLotteryPatter.PairOdd));
            return Json(lt_pie, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 时间分布
        /// </summary>
        /// <param name="lt_lotterys"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        private PieData getSomeRangeTimePie(List<Tendency2Model> lt_lotterys, EnumLotteryPatter type = 0)
        {
            string strType = null;
            switch (type)
            {
                case EnumLotteryPatter.Big:
                    strType = "大大";
                    break;
                case EnumLotteryPatter.Small:
                    strType = "小小";
                    break;
                case EnumLotteryPatter.BigSmall:
                    strType = "大小";
                    break;
                case EnumLotteryPatter.SmallBig:
                    strType = "小大";
                    break;
                case EnumLotteryPatter.Dbl:
                    strType = "重重";
                    break;
                case EnumLotteryPatter.Odd:
                    strType = "奇奇";
                    break;
                case EnumLotteryPatter.Pair:
                    strType = "偶偶";
                    break;
                case EnumLotteryPatter.OddPair:
                    strType = "奇偶";
                    break;
                case EnumLotteryPatter.PairOdd:
                    strType = "偶奇";
                    break;
            }

            PieData pie = new PieData() { Name = strType };


            List<Tendency2Model> lt_0 = null;

            switch (type)
            {
                case EnumLotteryPatter.Big:
                    lt_0 = lt_lotterys.Where(l => l.Big == 0).ToList();
                    break;
                case EnumLotteryPatter.Small:
                    lt_0 = lt_lotterys.Where(l => l.Small == 0).ToList();
                    break;
                case EnumLotteryPatter.BigSmall:
                    lt_0 = lt_lotterys.Where(l => l.BigSmall == 0).ToList();
                    break;
                case EnumLotteryPatter.SmallBig:
                    lt_0 = lt_lotterys.Where(l => l.SmallBig == 0).ToList();
                    break;
                case EnumLotteryPatter.Dbl:
                    lt_0 = lt_lotterys.Where(l => l.Dbl == 0).ToList();
                    break;
                case EnumLotteryPatter.Odd:
                    lt_0 = lt_lotterys.Where(l => l.Odd == 0).ToList();
                    break;
                case EnumLotteryPatter.Pair:
                    lt_0 = lt_lotterys.Where(l => l.Pair == 0).ToList();
                    break;
                case EnumLotteryPatter.OddPair:
                    lt_0 = lt_lotterys.Where(l => l.OddPair == 0).ToList();
                    break;
                case EnumLotteryPatter.PairOdd:
                    lt_0 = lt_lotterys.Where(l => l.PairOdd == 0).ToList();
                    break;
            }

            ///05/24 06:34
            lt_0.ForEach(item =>
            {
                if (!string.IsNullOrEmpty(item.Dtime) && item.Dtime.Length == 11)
                    item.Dtime = item.Dtime.Substring(6, 2);//取小时
            });

            List<PieDataCount> lt_count = lt_0.GroupBy(l => l.Dtime).Select(l => (new PieDataCount() { Num = l.Key, Count = l.Count() })).ToList();

            string per = ((lt_0.Count / (double)lt_lotterys.Count) * 100).ToString("f2") + "%";
            pie.Name = pie.Name + "(max:" + lt_count.Max(l => l.Num) + ")";

            pie.SubName = "p.c: " + lt_0.Count + "/" + lt_lotterys.Count + " = " + per;
            pie.List = lt_count.OrderBy(l => l.Num).ToList();
            return pie;
        }


    }

    public class PieData
    {
        public string Name { get; set; }
        public string SubName { get; set; }
        public List<PieDataCount> List { get; set; }
    }

    public class PieDataCount
    {
        public string Num { get; set; }
        public int Count { get; set; }
    }
}
