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
    public class ExcuteJob : IJob
    {
        private static string xmlCookiesPath = AppDomain.CurrentDomain.BaseDirectory + "CookieData.xml";
        private readonly ILog _logger = LogManager.GetLogger(typeof(ExcuteJob));
        public void Execute(IJobExecutionContext context)
        {

            //List<string> ltData1 = new List<string>();
            //ltData1.Add("0530,5,2,5,1,8");
            //ltData1.Add("0529,9,4,8,0,7");
            //ltData1.Add("0528,5,0,8,9,0");
            //ltData1.Add("0527,9,9,9,0,4");
            //ltData1.Add("0526,9,2,7,1,8");
            //ltData1.Add("0525,4,6,1,5,7");
            //ltData1.Add("0524,3,6,5,7,0");
            //ltData1.Add("0523,9,7,9,3,3");
            //ltData1.Add("0522,7,2,9,9,8");
            //ltData1.Add("0521,2,8,8,3,7");

            //XscpMysqlBLL.Update(CompanyType.Xscp, DateTime.Parse("2017-09-01"), ltData1);

            ////MysqlHelper.CreateConnection();
            ////return;

            DateTime currentDate = DateTime.Now;
            if (DateTime.Now.Hour <= 7)
            {
                currentDate = currentDate.AddDays(-1);
            }
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

            WebHelper.Cookie = WebHelper.GetCookies(config.Cookies[0]);

            DateTime startTime = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd ") + "08:03:00");
            TimeSpan timeSpan = startTime - DateTime.Now;
            if (timeSpan.TotalMinutes > 0 && timeSpan.TotalMinutes <= 60)
            {
                ProtocolInfo pinfo = WebHelper.GetProtocolInfo(config, true);
                if (pinfo.Method == ProtocolMethod.Post)
                {
                    ///Post 通信
                    WebHelper.Post(pinfo.Url, pinfo.Data);
                }
                _logger.InfoFormat("正在停牌");
                return;
            }

            Action action = () =>
            {
                string resultData = null;
                List<string> ltData = new List<string>();
                if (config.Cookies.Length > 0)
                {
                    //获取通信内容
                    ProtocolInfo pinfo = WebHelper.GetProtocolInfo(config);
                    if (pinfo.Method == ProtocolMethod.Get)
                    {
                        resultData = WebHelper.Get(pinfo.Url);
                        ltData = resultData.GetHtml();
                    }
                    else
                    {
                        ///Post 通信
                        resultData = WebHelper.Post(pinfo.Url, pinfo.Data);

                        XscpDataJsonModel objs = null;
                        try
                        {
                            objs = Newtonsoft.Json.JsonConvert.DeserializeObject<XscpDataJsonModel>(resultData);
                        }
                        catch (Exception er)
                        {
                            Console.WriteLine(er.ToString());
                            _logger.ErrorFormat("解析数据出错");
                            return;
                        }

                        if (objs != null && objs.reslist != null && objs.reslist.Count > 0)
                        {
                            objs.reslist.ForEach(item =>
                            {
                                ltData.Add(item.issue + item.winnumber);
                            });
                        }
                    }
                }

                if (ltData != null && ltData.Count > 0)
                {
                    bool bl = XscpMysqlBLL.Update(CompanyType.Xscp, currentDate, ltData);
                    if (bl)
                    {
                        int index = -1;
                        string strLottery = "新生娱乐-";
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
