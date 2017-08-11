
namespace XSCP.Common.Model
{
    /// <summary>
    /// 前二、后二
    /// </summary>
    public class Tendency2Model : TendencyModel
    {
        /// <summary>
        /// 大小数
        /// </summary>
        public int BigSmall { get; set; }
        /// <summary>
        /// 小大数
        /// </summary>
        public int SmallBig { get; set; }
        /// <summary>
        /// 奇偶数
        /// </summary>
        public int OddPair { get; set; }
        /// <summary>
        /// 偶奇数
        /// </summary>
        public int PairOdd { get; set; }
        /// <summary>
        /// 重数
        /// </summary>
        public int Dbl { get; set; }
    }
}
