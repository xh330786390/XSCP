using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XSCP.Service.Model
{
    public class TendencyUnitModel
    {
        private string sno;
        /// <summary>
        /// 开奖期数
        /// </summary>
        public string Sno
        {
            get { return this.sno; }
            set { this.sno = value; }
        }

        private int num1;
        public int Num1
        {
            get { return this.num1; }
            set { this.num1 = value; }
        }

        private int num2;
        public int Num2
        {
            get { return this.num2; }
            set { this.num2 = value; }
        }

        private int num3;
        public int Num3
        {
            get { return this.num3; }
            set { this.num3 = value; }
        }

        private int num4;
        public int Num4
        {
            get { return this.num4; }
            set { this.num4 = value; }
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
}
