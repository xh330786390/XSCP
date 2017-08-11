using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XSCP.Service.Model
{
    /// <summary>
    /// 奖号 未中奖的次数模型
    /// </summary>
    public class TendencyModel
    {
        private int id;
        public int ID
        {
            get { return this.id; }
            set { this.id = value; }
        }

        private string sno;
        /// <summary>
        /// 开奖期数
        /// </summary>
        public string SNO
        {
            get { return this.sno; }
            set { this.sno = value; }
        }

        private string lottery;
        /// <summary>
        /// 开奖期数
        /// </summary>
        public string Lottery
        {
            get { return this.lottery; }
            set { this.lottery = value; }
        }

        private int big;
        /// <summary>
        /// 大数
        /// </summary>
        public int Big
        {
            get { return this.big; }
            set { this.big = value; }
        }

        private int small;
        /// <summary>
        /// 小数
        /// </summary>
        public int Small
        {
            get { return this.small; }
            set { this.small = value; }
        }

        private int odd;
        /// <summary>
        /// 奇数
        /// </summary>
        public int Odd
        {
            get { return this.odd; }
            set { this.odd = value; }
        }

        private int pair;
        /// <summary>
        /// 偶数
        /// </summary>
        public int Pair
        {
            get { return this.pair; }
            set { this.pair = value; }
        }

        private int bigSmall;
        /// <summary>
        /// 大小数
        /// </summary>
        public int BigSmall
        {
            get { return this.bigSmall; }
            set { this.bigSmall = value; }
        }

        private int smallBig;
        /// <summary>
        /// 小大数
        /// </summary>
        public int SmallBig
        {
            get { return this.smallBig; }
            set { this.smallBig = value; }
        }

        private int oddPair;
        /// <summary>
        /// 奇偶数
        /// </summary>
        public int OddPair
        {
            get { return this.oddPair; }
            set { this.oddPair = value; }
        }

        private int pairOdd;
        /// <summary>
        /// 偶奇数
        /// </summary>
        public int PairOdd
        {
            get { return this.pairOdd; }
            set { this.pairOdd = value; }
        }

        private int dbl;
        /// <summary>
        /// 重数
        /// </summary>
        public int Dbl
        {
            get { return this.dbl; }
            set { this.dbl = value; }
        }

        private string dt;
        /// <summary>
        /// 开奖时间
        /// </summary>
        public string Dtime
        {
            get { return this.dt; }
            set { this.dt = value; }
        }
    }
}
