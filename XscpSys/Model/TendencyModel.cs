using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XscpSys.Model
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
        public string Sno
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

        private string dtime;
        /// <summary>
        /// 开奖时间
        /// </summary>
        public string Dtime
        {
            get { return this.dtime; }
            set { this.dtime = value; }
        }
    }

    /// <summary>
    /// 定位胆
    /// </summary>
    public class Tendency1Model : TendencyModel
    {
        private int num0;
        /// <summary>
        /// 数字0
        /// </summary>
        public int Num0
        {
            get { return this.num0; }
            set { this.num0 = value; }
        }

        private int num1;
        /// <summary>
        /// 数字1
        /// </summary>
        public int Num1
        {
            get { return this.num1; }
            set { this.num1 = value; }
        }

        private int num2;
        /// <summary>
        /// 数字2
        /// </summary>
        public int Num2
        {
            get { return this.num2; }
            set { this.num2 = value; }
        }

        private int num3;
        /// <summary>
        /// 数字3
        /// </summary>
        public int Num3
        {
            get { return this.num3; }
            set { this.num3 = value; }
        }

        private int num4;
        /// <summary>
        /// 数字4
        /// </summary>
        public int Num4
        {
            get { return this.num4; }
            set { this.num4 = value; }
        }

        private int num5;
        /// <summary>
        /// 数字5
        /// </summary>
        public int Num5
        {
            get { return this.num5; }
            set { this.num5 = value; }
        }

        private int num6;
        /// <summary>
        /// 数字6
        /// </summary>
        public int Num6
        {
            get { return this.num6; }
            set { this.num6 = value; }
        }

        private int num7;
        /// <summary>
        /// 数字7
        /// </summary>
        public int Num7
        {
            get { return this.num7; }
            set { this.num7 = value; }
        }

        private int num8;
        /// <summary>
        /// 数字8
        /// </summary>
        public int Num8
        {
            get { return this.num8; }
            set { this.num8 = value; }
        }

        private int num9;
        /// <summary>
        /// 数字9
        /// </summary>
        public int Num9
        {
            get { return this.num9; }
            set { this.num9 = value; }
        }
    }

    /// <summary>
    /// 前二、后二
    /// </summary>
    public class Tendency2Model : TendencyModel
    {
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
    }

    /// <summary>
    /// 前三、后三
    /// </summary>
    public class Tendency3Model : TendencyModel
    {
        private int unitThree;
        /// <summary>
        /// 组三
        /// </summary>
        public int UnitThree
        {
            get { return this.unitThree; }
            set { this.unitThree = value; }
        }

        private int unitSex;
        /// <summary>
        /// 组六
        /// </summary>
        public int UnitSex
        {
            get { return this.unitSex; }
            set { this.unitSex = value; }
        }
    }

    /// <summary>
    /// 前四、后四
    /// </summary>
    public class Tendency4Model : TendencyModel
    {

    }

    /// <summary>
    /// 五星
    /// </summary>
    public class Tendency5Model : TendencyModel
    {
        private int fiveStart;
        /// <summary>
        /// 五星
        /// </summary>
        public int FiveStart
        {
            get { return this.fiveStart; }
            set { this.fiveStart = value; }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class TendencyAllModel : TendencyModel
    {
        private int threeBeforeStart;
        /// <summary>
        /// 前组三
        /// </summary>
        public int ThreeBeforeStart
        {
            get { return this.threeBeforeStart; }
            set { this.threeBeforeStart = value; }
        }

        private int sexBeforeStart;
        /// <summary>
        /// 前组六
        /// </summary>
        public int SexBeforeStart
        {
            get { return this.sexBeforeStart; }
            set { this.sexBeforeStart = value; }
        }

        private int threeAfterStart;
        /// <summary>
        /// 后组三
        /// </summary>
        public int ThreeAfterStart
        {
            get { return this.threeAfterStart; }
            set { this.threeAfterStart = value; }
        }

        private int sexAfterStart;
        /// <summary>
        /// 后组六
        /// </summary>
        public int SexAfterStart
        {
            get { return this.sexAfterStart; }
            set { this.sexAfterStart = value; }
        }

        private int fiveStart;
        /// <summary>
        /// 五星
        /// </summary>
        public int FiveStart
        {
            get { return this.fiveStart; }
            set { this.fiveStart = value; }
        }
    }
}
