using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;

namespace XSCP.Service.BLL
{
    public class Xscp3D
    {
        public static Int64 Id = 0;

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(string result)
        {
            return 0;
            Xscpff ff = new Xscpff();
            //MessageBox.Show(ff.MaxId().ToString());
            //    return;

            //string result = new Common.TransService().CurrentRecord("15", "10");
            //result = Transfer.GetXscpData(result);

            //string[] arr1 = result.Split('\n');
            //for (int i = arr1.Length - 1, k = 1; i >= 0; i--)
            //{
            //    string[] arr2 = arr1[i].Split(',');

            //    try
            //    {
            //        ff.Add(new Model.LotteryModel()
            //        {
            //            ID = ff.MaxId() + 1,
            //            Ymd = DateTime.Now.ToString("yyyyMM"),
            //            Sno = arr2[0],
            //            Num1 = int.Parse(arr2[1]),
            //            Num2 = int.Parse(arr2[2]),
            //            Num3 = int.Parse(arr2[3]),
            //            Num4 = int.Parse(arr2[4]),
            //            Num5 = int.Parse(arr2[5]),
            //            Dtime = Transfer.GetDateTime(DateTime.Now, int.Parse(arr2[0]))
            //        });
            //        k++;
            //    }
            //    catch
            //    {
            //    }
            //}
        }

        /// <summary>
        /// 获取最大ID
        /// </summary>
        public int MaxId()
        {
            try
            {
                return 0;// DbHelperOleDb.GetMaxID("ID", "lottery" + DateTime.Now.ToString("yyyyMM"));
            }
            catch (Exception er)
            {
                throw new Exception(er.ToString());
            }
        }
    }
}
