using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;
using XSCP.Service.Dbole;

namespace XSCP.Service.DAL
{
    public class Xscpff
    {
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Model.LotteryModel model)
        {
            string sqlString = "insert into lottery" + DateTime.Now.ToString("yyyyMM") + " (ID,ymd,sno,num1,num2,num3,num4,num5,dtime) values (" +
                model.ID + ",'"
                + model.Ymd + "','"
                + model.Sno + "',"
                + model.Num1 + ","
                + model.Num2 + ","
                + model.Num3 + ","
                + model.Num4 + ","
                + model.Num5 + ",'"
                + model.Dtime + "')";
            try
            {
                return DbHelperOleDb.ExecuteSql(sqlString);
            }
            catch (Exception er)
            {
                throw new Exception(er.ToString());
            }
        }

        /// <summary>
        /// 获取最大ID
        /// </summary>
        public int MaxId()
        {
            try
            {
                return DbHelperOleDb.GetMaxID("ID", "lottery" + DateTime.Now.ToString("yyyyMM"));
            }
            catch (Exception er)
            {
                throw new Exception(er.ToString());
            }
        }
    }
}
