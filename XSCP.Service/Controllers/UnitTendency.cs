using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XSCP.Service.Model;

namespace XSCP.Service.Controllers
{
    public class UnitTendency
    {
        public static List<TendencyType> Lt_BigSmallUnits = new List<TendencyType>();
        public static List<TendencyType> Lt_OddPairUnits = new List<TendencyType>();

        static UnitTendency()
        {
            Lt_OddPairUnits.Add(new TendencyType() { EnName = "Odd", ChName = "奇奇" });
            Lt_OddPairUnits.Add(new TendencyType() { EnName = "OddPair", ChName = "奇偶" });
            Lt_OddPairUnits.Add(new TendencyType() { EnName = "PairOdd", ChName = "偶奇" });
            Lt_OddPairUnits.Add(new TendencyType() { EnName = "Pair", ChName = "偶偶" });

            Lt_BigSmallUnits.Add(new TendencyType() { EnName = "Big", ChName = "大大" }); ;
            Lt_BigSmallUnits.Add(new TendencyType() { EnName = "BigSmall", ChName = "大小" });
            Lt_BigSmallUnits.Add(new TendencyType() { EnName = "SmallBig", ChName = "小大" });
            Lt_BigSmallUnits.Add(new TendencyType() { EnName = "Small", ChName = "小小" });
        }
    }
}
