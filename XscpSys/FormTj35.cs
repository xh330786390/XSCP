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


namespace XscpSys
{
    public partial class FormTj35 : Form
    {
        #region 字段定义
        private string text;
        public Tendency<TendencyAllModel> Tendency;
        private WinLottery<TendencyAllModel> winLottery = new WinLottery<TendencyAllModel>();
        private static List<TendencyType> lt_Tt = new List<TendencyType>();
        private static List<TendencyType> lt_TtUnits = new List<TendencyType>();
        #endregion

        static FormTj35()
        {
            lt_Tt.Add(new TendencyType() { EnName = "FiveStart", ChName = "五星" });
            lt_Tt.Add(new TendencyType() { EnName = "ThreeAfterStart", ChName = "后三" });
            lt_Tt.Add(new TendencyType() { EnName = "ThreeBeforeStart", ChName = "前三" });
            lt_Tt.Add(new TendencyType() { EnName = "All", ChName = "所有" });
        }

        public FormTj35(string text)
        {
            InitializeComponent();
            this.text = text;
            this.Text = this.text;
        }

        private void FormTendency_Load(object sender, EventArgs e)
        {
            DataTable dt = DataTableExtension.ToDataTable<TendencyType>(lt_Tt);
            this.comboBox1.DataSource = dt;
            this.comboBox1.ValueMember = "EnName";
            this.comboBox1.DisplayMember = "ChName";

            initUnit();
        }

        #region 选前三、后三
        private List<TendencyUnitModel> Lt_Units = new List<TendencyUnitModel>();
        private void initUnit()
        {
            analyzeTendencyUnit(Lt_Units, "ThreeBeforeStart", "ThreeAfterStart");
            winLottery.CalculatorUnitWinLottery(Lt_Units, "Num1", this.Before_After_Threel.HeaderText);

            if (winLottery.Lt_UnitWinLotterys.Count > 0)
            {
                var vs = winLottery.Lt_UnitWinLotterys.GroupBy(a => new { a.UnitName, a.KjLong }).Select(g => (new { UnitName = g.Key.UnitName, KjLong = g.Key.KjLong, Count = g.Count() })).OrderBy(l => l.UnitName).ThenBy(l => l.KjLong).ToList();
                int sum = vs.Sum(l => l.Count);
                int max = vs.Max(l => l.KjLong);

                var vSum = vs.GroupBy(l => l.UnitName).Select(g => new { UnitName = g.Key, Sum = g.Sum(k => k.Count) }).ToList();
                DgvController.AddRows(this.dgvUnitTj, vs.Count + 1);
                for (int i = 0; i < vs.Count; i++)
                {
                    this.dgvUnitTj[0, i].Value = i + 1;
                    this.dgvUnitTj[1, i].Value = vs[i].UnitName;
                    this.dgvUnitTj[2, i].Value = vs[i].KjLong;
                    this.dgvUnitTj[3, i].Value = vs[i].Count;
                }

                this.dgvUnitTj[0, vs.Count].Value = vs.Count + 1;
                this.dgvUnitTj[1, vs.Count].Value = "总开奖次数";
                this.dgvUnitTj[2, vs.Count].Value = max;
                this.dgvUnitTj[3, vs.Count].Value = sum;
            }

            if (Lt_Units.Count > 0)
            {
                DgvController.AddRows(this.dgvUnit, Lt_Units.Count);
                initDgv1();
            }
        }

        private void analyzeTendencyUnit(List<TendencyUnitModel> lt, string property1, string property2)
        {
            lt.Clear();
            TendencyUnitModel tum;
            TendencyAllModel tm;
            AnalyzeTendencyUnit<TendencyAllModel> atu = new AnalyzeTendencyUnit<TendencyAllModel>();
            for (int i = 0; i < this.Tendency.Lt_Tendencys.Count; i++)
            {
                tm = this.Tendency.Lt_Tendencys[i];
                tum = new TendencyUnitModel();
                tum.Sno = tm.Sno;
                tum.Num1 = atu.GetUnitValue(this.Tendency.Lt_Tendencys, i, property1, property2);
                tum.Dtime = tm.Dtime;
                lt.Add(tum);
            }
        }

        private void initDgv1()
        {
            TendencyUnitModel tm;
            for (int i = Lt_Units.Count - 1, j = 0; i >= 0; i--)
            {
                tm = Lt_Units[i];
                this.dgvUnit[0, j].Value = j + 1;//局数
                this.dgvUnit[1, j].Value = tm.Sno;//开奖期号
                this.dgvUnit[2, j].Value = tm.Num1;//
                this.dgvUnit[3, j].Value = tm.Dtime;//开奖时间
                j++;
            }
        }

        #endregion

        #region 总开奖
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataRowView dr = (DataRowView)this.comboBox1.SelectedItem;
            string enName = dr.Row.ItemArray[0].ToString();
            if (enName.Contains("All"))
            {
                winLottery.CalculatorWinLottery(Tendency.Lt_Tendencys, lt_Tt);
            }
            else
            {
                winLottery.CalculatorWinLottery(Tendency.Lt_Tendencys, enName, dr.Row.ItemArray[1].ToString());
            }

            if (winLottery.Lt_WinLotterys.Count > 0)
            {
                DgvController.AddRows(this.dgv1, winLottery.Lt_WinLotterys.Count);

                var vs = winLottery.Lt_WinLotterys.GroupBy(a => new { a.UnitName, a.KjLong }).Select(g => (new { UnitName = g.Key.UnitName, KjLong = g.Key.KjLong, Count = g.Count() })).OrderBy(l => l.UnitName).ThenBy(l => l.KjLong).ToList();
                int sum = vs.Sum(l => l.Count);
                int max = vs.Max(l => l.KjLong);

                var vSum = vs.GroupBy(l => l.UnitName).Select(g => new { UnitName = g.Key, Sum = g.Sum(k => k.Count) }).ToList();
                DgvController.AddRows(this.dgv1, vs.Count + 1);
                for (int i = 0; i < vs.Count; i++)
                {
                    this.dgv1[0, i].Value = i + 1;
                    this.dgv1[1, i].Value = vs[i].UnitName;
                    this.dgv1[2, i].Value = vs[i].KjLong;
                    this.dgv1[3, i].Value = vs[i].Count;
                }

                this.dgv1[0, vs.Count].Value = vs.Count + 1;
                this.dgv1[1, vs.Count].Value = enName != "Dbl" ? "总开奖次数" : "总重复次数";
                this.dgv1[2, vs.Count].Value = max;
                this.dgv1[3, vs.Count].Value = sum;
            }
        }
        #endregion
    }
}

