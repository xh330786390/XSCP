using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using XSCP.Service.Model;
using System.Collections;

namespace XSCP.Service.Controllers
{
    /// <summary>
    /// 控件操作
    /// </summary>
    public class DgvController
    {
        public static List<string> Lt_Type = new List<string>() { "前大", "前定", "前二", "后二", "后定", "后大" };
        public static List<TendencyType> Lt_Names = new List<TendencyType>();

        static DgvController()
        {
            Lt_Names.Add(new TendencyType() { EnName = "", ChName = "" });
            Lt_Names.Add(new TendencyType() { EnName = "Big", ChName = "大大" });
            Lt_Names.Add(new TendencyType() { EnName = "Small", ChName = "小小" });
            Lt_Names.Add(new TendencyType() { EnName = "BigSmall", ChName = "大小" });
            Lt_Names.Add(new TendencyType() { EnName = "SmallBig", ChName = "小大" });
            Lt_Names.Add(new TendencyType() { EnName = "", ChName = "" });
            Lt_Names.Add(new TendencyType() { EnName = "Odd", ChName = "奇奇" });
            Lt_Names.Add(new TendencyType() { EnName = "Pair", ChName = "偶偶" });
            Lt_Names.Add(new TendencyType() { EnName = "OddPair", ChName = "奇偶" });
            Lt_Names.Add(new TendencyType() { EnName = "PairOdd", ChName = "偶奇" });
            Lt_Names.Add(new TendencyType() { EnName = "Dbl", ChName = "重重" });
        }
        /// <summary>
        /// 加Dgv行数
        /// </summary>
        /// <param name="dgv"></param>
        /// <param name="count"></param>
        public static void AddRows(DataGridView dgv, int count)
        {
            dgv.Rows.Clear();
            if (count > 0) dgv.Rows.Add(count);
        }

        /// <summary>
        /// 设置单元格格式
        /// </summary>
        /// <param name="i"></param>
        public static void SetDgvForeColorStyle(DataGridView dgv, int row, int col, Color color, int size)
        {
            dgv.Rows[row].Cells[col].Style.ForeColor = color;
            dgv.Rows[row].Cells[col].Style.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold);
            dgv.Rows[row].Cells[col].Style.Font = new Font("Tahoma", size);
        }

        /// <summary>
        /// 设置单元格格式
        /// </summary>
        /// <param name="i"></param>
        public static void SetDgvBackColorStyle(DataGridView dgv, int row, int col, Color color, int size)
        {
            dgv.Rows[row].Cells[col].Style.BackColor = color;
            dgv.Rows[row].Cells[col].Style.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold);
            dgv.Rows[row].Cells[col].Style.Font = new Font("Tahoma", size);
        }

        public static void SetDgvValue(DataGridView dgv, List<string> lt_Type, int col)
        {
            for (int i = 0; i < Lt_Type.Count; i++)
            {
                dgv[col, i].Value = lt_Type[i];
            }
        }

        /// <summary>
        /// 设置dgv值
        /// </summary>
        /// <param name="dgv"></param>
        /// <param name="tm"></param>
        /// <param name="row"></param>
        public static void SetDgvValue(DataGridView dgv, TendencyModel tm, int row, List<WinLotteryModel> lt)
        {
            dgv[1, row].Value = tm.Big;//大
            dgv[2, row].Value = tm.Small;//小
            dgv[3, row].Value = tm.BigSmall;//大小
            dgv[4, row].Value = tm.SmallBig;//小大
            dgv[5, row].Value = "";
            dgv[6, row].Value = tm.Odd;//奇
            dgv[7, row].Value = tm.Pair;//偶
            dgv[8, row].Value = tm.OddPair;//奇偶
            dgv[9, row].Value = tm.PairOdd;//偶奇
            dgv[10, row].Value = tm.Dbl;//重
            dgv[11, row].Value = "趋势";
            dgv[12, row].Value = "统计";

            if (lt != null)
            {
                int index = 0;
                for (int i = 0; i < lt.Count; i++)
                {
                    string[] ar = getIndex(lt[i]).Split(',');
                    index = Convert.ToInt32(ar[0]);
                    dgv[index, row].ToolTipText = ar[1] + ": " + lt[i].KjLong.ToString();
                }
            }
        }

        private static string getIndex(WinLotteryModel wm)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 1; i < Lt_Names.Count; i++)
            {
                if (wm.UnitName == Lt_Names[i].EnName)
                {
                    sb.Append(i + "," + Lt_Names[i].ChName);
                    break;
                }
            }
            return sb.ToString();
        }


        /// <summary>
        /// 设置dgv值
        /// </summary>
        /// <param name="dgv"></param>
        /// <param name="tm"></param>
        /// <param name="row"></param>
        public static void SetDgvValue(DataGridView dgv, TendencyModel tm, int row)
        {
            dgv[1, row].Value = tm.Big;//大
            dgv[2, row].Value = tm.Small;//小
            dgv[3, row].Value = tm.BigSmall;//大小
            dgv[4, row].Value = tm.SmallBig;//小大
            dgv[5, row].Value = "";

            dgv[6, row].Value = tm.Odd;//奇
            dgv[7, row].Value = tm.Pair;//偶
            dgv[8, row].Value = tm.OddPair;//奇偶
            dgv[9, row].Value = tm.PairOdd;//偶奇
            dgv[10, row].Value = tm.Dbl;//重
            dgv[11, row].Value = "趋势";
            dgv[12, row].Value = "-";
        }

        /// <summary>
        /// 设置dgv值
        /// </summary>
        /// <param name="dgv"></param>
        /// <param name="tm"></param>
        /// <param name="row"></param>
        public static void SetDgvValue(DataGridView dgv, DwdTendencyModel dwd1, DwdTendencyModel dwd2, int row)
        {
            dgv[1, row].Value = dwd1.Big.ToString() +"|"+ dwd2.Big.ToString();//大
            dgv[2, row].Value = dwd1.Small.ToString() + "|" + dwd2.Small.ToString();//小
            dgv[3, row].Value = dwd1.Big.ToString() + "|" + dwd2.Small.ToString();//大小
            dgv[4, row].Value = dwd1.Small.ToString() + "|" + dwd2.Big.ToString();//小大
            dgv[5, row].Value = "";

            dgv[6, row].Value = dwd1.Odd.ToString() + "|" + dwd2.Odd.ToString(); //奇
            dgv[7, row].Value = dwd1.Pair.ToString() + "|" + dwd2.Pair.ToString(); //偶
            dgv[8, row].Value = dwd1.Odd.ToString() + "|" + dwd2.Pair.ToString(); //奇偶
            dgv[9, row].Value = dwd1.Pair.ToString() + "|" + dwd2.Odd.ToString(); //偶奇
            dgv[10, row].Value = "-";//重
            dgv[11, row].Value = "趋势";
            dgv[12, row].Value = "-";

            for (int i = 1; i < 10; i++)
            {
                if (dgv[i, row].Value != null && dgv[i, row].Value.ToString() == "0|0")
                {
                    dgv.Rows[row].Cells[i].Style.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold);
                    dgv.Rows[row].Cells[i].Style.Font = new Font("Tahoma", 13);
                    dgv.Rows[row].Cells[i].Style.BackColor = Color.LightGray;
                }
            }
        }

        #region 设置Dgv单元格样式

        public static void RefreshDgvTencenyColor(DataGridView dgv)
        {
            int value = 0;
            for (int i = 0; i < dgv.Rows.Count; i++)
            {
                for (int j = 3; j < dgv.Columns.Count - 1; j++)
                {
                    if (dgv[j, i].Value != null && !string.IsNullOrEmpty(dgv[j, i].Value.ToString()))
                    {
                        try
                        {
                            value = Convert.ToInt32(dgv[j, i].Value.ToString());
                            if (value >= XscpWarning.Lt_Warnings[0].Value)
                            {
                                Color color = GetWarningColor(value);
                                DgvController.SetDgvBackColorStyle(dgv, i, j, color, 12);
                            }

                            if (value == 0) DgvController.SetDgvBackColorStyle(dgv, i, j, Color.LightGray, 12);
                        }
                        catch
                        {
                        }
                    }
                }
            }
        }

        public static void RefreshDgvColor(DataGridView dgv, int row)
        {
            int value = 0;

            for (int j = 0; j < dgv.Columns.Count - 2; j++)
            {
                if (dgv[j, row].Value != null && !string.IsNullOrEmpty(dgv[j, row].Value.ToString()))
                {
                    try
                    {
                        if (j == 0)
                        {
                            if (row == 2) dgv.Rows[row].Cells[j].Style.BackColor = Color.LightSteelBlue;
                            else if (row == 3) dgv.Rows[row].Cells[j].Style.BackColor = Color.LightBlue;
                            continue;
                        }

                        value = Convert.ToInt32(dgv[j, row].Value.ToString());
                        if (j < dgv.Columns.Count - 3)
                        {
                            dgv.Rows[row].Cells[j].Style.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold);
                            dgv.Rows[row].Cells[j].Style.Font = new Font("Tahoma", 14);
                            if (value >= XscpWarning.Lt_Warnings[0].Value)
                            {
                                Color color = GetWarningColor(value);
                                dgv.Rows[row].Cells[j].Style.BackColor = color;
                            }
                            else if (row == 2) dgv.Rows[row].Cells[j].Style.BackColor = Color.LightSteelBlue;
                            else if (row == 3) dgv.Rows[row].Cells[j].Style.BackColor = Color.LightBlue;
                        }

                        if (value == 0) dgv.Rows[row].Cells[j].Style.BackColor = Color.LightGray;
                    }
                    catch
                    {
                    }
                }
            }
        }

        /// <summary>
        /// 分析值的大小，确定颜色
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Color GetWarningColor(int value)
        {
            Color color = new Color();
            if (value >= XscpWarning.Lt_Warnings[3].Value)
                color = XscpWarning.Lt_Warnings[3].Color;
            else if (value >= XscpWarning.Lt_Warnings[2].Value)
                color = XscpWarning.Lt_Warnings[2].Color;
            else if (value >= XscpWarning.Lt_Warnings[1].Value)
                color = XscpWarning.Lt_Warnings[1].Color;
            else if (value >= XscpWarning.Lt_Warnings[0].Value)
                color = XscpWarning.Lt_Warnings[0].Color;

            return color;
        }
        #endregion
    }
}
