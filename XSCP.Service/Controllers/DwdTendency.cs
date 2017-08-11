using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XSCP.Service.Model;

namespace XSCP.Service.Controllers
{
    public class DwdTendency
    {
        /// <summary>
        /// 定位胆
        /// </summary>
        public List<DwdTendencyModel> Lt_Dwds = new List<DwdTendencyModel>();

        /// <summary>
        /// 当前走势
        /// </summary>
        public DwdTendencyModel CurrDwdTendency
        {
            get
            {
                if (this.Lt_Dwds.Count > 0)
                {
                    return GetDwdTendency(this.Lt_Dwds.Count - 1);
                }
                else
                {
                    return new DwdTendencyModel();
                }
            }
        }

        /// <summary>
        /// 获取趋势记录
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public DwdTendencyModel GetDwdTendency(int index)
        {
            if (this.Lt_Dwds.Count > index)
                return this.Lt_Dwds[index];
            return null;
        }

        /// <summary>
        /// 新增趋势记录
        /// </summary>
        /// <param name="lottery"></param>
        public void AddDwdTendency(DwdTendencyModel tm)
        {
            Lt_Dwds.Add(tm);
        }

        /// <summary>
        /// 清除趋势记录
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public void ClearDwdTendencys()
        {
            Lt_Dwds.Clear();
        }
    }
}
