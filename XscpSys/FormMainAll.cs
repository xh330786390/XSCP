using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;


using XscpSys.Controllers;
using XscpSys.Model;

namespace XscpSys
{
    public partial class FormMainAll : Form
    {
        #region 字段定义
        /// <summary>
        /// 分分彩开奖号
        /// </summary>
        private Lottery lotteryFF = new LotteryFF();

        /// <summary>
        /// 分分彩-定位胆数字走势
        /// </summary>
        private Tendency<Tendency1Model> dwdDigitTendencys = new Tendency<Tendency1Model>();

        /// <summary>
        /// 个十百千万趋势
        /// </summary>
        private List<object> lt_dwds = new List<object>();

        /// <summary>
        /// 分分彩-前二走势
        /// </summary>
        private Tendency<Tendency2Model> befor2FFTendency = new Tendency<Tendency2Model>();

        /// <summary>
        /// 分分彩-后二走势
        /// </summary>
        private Tendency<Tendency2Model> after2FFTendency = new Tendency<Tendency2Model>();

        /// <summary>
        /// 分分彩-后三走势
        /// </summary>
        //private Tendency<Tendency3Model> befor3FFTendency = new Tendency<Tendency3Model>();

        /// <summary>
        /// 分分彩-后三走势
        /// </summary>
        //private Tendency<Tendency3Model> after3FFTendency = new Tendency<Tendency3Model>();

        /// <summary>
        /// 分分彩-五星走势
        /// </summary>
        //private Tendency<Tendency5Model> fiveStartFFTendency = new Tendency<Tendency5Model>();

        /// <summary>
        /// 分分彩-所有
        /// </summary>
        private Tendency<TendencyAllModel> allFFTendency = new Tendency<TendencyAllModel>();

        /// <summary>
        /// 趋势分析
        /// </summary>
        AnalyzeTendency At = new AnalyzeTendency();
        /// <summary>
        /// 进入监控
        /// </summary>
        private int enterNum = 0;

        /// <summary>
        /// 文件是否启动监听
        /// </summary>
        private bool isAddEvent = false;

        private string currentFile = "";

        private int time;//次数
        private bool blTime = false;
        #endregion

        public FormMainAll()
        {
            InitializeComponent();

            lt_dwds.Add(new Tendency<Tendency1Model>());
            lt_dwds.Add(new Tendency<Tendency1Model>());
            lt_dwds.Add(new Tendency<Tendency1Model>());
            lt_dwds.Add(new Tendency<Tendency1Model>());
            lt_dwds.Add(new Tendency<Tendency1Model>());
        }

        #region 按钮事件
        private void tsMenuStart_Click(object sender, EventArgs e)
        {
            if (!isAddEvent)
            {
                this.fswFF.Path = Param.FileBase + @"分分彩\";
                this.fswFF.Filter = "*.txt";
                this.fswFF.Changed += new FileSystemEventHandler(fswFF_Changed);
                this.fswFF.NotifyFilter = NotifyFilters.LastWrite;
                this.fswFF.EnableRaisingEvents = true;
                isAddEvent = true;

                openFile();

                warning();//预警
            }
        }

        private void FormMainDigit_Load(object sender, EventArgs e)
        {
            timer.Enabled = true;
            timer.Start();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            if (!blTime)
            {
                this.lblWarning.Location = new Point(450 + time * 5, 17);
                time++;
                if (time > 20) { blTime = true; time = 20; }
            }
            else
            {
                this.lblWarning.Location = new Point(550 - (20 - time) * 5, 17);
                time--;
                if (time < 0) { blTime = false; time = 0; };
            }
        }

        private void openFile()
        {
            startProcess();//打开文件
            formateFile();
        }

        private void startProcess()
        {
            currentFile = Param.FileBase + @"分分彩\" + "ffcp" + this.dtp.Value.ToString("yyyyMMdd") + ".txt";
            if (!File.Exists(currentFile)) File.Create(currentFile).Close();

            //声明一个程序信息类
            System.Diagnostics.ProcessStartInfo Info = new System.Diagnostics.ProcessStartInfo();

            Info.FileName = "editplus.exe";//设置外部程序名

            //设置外部程序的启动参数（命令行参数）为test.txt
            Info.Arguments = currentFile;

            //设置外部程序工作目录为 
            Info.WorkingDirectory = @"C:\Program Files (x86)\EditPlus 3\";

            System.Diagnostics.Process Proc;//声明一个程序类

            try
            {

                Proc = System.Diagnostics.Process.Start(Info);//启动外部程序
            }
            catch (System.ComponentModel.Win32Exception e)
            {
                MessageBox.Show("系统找不到指定的程序文件。\r{0}", e.Message);
            }
        }

        private void tsMenuStop_Click(object sender, EventArgs e)
        {
            if (isAddEvent)
            {
                XscpWarning.Lt_Warnings.Clear();
                this.fswFF.Changed -= new FileSystemEventHandler(fswFF_Changed);
                this.fswFF.EnableRaisingEvents = false;
                isAddEvent = false;
            }
        }

        private void tsMenuLottery_Click(object sender, EventArgs e)
        {
            FormData fd = new FormData();
            fd.Show();
        }

        private void tsMenuTest_Click(object sender, EventArgs e)
        {
            FormTest fd = new FormTest(lotteryFF);
            fd.ShowDialog();
        }

        private bool formateFile()
        {
            currentFile = Param.FileBase + @"分分彩\" + "ffcp" + this.dtp.Value.ToString("yyyyMMdd") + ".txt";
            if (!File.Exists(currentFile))
            {
                MessageBox.Show("文件不存在");
                return false;
            }
            string result = System.IO.File.ReadAllText(currentFile, Encoding.UTF8).Replace("期", ",");
            result = result.Replace("	", "");
            System.IO.File.WriteAllText(currentFile, result, Encoding.UTF8);
            return true;
        }

        private void tsMenuFomate_Click(object sender, EventArgs e)
        {
            if (formateFile()) MessageBox.Show("     格式完成");
        }

        private void tsMenuOpen_Click(object sender, EventArgs e)
        {
            openFile();
        }

        private void fswFF_Changed(object sender, FileSystemEventArgs e)
        {
            try
            {
                enterNum++;
                if (enterNum % 4 == 0)
                {
                    Param.FilePathFF = this.fswFF.Path + "ffcp" + this.dtp.Value.ToString("yyyyMMdd") + ".txt";
                    statisticsFF(lotteryFF, Param.FilePathFF);
                    enterNum = 0;
                }
            }
            catch
            {
                enterNum = 0;
            }
        }

        private void dtp_ValueChanged(object sender, EventArgs e)
        {
            if (isAddEvent)
            {
                Param.FilePathFF = this.fswFF.Path + "ffcp" + this.dtp.Value.ToString("yyyyMMdd") + ".txt";
                if (!File.Exists(Param.FilePathFF)) clearFFCtrl();
                else
                {
                    statisticsFF(lotteryFF, Param.FilePathFF);
                }
            }
        }

        private void dgvFF_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;

            if (e.ColumnIndex == 11 && (e.RowIndex == 0 || e.RowIndex == 5))
            {
                FormMaxTendency ft = new FormMaxTendency("分分彩-" + this.dgvFF[0, e.RowIndex].Value.ToString());
                if (e.RowIndex == 0) { ft.num1 = 1; ft.num2 = 2; }
                else { ft.num1 = 4; ft.num2 = 5; }
                ft.Show();
            }

            if (e.ColumnIndex == 11 && (e.RowIndex == 1 || e.RowIndex == 4))
            {
                FormTendencyDwd ft = new FormTendencyDwd("分分彩-" + this.dgvFF[0, e.RowIndex].Value.ToString());
                if (e.RowIndex == 1)
                {
                    ft.DwdTendency1 = ((Tendency<Tendency1Model>)this.lt_dwds[0]).Lt_Tendencys;
                    ft.DwdTendency2 = ((Tendency<Tendency1Model>)this.lt_dwds[1]).Lt_Tendencys;
                }
                else
                {
                    ft.DwdTendency1 = ((Tendency<Tendency1Model>)this.lt_dwds[3]).Lt_Tendencys;
                    ft.DwdTendency2 = ((Tendency<Tendency1Model>)this.lt_dwds[4]).Lt_Tendencys;
                }
                ft.Show();
            }

            if (e.ColumnIndex == 11 && (e.RowIndex == 2 || e.RowIndex == 3))
            {
                FormTendency2 ft = new FormTendency2("分分彩-" + this.dgvFF[0, e.RowIndex].Value.ToString());
                if (e.RowIndex == 2) ft.Tendency = this.befor2FFTendency;
                else ft.Tendency = this.after2FFTendency;
                ft.Show();
            }

            if (e.ColumnIndex == 12 && (e.RowIndex == 2 || e.RowIndex == 3))
            {
                FormTj2 ft = new FormTj2("分分彩-" + this.dgvFF[0, e.RowIndex].Value.ToString());
                if (e.RowIndex == 2) ft.Tendency = this.befor2FFTendency;
                else ft.Tendency = this.after2FFTendency;
                ft.LotteryFF = this.lotteryFF;
                ft.Show();
            }

            this.Cursor = null;
        }

        private void dgvDigit_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            if (e.ColumnIndex == 11 && e.RowIndex == 1)
            {
                FormTendency1 ft = new FormTendency1("分分彩-" + this.dgvDigit[0, e.RowIndex].Value.ToString());
                ft.Tendency = this.dwdDigitTendencys;
                ft.Show();
            }
            this.Cursor = null;
        }

        #endregion

        #region 预警
        private void warning()
        {
            checkWarmValue(this.textBox1);//绿
            checkWarmValue(this.textBox2);//黄
            checkWarmValue(this.textBox3);//红
            checkWarmValue(this.textBox4);//紫

            addWarning(this.textBox1);
            addWarning(this.textBox2);
            addWarning(this.textBox3);
            addWarning(this.textBox4);
        }

        /// <summary>
        /// 检验预警值的 输入的正确性
        /// </summary>
        /// <returns></returns>
        private bool checkWarmValue(TextBox textBox)
        {
            try
            {
                if (string.IsNullOrEmpty(textBox.Text))
                {
                    MessageBox.Show("请填写：预警值！");
                    return false;
                }

                int num = Convert.ToInt32(textBox.Text);
                if (num <= 0)
                {
                    MessageBox.Show("预警值必须是：大于零");
                    return false;
                }
            }
            catch
            {
                MessageBox.Show("预警值必须是：整数");
                return false;
            }
            return true;
        }

        /// <summary>
        /// 添加预警值
        /// </summary>
        /// <param name="textBox"></param>
        private void addWarning(TextBox textBox)
        {
            XscpWarning.Lt_Warnings.Add(new Waring() { Value = int.Parse(textBox.Text), Color = textBox.BackColor });
        }
        #endregion

        #region 核心方法
        #region 读取文件
        private void readFile(Lottery lottery, string fileName)
        {
            lottery.ClearLotterys();// 清空开奖记录

            using (StreamReader sr = new StreamReader(fileName, Encoding.Default))
            {
                String line;
                while ((line = sr.ReadLine()) != null)
                {
                    if (string.IsNullOrEmpty(line)) continue;
                    lottery.AddLottery(this.dtp.Value, line);
                }
                sr.Close();
                sr.Dispose();
            }

        }
        #endregion

        #region 分析开奖趋势
        /// <summary>
        /// 分析各趋势
        /// </summary>
        private void analyzeTendency()
        {
            //分析定位胆数字趋势
            analyzeDwdDigitTendency();

            //分析定位胆：个十百千万趋势
            analyzeDwdBitTendency(1);
            analyzeDwdBitTendency(2);
            analyzeDwdBitTendency(3);
            analyzeDwdBitTendency(4);
            analyzeDwdBitTendency(5);

            analyze2Tendency(this.lotteryFF, befor2FFTendency, 1, 2);//分析前二趋势
            analyze2Tendency(this.lotteryFF, after2FFTendency, 4, 5); //分析后二趋势

            //analyze3Tendency(this.lotteryFF, befor3FFTendency, 1, 2, 3);//分析前三趋势
            //analyze3Tendency(this.lotteryFF, after3FFTendency, 3, 4, 5);//分析后三趋势

            //analyze5Tendency(this.lotteryFF, fiveStartFFTendency);//分析五星趋势
            analyzeAllTendency(this.lotteryFF, allFFTendency);
        }

        #region 定位胆趋势分析
        /// <summary>
        /// 定位胆数字
        /// </summary>
        private void analyzeDwdDigitTendency()
        {
            dwdDigitTendencys.ClearTendencys();//清空记录
            Tendency1Model tm;
            for (int i = lotteryFF.Lt_Lotterys.Count - 1; i >= 0; i--)
            {
                tm = new Tendency1Model();
                tm.Num0 = At.Digital(lotteryFF.Lt_Lotterys, i, 0);
                tm.Num1 = At.Digital(lotteryFF.Lt_Lotterys, i, 1);
                tm.Num2 = At.Digital(lotteryFF.Lt_Lotterys, i, 2);
                tm.Num3 = At.Digital(lotteryFF.Lt_Lotterys, i, 3);
                tm.Num4 = At.Digital(lotteryFF.Lt_Lotterys, i, 4);
                tm.Num5 = At.Digital(lotteryFF.Lt_Lotterys, i, 5);
                tm.Num6 = At.Digital(lotteryFF.Lt_Lotterys, i, 6);
                tm.Num7 = At.Digital(lotteryFF.Lt_Lotterys, i, 7);
                tm.Num8 = At.Digital(lotteryFF.Lt_Lotterys, i, 8);
                tm.Num9 = At.Digital(lotteryFF.Lt_Lotterys, i, 9);
                tm.Sno = (lotteryFF.Lt_Lotterys[i].Sno.ToString() + "期").PadLeft(5, '0');
                tm.Lottery = lotteryFF.Lt_Lotterys[i].Lottery;
                tm.Dtime = lotteryFF.Lt_Lotterys[i].Dtime;
                dwdDigitTendencys.AddTendency(tm);//添加定位胆趋势记录
            }
        }

        /// <summary>
        /// 分析定位胆：个十百千万
        /// </summary>
        private void analyzeDwdBitTendency(int index)
        {
            Tendency<Tendency1Model> tendency = (Tendency<Tendency1Model>)lt_dwds[index - 1];
            tendency.ClearTendencys();

            Tendency1Model tm;

            for (int i = lotteryFF.Lt_Lotterys.Count - 1; i >= 0; i--)
            {
                tm = new Tendency1Model();
                tm.Big = At.BigNum(lotteryFF.Lt_Lotterys, i, index);//大
                tm.Small = At.SmallNum(lotteryFF.Lt_Lotterys, i, index);//小

                tm.Odd = At.OddPairNum(lotteryFF.Lt_Lotterys, i, index, 1);//奇
                tm.Pair = At.OddPairNum(lotteryFF.Lt_Lotterys, i, index, 0);//偶

                tm.Sno = (lotteryFF.Lt_Lotterys[i].Sno.ToString() + "期").PadLeft(5, '0');
                tm.Dtime = lotteryFF.Lt_Lotterys[i].Dtime;
                tm.Lottery = lotteryFF.Lt_Lotterys[i].Lottery;
                tendency.AddTendency(tm);//添加趋势记录
            }
        }
        #endregion

        #region 前二、后二趋势分析
        /// <summary>
        /// 开奖趋势分析
        /// </summary>
        private void analyze2Tendency(Lottery lottery, Tendency<Tendency2Model> tendency, int index1, int index2)
        {
            tendency.ClearTendencys();//清空记录
            Tendency2Model tm;

            for (int i = lottery.Lt_Lotterys.Count - 1; i >= 0; i--)
            {
                tm = new Tendency2Model();
                tm.Lottery = lottery.Lt_Lotterys[i].Lottery;
                tm.Big = At.BigNum(lottery.Lt_Lotterys, i, index1, index2);//大大
                tm.BigSmall = At.BigSmallNum(lottery.Lt_Lotterys, i, index1, index2);//大小
                tm.SmallBig = At.SmallBigNum(lottery.Lt_Lotterys, i, index1, index2);//小大
                tm.Small = At.SmallNum(lottery.Lt_Lotterys, i, index1, index2);//小小

                tm.Odd = At.OddPairNum(lottery.Lt_Lotterys, i, index1, index2, 1, 1);//奇奇
                tm.OddPair = At.OddPairNum(lottery.Lt_Lotterys, i, index1, index2, 1, 0);//奇偶
                tm.PairOdd = At.OddPairNum(lottery.Lt_Lotterys, i, index1, index2, 0, 1);//偶奇
                tm.Pair = At.OddPairNum(lottery.Lt_Lotterys, i, index1, index2, 0, 0);//偶偶
                tm.Dbl = At.DblNum(lottery.Lt_Lotterys, i, index1, index2);//重数

                tm.Sno = (lottery.Lt_Lotterys[i].Sno.ToString() + "期").PadLeft(5, '0');
                tm.Dtime = lottery.Lt_Lotterys[i].Dtime;

                tendency.AddTendency(tm);//添加趋势记录
            }
        }
        #endregion

        #region 五星趋势分析
        private void analyze5Tendency(Lottery lottery, Tendency<Tendency5Model> tendency)
        {
            tendency.ClearTendencys();//清空记录
            Tendency5Model tm;

            for (int i = lottery.Lt_Lotterys.Count - 1; i >= 0; i--)
            {
                tm = new Tendency5Model();
                tm.FiveStart = At.FiveStart(lottery.Lt_Lotterys, i);
                tm.Sno = (lottery.Lt_Lotterys[i].Sno.ToString() + "期").PadLeft(5, '0');
                tm.Dtime = lottery.Lt_Lotterys[i].Dtime;
                tendency.AddTendency(tm);//添加趋势记录
            }
        }
        #endregion

        private void analyzeAllTendency(Lottery lottery, Tendency<TendencyAllModel> tendency)
        {
            tendency.ClearTendencys();//清空记录
            TendencyAllModel tm;

            for (int i = lottery.Lt_Lotterys.Count - 1; i >= 0; i--)
            {
                tm = new TendencyAllModel();
                tm.ThreeBeforeStart = At.UnitThree(lottery.Lt_Lotterys, i, 1, 2, 3);
                tm.SexBeforeStart = At.UnitSex(lottery.Lt_Lotterys, i, 1, 2, 3);

                tm.ThreeAfterStart = At.UnitThree(lottery.Lt_Lotterys, i, 3, 4, 5);
                tm.SexAfterStart = At.UnitSex(lottery.Lt_Lotterys, i, 3, 4, 5);

                tm.FiveStart = At.FiveStart(lottery.Lt_Lotterys, i);
                tm.Sno = (lottery.Lt_Lotterys[i].Sno.ToString() + "期").PadLeft(5, '0');
                tm.Dtime = lottery.Lt_Lotterys[i].Dtime;
                tendency.AddTendency(tm);//添加趋势记录
            }
        }
        #endregion

        private void statisticsFF(Lottery lottery, string filePath)
        {
            //读取文件，获取开奖记录
            readFile(lottery, filePath);

            try
            {
                analyzeTendency();
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message);
            }

            //给控件赋值
            initControlFF();
        }

        #endregion

        #region 给控件赋值
        private void initControlFF()
        {
            initFFLabel();

            initDgvDigit();

            initDgvFF();

            initDgvAll();
        }

        private void initFFLabel()
        {
            if (lotteryFF.Lt_Lotterys.Count > 0)
            {
                //本次开奖号码
                LotteryModel lm = lotteryFF.Lt_Lotterys[0];
                this.lblFFSno.Text = lm.Sno + "期";
                this.lblFF1.Text = lm.Num1.ToString();
                this.lblFF2.Text = lm.Num2.ToString();
                this.lblFF3.Text = lm.Num3.ToString();
                this.lblFF4.Text = lm.Num4.ToString();
                this.lblFF5.Text = lm.Num5.ToString();
                this.lblFFTime.Text = lm.Dtime;
            }
        }

        /// <summary>
        /// 定位胆数字
        /// </summary>
        private void initDgvDigit()
        {
            if (dwdDigitTendencys.Lt_Tendencys.Count <= 0) return;

            DgvController.AddRows(this.dgvDigit, 2);
            this.dgvDigit[0, 0].Value = "定大";
            this.dgvDigit[0, 1].Value = "定数";

            DgvController.SetDgvValue(this.dgvDigit, XsMath.GetMaxTendency(dwdDigitTendencys.Lt_Tendencys), 0);//定大
            DgvController.SetDgvValue(this.dgvDigit, dwdDigitTendencys.CurrTendency, 1);//定数

            List<int> lt = new List<int>();

            for (int j = 1; j <= 10; j++)
            {
                int value = Convert.ToInt32(this.dgvDigit[j, 1].Value.ToString());
                lt.Add(value);
                if (value == 0) DgvController.SetDgvBackColorStyle(this.dgvDigit, 1, j, Color.LightGray, 12);
            }

            int max = lt.OrderByDescending(l => l).ToList()[0];

            for (int j = 1; j <= 10; j++)
            {
                int value = Convert.ToInt32(this.dgvDigit[j, 1].Value.ToString());
                if (value == max) DgvController.SetDgvBackColorStyle(this.dgvDigit, 1, j, Color.Yellow, 12);
            }
        }

        /// <summary>
        /// 前二、后二趋势
        /// </summary>
        private void initDgvFF()
        {
            if (befor2FFTendency.Lt_Tendencys.Count <= 0) return;

            DgvController.AddRows(this.dgvFF, 6);//添加记录
            DgvController.SetDgvValue(this.dgvFF, DgvController.Lt_Type, 0);

            DgvController.SetDgvValue(this.dgvFF, XsMath.GetMaxTendency(befor2FFTendency.Lt_Tendencys), 0);//前大
            DgvController.SetDgvValue(this.dgvFF, ((Tendency<Tendency1Model>)this.lt_dwds[0]).CurrTendency, ((Tendency<Tendency1Model>)this.lt_dwds[1]).CurrTendency, 1);//前定

            WinLottery wl = new WinLottery();
            DgvController.SetDgvValue(this.dgvFF, befor2FFTendency.CurrTendency, 2, wl.CalculatorWinLottery(this.befor2FFTendency.Lt_Tendencys));//前二
            DgvController.SetDgvValue(this.dgvFF, after2FFTendency.CurrTendency, 3, wl.CalculatorWinLottery(this.after2FFTendency.Lt_Tendencys));//后二


            DgvController.SetDgvValue(this.dgvFF, ((Tendency<Tendency1Model>)this.lt_dwds[3]).CurrTendency, ((Tendency<Tendency1Model>)this.lt_dwds[4]).CurrTendency, 4);//后定
            DgvController.SetDgvValue(this.dgvFF, XsMath.GetMaxTendency(after2FFTendency.Lt_Tendencys), 5);//后大

            DgvController.RefreshDgvColor(this.dgvFF, 2);
            DgvController.RefreshDgvColor(this.dgvFF, 3);
        }

        private void initDgvAll()
        {
            DgvController.AddRows(this.dgvAll, 2);
            this.dgvAll[0, 0].Value = "星大";
            this.dgvAll[0, 1].Value = "星值";

            DgvController.SetDgvValue(this.dgvAll, XsMath.GetMaxTendency(allFFTendency.Lt_Tendencys), 0);//星大
            DgvController.SetDgvValue(this.dgvAll, allFFTendency.CurrTendency, 1);//星值
            DgvController.RefreshDgvColor(this.dgvAll, 1);
        }

        /// <summary>
        /// 清空控件值 
        /// </summary>
        private void clearFFCtrl()
        {
            this.lblFFSno.Text = "XXXX期";
            this.lblFF1.Text = "X";
            this.lblFF2.Text = "X";
            this.lblFF3.Text = "X";
            this.lblFF4.Text = "X";
            this.lblFF5.Text = "X";
            this.lblFFTime.Text = "01/01 12:00";

            this.dgvDigit.Rows.Clear();
            this.dgvFF.Rows.Clear();
            this.dgvAll.Rows.Clear();
        }
        #endregion
    }
}
