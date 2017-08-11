using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XSCP.Service.Model
{
    public class TendencyType
    {
        private string enName;
        public string EnName
        {
            get { return this.enName; }
            set { this.enName = value; }
        }

        private string chName;
        public string ChName
        {
            get { return this.chName; }
            set { this.chName = value; }
        }
    }
}
