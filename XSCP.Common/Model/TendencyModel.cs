
/*定位胆：各种走势
 *  日期：2017-10-16
 *  作者：xhteng
 */

namespace XSCP.Common.Model
{
    /// <summary>
    /// 奖号未中奖的次数实体
    /// </summary>
    public class TendencyModel
    {
        public int ID { get; set; }
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
        public int Big { get; set; }
        /// <summary>
        /// 小数
        /// </summary>
        public int Small { get; set; }
        /// <summary>
        /// 奇数
        /// </summary>
        public int Odd { get; set; }
        /// <summary>
        /// 偶数
        /// </summary>
        public int Pair { get; set; }
        /// <summary>
        /// 质数
        /// </summary>
        public int Prime { get; set; }
        /// <summary>
        /// 合数
        /// </summary>
        public int Composite { get; set; }
        /// <summary>
        /// 大
        /// </summary>
        public int Big_1 { get; set; }
        /// <summary>
        /// 中
        /// </summary>
        public int Mid_1 { get; set; }
        /// <summary>
        /// 小
        /// </summary>
        public int Small_1 { get; set; }
        /// <summary>
        /// 0路
        /// </summary>
        public int No_0 { get; set; }
        /// <summary>
        /// 1路
        /// </summary>
        public int No_1 { get; set; }
        /// <summary>
        /// 2路
        /// </summary>
        public int No_2 { get; set; }
        /// <summary>
        /// 开奖时间
        /// </summary>
        public string Dtime { get; set; }
    }
}
