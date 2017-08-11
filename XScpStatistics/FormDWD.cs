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
    public partial class FormDWD : Form
    {
        public FormDWD()
        {
            InitializeComponent();
        }

        private void FormTendency_Load(object sender, EventArgs e)
        {
            if (Tendency.Lt_Tendencys.Count > 0)
            {
                initDgv1();

                DgvController.RefreshDgvColor(this.dgv1);
            }
        }

        private void initDgv1()
        {
            int rows = DwdTendency.GetCount();
            DgvController.AddRows(this.dgv1, rows);

            var vs = DwdTendency.Lt_Dwd.Where(l => l.type == 1).ToList();
            if (FormStatistics.EnumStyle == EnumStyle.后二)
            {
                vs = DwdTendency.Lt_Dwd.Where(l => l.type == FormXscp.xscpStyle.num1).ToList();
                initDigit1(vs);

                vs = DwdTendency.Lt_Dwd.Where(l => l.type == FormXscp.xscpStyle.num1).ToList();
                initDigit2(vs);
            }

            if (FormStatistics.EnumStyle == EnumStyle.前二)
            {
                vs = DwdTendency.Lt_Dwd.Where(l => l.type == FormXscp.xscpStyle.num1).ToList();
                initDigit1(vs);

                vs = DwdTendency.Lt_Dwd.Where(l => l.type == FormXscp.xscpStyle.num1).ToList();
                initDigit2(vs);
            }

            DgvController.RefreshDwdColor(this.dgv1);
        }

        private void initDigit1(List<DwdTendencyModel> lt_dwd)
        {
            DwdTendencyModel dtm;
            for (int i = lt_dwd.Count - 1, j = 0; i >= 0; i--)
            {
                dtm = lt_dwd[i];
                this.dgv1[0, j].Value = j + 1;//大
                this.dgv1[1, j].Value = dtm.Big;//大
                this.dgv1[2, j].Value = dtm.Small;//小
                this.dgv1[3, j].Value = dtm.Odd;//奇
                this.dgv1[4, j].Value = dtm.Pair;//偶
                j++;
            }
        }

        private void initDigit2(List<DwdTendencyModel> lt_dwd)
        {
            DwdTendencyModel dtm;
            for (int i = lt_dwd.Count - 1, j = 0; i >= 0; i--)
            {
                dtm = lt_dwd[i];
                this.dgv1[6, j].Value = dtm.Big;//大
                this.dgv1[7, j].Value = dtm.Small;//小
                this.dgv1[8, j].Value = dtm.Odd;//奇
                this.dgv1[9, j].Value = dtm.Pair;//偶
                j++;
            }
        }
    }
}
