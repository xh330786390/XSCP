using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using XscpSys.Controllers;
using XscpSys.Model;

namespace XscpSys
{
    public partial class FormTest : Form
    {
        private Lottery lotteryFF;
        public FormTest(Lottery lottery)
        {
            InitializeComponent();
            this.lotteryFF = lottery;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.listBox1.Items.Clear();
            this.listBox2.Items.Clear();

            var vs = this.lotteryFF.Lt_Lotterys.GroupBy(a => new { a.Sno }).Where(l => l.Count() > 1).Select(g => (new { Sno = g.Key.Sno, Count = g.Count() })).OrderByDescending(l => l.Sno).ToList();
            for (int i = 0; i < vs.Count; i++)
                this.listBox1.Items.Add(vs[i].Sno + "期");

            if (this.lotteryFF != null && this.lotteryFF.Lt_Lotterys.Count > 0)
            {
                int maxSno = Convert.ToInt32(this.lotteryFF.Lt_Lotterys[0].Sno);
                int count = this.lotteryFF.Lt_Lotterys.Count;

                int snoInt = 0;
                int index = -1;
                for (int i = maxSno; i >= 1; i--)
                {
                    for (int j = 0; j < count; j++)
                    {
                        snoInt = Convert.ToInt32(this.lotteryFF.Lt_Lotterys[j].Sno);
                        if (snoInt == i) { index = 0; break; }
                        index = j;
                    }

                    if (index == count - 1) this.listBox2.Items.Add(i.ToString().PadLeft(4, '0') + "期");
                }
            }

            if (this.listBox1.Items.Count == 0)
            {
                this.listBox1.Items.Add("无重复奖号");
            }

            if (this.listBox2.Items.Count == 0)
            {
                this.listBox2.Items.Add("无缺省奖号");
            }
        }
    }
}
