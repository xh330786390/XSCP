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
    public partial class FormTendency1 : Form
    {
        private string text;
        private int count;
        public Tendency<Tendency1Model> Tendency;

        private static List<TendencyType> lt_Tt = new List<TendencyType>();
        private string selectType;
        private string comparison;

        static FormTendency1()
        {
            lt_Tt.Add(new TendencyType() { EnName = "Num0", ChName = "0" });
            lt_Tt.Add(new TendencyType() { EnName = "Num1", ChName = "1" });
            lt_Tt.Add(new TendencyType() { EnName = "Num2", ChName = "2" });
            lt_Tt.Add(new TendencyType() { EnName = "Num3", ChName = "3" });
            lt_Tt.Add(new TendencyType() { EnName = "Num4", ChName = "4" });
            lt_Tt.Add(new TendencyType() { EnName = "Num5", ChName = "5" });
            lt_Tt.Add(new TendencyType() { EnName = "Num6", ChName = "6" });
            lt_Tt.Add(new TendencyType() { EnName = "Num7", ChName = "7" });
            lt_Tt.Add(new TendencyType() { EnName = "Num8", ChName = "8" });
            lt_Tt.Add(new TendencyType() { EnName = "Num9", ChName = "9" });
            lt_Tt.Add(new TendencyType() { EnName = "All", ChName = "All" });
        }

        public FormTendency1(string text)
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

            this.comboBox1.SelectedIndex = 0;
            find(this.Tendency.Lt_Tendencys);
            this.cbCompare.SelectedIndex = 0;
        }

        private void find(List<Tendency1Model> lt)
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

        private void initDgv1(List<Tendency1Model> lt)
        {
            int index = 0;
            if (lt.Count - count >= 0)
                index = lt.Count - count;
            Tendency1Model tm;

            for (int i = lt.Count - 1, j = 0; i >= index; i--)
            {
                tm = lt[i];
                this.dgv1[0, j].Value = j + 1;//局数
                this.dgv1[1, j].Value = tm.Sno;//开奖期号
                this.dgv1[2, j].Value = tm.Lottery;//开奖号码
                this.dgv1[3, j].Value = tm.Num0;//0
                this.dgv1[4, j].Value = tm.Num1;//1
                this.dgv1[5, j].Value = tm.Num2;//2
                this.dgv1[6, j].Value = tm.Num3;//3
                this.dgv1[7, j].Value = tm.Num4;//4
                this.dgv1[8, j].Value = tm.Num5;//5
                this.dgv1[9, j].Value = tm.Num6;//6
                this.dgv1[10, j].Value = tm.Num7;//7
                this.dgv1[11, j].Value = tm.Num8;//8
                this.dgv1[12, j].Value = tm.Num9;//9
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
            DataTable dt = DataTableExtension.ToDataTable<Tendency1Model>(this.Tendency.Lt_Tendencys);
            List<Tendency1Model> lt = getList(dt, getFilterExpression());
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
                    if (lt_Tt[i].EnName == "All") continue;
                    if (i > 0) filterExpression.Append(" or ");
                    filterExpression.Append(lt_Tt[i].EnName + this.comparison + this.textBox1.Text);
                }
            }
            else
            {
                filterExpression.Append(this.selectType + this.comparison + this.textBox1.Text);
            }
            return filterExpression.ToString();
        }

        private List<Tendency1Model> getList(DataTable dtable, string filterExpression)
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
            return ConvertHelper<Tendency1Model>.ConvertToList(dt);
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
