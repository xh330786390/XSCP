
namespace XSCP.Common.Model
{
    /// <summary>
    /// 
    /// </summary>
    public class TendencyAllModel : TendencyModel
    {
        /// <summary>
        /// 前组三
        /// </summary>
        public int ThreeBeforeStart{ get; set; }
        /// <summary>
        /// 前组六
        /// </summary>
        public int SexBeforeStart{ get; set; }
        /// <summary>
        /// 后组三
        /// </summary>
        public int ThreeAfterStart{ get; set; }
        /// <summary>
        /// 后组六
        /// </summary>
        public int SexAfterStart{ get; set; }
        /// <summary>
        /// 五星
        /// </summary>
        public int FiveStart{ get; set; }
    }
}
