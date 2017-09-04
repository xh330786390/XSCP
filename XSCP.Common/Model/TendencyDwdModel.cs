using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XSCP.Common.Model
{
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
        /// 重重 
        /// </summary>
        public string Dbl { get; set; }
        /// <summary>
        /// 开奖时间
        /// </summary>
        public string Dtime { get; set; }
    }
}
