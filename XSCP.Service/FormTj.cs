using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using XSCP.Service.Model;
using XSCP.Service.Controllers;

namespace XSCP.Service
{
    public partial class FormTj : Form
    {
        #region 字段定义
        private string text;
        public Tendency Tendency;
        private WinLottery winLottery = new WinLottery();
        private EnumNumberType enumNumberType;
        private EnumSelectType enumSelectType;
        private static List<TendencyType> lt_Tt = new List<TendencyType>();
        private static List<TendencyType> lt_TtUnits = new List<TendencyType>();
        private AnalyzeTendencyUnit atuBigSmall = new AnalyzeTendencyUnit();
        private AnalyzeTendencyUnit atuOddPair = new AnalyzeTendencyUnit();
        #endregion

        static FormTj()
        {
            lt_Tt.Add(new TendencyType() { EnName = "Dbl", ChName = "重重" });
            lt_Tt.Add(new TendencyType() { EnName = "PairOdd", ChName = "偶奇" });
            lt_Tt.Add(new TendencyType() { EnName = "OddPair", ChName = "奇偶" });
            lt_Tt.Add(new TendencyType() { EnName = "Pair", ChName = "偶偶" });
            lt_Tt.Add(new TendencyType() { EnName = "Odd", ChName = "奇奇" });
            lt_Tt.Add(new TendencyType() { EnName = "SmallBig", ChName = "小大" });
            lt_Tt.Add(new TendencyType() { EnName = "BigSmall", ChName = "大小" });
            lt_Tt.Add(new TendencyType() { EnName = "Small", ChName = "小小" });
            lt_Tt.Add(new TendencyType() { EnName = "Big", ChName = "大大" });
            lt_Tt.Add(new TendencyType() { EnName = "AllOddPair", ChName = "所有奇偶" });
            lt_Tt.Add(new TendencyType() { EnName = "AllBigSmall", ChName = "所有大小" });

            lt_TtUnits.Add(new TendencyType() { EnName = "Num2", ChName = "奇偶-偶奇" });
            lt_TtUnits.Add(new TendencyType() { EnName = "Num1", ChName = "奇奇-偶偶" });
            lt_TtUnits.Add(new TendencyType() { EnName = "Num2", ChName = "大小-小大" });
            lt_TtUnits.Add(new TendencyType() { EnName = "Num1", ChName = "大大-小小" });
        }

        public FormTj(string text)
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

            this.radioButton1.Checked = true;
            this.comboBox2.SelectedIndex = 0;

            this.rtbBigSmall.Checked = true;
            initUnit();
        }

        #region 选二
        private List<TendencyUnitModel> Lt_Units = new List<TendencyUnitModel>();
        private void initUnit()
        {
            if (this.rtbBigSmall.Checked)
            {
                analyzeTendencyUnit(Lt_Units, "Big", "Small", "BigSmall", "SmallBig");
            }
            else if (this.rtbOddPair.Checked)
            {
                analyzeTendencyUnit(Lt_Units, "Odd", "Pair", "OddPair", "PairOdd");
            }

            winLottery.Lt_UnitWinLotterys.Clear();
            winLottery.CalculatorUnitWinLottery(Lt_Units, "Num1", this.Big_Small.HeaderText);
            winLottery.CalculatorUnitWinLottery(Lt_Units, "Num2", this.dataGridViewTextBoxColumn6.HeaderText);

            if (winLottery.Lt_UnitWinLotterys.Count > 0)
            {
                //DgvController.AddRows(this.dgvUnitTj, winLottery.Lt_UnitWinLotterys.Count);

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

        private void analyzeTendencyUnit(List<TendencyUnitModel> lt, string property1, string property2, string property3, string property4)
        {
            lt.Clear();
            TendencyUnitModel tum;
            TendencyModel tm;
            AnalyzeTendencyUnit atu = new AnalyzeTendencyUnit();
            for (int i = 0; i < this.Tendency.Lt_Tendencys.Count; i++)
            {
                tm = this.Tendency.Lt_Tendencys[i];
                tum = new TendencyUnitModel();
                tum.Sno = tm.SNO;
                tum.Num1 = atu.GetUnitValue(this.Tendency.Lt_Tendencys, i, property1, property2);
                tum.Num2 = atu.GetUnitValue(this.Tendency.Lt_Tendencys, i, property3, property4);
                tum.Dtime = tm.Dtime;
                lt.Add(tum);
            }
        }

        private void rtbBigSmall_CheckedChanged(object sender, EventArgs e)
        {
            this.Big_Small.HeaderText = "大大-小小";
            this.dataGridViewTextBoxColumn6.HeaderText = "大小-小大";
            initUnit();
        }

        private void rtbOddPair_CheckedChanged(object sender, EventArgs e)
        {
            this.Big_Small.HeaderText = "奇奇-偶偶";
            this.dataGridViewTextBoxColumn6.HeaderText = "奇偶-偶奇";
            initUnit();
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
                this.dgvUnit[3, j].Value = tm.Num2;//
                this.dgvUnit[4, j].Value = tm.Dtime;//开奖时间
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
                winLottery.CalculatorWinLottery(Tendency.Lt_Tendencys, lt_Tt, enName);
            }
            else
            {
                winLottery.CalculatorWinLottery(Tendency.Lt_Tendencys, dr.Row.ItemArray[0].ToString(), dr.Row.ItemArray[1].ToString());
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

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.tabControl1.SelectedIndex == 2)
            {
                find();
            }
        }
        #endregion

        #region 开奖组合
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            enumNumberType = EnumNumberType.BigSmall;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            enumNumberType = EnumNumberType.OddPair;
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.comboBox2.SelectedIndex == 0)
            {
                enumSelectType = EnumSelectType.选1;
            }
            else if (this.comboBox2.SelectedIndex == 1)
            {
                enumSelectType = EnumSelectType.选2;
            }
            else if (this.comboBox2.SelectedIndex == 2)
            {
                enumSelectType = EnumSelectType.选3;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            find();
        }

        private void find()
        {
            AnalyzeTendencyUnit atu = new AnalyzeTendencyUnit();
            atu.SetTendencyUnits(this.Tendency.Lt_Tendencys, enumNumberType, enumSelectType);
            if (atu.Lt_TendencyUnits.Count > 0)
            {
                DgvController.AddRows(this.dgv2, atu.Lt_TendencyUnits.Count);

                for (int j = atu.Lt_TendencyUnits.Count - 1, i = 0; j >= 0; j--)
                {
                    TendencyUnitModel tum = atu.Lt_TendencyUnits[j];
                    this.dgv2[0, i].Value = i + 1;
                    this.dgv2[1, i].Value = tum.Sno;
                    this.dgv2[2, i].Value = tum.Num1;
                    this.dgv2[3, i].Value = tum.Num2;
                    this.dgv2[4, i].Value = tum.Num3;
                    this.dgv2[5, i].Value = tum.Num4;
                    this.dgv2[6, i].Value = tum.Dtime;
                    i++;
                }
            }
        }
        #endregion


    }
}

