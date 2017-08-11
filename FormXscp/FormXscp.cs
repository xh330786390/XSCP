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
    public partial class FormXscp : Form
    {
        private int nums = 0;//预计局数
        private int muls = 0;//起始倍数
        private int tzCount = 0;//投入注数

        private double singleMoney = 0;//每个模式下的单注金额
        private double rewardMoney = 0;//获得的奖金额

        private double trMoneys = 0;//总投入金额
        private ModelType modelType = ModelType.分;//模式类型
        private double rewardMoneys = 0;//获得的总奖金额
        private double profit = 0;//盈利值



        public FormXscp()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            this.textBox1.Text = "20";
            this.numericUpDown1.Value = 1;
            this.comboBox1.Text = "分";
            this.numericUpDown2.Value = 1;
            this.rbtnAfter.Checked = true;
            this.tzCount = 10;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            calculate();
        }

        private void calculate()
        {
            //核实预计局数
            if (!checkNums())
            {
                this.textBox1.Text = string.Empty;
                this.textBox1.Focus();
                return;
            }

            //核实倍数
            muls = Convert.ToInt32(this.numericUpDown1.Value);

            //核实盈利值
            profit = Convert.ToInt32(this.numericUpDown2.Value);

            addDgvRows();//添加dgv行数

            calculateDetail();//分析详情
        }

        /// <summary>
        /// 分析详情
        /// </summary>
        private void calculateDetail()
        {
            for (int i = 0; i < nums; i++)
            {
                this.dataGridView1[0, i].Value = i + 1;//局数
                this.dataGridView1[1, i].Value = muls;//倍数
                this.dataGridView1[2, i].Value = formate(getValue(muls, 1, singleMoney));//单注金额
                this.dataGridView1[3, i].Value = this.tzCount;//注数
                this.dataGridView1[4, i].Value = formate(getValue(muls, this.tzCount, singleMoney));//本次投入金额,投10注
                this.dataGridView1[5, i].Value = formate(getValue(muls, 1, rewardMoney));//本次奖励金额,中1注

                trMoneys = getSum(i);
                this.dataGridView1[6, i].Value = formate(trMoneys);
                rewardMoneys = muls * rewardMoney - trMoneys;
                this.dataGridView1[7, i].Value = formate(rewardMoneys);

                setDgvStyle(i);//设置单元格格式

                allowTr();
            }
        }

        /// <summary>
        /// 设置单元格格式
        /// </summary>
        /// <param name="i"></param>
        private void setDgvStyle(int i)
        {
            //if (i % 2 == 0)
            //    this.dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.LightSteelBlue;
            //else
            //    this.dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.LightYellow;// Color.BlanchedAlmond;
            Common.SetDgvStyle(this.dataGridView1, i, 1, Color.Red, 11);
            Common.SetDgvStyle(this.dataGridView1, i, 4, Color.Red, 11);
            Common.SetDgvStyle(this.dataGridView1, i, 6, Color.Red, 11);
            Common.SetDgvStyle(this.dataGridView1, i, 7, Color.Green, 11);
        }

        private void allowTr()
        {
            while (true)
            {
                muls++;
                double db1 = trMoneys + getValue(muls, this.tzCount, singleMoney);//总投入的金额
                double db2 = getValue(muls, 1, rewardMoney);//本次获取的奖励
                double db3 = db2 - db1;//总获取的奖金

                if (db3 - rewardMoneys >= profit) break;
            }
        }

        /// <summary>
        /// 总投入
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        private double getSum(int index)
        {
            double sum = 0;
            for (int i = 0; i <= index; i++)
            {
                sum += Convert.ToDouble(this.dataGridView1[4, i].Value);
            }
            return sum;
        }

        /// <summary>
        /// 获取
        /// </summary>
        /// <param name="num"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        private double getValue(int number, int num, double db)
        {
            return number * num * db;
        }

        #region 方法
        /// <summary>
        /// 添加dgv行数
        /// </summary>
        private void addDgvRows()
        {
            this.dataGridView1.Rows.Clear();
            for (int i = 0; i < nums; i++)
            {
                this.dataGridView1.Rows.Add();//添加记录数
            }
        }

        /// <summary>
        /// 检验预计局数的 输入的正确性
        /// </summary>
        /// <returns></returns>
        private bool checkNums()
        {
            try
            {
                if (string.IsNullOrEmpty(this.textBox1.Text))
                {
                    MessageBox.Show("请填写：预计局数！");
                    return false;
                }

                nums = Convert.ToInt32(this.textBox1.Text);
                if (nums <= 0)
                {
                    MessageBox.Show("局数必须是：大于零");
                    return false;
                }
            }
            catch
            {
                MessageBox.Show("局数必须是：整数");
                return false;
            }
            return true;
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

        /// <summary>
        /// 模式选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            initRewardMoney();
        }

        private void initRewardMoney()
        {
            switch (this.comboBox1.Text)
            {
                case "分":
                    modelType = ModelType.分;
                    singleMoney = 0.02;
                    if (this.rbtnAfter.Checked)
                        rewardMoney = 0.9;
                    else if (this.rbtnBefore.Checked)
                        rewardMoney = 1.8;
                    else if (this.rbtDwd.Checked)
                        rewardMoney = 0.18;
                    break;
                case "角":
                    modelType = ModelType.角;
                    singleMoney = 0.2;
                    if (this.rbtnAfter.Checked)
                        rewardMoney = 9;
                    else if (this.rbtnBefore.Checked)
                        rewardMoney = 18;
                    else if (this.rbtDwd.Checked)
                        rewardMoney = 1.8;
                    break;
                case "元":
                    modelType = ModelType.元;
                    singleMoney = 2;
                    if (this.rbtnAfter.Checked)
                        rewardMoney = 90;
                    else if (this.rbtnBefore.Checked)
                        rewardMoney = 180;
                    else if (this.rbtDwd.Checked)
                        rewardMoney = 18;
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 检验预计局数的 输入的正确性
        /// </summary>
        /// <returns></returns>
        private bool checkProfit()
        {
            try
            {
                profit = Convert.ToInt32(this.numericUpDown2.Value);
                if (profit < 0)
                {
                    MessageBox.Show("盈利值必须是：大于或等于零");
                    return false;
                }
            }
            catch
            {
                MessageBox.Show("盈利值必须是：整数");
                return false;
            }
            return true;
        }

        /// <summary>
        /// 保留两位小数
        /// </summary>
        /// <param name="dbl"></param>
        /// <returns></returns>
        private string formate(double dbl)
        {
            return String.Format("{0:F}", dbl);
        }
        #endregion

        private void rbtnAfter_CheckedChanged(object sender, EventArgs e)
        {
            this.tzCount = 10;
            initRewardMoney();
            calculate();
        }

        private void rbtnBefore_CheckedChanged(object sender, EventArgs e)
        {
            this.tzCount = 25;
            initRewardMoney();
            calculate();
        }

        private void rbtDwd_CheckedChanged(object sender, EventArgs e)
        {
            this.tzCount = 5;
            initRewardMoney();
            calculate();
        }
    }

    /// <summary>
    /// 模式类型
    /// </summary>
    public enum ModelType
    {
        分 = 1,
        角 = 2,
        元 = 3
    }

    public class XscpModel
    {

    }
}
