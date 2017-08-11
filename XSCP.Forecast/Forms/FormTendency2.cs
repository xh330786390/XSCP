using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using XSCP.Common;
using XSCP.Common.Model;

namespace XSCP.Forecast
{
    public partial class FormTendency2 : Form
    {
        /// <summary>
        ///前二、后二
        /// </summary>
        private Tendency2Enum type;
        private string date;
        public FormTendency2()
        {
            InitializeComponent();
        }

        public FormTendency2(Tendency2Enum type, string date)
            : this()
        {
            this.type = type;
            this.Text = type == Tendency2Enum.Before ? "前二" : "后二";
            this.Text = "分分彩-" + this.Text;
            this.date = date;
        }

        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGet_Click(object sender, EventArgs e)
        {
            List<Tendency2Model> lt = XscpBLL.QueryTendency2(this.type, this.date, int.Parse(this.textBox3.Text));
            LoadData(lt);
        }

        /// <summary>
        /// 加载数据
        /// </summary>
        /// <param name="lt"></param>
        private void LoadData(List<Tendency2Model> lt)
        {
            for (int i = 0; i < lt.Count; i++)
            {
                lt[i].ID = i + 1;
            }

            this.dgv1.DataSource = lt;

            ///重置标题显示顺序
            resetDisplayIndex();

            if (this.dgv1.Columns.Contains("Ymd"))
            {
                this.dgv1.Columns["Ymd"].Visible = false;
            }

            for (int i = 0; i < this.dgv1.Rows.Count; i++)
            {
                ControlHelper.Tendency2WarningStyle(this.dgv1.Rows[i]);
                ControlHelper.SetOpenLotteryStyle(this.dgv1.Rows[i], Color.LightGray, 12);
            }
        }

        /// <summary>
        /// 重置标题显示顺序
        /// </summary>
        private void resetDisplayIndex()
        {
            this.dgv1.Columns["ID"].DisplayIndex = 0;
            this.dgv1.Columns["Sno"].DisplayIndex = 1;
            this.dgv1.Columns["Lottery"].DisplayIndex = 2;
            this.dgv1.Columns["Big"].DisplayIndex = 3;
            this.dgv1.Columns["Small"].DisplayIndex = 4;
            this.dgv1.Columns["BigSmall"].DisplayIndex = 5;
            this.dgv1.Columns["SmallBig"].DisplayIndex = 6;
            this.dgv1.Columns["Empty"].DisplayIndex = 7;
            this.dgv1.Columns["Odd"].DisplayIndex = 8;
            this.dgv1.Columns["Pair"].DisplayIndex = 9;
            this.dgv1.Columns["OddPair"].DisplayIndex = 10;
            this.dgv1.Columns["PairOdd"].DisplayIndex = 11;
            this.dgv1.Columns["Dbl"].DisplayIndex = 12;
            this.dgv1.Columns["Dtime"].DisplayIndex = 13;
        }

        private void FormTendency2_Load(object sender, EventArgs e)
        {
            this.comboBox1.SelectedIndex = 0;

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int count = 0;

            if (comboBox1.SelectedIndex == 0)
            {
                count = 30;
            }
            else if (comboBox1.SelectedIndex == 1)
            {
                count = 50;
            }
            else if (comboBox1.SelectedIndex == 2)
            {
                count = 100;
            }
            else if (comboBox1.SelectedIndex == 3)
            {
                count = 1380;
            }

            List<Tendency2Model> lt = XscpBLL.QueryTendency2(this.type, this.date, count);
            LoadData(lt);
        }
    }
}
