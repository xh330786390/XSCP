using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace XSCP.Service.Common
{

    public abstract class ExecuteServiceCall
    {
        /// <summary>
        /// ？？
        /// </summary>
        /// <param name="action"></param>
        public void SurroundWithTryCath(Action action)
        {
            try
            {
                action();
            }
            catch (Exception ex)
            {
                throw new WebException("网络请求异常！");
            }
        }
        /// <summary>
        /// ？？？匿名函数
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="func"></param>
        /// <returns></returns>
        public T SurroundWithTryCath<T>(Func<T> func)
        {
            try
            {
                return func();
            }
            catch
            {
                throw new WebException("网络请求异常！");
            }
        }
    }
}
