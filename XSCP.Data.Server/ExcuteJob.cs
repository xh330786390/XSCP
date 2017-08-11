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
    /// 测试任务
    /// </summary>
    public class ExcuteJob : IJob
    {
        private static string xmlCookiesPath = AppDomain.CurrentDomain.BaseDirectory + "CookieData.xml";
        private readonly ILog _logger = LogManager.GetLogger(typeof(ExcuteJob));
        public void Execute(IJobExecutionContext context)
        {
            DateTime currentDate = DateTime.Now;
            if (DateTime.Now.Hour <= 7)
            {
                currentDate = currentDate.AddDays(-1);
            }
            var result = MySteel.Common.Helper.XmlHelper.LoadXmlFile<XsConfig>(xmlCookiesPath);
            if (result == null)
            {
                _logger.ErrorFormat("未读取到【Cookie】文件");
                return;
            }
            else if (result.FFCP.Id <= 0 || result.FFCP.Num <= 0)
            {
                _logger.ErrorFormat("没有选择彩种或更新数目");
                return;
            }
            else if (result.Cookies == null || result.Cookies.Length == 0)
            {
                _logger.ErrorFormat("没有Cookie信息");
                return;
            }

            Action action = () =>
            {
                //设置Cookie
                WebHelper.Cookie = WebHelper.GetCookies(result.Cookies.ToList());

                ///设置Url
                //WebHelper.Url = WebHelper.GetUrl(result);
                //string resultData = WebHelper.Get(WebHelper.Url);

                string baseUrl = WebHelper.GetBaseUrl(result);
                string data = "flag=UIWinOpenNumberBean&" + WebHelper.GetData(result);
                string resultData = null;

                XscpDataJsonModel objs = null;
                try
                {
                    resultData = WebHelper.Post(baseUrl, data);
                    objs = Newtonsoft.Json.JsonConvert.DeserializeObject<XscpDataJsonModel>(resultData);


                }
                catch (Exception er)
                {
                    Console.WriteLine(er.ToString());
                }


                DateTime startTime = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd ") + "08:01:00");
                TimeSpan timeSpan = startTime - DateTime.Now;
                if (timeSpan.TotalMinutes > 0 && timeSpan.TotalMinutes <= 60)
                {
                    _logger.InfoFormat("正在停牌");
                    return;
                }

                //List<string> ltData = resultData.TransLottery();

                List<string> ltData = new List<string>();
                if (objs != null && objs.reslist != null && objs.reslist.Count > 0)
                {
                    objs.reslist.ForEach(item =>
                    {
                        ltData.Add(item.issue + item.winnumber);
                    });
                }


                if (ltData != null && ltData.Count > 0)
                {
                    bool bl = XscpBLL.Update(currentDate, ltData);
                    if (bl)
                    {
                        _logger.InfoFormat(ltData[0]);
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
