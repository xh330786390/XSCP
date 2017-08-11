using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using   XSCP.Common.Model;

namespace   XSCP.Data.Controllers
{
    public class Tendency<T>
    {
        /// <summary>
        /// 记录有多少次未中奖了(趋势记录)
        /// </summary>
        public List<T> Lt_Tendencys = new List<T>();

        /// <summary>
        /// 当前走势
        /// </summary>
        public T CurrTendency
        {
            get
            {
                if (this.Lt_Tendencys.Count > 0)
                {
                    return GetTendency(this.Lt_Tendencys.Count - 1);
                }
                else
                {
                    return Activator.CreateInstance<T>();
                }
            }
        }

        /// <summary>
        /// 新增趋势记录
        /// </summary>
        /// <param name="lottery"></param>
        public void AddTendency(T t)
        {
            this.Lt_Tendencys.Add(t);
        }

        /// <summary>
        /// 获取趋势记录
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public T GetTendency(int index)
        {
            if (this.Lt_Tendencys.Count > index)
                return this.Lt_Tendencys[index];
            return default(T);
        }

        /// <summary>
        /// 清除趋势记录
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public void ClearTendencys()
        {
            this.Lt_Tendencys.Clear();
        }
    }
}
