using System;
using System.Drawing;
using System.Windows.Forms;
using XSCP.Common.Extend;
using XSCP.Common.Model;
using XSCP.Common;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
namespace XSCP.Forecast
{
    public delegate void UpdateHandler();
    public partial class FormMain : Form
    {
        #region 字段定义
        private int time;//次数
        private bool blTime = false;
        private string date = null;

        Thread thread = null;
        #endregion

        public FormMain()
        {
            InitializeComponent();
            Control.CheckForIllegalCrossThreadCalls = false;
            this.dtp.Value = DateTime.Now.ToXscpDateTime();
            this.date = this.dtp.Value.ToString("yyyyMMdd");
        }

        /// <summary>
        /// 更新数据
        /// </summary>
        /// <returns></returns>
        private bool updateData()
        {
            string result = WebHelper.Get(WebHelper.Url);
            List<string> ltData = result.TransLottery();
            if (ltData == null)
            {
                WebHelper.Connected = false;
                return false;
            }
            return LotteryHelper.Update(this.dtp.Value, ltData);
        }

        /// <summary>
        /// 启动自动更新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsMenuStart_Click(object sender, EventArgs e)
        {
            bool isSetting = false;
            using (FormWebSetting fs = new FormWebSetting())
            {
                if (fs.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    isSetting = true;
                }
            }

            if (isSetting)
            {
                if (thread != null)
                {
                    thread.Abort();
                    thread = null;
                }

                Action action = () =>
                {
                    bool nextData = false;
                    while (WebHelper.Connected)
                    {
                        ///更新下一天
                        if (nextData) this.dtp.Value = DateTime.Now;
                        int second = getWaitSecond();

                        bool success = false;   //更新数据是否成功
                        while (!success && second > 40 && second <= 50)
                        {
                            success = updateData();

                            if (!success) Thread.Sleep(2000);
                        }

                        if (success)
                        {
                            initControl();

                            //由于更新数据、控件赋值值 存在耗时，所以这时从新获得最新等待时间；
                            second = getWaitSecond();
                        }

                        ///标识为将进入下一天
                        nextData = second > 60 ? true : false;
                        Thread.Sleep((second + 10) * 1000);
                    }

                    if (thread != null)
                    {
                        thread.Abort();
                        thread = null;
                        WebHelper.Connected = false;
                    }
                    MessageBox.Show("网络已断开连接，请重新连接");
                };

                if (WebHelper.Connected)
                {
                    if (action != null)
                    {
                        thread = new Thread(() => action());
                        thread.Start();
                    }
                }
            }
        }

        /// <summary>
        /// 初始化控件
        /// </summary>
        private void initControl()
        {
            try
            {
                ///清空数据
                clearFFCtrl();

                ///刷新数据
                refreshData();

                ///刷新样式
                refreshStyle();
            }
            catch (Exception er)
            { }
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
            this.lblFFTime.Text = "12:00";
            this.dgvFF.Rows.Clear();
        }

        /// <summary>
        /// 刷新数据
        /// </summary>
        private void refreshData()
        {
            //最新开奖号码
            var lt_lotterys = XscpMysqlBLL.QueryLottery(this.date, 1);
            if (lt_lotterys == null || lt_lotterys.Count == 0) return;

            ///加载最新开奖号
            initLottery(lt_lotterys[0]);
            return;

            List<Tendency2Model> lt_tendency2 = new List<Tendency2Model>();

            ///前二星最最大走势
            Tendency2Model maxBefore = XscpMysqlBLL.QueryMaxTendency2(Tendency2Enum.Before, this.date);
            maxBefore.Sno = "前大";
            lt_tendency2.Add(maxBefore);

            ///前二星最近走势势
            var lt_2Before = XscpMysqlBLL.QueryTendency2(Tendency2Enum.Before, this.date, 1);
            if (lt_2Before != null && lt_2Before.Count > 0)
            {
                lt_2Before[0].Sno = "前二";
                lt_tendency2.Add(lt_2Before[0]);
            }

            ///后二星最近走势势
            var lt_2After = XscpMysqlBLL.QueryTendency2(Tendency2Enum.After, this.date, 1);
            if (lt_2After != null && lt_2After.Count > 0)
            {
                lt_2After[0].Sno = "后二";
                lt_tendency2.Add(lt_2After[0]);
            }

            ///后二星最最大走势
            Tendency2Model maxAfter = XscpMysqlBLL.QueryMaxTendency2(Tendency2Enum.After, this.date);
            maxAfter.Sno = "后大";
            lt_tendency2.Add(maxAfter);

            List<TendencyModel> lt_Tendency = new List<TendencyModel>();
            ///前定二星最近走势势
            var lt_TenThousand = XscpMysqlBLL.QueryTendency1(Tendency1Enum.TenThousand, this.date, 1);  //万位
            var lt_Thousand = XscpMysqlBLL.QueryTendency1(Tendency1Enum.Thousand, this.date, 1);        //千位

            ///后定最近走势势
            var lt_Ten = XscpMysqlBLL.QueryTendency1(Tendency1Enum.Ten, this.date, 1); //十位
            var lt_One = XscpMysqlBLL.QueryTendency1(Tendency1Enum.One, this.date, 1); //个位

            lt_TenThousand[0].Sno = "前定";
            lt_Thousand[0].Sno = "前定";
            lt_Ten[0].Sno = "后定";
            lt_One[0].Sno = "后定";

            ///初始化一星
            lt_Tendency.Add(lt_TenThousand[0]);
            lt_Tendency.Add(lt_Thousand[0]);
            lt_Tendency.Add(lt_Ten[0]);
            lt_Tendency.Add(lt_One[0]);

            ///加载最新开奖号
            initLottery(lt_lotterys[0]);

            //添加6行
            this.dgvFF.Rows.Add(6);

            ///初始化一星
            initDgvTendency1(lt_Tendency);

            ///初始化二星
            initDgvTendency2(lt_tendency2);
        }

        /// <summary>
        /// 设置样式
        /// </summary>
        private void refreshStyle()
        {
            if (this.dgvFF.Rows.Count < 6) return;
            ///设置行背景色
            ControlHelper.SetRowBackColorStyle(this.dgvFF.Rows[2], Color.LightSteelBlue, 14);
            ControlHelper.SetRowBackColorStyle(this.dgvFF.Rows[3], Color.LightBlue, 14);

            ///设置预警背景色
            ControlHelper.Tendency2WarningStyle(this.dgvFF.Rows[2]);
            ControlHelper.Tendency2WarningStyle(this.dgvFF.Rows[3]);

            ///设置开奖背景色
            ControlHelper.SetOpenLotteryStyle(this.dgvFF.Rows[1], Color.LightGray, 14);
            ControlHelper.SetOpenLotteryStyle(this.dgvFF.Rows[2], Color.LightGray, 14);
            ControlHelper.SetOpenLotteryStyle(this.dgvFF.Rows[3], Color.LightGray, 14);
            ControlHelper.SetOpenLotteryStyle(this.dgvFF.Rows[4], Color.LightGray, 14);
        }

        private void FormMainDigit_Load(object sender, EventArgs e)
        {
            timer.Enabled = true;
            timer.Start();

            initControl();
        }

        /// <summary>
        /// 倒计秒数()
        /// </summary>
        /// <returns></returns>
        private string getTimeDown()
        {
            string result = null;
            string nowTime = DateTime.Now.ToString("HH:mm:ss");

            if (nowTime.CompareTo("07:01:00") == -1 || nowTime.CompareTo("08:01:00") == 1)
            {
                result = "00:" + (60 - DateTime.Now.Second).ToString().PadLeft(2, '0');
            }
            else
            {
                DateTime startTime = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd ") + "08:01:00");
                TimeSpan timeSpan = startTime - DateTime.Now;
                result = timeSpan.ToString(@"mm\:ss");
            }
            return result;
        }

        /// <summary>
        /// 倒计秒数
        /// </summary>
        /// <returns></returns>
        private static int getWaitSecond()
        {
            int result = 0;
            string nowTime = DateTime.Now.ToString("HH:mm:ss");

            if (nowTime.CompareTo("07:01:00") == -1 || nowTime.CompareTo("08:01:00") == 1)
            {
                result = 60 - DateTime.Now.Second;
            }
            else
            {
                DateTime startTime = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd ") + "08:01:00");
                TimeSpan timeSpan = startTime - DateTime.Now;
                result = (int)timeSpan.TotalSeconds;
            }
            return result;
        }

        /// <summary>
        /// 距离下次开奖时间
        /// </summary>
        private void displayOpenTime()
        {
            this.lblTime.Text = getTimeDown();
        }

        /// <summary>
        /// 跑马灯
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer_Tick(object sender, EventArgs e)
        {
            //距离下次开奖时间
            displayOpenTime();

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

        private void tsMenuStop_Click(object sender, EventArgs e)
        {
            if (thread != null)
            {
                thread.Abort();
                thread = null;
                WebHelper.Connected = false;
            }
        }

        /// <summary>
        /// 手动更新号码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsMenuLottery_Click(object sender, EventArgs e)
        {
            FormManualData fmd = new FormManualData();
            fmd.NewLotteryHander = () =>
            {
                initControl();
            };
            fmd.ShowDialog();
        }

        /// <summary>
        /// 最新奖号
        /// </summary>
        private void initLottery(LotteryModel lm)
        {
            if (lm == null) return;

            //本次开奖号码
            this.lblFFSno.Text = lm.Sno + "期";
            this.lblFF1.Text = lm.Num1.ToString();
            this.lblFF2.Text = lm.Num2.ToString();
            this.lblFF3.Text = lm.Num3.ToString();
            this.lblFF4.Text = lm.Num4.ToString();
            this.lblFF5.Text = lm.Num5.ToString();
            this.lblFFTime.Text = lm.Dtime.Split(' ')[1];
        }

        /// <summary>
        /// 初始化一星走势
        /// </summary>
        /// <param name="ltData"></param>
        private void initDgvTendency1(List<TendencyModel> ltData)
        {
            if (ltData == null || ltData.Count < 4) return;
            ControlHelper.SetTendency1Value(this.dgvFF.Rows[1], ltData[0], ltData[1]);
            ControlHelper.SetTendency1Value(this.dgvFF.Rows[4], ltData[2], ltData[3]);
        }

        /// <summary>
        /// 初始化二星走势
        /// </summary>
        private void initDgvTendency2(List<Tendency2Model> ltData)
        {
            if (ltData == null || ltData.Count < 4) return;

            ControlHelper.SetTendency2Value(this.dgvFF.Rows[0], ltData[0]);
            ControlHelper.SetTendency2Value(this.dgvFF.Rows[2], ltData[1]);
            ControlHelper.SetTendency2Value(this.dgvFF.Rows[3], ltData[2]);
            ControlHelper.SetTendency2Value(this.dgvFF.Rows[5], ltData[3]);
        }

        /// <summary>
        /// 缺失号码检测
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsMenuTest_Click(object sender, EventArgs e)
        {
            FormCheck fc = new FormCheck(this.dtp.Value);
            fc.NewLotteryHander = () =>
            {
                initControl();
            };
            fc.ShowDialog();
        }

        private void dtp_ValueChanged(object sender, EventArgs e)
        {
            this.date = this.dtp.Value.ToXscpDateTime().ToString("yyyyMMdd");
            initControl();
        }

        private void dgvFF_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 11 && (e.RowIndex == 2 || e.RowIndex == 3))
            {
                FormTendency2 ft = null;
                if (e.RowIndex == 2) ft = new FormTendency2(Tendency2Enum.Before, this.date);
                else ft = new FormTendency2(Tendency2Enum.After, this.date);
                ft.ShowDialog();
            }
        }



        private void FormMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (thread != null)
            {
                thread.Abort();
                thread = null;
                WebHelper.Connected = false;
            }
        }
    }
}
