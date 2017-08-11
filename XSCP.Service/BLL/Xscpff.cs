using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;
using XSCP.Service.DAL;
namespace XSCP.Service.BLL
{
    public class Xscpff
    {
        public static Int64 Id = 0;
        private DAL.Xscpff dal = new DAL.Xscpff();

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(string result)
        {
            return 0;
        }

        /// <summary>
        /// 获取最大ID
        /// </summary>
        public int MaxId()
        {
            try
            {
                //return DbHelperOleDb.GetMaxID("ID", "lottery" + DateTime.Now.ToString("yyyyMM"));
            }
            catch (Exception er)
            {
                throw new Exception(er.ToString());
            }
            return 0;
        }
    }
}
