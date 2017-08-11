using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FormXscp
{
    public partial class FormBase : Form
    {
        private int muls = 0;//起始倍数
        //private List<int> lt_data = new List<int>() { 1, 2, 3, 4, 6, 9, 13, 16, 21, 27, 35, 45, 59, 76, 98, 127, 164, 212, 274, 355, 459, 593, 767, 992, 1283, 1659, 2145 };
        private List<int> lt_data = new List<int>() { 1, 2, 4, 6, 9, 13, 16, 21, 27, 35, 45, 59, 76, 98, 127, 164, 212, 274, 355, 459, 593, 767, 992, 1283, 1659, 2145 };

        public FormBase()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.numericUpDown1.Value = 1;

        }


        private void initGrid()
        {
            this.dataGridView1.Rows.Clear();

            var v = lt_data.Where(l => l >= muls).ToList();

            for (int i = 0; i < v.Count; i++)
            {
                this.dataGridView1.Rows.Add();
            }

            for (int i = 0; i < v.Count; i++)
            {
                this.dataGridView1[1, i].Value = v[i];
                double dbdata = (v[i] * 0.02);
                this.dataGridView1[2, i].Value = String.Format("{0:F}", dbdata);
                this.dataGridView1[3, i].Value = 10;
                this.dataGridView1[4, i].Value = String.Format("{0:F}", 10 * dbdata);
                this.dataGridView1[5, i].Value = String.Format("{0:F}", v[i] * 0.9);

                double dbsum = getSum1(i);
                this.dataGridView1[6, i].Value = String.Format("{0:F}", dbsum);


                this.dataGridView1[7, i].Value = String.Format("{0:F}", v[i] * 0.9 - dbsum);
                this.dataGridView1[0, i].Value = i + 1;
            }
        }

        private double getSum1(int index)
        {
            double sum = 0;
            for (int i = 0; i <= index; i++)
            {
                sum += Convert.ToDouble(this.dataGridView1[4, i].Value);
            }
            return sum;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //核实倍数
            if (!checkMuls())
            {
                this.numericUpDown1.Focus();
                return;
            }

            initGrid();
        }

        /// <summary>
        /// 检验倍数的正确性
        /// </summary>
        /// <returns></returns>
        private bool checkMuls()
        {
            try
            {
                muls = Convert.ToInt32(this.numericUpDown1.Value);
                if (muls == 0)
                {
                    MessageBox.Show("倍数必须是：大于零");
                    return false;
                }
            }
            catch
            {
            }
            return true;
        }
    }
}
