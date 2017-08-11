using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using XScpStatistics.Common;
using XScpStatistics.Model;

namespace XScpStatistics
{
    public partial class FormTendency : Form
    {
        private string text;
        public FormTendency(string text)
        {
            InitializeComponent();
            this.text = text;
            this.Text = this.text;
        }

        private void FormTendency_Load(object sender, EventArgs e)
        {
            //DataTable dt = DataTableExtension.ToDataTable<TendencyModel>(Tendency.Lt_Tendencys);
            //this.dgv1.DataSource = dt;
            //DgvController.RefreshDgvColor(this.dgv1);
            //return;

            if (Tendency.Lt_Tendencys.Count > 0)
            {
                initDgv1();

                DgvController.RefreshDgvColor(this.dgv1);
            }
        }

        private void initDgv1()
        {
            DgvController.AddRows(this.dgv1, Tendency.Lt_Tendencys.Count);
            TendencyModel tm;
            for (int i = Tendency.Lt_Tendencys.Count - 1, j = 0; i >= 0; i--)
            {
                tm = Tendency.Lt_Tendencys[i];
                this.dgv1[0, j].Value = j + 1;//局数
                this.dgv1[1, j].Value = tm.SNO;//开奖期号
                this.dgv1[2, j].Value = tm.Big;//大
                this.dgv1[3, j].Value = tm.Small;//小
                this.dgv1[4, j].Value = tm.BigSmall;//大小
                this.dgv1[5, j].Value = tm.SmallBig;//小大

                this.dgv1[7, j].Value = tm.Odd;//奇
                this.dgv1[8, j].Value = tm.Pair;//偶
                this.dgv1[9, j].Value = tm.OddPair;//奇偶
                this.dgv1[10, j].Value = tm.PairOdd;//偶奇
                this.dgv1[11, j].Value = tm.Dbl;//重
                this.dgv1[12, j].Value = tm.Dt;//开奖时间
                j++;
            }
        }
    }
}
