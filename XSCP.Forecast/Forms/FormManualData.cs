using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using XSCP.Common;
using XSCP.Common.Extend;
using XSCP.Common.Model;

namespace XSCP.Forecast
{
    public delegate void NewLotteryHander();
    public partial class FormManualData : Form
    {
        public NewLotteryHander NewLotteryHander;
        public FormManualData()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 转换 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            string result = this.richTextBox1.Text;
            if (string.IsNullOrEmpty(result)) return;

            List<string> ltData = result.TransLottery();
            if (ltData == null) return;

            StringBuilder sb = new StringBuilder();
            ltData.ForEach(l => { sb.Append(l + "\n"); });
            this.richTextBox2.Text = sb.ToString();

            bool bl = LotteryHelper.Update(this.dtp.Value, ltData);
            if (bl && NewLotteryHander != null)
            {
                NewLotteryHander();
            }
        }

        /// <summary>
        /// 清空
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            this.richTextBox1.Text = "";
            this.richTextBox2.Text = "";
        }

        private void FormData_Load(object sender, EventArgs e)
        {
            this.dtp.Value = DateTime.Now.ToXscpDateTime();
        }
    }
}
