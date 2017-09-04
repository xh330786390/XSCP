using MySteel.Common.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XSCP.Common
{
    public class ConstParam
    {
        private static string Dbsource = AppSettingsHelper.GetStringValue("Dbsource");

        /// <summary>
        /// 数据库类型
        /// </summary>
        public static DbType DbType
        {
            get
            {
                DbType DbType = DbType.SqlLite;
                if (Dbsource == "mysql")
                {
                    DbType = Common.DbType.MySql;

                }
                else
                {
                    DbType = Common.DbType.SqlLite;
                }
                return DbType;
            }
        }
    }

    public enum DbType
    {
        SqlLite = 0,
        MySql = 1
    }
}
