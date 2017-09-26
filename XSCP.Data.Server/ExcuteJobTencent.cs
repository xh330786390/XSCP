using log4net;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using XSCP.Common;
using XSCP.Common.Extend;
using XSCP.Common.Model;

namespace XSCP.Data.Server
{
    /// <summary>
    /// 执行任务
    /// </summary>
    public class ExcuteJobTencent : IJob
    {
        private static string xmlCookiesPath = AppDomain.CurrentDomain.BaseDirectory + "CookieDataTencent.xml";
        private readonly ILog _logger = LogManager.GetLogger(typeof(ExcuteJobTencent));
        public void Execute(IJobExecutionContext context)
        {
            DateTime currentDate = DateTime.Now;

            var config = MySteel.Common.Helper.XmlHelper.LoadXmlFile<XsConfig>(xmlCookiesPath);
            if (config == null)
            {
                _logger.ErrorFormat("未读取到【Cookie】文件");
                return;
            }
            else if (config.FFCP.Id <= 0 || config.FFCP.Num <= 0)
            {
                _logger.ErrorFormat("没有选择彩种或更新数目");
                return;
            }
            else if (config.Cookies == null || config.Cookies.Length == 0)
            {
                _logger.ErrorFormat("没有Cookie信息");
                return;
            }

            //获取Cookie
            WebHelperMillion.Cookie = WebHelperMillion.GetCookies(config.Cookies[0]);

            //DateTime startTime = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd ") + "07:03:00");
            //TimeSpan timeSpan = startTime - DateTime.Now;
            //if (timeSpan.TotalMinutes > 0 && timeSpan.TotalMinutes <= 120)
            //{
            //    ProtocolInfo pinfo = WebHelperMillion.GetProtocolInfo(config, true);

            //    if (pinfo.Method == ProtocolMethod.Get)
            //    {
            //        //get 通信
            //        WebHelperMillion.Get(pinfo.Url);
            //    }

            //    _logger.InfoFormat("正在停牌");
            //    return;
            //}

            Action action = () =>
            {
                string resultData = null;
                List<string> ltData = new List<string>();
                if (config.Cookies.Length > 0)
                {
                    //获取通信内容
                    ProtocolInfo pinfo = WebHelperTencent.GetProtocolInfo(config);
                    if (pinfo.Method == ProtocolMethod.Get)
                    {
                        //resultData = WebHelperMillion.Get(pinfo.Url);

                        //MillionJsonModel objs = null;
                        //try
                        //{
                        //    objs = Newtonsoft.Json.JsonConvert.DeserializeObject<MillionJsonModel>(resultData);
                        //    if (objs != null)
                        //    {
                        //        ltData.Add(objs.period.Substring(9) + "," + objs.ball);
                        //        ltData.Add(objs.historyBall.period1.issue.Substring(9) + "," + objs.historyBall.period1.code.Replace(' ', ','));
                        //        ltData.Add(objs.historyBall.period2.issue.Substring(9) + "," + objs.historyBall.period2.code.Replace(' ', ','));
                        //        ltData.Add(objs.historyBall.period3.issue.Substring(9) + "," + objs.historyBall.period3.code.Replace(' ', ','));
                        //        ltData.Add(objs.historyBall.period4.issue.Substring(9) + "," + objs.historyBall.period4.code.Replace(' ', ','));
                        //        ltData.Add(objs.historyBall.period5.issue.Substring(9) + "," + objs.historyBall.period5.code.Replace(' ', ','));
                        //    }
                        //}
                        //catch (Exception er)
                        //{
                        //    Console.WriteLine(er.ToString());
                        //    _logger.ErrorFormat("解析数据出错");
                        //    return;
                        //}
                    }
                    else
                    {
                        ///Post 通信
                        resultData = WebHelperTencent.Post(pinfo.Url, pinfo.Data);

                        List<TententWinLottery> objs = null;
                        try
                        {
                            objs = Newtonsoft.Json.JsonConvert.DeserializeObject<List<TententWinLottery>>(resultData);
                        }
                        catch (Exception er)
                        {
                            Console.WriteLine(er.ToString());
                            _logger.ErrorFormat("解析数据出错");
                            return;
                        }

                        if (objs != null && objs.Count > 0)
                        {
                            objs.ForEach(item =>
                            {
                                ltData.Add(item.issue.Substring(9) + "," + item.code);
                            });
                        }
                    }
                }

                if (ltData != null && ltData.Count > 0)
                {
                    if (config.FFCP.Num > ltData.Count)
                    {
                        config.FFCP.Num = ltData.Count;
                    }

                    bool bl = XscpMysqlBLL.Update(CompanyType.Tencent, currentDate, ltData.Take(config.FFCP.Num).ToList());
                    if (bl)
                    {
                        int index = -1;
                        string strLottery = "腾讯-";
                        if (ltData[0].Contains("期"))
                        {
                            index = ltData[0].IndexOf('期');
                            strLottery += "【" + ltData[0].Substring(0, index + 1) + "】-【" + ltData[0].Substring(index + 1) + "】";
                            _logger.InfoFormat(strLottery);
                        }
                        else
                        {
                            index = ltData[0].IndexOf(',');
                            strLottery += "【" + ltData[0].Substring(0, index) + "期】-【" + ltData[0].Substring(index + 1) + "】";
                            _logger.InfoFormat(strLottery);
                        }
                    }
                }
                else
                {
                    _logger.ErrorFormat("未开奖或者没有登录");
                }
            };

            if (action != null)
            {
                action();
            }
        }
    }
}
