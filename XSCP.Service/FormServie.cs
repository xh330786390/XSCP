using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using XSCP.Service.Dbole;
using XSCP.Service.BLL;
using XSCP.Service.Common;
using XSCP.Service.Controllers;
using XSCP.Service.Model;
using System.IO;

namespace XSCP.Service
{
    public delegate void LotteryServiceDelegate(bool success);

    public partial class FormServie : Form
    {
        #region 字段定义
        /// <summary>
        /// 3D彩开奖号
        /// </summary>
        private Lottery lottery3D = new Lottery3D();

        /// <summary>
        /// 分分彩开奖号
        /// </summary>
        private Lottery lotteryFF = new LotteryFF();

        /// <summary>
        /// 3D彩-前二走势
        /// </summary>
        private Tendency befor3DTendency = new Tendency();

        /// <summary>
        /// 3D彩-后二走势
        /// </summary>
        private Tendency after3DTendency = new Tendency();

        /// <summary>
        /// 分分彩-前二走势
        /// </summary>
        private Tendency beforFFTendency = new Tendency();

        /// <summary>
        /// 分分彩-后二走势
        /// </summary>
        private Tendency afterFFTendency = new Tendency();

        /// <summary>
        /// 3D彩-定位胆走势 第一位
        /// </summary>
        private DwdTendency Dwd3DTendency1 = new DwdTendency();

        /// <summary>
        /// 3D彩-定位胆走势 第二位
        /// </summary>
        private DwdTendency Dwd3DTendency2 = new DwdTendency();

        /// <summary>
        /// 3D彩-定位胆走势 第三位
        /// </summary>
        private DwdTendency Dwd3DTendency3 = new DwdTendency();

        /// <summary>
        /// 分分彩-定位胆走势 第一位
        /// </summary>
        private DwdTendency DwdFFTendency1 = new DwdTendency();

        /// <summary>
        /// 分分彩-定位胆走势 第一位
        /// </summary>
        private DwdTendency DwdFFTendency2 = new DwdTendency();

        /// <summary>
        /// 分分彩-定位胆走势 第四位
        /// </summary>
        private DwdTendency DwdFFTendency4 = new DwdTendency();

        /// <summary>
        /// 分分彩-定位胆走势 第五位
        /// </summary>
        private DwdTendency DwdFFTendency5 = new DwdTendency();

        /// <summary>
        /// 进入监控
        /// </summary>
        private int enterNum = 0;

        /// <summary>
        /// 文件是否启动监听
        /// </summary>
        private bool isAddEvent = false;

        private Thread threadMain;

        #endregion
        public FormServie()
        {
            InitializeComponent();
        }

        private void tsBtnLogin_Click(object sender, EventArgs e)
        {
            FormLogin fl = new FormLogin();
            fl.LotteryServiceDelegate += LotteryService;
            fl.ShowDialog();

            //try
            //{
            //    new Common.TransService().Logout();
            //}
            //catch
            //{
            //}
            //finally
            //{
            //    Param.Count = 0;
            //}
            //}
        }

        private void FormServie_Load(object sender, EventArgs e)
        {

        }

        #region 请求自动获取
        private void LotteryService(bool success)
        {
            this.tsBtnLogin.Text = "退出";
            if (success)
            {
                Param.FilePath3D = this.fsw3D.Path + "d3cp" + this.dtp.Value.ToString("yyyyMMdd") + ".txt";
                Param.FilePathFF = this.fswFF.Path + "ffcp" + this.dtp.Value.ToString("yyyyMMdd") + ".txt";

                threadMain = new Thread(new ThreadStart(excute));
                threadMain.IsBackground = true;
                threadMain.Start();
            }
        }

        private void excute()
        {
            while (true)
            {
                try
                {
                    excutXscp(Param.FilePath3D, "17");
                    //new Common.TransService().LotteryService();
                    System.Threading.Thread.Sleep(30000);
                }
                catch
                {

                }
            }
        }

        /// <summary>
        /// 获取新生分分彩票
        /// </summary>
        private void excutXscp(string filePath, string id)
        {
            string result = new Common.TransService().CurrentRecord(id, "10");
            result = XscpControllers.GetXscpData(result);
            int maxFileSno = readFileFirstLine(filePath);
            int curSno = getSno(result.Split('\n')[0]);
            if (curSno - maxFileSno > 0)
            {
                result = new Common.TransService().CurrentRecord(id, (curSno - maxFileSno).ToString());
                result = XscpControllers.GetXscpData(result);

                List<string> lt = new List<string>();
                string[] arr = result.Split('\n');
                foreach (string s in arr) lt.Add(s);
                using (StreamReader sr = new StreamReader(filePath, Encoding.Default))
                {
                    String line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        if (string.IsNullOrEmpty(line)) continue;
                        lt.Add(line.Replace("期", ","));
                    }
                    sr.Close();
                    sr.Dispose();
                }
                using (StreamWriter sw = new StreamWriter(filePath, false, Encoding.Default))
                {
                    lt.ForEach(f => sw.WriteLine(f));
                    sw.Close();
                    sw.Dispose();
                }
            }
        }

        /// <summary>
        /// 获取新生3D彩票
        /// </summary>
        private void excutXscp3d()
        {
            //string result = new Common.TransService().CurrentRecord("17", "10");
            //result = Transfer.GetXscpData(result);
            //Xscp3D xs = new Xscp3D();
            //try
            //{
            //    xs.Add(result);
            //}
            //catch
            //{
            //}
        }
        #endregion

        private void tsBtnSet_Click(object sender, EventArgs e)
        {
            FormSystemSet fs = new FormSystemSet();
            fs.ShowDialog();
        }

        private void dgv3D_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 11 && (e.RowIndex == 2 || e.RowIndex == 3))
            {
                FormTendency ft = new FormTendency("3D彩-" + this.dgv3D[0, e.RowIndex].Value.ToString());
                if (e.RowIndex == 2) ft.Tendency = this.befor3DTendency;
                else ft.Tendency = this.after3DTendency;
                ft.Show();
            }

            if (e.ColumnIndex == 12 && (e.RowIndex == 2 || e.RowIndex == 3))
            {
                FormTj ft = new FormTj("3D彩-" + this.dgv3D[0, e.RowIndex].Value.ToString());
                if (e.RowIndex == 2) ft.Tendency = this.befor3DTendency;
                else ft.Tendency = this.after3DTendency;
                ft.Show();
            }
        }

        private void dgvFF_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 11 && (e.RowIndex == 2 || e.RowIndex == 3))
            {
                FormTendency ft = new FormTendency("分分彩-" + this.dgvFF[0, e.RowIndex].Value.ToString());
                if (e.RowIndex == 2) ft.Tendency = this.beforFFTendency;
                else ft.Tendency = this.afterFFTendency;
                ft.Show();
            }

            if (e.ColumnIndex == 12 && (e.RowIndex == 2 || e.RowIndex == 3))
            {
                FormTj ft = new FormTj("分分彩-" + this.dgv3D[0, e.RowIndex].Value.ToString());
                if (e.RowIndex == 2) ft.Tendency = this.beforFFTendency;
                else ft.Tendency = this.afterFFTendency;
                ft.Show();
            }
        }

        #region 按钮事件
        private void tsMenuStart_Click(object sender, EventArgs e)
        {
            if (!isAddEvent)
            {
                this.fsw3D.Path = Param.FileBase + @"D3彩\";
                this.fsw3D.Filter = "*.txt";
                this.fsw3D.Changed += new FileSystemEventHandler(fsw3D_Changed);
                this.fsw3D.NotifyFilter = NotifyFilters.LastWrite;
                this.fsw3D.EnableRaisingEvents = true;

                this.fswFF.Path = Param.FileBase + @"分分彩\";
                this.fswFF.Filter = "*.txt";
                this.fswFF.Changed += new FileSystemEventHandler(fswFF_Changed);
                this.fswFF.NotifyFilter = NotifyFilters.LastWrite;
                this.fswFF.EnableRaisingEvents = true;
                isAddEvent = true;

                warning();//预警
            }
        }

        private void tsMenuStop_Click(object sender, EventArgs e)
        {
            if (isAddEvent)
            {
                XscpWarning.Lt_Warnings.Clear();
                this.fswFF.Changed -= new FileSystemEventHandler(fswFF_Changed);
                this.fswFF.EnableRaisingEvents = false;

                this.fsw3D.Changed -= new FileSystemEventHandler(fsw3D_Changed);
                this.fsw3D.EnableRaisingEvents = false;
                isAddEvent = false;
            }
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

        private void fsw3D_Changed(object sender, FileSystemEventArgs e)
        {
            try
            {
                enterNum++;
                if (enterNum % 4 == 0)
                {
                    Param.FilePath3D = this.fsw3D.Path + "d3cp" + this.dtp.Value.ToString("yyyyMMdd") + ".txt";
                    statistics3D(lottery3D, Param.FilePath3D);
                    enterNum = 0;
                }
            }
            catch
            {
                enterNum = 0;
            }
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
        private int readFileFirstLine(string fileName)
        {
            using (StreamReader sr = new StreamReader(fileName, Encoding.Default))
            {
                String line;
                while ((line = sr.ReadLine()) != null)
                {
                    if (string.IsNullOrEmpty(line)) continue;
                    else
                    {
                        return getSno(line);
                    }
                }
                sr.Close();
                sr.Dispose();
            }
            return 0;
        }

        /// <summary>
        /// 获取最大的开奖期数
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        private int getSno(string line)
        {
            if (line.Contains("-"))
            {
                line = line.Split('-')[1];
            }
            string[] array = line.Replace("期", ",").Replace("，", ",").Split(',');
            int[] arrayint = new int[array.Length];
            return Convert.ToInt32(array[0]);
        }

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
        /// 开奖趋势分析
        /// </summary>
        private void analyzeTendency(Lottery lottery, Tendency tendency, int index1, int index2)
        {
            tendency.ClearTendencys();//清空记录
            TendencyModel tm;

            AnalyzeTendency at = new AnalyzeTendency();
            for (int i = lottery.Lt_Lotterys.Count - 1; i >= 0; i--)
            {
                tm = new TendencyModel();
                tm.ID = i + 1;
                tm.Big = at.BigNum(lottery.Lt_Lotterys, i, index1, index2);//大大
                tm.BigSmall = at.BigSmallNum(lottery.Lt_Lotterys, i, index1, index2);//大小
                tm.SmallBig = at.SmallBigNum(lottery.Lt_Lotterys, i, index1, index2);//小大
                tm.Small = at.SmallNum(lottery.Lt_Lotterys, i, index1, index2);//小小

                tm.Odd = at.OddPairNum(lottery.Lt_Lotterys, i, index1, index2, 1, 1);//奇奇
                tm.OddPair = at.OddPairNum(lottery.Lt_Lotterys, i, index1, index2, 1, 0);//奇偶
                tm.PairOdd = at.OddPairNum(lottery.Lt_Lotterys, i, index1, index2, 0, 1);//偶奇
                tm.Pair = at.OddPairNum(lottery.Lt_Lotterys, i, index1, index2, 0, 0);//偶偶
                tm.Dbl = at.DblNum(lottery.Lt_Lotterys, i, index1, index2);//重数

                tm.SNO = (lottery.Lt_Lotterys[i].Sno.ToString() + "期").PadLeft(5, '0');
                tm.Dtime = lottery.Lt_Lotterys[i].Dtime;

                tendency.AddTendency(tm);//添加趋势记录
            }
        }

        #endregion

        #region 定位胆开奖趋势分析
        /// <summary>
        /// 定位胆开奖趋势分析
        /// </summary>
        private void analyzeDwdTendency(Lottery lottery, DwdTendency dwdTendency, int index)
        {
            dwdTendency.ClearDwdTendencys();//清空记录
            DwdTendencyModel tm;

            AnalyzeDwdTendency at = new AnalyzeDwdTendency();
            for (int i = lottery.Lt_Lotterys.Count - 1; i >= 0; i--)
            {
                tm = new DwdTendencyModel();
                tm.Big = at.BigNum(lottery.Lt_Lotterys, i, index);//大
                tm.Small = at.SmallNum(lottery.Lt_Lotterys, i, index);//小

                tm.Odd = at.OddPairNum(lottery.Lt_Lotterys, i, index, 1);//奇
                tm.Pair = at.OddPairNum(lottery.Lt_Lotterys, i, index, 0);//偶
                dwdTendency.AddDwdTendency(tm);//添加定位胆趋势记录
            }
        }
        #endregion

        /// <summary>
        /// 统计
        /// </summary>
        private void statistics3D(Lottery lottery, string filePath)
        {
            //读取文件，获取开奖记录
            readFile(lottery, filePath);

            //开奖趋势分析
            analyzeTendency(lottery, befor3DTendency, 1, 2);
            analyzeTendency(lottery, after3DTendency, 2, 3);

            //分析定位胆开奖走势，记录信息
            analyzeDwdTendency(lottery, Dwd3DTendency1, 1);
            analyzeDwdTendency(lottery, Dwd3DTendency2, 2);
            analyzeDwdTendency(lottery, Dwd3DTendency3, 3);

            //给控件赋值
            initControl3D();
        }

        private void statisticsFF(Lottery lottery, string filePath)
        {
            //读取文件，获取开奖记录
            readFile(lottery, filePath);

            //开奖趋势分析
            analyzeTendency(lottery, beforFFTendency, 1, 2);
            analyzeTendency(lottery, afterFFTendency, 4, 5);

            //分析定位胆开奖走势，记录信息
            analyzeDwdTendency(lottery, DwdFFTendency1, 1);
            analyzeDwdTendency(lottery, DwdFFTendency2, 2);
            analyzeDwdTendency(lottery, DwdFFTendency4, 4);
            analyzeDwdTendency(lottery, DwdFFTendency5, 5);

            //给控件赋值
            initControlFF();
        }

        #endregion

        #region 给控件赋值
        private void initControl3D()
        {
            init3DLabel();
            initDgv3D();
        }

        private void initControlFF()
        {
            initFFLabel();
            initDgvFF();
        }

        private void init3DLabel()
        {
            if (lottery3D.Lt_Lotterys.Count > 0)
            {
                //本次开奖号码
                LotteryModel lm = lottery3D.Lt_Lotterys[0];
                this.lbl3DSno.Text = lm.Sno + "期";
                this.lbl3D1.Text = lm.Num1.ToString();
                this.lbl3D2.Text = lm.Num2.ToString();
                this.lbl3D3.Text = lm.Num3.ToString();
                this.lbl3DTime.Text = lm.Dtime;
            }
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

        private void initDgv3D()
        {
            DgvController.AddRows(this.dgv3D, 6);//添加记录数
            DgvController.SetDgvValue(this.dgv3D, DgvController.Lt_Type, 0);//添加类型

            DgvController.SetDgvValue(this.dgv3D, befor3DTendency.GetMaxTendency(), 0);//前大
            DgvController.SetDgvValue(this.dgv3D, this.Dwd3DTendency1.CurrDwdTendency, this.Dwd3DTendency2.CurrDwdTendency, 1);//前定

            WinLottery wl = new WinLottery();
            DgvController.SetDgvValue(this.dgv3D, befor3DTendency.CurrTendency, 2, wl.CalculatorWinLottery(this.befor3DTendency.Lt_Tendencys));//前二
            DgvController.SetDgvValue(this.dgv3D, after3DTendency.CurrTendency, 3, wl.CalculatorWinLottery(this.after3DTendency.Lt_Tendencys));//后二

            DgvController.SetDgvValue(this.dgv3D, this.Dwd3DTendency2.CurrDwdTendency, this.Dwd3DTendency3.CurrDwdTendency, 4);//后定
            DgvController.SetDgvValue(this.dgv3D, after3DTendency.GetMaxTendency(), 5);//后大

            DgvController.RefreshDgvColor(this.dgv3D, 2);
            DgvController.RefreshDgvColor(this.dgv3D, 3);

        }

        private void initDgvFF()
        {
            DgvController.AddRows(this.dgvFF, 6);//添加记录
            DgvController.SetDgvValue(this.dgvFF, DgvController.Lt_Type, 0);

            DgvController.SetDgvValue(this.dgvFF, beforFFTendency.GetMaxTendency(), 0);//前大
            DgvController.SetDgvValue(this.dgvFF, this.DwdFFTendency1.CurrDwdTendency, this.DwdFFTendency2.CurrDwdTendency, 1);//前定

            WinLottery wl = new WinLottery();
            DgvController.SetDgvValue(this.dgvFF, beforFFTendency.CurrTendency, 2, wl.CalculatorWinLottery(this.beforFFTendency.Lt_Tendencys));//前二
            DgvController.SetDgvValue(this.dgvFF, afterFFTendency.CurrTendency, 3, wl.CalculatorWinLottery(this.afterFFTendency.Lt_Tendencys));//后二

            DgvController.SetDgvValue(this.dgvFF, this.DwdFFTendency4.CurrDwdTendency, this.DwdFFTendency5.CurrDwdTendency, 4);//后定
            DgvController.SetDgvValue(this.dgvFF, afterFFTendency.GetMaxTendency(), 5);//后大

            DgvController.RefreshDgvColor(this.dgvFF, 2);
            DgvController.RefreshDgvColor(this.dgvFF, 3);
        }
        #endregion

        private void dtp_ValueChanged(object sender, EventArgs e)
        {
            if (isAddEvent)
            {
                Param.FilePath3D = this.fsw3D.Path + "d3cp" + this.dtp.Value.ToString("yyyyMMdd") + ".txt";

                if (!File.Exists(Param.FilePath3D)) clear3DCtrl();
                else
                {
                    statistics3D(lottery3D, Param.FilePath3D);
                }

                Param.FilePathFF = this.fswFF.Path + "ffcp" + this.dtp.Value.ToString("yyyyMMdd") + ".txt";
                if (!File.Exists(Param.FilePathFF)) clearFFCtrl();
                else
                {
                    statisticsFF(lotteryFF, Param.FilePathFF);
                }
            }
        }

        #region

        /// <summary>
        /// 清空控件值
        /// </summary>
        private void clear3DCtrl()
        {
            this.lbl3DSno.Text = "XXXX期";
            this.lbl3D1.Text = "X";
            this.lbl3D2.Text = "X";
            this.lbl3D3.Text = "X";
            this.lbl3DTime.Text = "01/01 12:00";

            this.dgv3D.Rows.Clear();

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

            this.dgvFF.Rows.Clear();
        }
        #endregion
    }
}
