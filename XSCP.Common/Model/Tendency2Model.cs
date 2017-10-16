
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

        #region 012路
        /// <summary>
        /// 00路
        /// </summary>
        public int No_00 { get; set; }
        /// <summary>
        /// 01路
        /// </summary>
        public int No_01 { get; set; }
        /// <summary>
        /// 02路
        /// </summary>
        public int No_02 { get; set; }
        /// <summary>
        /// 10路
        /// </summary>
        public int No_10 { get; set; }
        /// <summary>
        /// 11路
        /// </summary>
        public int No_11 { get; set; }
        /// <summary>
        /// 11路
        /// </summary>
        public int No_12 { get; set; }
        /// <summary>
        /// 20路
        /// </summary>
        public int No_20 { get; set; }
        /// <summary>
        /// 21路
        /// </summary>
        public int No_21 { get; set; }
        /// <summary>
        /// 22路
        /// </summary>
        public int No_22 { get; set; }
        #endregion

        #region 大中小
        /// <summary>
        /// 大大
        /// </summary>
        public int Big1Big1 { get; set; }
        /// <summary>
        /// 大中
        /// </summary>
        public int Big1Mid1 { get; set; }
        /// <summary>
        /// 大小
        /// </summary>
        public int Big1Small1 { get; set; }
        /// <summary>
        /// 中大
        /// </summary>
        public int Mid1Big1 { get; set; }
        /// <summary>
        /// 中中
        /// </summary>
        public int Mid1Mid1 { get; set; }
        /// <summary>
        /// 中小
        /// </summary>
        public int Mid1Small1 { get; set; }
        /// <summary>
        /// 小大
        /// </summary>
        public int Small1Big1 { get; set; }
        /// <summary>
        /// 小中
        /// </summary>
        public int Small1Mid1 { get; set; }
        /// <summary>
        /// 小小
        /// </summary>
        public int Small1Small1 { get; set; }
        #endregion

        #region 质合数
        /// <summary>
        /// 质质数
        /// </summary>
        public int PrimePrime { get; set; }
        /// <summary>
        /// 质合数
        /// </summary>
        public int PrimeComposite { get; set; }
        /// <summary>
        /// 合质数
        /// </summary>
        public int CompositePrime { get; set; }
        /// <summary>
        /// 合合数
        /// </summary>
        public int CompositeComposite { get; set; }
        #endregion
    }
}
