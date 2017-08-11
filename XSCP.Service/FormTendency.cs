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
using System.Reflection;

namespace XSCP.Service
{
    public partial class FormTendency : Form
    {
        private string text;
        private int count;
        public Tendency Tendency;

        private static List<TendencyType> lt_Tt = new List<TendencyType>();
        private string selectType;
        private string comparison;

        static FormTendency()
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
            lt_Tt.Add(new TendencyType() { EnName = "All", ChName = "所有" });
        }

        public FormTendency(string text)
        {
            InitializeComponent();
            this.text = text;
            this.Text = this.text;
        }

        private void FormTendency_Load(object sender, EventArgs e)
        {
            DataTable dt = DataTableExtension.ToDataTable<TendencyType>(lt_Tt);
            this.cbType.DataSource = dt;
            this.cbType.ValueMember = "EnName";
            this.cbType.DisplayMember = "ChName";

            this.comboBox1.SelectedIndex = 2;
            find(this.Tendency.Lt_Tendencys);
            this.cbCompare.SelectedIndex = 0;
        }

        private void find(List<TendencyModel> lt)
        {
            this.Cursor = Cursors.WaitCursor;

            DgvController.AddRows(this.dgv1, count);

            if (lt != null && lt.Count > 0)
            {
                initDgv1(lt);

                DgvController.RefreshDgvTencenyColor(this.dgv1);
            }
            this.Cursor = null;
        }

        private void initDgv1(List<TendencyModel> ltt)
        {

            var lt = ltt.OrderBy(l => l.SNO).ToList();

            int index = 0;
            if (lt.Count - count >= 0)
                index = lt.Count - count;
            TendencyModel tm;

            for (int i = lt.Count - 1, j = 0; i >= index; i--)
            {
                tm = lt[i];
                this.dgv1[0, j].Value = j + 1;//局数
                this.dgv1[1, j].Value = tm.SNO;//开奖期号
                this.dgv1[2, j].Value = tm.Lottery;//开奖号码
                this.dgv1[3, j].Value = tm.Big;//大
                this.dgv1[4, j].Value = tm.Small;//小
                this.dgv1[5, j].Value = tm.BigSmall;//大小
                this.dgv1[6, j].Value = tm.SmallBig;//小大

                this.dgv1[8, j].Value = tm.Odd;//奇
                this.dgv1[9, j].Value = tm.Pair;//偶
                this.dgv1[10, j].Value = tm.OddPair;//奇偶
                this.dgv1[11, j].Value = tm.PairOdd;//偶奇
                this.dgv1[12, j].Value = tm.Dbl;//重
                this.dgv1[13, j].Value = tm.Dtime;//开奖时间
                j++;
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.comboBox1.SelectedIndex == 0)
            {
                if (this.Tendency.Lt_Tendencys.Count >= 30) count = 30;
                else count = this.Tendency.Lt_Tendencys.Count;
            }
            else if (this.comboBox1.SelectedIndex == 1)
            {
                if (this.Tendency.Lt_Tendencys.Count >= 50) count = 50;
                else count = this.Tendency.Lt_Tendencys.Count;
            }
            else if (this.comboBox1.SelectedIndex == 2)
            {
                if (this.Tendency.Lt_Tendencys.Count >= 100) count = 100;
                else count = this.Tendency.Lt_Tendencys.Count;
            }
            else if (this.comboBox1.SelectedIndex == 3)
            {
                count = this.Tendency.Lt_Tendencys.Count;
            }

            find(this.Tendency.Lt_Tendencys);
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            DataTable dt = DataTableExtension.ToDataTable<TendencyModel>(this.Tendency.Lt_Tendencys);
            List<TendencyModel> lt = getList(dt, getFilterExpression());
            count = lt.Count;
            find(lt);
        }

        private string getFilterExpression()
        {
            StringBuilder filterExpression = new StringBuilder();
            if (this.selectType == "All")
            {
                for (int i = 0; i < lt_Tt.Count; i++)
                {
                    if (lt_Tt[i].EnName == "All" || lt_Tt[i].EnName == "Dbl") continue;
                    if (!string.IsNullOrEmpty(filterExpression.ToString())) filterExpression.Append(" or ");
                    filterExpression.Append(lt_Tt[i].EnName + this.comparison + this.textBox1.Text);
                }
            }
            else
            {
                filterExpression.Append(this.selectType + this.comparison + this.textBox1.Text);
            }
            return filterExpression.ToString();
        }

        private List<TendencyModel> getList(DataTable dtable, string filterExpression)
        {
            DataTable dt = dtable.Clone();
            DataRow[] drs = dtable.Select(filterExpression);
            foreach (DataRow dr in drs)
            {
                DataRow drow = dt.NewRow();
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    drow[i] = dr[i];
                }
                dt.Rows.Add(drow);
            }
            return ConvertHelper<TendencyModel>.ConvertToList(dt);
        }

        private void cbType_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataRowView dr = (DataRowView)this.cbType.SelectedItem;
            selectType = dr.Row.ItemArray[0].ToString();
        }

        private void cbCompare_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.cbCompare.SelectedIndex == 0) comparison = " >= ";
            else if (this.cbCompare.SelectedIndex == 1) comparison = " <= ";
            else comparison = " != ";
        }
    }
}
