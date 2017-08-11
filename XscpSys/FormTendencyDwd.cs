using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using XscpSys.Model;
using XscpSys.Controllers;
using System.Reflection;

namespace XscpSys
{
    public partial class FormTendencyDwd : Form
    {
        private string text;
        public List<Tendency1Model> DwdTendency1;
        public List<Tendency1Model> DwdTendency2;

        public FormTendencyDwd(string text)
        {
            InitializeComponent();
            this.text = text;
            this.Text = this.text;
        }

        private void FormTendency_Load(object sender, EventArgs e)
        {
            initDgv1(null);
        }

        private void initDgv1(List<TendencyModel> ltt)
        {
            DgvController.AddRows(this.dgv1, DwdTendency1.Count);

            List<Tendency1Model> Lt_Dwds1 = DwdTendency1.OrderByDescending(l => l.Sno).ToList();
            List<Tendency1Model> Lt_Dwds2 = DwdTendency2.OrderByDescending(l => l.Sno).ToList();

            Tendency1Model tm1;
            Tendency1Model tm2;

            for (int row = Lt_Dwds1.Count - 1, j = 0; row >= 0; row--)
            {
                tm1 = Lt_Dwds1[row];
                tm2 = Lt_Dwds2[row];

                dgv1[0, j].Value = j + 1;//局数
                dgv1[1, row].Value = tm1.Sno;
                dgv1[2, row].Value = tm1.Lottery;
                dgv1[3, row].Value = tm1.Big.ToString() + "|" + tm2.Big.ToString();//大
                dgv1[4, row].Value = tm1.Small.ToString() + "|" + tm2.Small.ToString();//小
                dgv1[5, row].Value = tm1.Big.ToString() + "|" + tm2.Small.ToString();//大小
                dgv1[6, row].Value = tm1.Small.ToString() + "|" + tm2.Big.ToString();//小大
                dgv1[7, row].Value = "";
                dgv1[8, row].Value = tm1.Odd.ToString() + "|" + tm2.Odd.ToString(); //奇
                dgv1[9, row].Value = tm1.Pair.ToString() + "|" + tm2.Pair.ToString(); //偶
                dgv1[10, row].Value = tm1.Odd.ToString() + "|" + tm2.Pair.ToString(); //奇偶
                dgv1[11, row].Value = tm1.Pair.ToString() + "|" + tm2.Odd.ToString(); //偶奇
                dgv1[12, row].Value = tm1.Dtime;
                for (int i = 3; i < 12; i++)
                {
                    if (i == 7) continue;
                    if (dgv1[i, row].Value != null && dgv1[i, row].Value.ToString() == "0|0")
                    {
                        dgv1.Rows[row].Cells[i].Style.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold);
                        dgv1.Rows[row].Cells[i].Style.Font = new Font("Tahoma", 13);
                        dgv1.Rows[row].Cells[i].Style.BackColor = Color.LightGray;
                    }
                }
                j++;
            }
        }
    }
}
