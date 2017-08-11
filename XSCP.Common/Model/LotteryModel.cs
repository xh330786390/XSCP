using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XSCP.Common.Model
{
    /// <summary>
    /// 开奖模型
    /// </summary>
    public class LotteryModel
    {
        /// <summary>
        /// 主键
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// 年月日
        /// </summary>
        public string Ymd { get; set; }
        /// <summary>
        /// 开奖序号
        /// </summary>
        public string Sno { get; set; }
        /// <summary>
        /// 开奖号码
        /// </summary>
        public string Lottery { get; set; }
        /// <summary>
        /// 万位(第1位数)
        /// </summary>
        public int Num1 { get; set; }
        /// <summary>
        /// 千位(第2位数)
        /// </summary>
        public int Num2 { get; set; }
        /// <summary>
        /// 百位(第3位数)
        /// </summary>
        public int Num3 { get; set; }
        /// <summary>
        /// 十位(第4位数)
        /// </summary>
        public int Num4 { get; set; }
        /// <summary>
        /// 个位(第5位数)
        /// </summary>
        public int Num5 { get; set; }
        /// <summary>
        /// 开奖时间
        /// </summary>
        public string Dtime { get; set; }
    }
}
