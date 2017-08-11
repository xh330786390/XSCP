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
    public partial class FormServieStatistic : Form
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

        public FormServieStatistic()
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

        private void dgvFF_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == 2 || e.RowIndex == 3)
            {
                if (e.ColumnIndex == 11)
                {
                    FormTendency ft = new FormTendency("分分彩-" + this.dgvFF[0, e.RowIndex].Value.ToString());
                    if (e.RowIndex == 2) ft.Tendency = this.beforFFTendency;
                    else ft.Tendency = this.afterFFTendency;
                    ft.Show();
                }

                if (e.ColumnIndex == 12)
                {
                    FormTj ft = new FormTj("分分彩-" + this.dgvFF[0, e.RowIndex].Value.ToString());
                    if (e.RowIndex == 2) ft.Tendency = this.beforFFTendency;
                    else ft.Tendency = this.afterFFTendency;
                    ft.Show();
                }
            }


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
                if (e.RowIndex == 1) { ft.DwdTendency1 = DwdFFTendency1; ft.DwdTendency2 = DwdFFTendency2; }
                else { ft.DwdTendency1 = DwdFFTendency4; ft.DwdTendency2 = DwdFFTendency5; }
                ft.Show();
            }
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

        private void rbtnBefore_CheckedChanged(object sender, EventArgs e)
        {
            if (this.rbtnBefore.Checked)
            {
                this.initDgv3(this.beforFFTendency);
            }
        }

        private void rbtnAfter_CheckedChanged(object sender, EventArgs e)
        {
            if (this.rbtnAfter.Checked)
            {
                this.initDgv3(this.afterFFTendency);
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
                tm.Lottery = lottery.Lt_Lotterys[i].Lottery;
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
                tm.Lottery = lottery.Lt_Lotterys[i].Lottery;
                tm.Big = at.BigNum(lottery.Lt_Lotterys, i, index);//大
                tm.Small = at.SmallNum(lottery.Lt_Lotterys, i, index);//小

                tm.Odd = at.OddPairNum(lottery.Lt_Lotterys, i, index, 1);//奇
                tm.Pair = at.OddPairNum(lottery.Lt_Lotterys, i, index, 0);//偶

                tm.Sno = (lottery.Lt_Lotterys[i].Sno.ToString() + "期").PadLeft(5, '0');
                tm.Dtime = lottery.Lt_Lotterys[i].Dtime;

                dwdTendency.AddDwdTendency(tm);//添加定位胆趋势记录
            }
        }
        #endregion

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
        private void initControlFF()
        {
            initFFLabel();
            initDgvFF();

            if (this.rbtnBefore.Checked)
                this.initDgv3(this.beforFFTendency);
            else if (this.rbtnAfter.Checked)
                this.initDgv3(this.afterFFTendency);
        }

        private void init()
        {
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

        #region 开奖组合
        private void initDgv3(Tendency tendency)
        {
            object objBigSmall = null;
            object objOddPair = null;

            DgvController.AddRows(this.dgv3, 4);
            if (tendency.Lt_Tendencys.Count > 0)
            {
                for (int i = 0; i < UnitTendency.Lt_BigSmallUnits.Count; i++)
                {
                    this.dgv3[0, i].Value = UnitTendency.Lt_BigSmallUnits[i].ChName;
                    for (int j = 0; j < UnitTendency.Lt_OddPairUnits.Count; j++)
                    {
                        objBigSmall = Common.Reflection.GetPropertyValue(typeof(TendencyModel), tendency.CurrTendency, UnitTendency.Lt_BigSmallUnits[i].EnName);
                        objOddPair = Common.Reflection.GetPropertyValue(typeof(TendencyModel), tendency.CurrTendency, UnitTendency.Lt_OddPairUnits[j].EnName);
                        object[] param = { objBigSmall, objOddPair };
                        this.dgv3[j + 1, i].Value = string.Format(getUinitInfo(i, j), param);//写值
                    }
                }

                for (int i = 0; i < this.dgv3.Rows.Count; i++)
                {
                    for (int j = 1; j < this.dgv3.Columns.Count; j++)
                    {
                        if ((i == 1 || i == 2) && (j == 2 || j == 3)) continue;
                        DgvController.SetDgvBackColorStyle(this.dgv3, i, j, Color.LightSteelBlue, 11);//设置单元格的格式
                    }
                }

                string zero = "【0,0】";
                for (int i = 0; i < this.dgv3.Rows.Count; i++)
                {
                    for (int j = 1; j < this.dgv3.Columns.Count; j++)
                    {

                        if (this.dgv3[j, i].Value.ToString().Contains(zero))
                            DgvController.SetDgvBackColorStyle(this.dgv3, i, j, Color.Green, 12);//设置单元格的格式
                    }
                }
            }
        }

        private string getUinitInfo(int row, int col)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(UnitTendency.Lt_BigSmallUnits[row].ChName);
            sb.Append(UnitTendency.Lt_OddPairUnits[col].ChName);
            sb.Append("【{0},{1}】");
            return sb.ToString(); ;
        }
        #endregion
    }
}
