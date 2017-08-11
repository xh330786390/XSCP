
namespace XSCP.Common.Model
{
    /// <summary>
    /// 奖号未中奖的次数模型
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
        /// 开奖时间
        /// </summary>
        public string Dtime { get; set; }
    }
}
