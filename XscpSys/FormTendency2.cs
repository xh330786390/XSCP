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
    public partial class FormTendency2 : Form
    {
        private string text;
        private int count;
        public Tendency<Tendency2Model> Tendency;

        private static List<TendencyType> lt_Tt = new List<TendencyType>();
        private string selectType;
        private string comparison;

        private bool bl = false;
        private int startIndex = -1;
        private int endIndex = -1;

        static FormTendency2()
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
            lt_Tt.Add(new TendencyType() { EnName = "All", ChName = "所有" });
        }

        public FormTendency2(string text)
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

        private void find(List<Tendency2Model> lt)
        {
            this.Cursor = Cursors.WaitCursor;

            DgvController.AddRows(this.dgv1, count);

            if (lt != null && lt.Count > 0)
            {
                initDgv1(lt);

                DgvController.RefreshDgvTencenyColor(this.dgv1);

                if (this.textBox1.Text.Trim() == "1" && this.cbCompare.SelectedIndex == 3)
                {
                    if (this.selectType == "AllBigSmall")
                        DgvController.RefreshDgvTencenyColorCompare(this.dgv1, 3, 6);
                    else if (this.selectType == "AllOddPair")
                        DgvController.RefreshDgvTencenyColorCompare(this.dgv1, 8, 11);
                }
            }
            this.Cursor = null;
        }

        private void initDgv1(List<Tendency2Model> ltt)
        {
            var lt = ltt.OrderBy(l => l.Sno).ToList();
            int index = 0;
            if (lt.Count - count >= 0)
                index = lt.Count - count;
            Tendency2Model tm;

            for (int i = lt.Count - 1, j = 0; i >= index; i--)
            {
                tm = lt[i];
                this.dgv1[0, j].Value = j + 1;//局数
                this.dgv1[1, j].Value = tm.Sno;//开奖期号
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
            DataTable dt = DataTableExtension.ToDataTable<Tendency2Model>(this.Tendency.Lt_Tendencys);
            List<Tendency2Model> lt = getList(dt, getFilterExpression());
            count = lt.Count;
            find(lt);
        }

        private string getFilterExpression()
        {
            StringBuilder filterExpression = new StringBuilder();
            if (bl)
            {
                for (int i = startIndex; i <= endIndex; i++)
                {
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

        private List<Tendency2Model> getList(DataTable dtable, string filterExpression)
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
            return ConvertHelper<Tendency2Model>.ConvertToList(dt);
        }

        private void cbType_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataRowView dr = (DataRowView)this.cbType.SelectedItem;
            selectType = dr.Row.ItemArray[0].ToString();

            if (this.selectType == "All")
            {
                startIndex = 1;
                endIndex = 8;
                bl = true;
            }
            else if (this.selectType == "AllOddPair")
            {
                startIndex = 1;
                endIndex = 4;
                bl = true;
            }
            else if (this.selectType == "AllBigSmall")
            {
                startIndex = 5;
                endIndex = 8;
                bl = true;
            }
            else
            {
                bl = false;
            }
        }

        private void cbCompare_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.cbCompare.SelectedIndex == 0) comparison = " >= ";
            else if (this.cbCompare.SelectedIndex == 1) comparison = " <= ";
            else if (this.cbCompare.SelectedIndex == 2) comparison = " != ";
            else comparison = " = ";
        }

        private void btnGet_Click(object sender, EventArgs e)
        {
            if (!checkStartTextBox(this.textBox2)) { this.textBox2.Focus(); return; };
            if (!checkEndTextBox(this.textBox3)) { this.textBox3.Focus(); return; };

            int startValue = Convert.ToInt32(this.textBox2.Text.Trim());
            int endValue = Convert.ToInt32(this.textBox3.Text.Trim());

            var lt = this.Tendency.Lt_Tendencys.Where(l => (Convert.ToInt32(l.Sno.Replace("期", "")) >= startValue && Convert.ToInt32(l.Sno.Replace("期", "")) <= endValue)).ToList();
            count = lt.Count;
            find(lt);
        }

        /// <summary>
        /// 检验开始开奖期号的 输入的正确性
        /// </summary>
        /// <returns></returns>
        private bool checkStartTextBox(TextBox textBox)
        {
            try
            {
                if (string.IsNullOrEmpty(textBox.Text))
                {
                    MessageBox.Show("请填写开始开奖期号！");
                    return false;
                }

                int num = Convert.ToInt32(textBox.Text);
                if (num <= 0)
                {
                    MessageBox.Show("开始开奖期号必须是：大于零");
                    return false;
                }
            }
            catch
            {
                MessageBox.Show("开始开奖期号必须是：整数");
                return false;
            }
            return true;
        }

        /// <summary>
        /// 检验结束开奖期号的 输入的正确性
        /// </summary>
        /// <returns></returns>
        private bool checkEndTextBox(TextBox textBox)
        {
            try
            {
                if (string.IsNullOrEmpty(textBox.Text))
                {
                    MessageBox.Show("请填写结束开奖期号！");
                    return false;
                }

                int num = Convert.ToInt32(textBox.Text);
                if (num <= 0)
                {
                    MessageBox.Show("结束开奖期号必须是：大于零");
                    return false;
                }

                int numStart = Convert.ToInt32(this.textBox2.Text);
                if (numStart > num)
                {
                    MessageBox.Show("结束开奖期号 必须大于 开始开奖期号!");
                    return false;
                }
            }
            catch
            {
                MessageBox.Show("结束开奖期号必须是：整数");
                return false;
            }
            return true;
        }
    }
}
