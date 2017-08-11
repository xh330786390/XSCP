using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace FormXscp
{
    public class Common
    {
        /// <summary>
        /// 设置单元格格式
        /// </summary>
        /// <param name="i"></param>
        public static void SetDgvStyle(DataGridView dgv, int row, int col, Color color, int size)
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

        /// <summary>
        /// 设置单元格格式
        /// </summary>
        /// <param name="i"></param>
        public static void SetDgvBackColorStyle1(DataGridView dgv, int row, int col, Color color, int size)
        {
            dgv.Rows[row].Cells[col].Style.BackColor = color;
            //dgv.Rows[row].Cells[col].Style.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold);
            dgv.Rows[row].Cells[col].Style.Font = new Font("Tahoma", 9);
        }
    }
}
