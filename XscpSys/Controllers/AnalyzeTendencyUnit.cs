using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using XscpSys.Model;


namespace XscpSys.Controllers
{
    public class AnalyzeTendencyUnit
    {
        public List<TendencyUnitModel> Lt_TendencyUnits = new List<TendencyUnitModel>();

        private Type type = typeof(Tendency2Model);
        private static List<TendencyType> Lt_BigSmalls = new List<TendencyType>();
        private static List<TendencyType> Lt_OddPairs = new List<TendencyType>();
        static AnalyzeTendencyUnit()
        {
            Lt_BigSmalls.Add(new TendencyType() { EnName = "Big", ChName = "大大" });
            Lt_BigSmalls.Add(new TendencyType() { EnName = "Small", ChName = "小小" });
            Lt_BigSmalls.Add(new TendencyType() { EnName = "BigSmall", ChName = "大小" });
            Lt_BigSmalls.Add(new TendencyType() { EnName = "SmallBig", ChName = "小大" });

            Lt_OddPairs.Add(new TendencyType() { EnName = "Odd", ChName = "奇奇" });
            Lt_OddPairs.Add(new TendencyType() { EnName = "Pair", ChName = "偶偶" });
            Lt_OddPairs.Add(new TendencyType() { EnName = "OddPair", ChName = "奇偶" });
            Lt_OddPairs.Add(new TendencyType() { EnName = "PairOdd", ChName = "偶奇" });
        }

        public void SetTendencyUnits(List<Tendency2Model> lt, EnumNumberType numberType, EnumSelectType selectType)
        {
            Lt_TendencyUnits.Clear();
            if (selectType == EnumSelectType.选1)
            {
                TendencyUnits选1(lt, numberType);
            }
            else if (selectType == EnumSelectType.选2)
            {

            }
            else if (selectType == EnumSelectType.选3)
            {
            }
        }

        private void TendencyUnits选1(List<Tendency2Model> lt, EnumNumberType numberType)
        {
            if (numberType == EnumNumberType.BigSmall)
            {
                getTendencyUnit(lt, Lt_BigSmalls);
            }
            else if (numberType == EnumNumberType.OddPair)
            {
                getTendencyUnit(lt, Lt_OddPairs);
            }
        }

        private void getTendencyUnit(List<Tendency2Model> lt, List<TendencyType> lt_names)
        {
            TendencyUnitModel tum;
            List<FieldValueModel> lt_fields;
            TendencyModel curfm;
            TendencyModel nextfm;
            PropertyInfo propertyInfo;
            PropertyInfo fieldInfo1;

            int value = -1;
            for (int i = 0; i < lt.Count; i++)
            {
                curfm = lt[i];
                tum = new TendencyUnitModel();
                lt_fields = new List<FieldValueModel>();
                for (int j = 0; j < lt_names.Count; j++)
                {
                    lt_fields.Add(new FieldValueModel() { FieldName = lt_names[j].EnName, FieldValue = Convert.ToInt16(Reflection.GetPropertyValue(type, curfm, lt_names[j].EnName)) });
                }

                var vs = lt_fields.OrderBy(l => l.FieldValue).ToList();

                for (int k = 0; k < lt_fields.Count; k++)
                {
                    fieldInfo1 = Reflection.GetPropertyInfo(typeof(TendencyUnitModel), "Num" + (k + 1).ToString());
                    propertyInfo = Reflection.GetPropertyInfo(type, vs[k].FieldName);
                    for (int l = i + 1; l < lt.Count; l++)
                    {
                        nextfm = lt[l];
                        value = (int)Reflection.GetPropertyValue(type, nextfm, vs[k].FieldName);
                        if (value == 0)
                        {
                            fieldInfo1.SetValue(tum, l - i, null);
                            break;
                        }
                    }
                }
                tum.Sno = lt[i].Sno;
                tum.Dtime = lt[i].Dtime;
                Lt_TendencyUnits.Add(tum);
            }
        }

        #region 组合
        public int GetUnitValue(List<Tendency2Model> lt, int index, string propertyName1, string propertyName2)
        {
            Tendency2Model tm;
            int pre = lt.Count;
            for (int i = index; i < lt.Count; i++)
            {
                tm = lt[i];
                if (isWinLoterry(tm, propertyName1, propertyName2))
                {
                    pre = i;
                    break;
                }
            }
            return pre - index;
        }

        /// <summary>
        /// 获取属性值
        /// </summary>
        /// <param name="tm"></param>
        /// <param name="fieldName"></param>
        /// <returns></returns>
        private bool isWinLoterry(Tendency2Model tm, string propertyName1, string propertyName2)
        {
            int value1 = (int)Reflection.GetPropertyValue(type, tm, propertyName1);
            int value2 = (int)Reflection.GetPropertyValue(type, tm, propertyName2);

            if (value1 == 0 || value2 == 0)
                return true;
            else
                return false;
        }
        #endregion

    }

    public class AnalyzeTendencyUnit<T>
    {
        private Type type = typeof(T);
        #region 组合
        public int GetUnitValue(List<T> lt, int index, string propertyName1, string propertyName2)
        {
            T tm;
            int pre = lt.Count;
            for (int i = index; i < lt.Count; i++)
            {
                tm = lt[i];
                if (isWinLoterry(tm, propertyName1, propertyName2))
                {
                    pre = i;
                    break;
                }
            }
            return pre - index;
        }

        /// <summary>
        /// 获取属性值
        /// </summary>
        /// <param name="tm"></param>
        /// <param name="fieldName"></param>
        /// <returns></returns>
        private bool isWinLoterry(T tm, string propertyName1, string propertyName2)
        {
            int value1 = (int)Reflection.GetPropertyValue(type, tm, propertyName1);
            int value2 = (int)Reflection.GetPropertyValue(type, tm, propertyName2);

            if (value1 == 0 || value2 == 0)
                return true;
            else
                return false;
        }
        #endregion
    }
}
