using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace XscpSys.Controllers
{
    public class XscpWarning
    {
        /// <summary>
        /// 预警提醒值
        /// </summary>
        public static List<Waring> Lt_Warnings = new List<Waring>();

        /// <summary>
        /// 定位胆预警提醒值
        /// </summary>
        public static List<int> Lt_DwdWarning = new List<int>() { 3, 5, 7 };
    }

    public class Waring
    {
        public int Value;
        public Color Color;
    }
}
