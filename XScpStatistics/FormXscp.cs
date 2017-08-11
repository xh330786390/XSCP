using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using XScpStatistics.Common;
using XScpStatistics.Model;

namespace XScpStatistics
{
    public partial class FormXscp : Form
    {
        private EnumXscpType enumXscpType = EnumXscpType.分分彩;

        /// <summary>
        /// 复式类型
        /// </summary>
        public static EnumStyle EnumStyle = EnumStyle.后二;

        /// <summary>
        /// 新生彩模式
        /// </summary>
        public static XscpStyle xscpStyle = new XscpStyle();

        /// <summary>
        /// 进入监控
        /// </summary>
        private int enterNum = 0;

        private bool isAddEvent = false;

        public FormXscp()
        {
            InitializeComponent();
            init();//初始化控件
        }

        /// <summary>
        /// 初始化控件
        /// </summary>
        private void init()
        {
            this.rbtnAfter.Checked = true;//选中后二
            setXscpStyle(4, 5);
            this.dtp.Value = DateTime.Today;//初始化日期
            this.Location = new System.Drawing.Point(0, 0);
        }

        #region 日期设置
        /// <summary>
        /// 通过设置日期来确定读取文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dtp_ValueChanged(object sender, EventArgs e)
        {
            getFilePath();
        }
        #endregion

        #region 按钮事件
        /// <summary>
        /// 启动
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStart_Click(object sender, EventArgs e)
        {
            if (!isAddEvent)
            {
                //this.fsw.Path = Param.FileBase;
                this.fsw.Filter = "*.txt";
                this.fsw.Changed += new FileSystemEventHandler(fsw_Changed);
                fsw.NotifyFilter = NotifyFilters.LastWrite;
                this.fsw.EnableRaisingEvents = true;
                isAddEvent = true;

                checkWarmValue(this.textBox1);//绿
                checkWarmValue(this.textBox2);//黄
                checkWarmValue(this.textBox3);//红
            }
        }

        /// <summary>
        /// 停止
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStop_Click(object sender, EventArgs e)
        {
            if (isAddEvent)
            {
                DgvController.Lt_Warning.Clear();
                this.fsw.Changed -= new FileSystemEventHandler(fsw_Changed);
                this.fsw.EnableRaisingEvents = false;
                isAddEvent = false;
            }
        }

        /// <summary>
        /// 选择后二
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rbtnAfter_CheckedChanged(object sender, EventArgs e)
        {
            if (this.rbtnAfter.Checked)
            {
                EnumStyle = EnumStyle.后二;
                refreshXscpStyle();
            }

            if (isAddEvent)
            {
                statistics();
            }
        }

        /// <summary>
        /// 选择前二
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rbtnBefore_CheckedChanged(object sender, EventArgs e)
        {
            if (this.rbtnBefore.Checked)
            {
                EnumStyle = EnumStyle.前二;
                refreshXscpStyle();
            }
            if (isAddEvent)
            {
                statistics();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string text = this.tbCtlXscp.TabPages[this.tbCtlXscp.SelectedIndex].Text;
            if (this.rbtnAfter.Checked) text += "-后二";
            else text += "-前二";
            FormTendency ft = new FormTendency(text);
            ft.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FormDWD ft = new FormDWD();
            ft.Show();
        }

        private void tbCtlXscp_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (this.tbCtlXscp.SelectedIndex)
            {
                case 0:
                    this.enumXscpType = EnumXscpType.分分彩;
                    break;
                case 1:
                    this.enumXscpType = EnumXscpType.两分彩;
                    break;
                case 2:
                    this.enumXscpType = EnumXscpType.D3彩;
                    break;
            }

            refreshXscpStyle();//刷新
        }
        #endregion

        #region 监测事件
        private void fsw_Changed(object sender, FileSystemEventArgs e)
        {
            try
            {
                enterNum++;
                if (enterNum % 2 == 0)
                {
                    statistics();
                    enterNum = 0;
                }
            }
            catch
            {
                enterNum = 0;
            }
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
                DgvController.Lt_Warning.Add(num);
                DgvController.Lt_Color.Add(textBox.BackColor);
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
        #endregion

        #region 核心方法
        #region 读取文件
        private void readFile()
        {
            Lottery.ClearLotterys();// 清空开奖记录

            StreamReader sr = new StreamReader(Param.FilePath, Encoding.Default);
            String line;
            while ((line = sr.ReadLine()) != null)
            {
                if (string.IsNullOrEmpty(line)) continue;
                Lottery.AddLottery(line);
            }
            sr.Close();
        }
        #endregion

        #region 分析开奖趋势
        private string getDate(int num)
        {
            string strDate = "";
            string md = this.dtp.Value.ToString("MM/dd");
            int hour = 8 + num / 60;
            int minute = num % 60;
            if (hour > 24)
            {
                hour -= 24;
                md = this.dtp.Value.AddDays(1).ToString("MM/dd");
            }
            strDate = md + " " + hour.ToString().PadLeft(2, '0') + ":" + minute.ToString().PadLeft(2, '0');
            return strDate;
        }

        /// <summary>
        /// 前二、后二开奖趋势分析
        /// </summary>
        private void analyzeTendency()
        {
            Tendency.ClearTendencys();//清空记录
            TendencyModel tm;

            AnalyzeTendency at = new AnalyzeTendency();
            for (int i = Lottery.Lt_Lotterys.Count - 1; i >= 0; i--)
            {
                tm = new TendencyModel();
                tm.ID = i + 1;
                tm.Big = at.BigNum(i, xscpStyle.num1, xscpStyle.num2);//大大
                tm.BigSmall = at.BigSmallNum(i, xscpStyle.num1, xscpStyle.num2);//大小
                tm.SmallBig = at.SmallBigNum(i, xscpStyle.num1, xscpStyle.num2);//小大
                tm.Small = at.SmallNum(i, xscpStyle.num1, xscpStyle.num2);//小小

                tm.Odd = at.OddPairNum(i, xscpStyle.num1, xscpStyle.num2, 1, 1);//奇奇
                tm.OddPair = at.OddPairNum(i, xscpStyle.num1, xscpStyle.num2, 1, 0);//奇偶
                tm.PairOdd = at.OddPairNum(i, xscpStyle.num1, xscpStyle.num2, 0, 1);//偶奇
                tm.Pair = at.OddPairNum(i, xscpStyle.num1, xscpStyle.num2, 0, 0);//偶偶
                tm.Dbl = at.DblNum(i, xscpStyle.num1, xscpStyle.num2);//重数

                tm.SNO = (Lottery.Lt_Lotterys[i].sno.ToString() + "期").PadLeft(5, '0');
                tm.Dt = getDate(Lottery.Lt_Lotterys[i].sno);

                Tendency.AddTendency(tm);//添加趋势记录
            }
        }

        /// <summary>
        /// 定位胆开奖趋势分析
        /// </summary>
        private void analyzeDwdTendency()
        {
            DwdTendency.ClearDwdTendencys();//清空记录
            DwdTendencyModel tm;

            DwdTendency dt = new DwdTendency();
            int count = enumXscpType == EnumXscpType.D3彩 ? 3 : 5;

            for (int k = 1; k <= count; k++)
            {
                for (int i = Lottery.Lt_Lotterys.Count - 1; i >= 0; i--)
                {
                    tm = new DwdTendencyModel();
                    tm.Big = dt.BigNum(i, k);//大
                    tm.Small = dt.SmallNum(i, k);//小

                    tm.Odd = dt.OddPairNum(i, 1, k);//奇
                    tm.Pair = dt.OddPairNum(i, 0, k);//偶
                    tm.type = k;//类型

                    DwdTendency.AddDwdTendency(tm);//添加定位胆趋势记录
                }
            }
        }
        #endregion

        #region 预测数据，趋势的和、差值
        public void initForecast()
        {
            Forecast.InitForecast();
        }
        #endregion

        #region 组合总共开奖的次数
        public void calculatorWinLottery()
        {
            WinLottery.CalculatorWinLottery();
        }
        #endregion

        #endregion
        /// <summary>
        /// 统计
        /// </summary>
        private void statistics()
        {
            readFile();//读取文件，获取开奖记录

            analyzeTendency();//分析开奖走势，记录信息

            analyzeDwdTendency();//分析定位胆开奖走势，记录信息

            initForecast();//统计数据

            calculatorWinLottery();//组合总共开奖的次数

            initControl();//给控件赋值
        }

        #region 给控件赋值
        private void initControl()
        {
            initLabel();
            initDgv1();
            initDgv2();
        }

        private void initLabel()
        {
            if (Lottery.Lt_Lotterys.Count > 0)
            {
                //本次开奖号码
                LotteryModel lm = Lottery.Lt_Lotterys[0];
                this.lbl1.Text = lm.num1.ToString();
                this.lbl2.Text = lm.num2.ToString();
                this.lbl3.Text = lm.num3.ToString();
                this.lbl4.Text = lm.num4.ToString();
                this.lbl5.Text = lm.num5.ToString();
            }

            if (Tendency.Lt_Tendencys.Count > 0)
            {
                //本次未开奖走势
                TendencyModel tm = Tendency.CurrTendency;
                this.label1.Text = tm.SNO;
                this.lblBig.Text = tm.Big.ToString();
                this.lblBigSmall.Text = tm.BigSmall.ToString();
                this.lblSmallBig.Text = tm.SmallBig.ToString();
                this.lblSmall.Text = tm.Small.ToString();

                this.lblOdd.Text = tm.Odd.ToString();
                this.lblOddPair.Text = tm.OddPair.ToString();
                this.lblPairOdd.Text = tm.PairOdd.ToString();
                this.lblPair.Text = tm.Pair.ToString();

                this.lblDbl.Text = tm.Dbl.ToString();
            }

            if (DwdTendency.Lt_Dwd.Count > 0)
            {
                DwdTendencyModel dtm1 = null;
                DwdTendencyModel dtm2 = null;

                if (this.enumXscpType == EnumXscpType.分分彩 || this.enumXscpType == EnumXscpType.两分彩)
                {
                    //本次定位胆未开奖走势
                    if (EnumStyle == Model.EnumStyle.后二)
                    {
                        dtm1 = DwdTendency.GetCurrentDwdTendency(4);
                        dtm2 = DwdTendency.GetCurrentDwdTendency(5);
                    }
                    //本次定位胆未开奖走势
                    else if (EnumStyle == Model.EnumStyle.前二)
                    {
                        dtm1 = DwdTendency.GetCurrentDwdTendency(1);
                        dtm2 = DwdTendency.GetCurrentDwdTendency(2);
                    }
                }
                else
                {
                    //本次定位胆未开奖走势
                    if (EnumStyle == Model.EnumStyle.后二)
                    {
                        dtm1 = DwdTendency.GetCurrentDwdTendency(2);
                        dtm2 = DwdTendency.GetCurrentDwdTendency(3);
                    }
                    //本次定位胆未开奖走势
                    else if (EnumStyle == Model.EnumStyle.前二)
                    {
                        dtm1 = DwdTendency.GetCurrentDwdTendency(1);
                        dtm2 = DwdTendency.GetCurrentDwdTendency(2);
                    }
                }

                this.lblDwdBig.Text = dtm1.Big.ToString() + dtm2.Big.ToString();
                this.lblDwdSmall.Text = dtm1.Small.ToString() + dtm2.Small.ToString();

                this.lblDwdBigSmall.Text = dtm1.Big.ToString() + dtm2.Small.ToString();
                this.lblDwdSmallBig.Text = dtm1.Small.ToString() + dtm2.Big.ToString();

                this.lblDwdOdd.Text = dtm1.Odd.ToString() + dtm2.Odd.ToString();
                this.lblDwdPair.Text = dtm1.Pair.ToString() + dtm2.Pair.ToString();
                this.lblDwdOddPair.Text = dtm1.Odd.ToString() + dtm2.Pair.ToString();
                this.lblDwdPairOdd.Text = dtm1.Pair.ToString() + dtm2.Odd.ToString();
            }
        }

        private void initDgv1()
        {
            DgvController.AddRows(this.dgv1, Forecast.Lt_Forecasts.Count);

            var vs = Forecast.Lt_Forecasts.OrderBy(l => l.Diff).ToList();
            for (int i = 0; i < vs.Count; i++)
            {
                this.dgv1[0, i].Value = i + 1;
                this.dgv1[1, i].Value = vs[i].UnitName;
                this.dgv1[2, i].Value = vs[i].Sum;
                this.dgv1[3, i].Value = vs[i].Diff;
                this.dgv1[4, i].Value = vs[i].Sum - vs[i].Diff;
                this.dgv1[5, i].Value = vs[i].Current;


                if (vs[i].Current >= DgvController.Lt_Warning[0])
                {
                    Color color = DgvController.GetWarningColor(vs[i].Current);
                    DgvController.SetDgvBackColorStyle(this.dgv1, i, 5, color, 11);
                }
            }
        }

        private void initDgv2()
        {
            DgvController.AddRows(this.dgv2, WinLottery.Lt_WinLotterys.Count);
            int sum = 0;
            WinLotteryModel wm;
            for (int i = 0; i < WinLottery.Lt_WinLotterys.Count; i++)
            {
                wm = WinLottery.Lt_WinLotterys[i];
                this.dgv2[0, i].Value = i + 1;
                this.dgv2[1, i].Value = wm.UnitName;
                this.dgv2[2, i].Value = wm.BigSmallValue;
                this.dgv2[3, i].Value = wm.OddPairValue;
                //this.dgv2[1, i].Value = wm.UnitName + "【" + wm.BigSmallValue + "," + wm.OddPairValue + "】";
                //this.dgv2[2, i].Value = wm.Sum;
                //if (wm.UnitName != null && !wm.UnitName.Contains("重"))
                //    sum += wm.Sum;
            }
        }

        private string getUinitInfo(int row, int col)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(Tendency.Lt_BigSmallNames[row]);
            sb.Append(Tendency.Lt_OddPairNames[col]);
            sb.Append("【");
            sb.Append(CurrentTendency.Lt_CurrentBS[row].Value);
            sb.Append(",");
            sb.Append(CurrentTendency.Lt_CurrentOP[col].Value);
            sb.Append("】");
            return sb.ToString(); ;
        }
        #endregion

        private void refreshXscpStyle()
        {
            if (this.enumXscpType == EnumXscpType.分分彩 || this.enumXscpType == EnumXscpType.两分彩)
            {
                if (EnumStyle == Model.EnumStyle.后二) setXscpStyle(4, 5);
                else if (EnumStyle == Model.EnumStyle.前二) setXscpStyle(1, 2);

                this.lbl4.Visible = true;
                this.lbl5.Visible = true;
            }
            else
            {
                if (EnumStyle == Model.EnumStyle.后二) setXscpStyle(2, 3);
                else if (EnumStyle == Model.EnumStyle.前二) setXscpStyle(1, 2);
                this.lbl4.Visible = false;
                this.lbl5.Visible = false;
            }

            getFilePath();
        }

        private void setXscpStyle(int num1, int num2)
        {
            xscpStyle.num1 = num1;
            xscpStyle.num2 = num2;
        }

        private void getFilePath()
        {
            string fileBase = string.Empty;
            string filePath = string.Empty;
            if (this.enumXscpType == EnumXscpType.分分彩)
            {
                fileBase = Param.FileBase + @"分分彩\";
                filePath = fileBase + "ffcp";
            }
            else if (this.enumXscpType == EnumXscpType.两分彩)
            {
                fileBase = Param.FileBase + @"两分彩\";
                filePath = fileBase + "lfcp";
            }
            else
            {
                fileBase = Param.FileBase + @"D3彩\";
                filePath = fileBase + "d3cp";
            }

            this.fsw.Path = fileBase;
            Param.FilePath = filePath + this.dtp.Value.ToString("yyyyMMdd") + ".txt";

            operateFile();
        }

        private void operateFile()
        {
            if (!File.Exists(Param.FilePath)) clearCtrl();
            else
            {
                if (isAddEvent) statistics();
            }
        }

        /// <summary>
        /// 清空控件值
        /// </summary>
        private void clearCtrl()
        {
            this.lbl1.Text = "X";
            this.lbl2.Text = "X";
            this.lbl3.Text = "X";
            this.lbl4.Text = "X";
            this.lbl5.Text = "X";

            this.lblBig.Text = "XX";
            this.lblBigSmall.Text = "XX";
            this.lblSmallBig.Text = "XX";
            this.lblSmall.Text = "XX";

            this.lblOdd.Text = "XX";
            this.lblOddPair.Text = "XX";
            this.lblPairOdd.Text = "XX";
            this.lblPair.Text = "XX";

            this.lblDbl.Text = "XX";

            this.lblDwdBig.Text = "XX";
            this.lblDwdBigSmall.Text = "XX";
            this.lblDwdSmallBig.Text = "XX";
            this.lblDwdSmall.Text = "XX";

            this.lblDwdOdd.Text = "XX";
            this.lblDwdOddPair.Text = "XX";
            this.lblDwdPairOdd.Text = "XX";
            this.lblDwdPair.Text = "XX";

            this.dgv1.Rows.Clear();
            this.dgv2.Rows.Clear();
        }
    }
}