using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XSCP.Common.Model
{
    public class XscpDataJsonModel
    {
        public string msg { get; set; }
        public List<WinLottery> reslist { get; set; }
    }

    public class WinLottery
    {
        public string issue { get; set; }
        public string winnumber { get; set; }
    }
}
