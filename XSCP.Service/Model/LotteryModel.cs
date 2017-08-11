using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XSCP.Service.Model
{
    /// <summary>
    /// 开奖模型
    /// </summary>
    public class LotteryModel
    {
        public int ID;//主键
        public string Ymd;//年月日
        public string Sno;//开奖序号
        public string Lottery;//开奖号码
        public int Num1;//万位(第1位数)
        public int Num2;//千位(第2位数)
        public int Num3;//百位(第3位数)
        public int Num4;//十位(第4位数)
        public int Num5;//个位(第5位数)
        public string Dtime;//开奖时间
    }
}
