using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using XSCP.Common.Model;

namespace XSCP.Forecast
{
    public class ControlHelper
    {
        /// <summary>
        /// 颜色预警
        /// </summary>
        public static Dictionary<Color, int> ColorWarning = new Dictionary<Color, int>();

        static ControlHelper()
        {
            ColorWarning[Color.Lime] = 10;
            ColorWarning[Color.Yellow] = 20;
            ColorWarning[Color.Red] = 25;
            ColorWarning[Color.DarkViolet] = 35;
            //ColorWarning[Color.Lime] = 5;
            //ColorWarning[Color.Yellow] = 7;
            //ColorWarning[Color.Red] = 9;
            //ColorWarning[Color.DarkViolet] = 11;
        }


        public static void SetTendency1Value(DataGridViewRow dgvRow, TendencyModel tm1, TendencyModel tm2)
        {
            if (tm1 == null || tm2 == null) return;

            dgvRow.Cells["Sno"].Value = tm1.Sno;
            dgvRow.Cells["Big"].Value = tm1.Big + "|" + tm2.Big;
            dgvRow.Cells["Small"].Value = tm1.Small + "|" + tm2.Small;
            dgvRow.Cells["BigSmall"].Value = tm1.Big + "|" + tm2.Small;
            dgvRow.Cells["SmallBig"].Value = tm1.Small + "|" + tm2.Big;
            dgvRow.Cells["Odd"].Value = tm1.Odd + "|" + tm2.Odd;
            dgvRow.Cells["Pair"].Value = tm1.Pair + "|" + tm2.Pair;
            dgvRow.Cells["OddPair"].Value = tm1.Odd + "|" + tm2.Pair;
            dgvRow.Cells["PairOdd"].Value = tm1.Pair + "|" + tm2.Odd;
            dgvRow.Cells["Dbl"].Value = "-";
            dgvRow.Cells["Tendency2"].Value = "走势";
            dgvRow.Cells["Statistics"].Value = "-";
        }

        /// <summary>
        /// 设置值
        /// </summary>
        /// <param name="dgvRow"></param>
        /// <param name="tm"></param>
        public static void SetTendency2Value(DataGridViewRow dgvRow, Tendency2Model tm)
        {
            if (tm == null) return;

            dgvRow.Cells["Sno"].Value = tm.Sno;
            dgvRow.Cells["Big"].Value = tm.Big;
            dgvRow.Cells["Small"].Value = tm.Small;
            dgvRow.Cells["BigSmall"].Value = tm.BigSmall;
            dgvRow.Cells["SmallBig"].Value = tm.SmallBig;
            dgvRow.Cells["Odd"].Value = tm.Odd;
            dgvRow.Cells["Pair"].Value = tm.Pair;
            dgvRow.Cells["OddPair"].Value = tm.OddPair;
            dgvRow.Cells["PairOdd"].Value = tm.PairOdd;
            dgvRow.Cells["Dbl"].Value = tm.Dbl;
            dgvRow.Cells["Tendency2"].Value = "走势";
            dgvRow.Cells["Statistics"].Value = "统计";
        }

        /// <summary>
        /// 刷新样式
        /// </summary>
        /// <param name="dgvRow"></param>
        public static void Tendency2WarningStyle(DataGridViewRow dgvRow)
        {
            int fontSize = 12;
            SetCellBackColorStyle(dgvRow.Cells["Big"], fontSize);
            SetCellBackColorStyle(dgvRow.Cells["Small"], fontSize);
            SetCellBackColorStyle(dgvRow.Cells["BigSmall"], fontSize);
            SetCellBackColorStyle(dgvRow.Cells["SmallBig"], fontSize);
            SetCellBackColorStyle(dgvRow.Cells["Odd"], fontSize);
            SetCellBackColorStyle(dgvRow.Cells["Pair"], fontSize);
            SetCellBackColorStyle(dgvRow.Cells["OddPair"], fontSize);
            SetCellBackColorStyle(dgvRow.Cells["PairOdd"], fontSize);
            SetCellBackColorStyle(dgvRow.Cells["Dbl"], fontSize);
        }

        /// <summary>
        /// 设置单元格样式
        /// </summary>
        /// <param name="cellStyle"></param>
        /// <param name="color"></param>
        /// <param name="size"></param>
        public static void SetCellBackColorStyle(DataGridViewCell dgvCell, int size)
        {
            if (dgvCell.Value == null) return;

            int value = int.Parse(dgvCell.Value.ToString());
            if (value < ColorWarning[Color.Lime]) return;

            dgvCell.Style.BackColor = GetWarningColor(value);
            dgvCell.Style.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold);
            dgvCell.Style.Font = new Font("Tahoma", size);
        }

        /// <summary>
        /// 设置单元格样式
        /// </summary>
        /// <param name="dgvCell"></param>
        /// <param name="color"></param>
        public static void SetCellBackColorStyle(DataGridViewCell dgvCell, Color color, int fontSize)
        {
            dgvCell.Style.BackColor = color;
            dgvCell.Style.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold);
            dgvCell.Style.Font = new Font("Tahoma", fontSize);
        }

        /// <summary>
        /// 设置行样式
        /// </summary>
        /// <param name="cellStyle"></param>
        /// <param name="color"></param>
        /// <param name="size"></param>
        public static void SetRowBackColorStyle(DataGridViewRow dgvRow, Color color, int size)
        {
            dgvRow.DefaultCellStyle.BackColor = color;
            dgvRow.DefaultCellStyle.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold);
            dgvRow.DefaultCellStyle.Font = new Font("Tahoma", size);
        }

        /// <summary>
        /// 设置开奖背景色
        /// </summary>
        /// <param name="dgvRow"></param>
        /// <param name="color"></param>
        public static void SetOpenLotteryStyle(DataGridViewRow dgvRow, Color color, int fontSize)
        {
            if (dgvRow.Cells["Big"].Value.ToString() == "0" || dgvRow.Cells["Big"].Value.ToString() == "0|0") SetCellBackColorStyle(dgvRow.Cells["Big"], color, fontSize);
            if (dgvRow.Cells["Small"].Value.ToString() == "0" || dgvRow.Cells["Small"].Value.ToString() == "0|0") SetCellBackColorStyle(dgvRow.Cells["Small"], color, fontSize);
            if (dgvRow.Cells["BigSmall"].Value.ToString() == "0" || dgvRow.Cells["BigSmall"].Value.ToString() == "0|0") SetCellBackColorStyle(dgvRow.Cells["BigSmall"], color, fontSize);
            if (dgvRow.Cells["SmallBig"].Value.ToString() == "0" || dgvRow.Cells["SmallBig"].Value.ToString() == "0|0") SetCellBackColorStyle(dgvRow.Cells["SmallBig"], color, fontSize);
            if (dgvRow.Cells["Odd"].Value.ToString() == "0" || dgvRow.Cells["Odd"].Value.ToString() == "0|0") SetCellBackColorStyle(dgvRow.Cells["Odd"], color, fontSize);
            if (dgvRow.Cells["Pair"].Value.ToString() == "0" || dgvRow.Cells["Pair"].Value.ToString() == "0|0") SetCellBackColorStyle(dgvRow.Cells["Pair"], color, fontSize);
            if (dgvRow.Cells["OddPair"].Value.ToString() == "0" || dgvRow.Cells["OddPair"].Value.ToString() == "0|0") SetCellBackColorStyle(dgvRow.Cells["OddPair"], color, fontSize);
            if (dgvRow.Cells["PairOdd"].Value.ToString() == "0" || dgvRow.Cells["PairOdd"].Value.ToString() == "0|0") SetCellBackColorStyle(dgvRow.Cells["PairOdd"], color, fontSize);
            if (dgvRow.Cells["Dbl"].Value.ToString() == "0" || dgvRow.Cells["Dbl"].Value.ToString() == "0|0") SetCellBackColorStyle(dgvRow.Cells["Dbl"], color, fontSize);
        }

        /// <summary>
        /// 分析值的大小，确定颜色
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Color GetWarningColor(int value)
        {
            Color color = new Color();
            if (value >= ColorWarning[Color.DarkViolet])
            {
                color = Color.DarkViolet;
            }
            else if (value >= ColorWarning[Color.Red])
            {
                color = Color.Red;
            }
            else if (value >= ColorWarning[Color.Yellow])
            {
                color = Color.Yellow;
            }
            else if (value >= ColorWarning[Color.Lime])
            {
                color = Color.Lime;
            }
            return color;
        }
    }
}
