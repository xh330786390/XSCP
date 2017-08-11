using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace XSCP.Service.Common
{
    public class TransService : ExecuteServiceCall
    {

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public string Login(string userName, string password)
        {
            var param = new[] { userName, password, userName, password };
            string urlData = string.Format(URL.Login, param);
            return HttpDataRequest.Login(URL.Main + "/login.shtml", urlData);// "AJAXREQUEST=LGForm%3Aj_id3&LGForm=LGForm&txtName=xh168&txtPsw=zxj361226&javax.faces.ViewState=j_id1&un=xh168&pwd=zxj361226&LGForm%3Aj_id4=LGForm%3Aj_id4&");
        }

        /// <summary>
        /// 登出
        /// </summary>
        /// <returns></returns>
        public string Logout()
        {
            return HttpDataRequest.GetData("/main.shtml", RequestMethod.Get, "/login.shtml");// "AJAXREQUEST=LGForm%3Aj_id3&LGForm=LGForm&txtName=xh168&txtPsw=zxj361226&javax.faces.ViewState=j_id1&un=xh168&pwd=zxj361226&LGForm%3Aj_id4=LGForm%3Aj_id4&");
        }

        /// <summary>
        /// 获取开奖号码
        /// </summary>
        /// <param name="type">彩票类型：15 分分彩；17 3D彩</param>
        /// <param name="num">最近记录条数</param>
        /// <returns></returns>
        public string CurrentRecord(string type, string num)
        {
            var param = new[] { type, num };
            string urlData = string.Format(URL.CurrentRecord, param);
            return HttpDataRequest.GetData("/ssccs.shtml", RequestMethod.Post, urlData);
        }


        /// <summary>
        /// 实时检测服务状态
        /// </summary>
        /// <returns></returns>
        public string LotteryService()
        {
            return HttpDataRequest.LotteryService("/main.shtml", RequestMethod.Post, URL.LotteryService);
        }
    }
}
