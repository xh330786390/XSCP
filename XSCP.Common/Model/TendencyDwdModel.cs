using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XSCP.Common.Model
{
    /// <summary>
    /// 二星定位胆组合实体
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

        #region 012路
        /// <summary>
        /// 00路
        /// </summary>
        public string No_00 { get; set; }
        /// <summary>
        /// 01路
        /// </summary>
        public string No_01 { get; set; }
        /// <summary>
        /// 02路
        /// </summary>
        public string No_02 { get; set; }
        /// <summary>
        /// 10路
        /// </summary>
        public string No_10 { get; set; }
        /// <summary>
        /// 11路
        /// </summary>
        public string No_11 { get; set; }
        /// <summary>
        /// 11路
        /// </summary>
        public string No_12 { get; set; }
        /// <summary>
        /// 20路
        /// </summary>
        public string No_20 { get; set; }
        /// <summary>
        /// 21路
        /// </summary>
        public string No_21 { get; set; }
        /// <summary>
        /// 22路
        /// </summary>
        public string No_22 { get; set; }
        #endregion

        #region 大中小
        /// <summary>
        /// 大大
        /// </summary>
        public string Big1Big1 { get; set; }
        /// <summary>
        /// 大中
        /// </summary>
        public string Big1Mid1 { get; set; }
        /// <summary>
        /// 大小
        /// </summary>
        public string Big1Small1 { get; set; }
        /// <summary>
        /// 中大
        /// </summary>
        public string Mid1Big1 { get; set; }
        /// <summary>
        /// 中中
        /// </summary>
        public string Mid1Mid1 { get; set; }
        /// <summary>
        /// 中小
        /// </summary>
        public string Mid1Small1 { get; set; }
        /// <summary>
        /// 小大
        /// </summary>
        public string Small1Big1 { get; set; }
        /// <summary>
        /// 小中
        /// </summary>
        public string Small1Mid1 { get; set; }
        /// <summary>
        /// 小小
        /// </summary>
        public string Small1Small1 { get; set; }
        #endregion

        /// <summary>
        /// 开奖时间
        /// </summary>
        public string Dtime { get; set; }
    }
}
