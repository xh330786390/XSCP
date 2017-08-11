using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XScpStatistics.Model
{
    /// <summary>
    /// 预测模型
    /// </summary>
    public class ForecastModel
    {
        public string UnitName;//组合名称
        public int Sum;//和
        public int Diff;//差值
        public int SumDiff;//和差
        public int Current;//当前走势
    }
}
