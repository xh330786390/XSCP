using System;
using System.Collections.Generic;
using System.Windows.Forms;
using XSCP.Common;
using System.Linq;
using XSCP.Common.Extend;

namespace XSCP.Forecast
{
    public partial class FormCheck : Form
    {
        public NewLotteryHander NewLotteryHander;
        private DateTime date = DateTime.Now;
        public FormCheck()
        {
            InitializeComponent();
        }

        public FormCheck(DateTime date)
            : this()
        {
            this.date = date;
        }

        /// <summary>
        /// 倒计秒数
        /// </summary>
        /// <returns></returns>
        private int getMinutes()
        {
            if ((DateTime.Now - this.date).Days > 0) return 1380;

            string nowTime = DateTime.Now.ToString("HH:mm:ss");
            if (nowTime.CompareTo("07:01:00") == -1 || nowTime.CompareTo("08:01:00") == 1)
            {
                DateTime startTime = DateTime.Parse(this.date.ToString("yyyy-MM-dd ") + "08:00:00");
                TimeSpan timeSpan = DateTime.Now - startTime;
                return (int)timeSpan.TotalMinutes;
            }
            return 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.listBox2.Items.Clear();

            int totalMinute = getMinutes();
            if (totalMinute == 0) return;

            List<int> ltData = XscpBLL.CheckLottery(this.date.ToString("yyyyMMdd"));

            if (ltData == null || ltData.Count == 0)
            {
                this.listBox2.Items.Add("无开奖");
                return;
            }

            for (int i = 1; i <= totalMinute; i++)
            {
                if (!ltData.Contains(i))
                {
                    string result = i.ToString().PadLeft(4, '0') + "期";
                    result = result.PadLeft(10, ' ');
                    result = result.PadRight(15, ' ');
                    this.listBox2.Items.Add(result);
                }
            }

            if (this.listBox2.Items.Count == 0)
            {
                this.listBox2.Items.Add("无缺省奖号");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (!WebHelper.Connected)
            {
                MessageBox.Show("请先连接网络");
                return;
            }
            int index = WebHelper.Url.LastIndexOf("=");
            string strSub = WebHelper.Url.Substring(index + 1);
            string url = WebHelper.Url.Replace("num=" + strSub, "num=" + this.txtNum.Text);

            string result = WebHelper.Get(url);
            List<string> ltData = result.TransLottery();
            if (ltData == null)
            {
                WebHelper.Connected = false;
                return;
            }

            bool bl = LotteryHelper.Update(this.date, ltData);
            if (bl && NewLotteryHander != null)
            {
                NewLotteryHander();
            }
        }
    }
}
