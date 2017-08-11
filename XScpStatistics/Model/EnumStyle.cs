using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XScpStatistics.Model
{
    /// <summary>
    /// 彩票类型
    /// </summary>
    public enum EnumXscpType
    {
        分分彩 = 1,
        两分彩 = 2,
        D3彩 = 3
    }

    /// <summary>
    /// 复式类型
    /// </summary>
    public enum EnumStyle
    {
        后二 = 1,
        前二 = 2
    }

    /// <summary>
    /// 位数
    /// </summary>
    public class XscpStyle
    {
        public int num1;
        public int num2;
    }
}
