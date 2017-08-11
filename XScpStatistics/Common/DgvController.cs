using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace XScpStatistics.Common
{
    /// <summary>
    /// 控件操作
    /// </summary>
    public class DgvController
    {
        /// <summary>
        /// 预警提醒值
        /// </summary>
        public static List<int> Lt_Warning = new List<int>();

        /// <summary>
        /// 定位胆预警提醒值
        /// </summary>
        public static List<int> Lt_DwdWarning = new List<int>() { 3, 5, 7 };

        /// <summary>
        /// 预警颜色
        /// </summary>
        public static List<Color> Lt_Color = new List<Color>();

        /// <summary>
        /// 加Dgv行数
        /// </summary>
        /// <param name="dgv"></param>
        /// <param name="count"></param>
        public static void AddRows(DataGridView dgv, int count)
        {
            dgv.Rows.Clear();
            for (int i = 0; i < count; i++)
            {
                dgv.Rows.Add();
            }
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

        #region 设置Dgv单元格样式
        public static void RefreshDgvColor(DataGridView dgv)
        {
            int value = 0;
            for (int i = 0; i < dgv.Rows.Count; i++)
            {
                for (int j = 2; j < dgv.Columns.Count-1; j++)
                {

                    if (dgv[j, i].Value == null || string.IsNullOrEmpty(dgv[j, i].Value.ToString()))
                    {
                        DgvController.SetDgvBackColorStyle(dgv, i, j, Color.DarkGray, 11);
                    }

                    if (dgv[j, i].Value != null && !string.IsNullOrEmpty(dgv[j, i].Value.ToString()))
                    {
                        try
                        {
                            value = Convert.ToInt32(dgv[j, i].Value.ToString());
                            if (value >= Lt_Warning[0])
                            {
                                Color color = GetWarningColor(value);
                                DgvController.SetDgvBackColorStyle(dgv, i, j, color, 11);
                            }

                            if (value == 0) DgvController.SetDgvBackColorStyle(dgv, i, j, Color.LightGray, 11);
                        }
                        catch
                        {
                        }
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
            Color color;
            if (value >= Lt_Warning[2])
                color = Lt_Color[2];//Color.Red;
            else if (value >= Lt_Warning[1])
                color = Lt_Color[1];// Color.Yellow;
            else if (value >= Lt_Warning[0])
                color = Lt_Color[0];// Color.Lime;
            else color = Color.White;
            return color;
        }
        #endregion


        #region 设置Dgv单元格样式
        public static void RefreshDwdColor(DataGridView dgv)
        {
            int value = 0;
            for (int i = 0; i < dgv.Rows.Count; i++)
            {
                for (int j = 1; j < dgv.Columns.Count; j++)
                {

                    if (dgv[j, i].Value == null || string.IsNullOrEmpty(dgv[j, i].Value.ToString()))
                    {
                        DgvController.SetDgvBackColorStyle(dgv, i, j, Color.DarkGray, 11);
                    }

                    if (dgv[j, i].Value != null && !string.IsNullOrEmpty(dgv[j, i].Value.ToString()))
                    {
                        value = Convert.ToInt32(dgv[j, i].Value.ToString());
                        if (value >= Lt_DwdWarning[0])
                        {
                            Color color = GetDwdWarningColor(value);
                            DgvController.SetDgvBackColorStyle(dgv, i, j, color, 11);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 分析值的大小，确定颜色
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Color GetDwdWarningColor(int value)
        {
            Color color;
            if (value >= Lt_DwdWarning[2])
                color = Lt_Color[2];//Color.Red;
            else if (value >= Lt_DwdWarning[1])
                color = Lt_Color[1];// Color.Yellow;
            else if (value >= Lt_DwdWarning[0])
                color = Lt_Color[0];// Color.Lime;
            else color = Color.White;
            return color;
        }
        #endregion
    }
}
