using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace FormXscp
{
    public partial class FormTendency : Form
    {
        private List<DigitModel> Lt_Digit = new List<DigitModel>();//开过的奖
        private List<FreqModel> Lt_Freq = new List<FreqModel>();//未开奖的所有次数
        private List<StatisticsModel> Lt_Statistics = new List<StatisticsModel>();//统计组合的开奖次数
        private List<WaringModel> Lt_Warning = new List<WaringModel>();//预警
        private List<ForecastModel> Lt_Forecast = new List<ForecastModel>();//预测数据

        private int parityNum = 0;
        private bool isAddEvent = false;
        private static string fileBase;//检测数据的目录
        private string filePath;//检测的数据文件

        static FormTendency()
        {
            fileBase = System.AppDomain.CurrentDomain.BaseDirectory + @"\data\";
            if (!Directory.Exists(fileBase))
                Directory.CreateDirectory(fileBase);
        }

        public FormTendency()
        {
            InitializeComponent();
            this.dateTimePicker1.Value = DateTime.Today;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            if (!isAddEvent)
            {
                this.fileSystemWatcher1.Path = fileBase;
                this.fileSystemWatcher1.Filter = "*.txt";
                this.fileSystemWatcher1.Changed += new FileSystemEventHandler(fileSystemWatcher_Changed);
                this.fileSystemWatcher1.EnableRaisingEvents = true;
                isAddEvent = true;

                checkWarmValue(this.textBox1, EnumWarning.绿);
                checkWarmValue(this.textBox2, EnumWarning.黄);
                checkWarmValue(this.textBox3, EnumWarning.红);
            }
        }

        /// <summary>
        /// 取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            if (isAddEvent)
            {
                Lt_Warning.Clear();
                this.fileSystemWatcher1.Changed -= new FileSystemEventHandler(fileSystemWatcher_Changed);
                this.fileSystemWatcher1.EnableRaisingEvents = false;
                isAddEvent = false;
            }
        }

        /// <summary>
        /// 监控文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fileSystemWatcher_Changed(object sender, FileSystemEventArgs e)
        {
            try
            {
                if (parityNum % 2 == 0)
                {
                    readFile();//读取文件
                    analyzeXscp();//分析文件
                    analyzeWarning();//
                    initDgv();//dgv赋值

                    initStatistics();//本次数据组合统计

                    statisticsSum();//本次开奖的组合统计

                    forecastData();//预测数据

                    refreshDgvColor(this.dataGridView1);//预警颜色
                    refreshDgvColor(this.dataGridView2);//预警颜色
                    parityNum = 1;
                }
                else
                {
                    parityNum = 0;
                }
            }
            catch (Exception er)
            {
                //MessageBox.Show(er.Message);
            }
        }

        #region 信息统计
        /// <summary>
        /// 当前信息统计
        /// </summary>
        private void initStatistics()
        {
            this.dataGridView3.Rows.Clear();
            for (int i = 0; i < 4; i++)
            {
                this.dataGridView3.Rows.Add();
            }

            this.dataGridView3[0, 0].Value = "大大";
            this.dataGridView3[0, 3].Value = "小小";

            if (Lt_Freq.Count > 0)
            {

                FreqModel fm = Lt_Freq[Lt_Freq.Count - 1];

                this.dataGridView3[1, 0].Value = "大大奇奇【" + fm.big + "," + fm.odd + "】";
                this.dataGridView3[2, 0].Value = "大大奇偶【" + fm.big + "," + fm.oddPair + "】";
                this.dataGridView3[3, 0].Value = "大大偶奇【" + fm.big + "," + fm.pairOdd + "】";
                this.dataGridView3[4, 0].Value = "大大偶偶【" + fm.big + "," + fm.pair + "】";

                this.dataGridView3[1, 1].Value = "大小奇奇【" + fm.bigSmall + "," + fm.odd + "】";
                this.dataGridView3[2, 1].Value = "大小奇偶【" + fm.bigSmall + "," + fm.oddPair + "】";
                this.dataGridView3[3, 1].Value = "大小偶奇【" + fm.bigSmall + "," + fm.pairOdd + "】";
                this.dataGridView3[4, 1].Value = "大小偶偶【" + fm.bigSmall + "," + fm.pair + "】";

                this.dataGridView3[1, 2].Value = "小大奇奇【" + fm.smallBig + "," + fm.odd + "】";
                this.dataGridView3[2, 2].Value = "小大奇偶【" + fm.smallBig + "," + fm.oddPair + "】";
                this.dataGridView3[3, 2].Value = "小大偶奇【" + fm.smallBig + "," + fm.pairOdd + "】";
                this.dataGridView3[4, 2].Value = "小大偶偶【" + fm.smallBig + "," + fm.pair + "】";

                this.dataGridView3[1, 3].Value = "小小奇奇【" + fm.small + "," + fm.odd + "】";
                this.dataGridView3[2, 3].Value = "小小奇偶【" + fm.small + "," + fm.oddPair + "】";
                this.dataGridView3[3, 3].Value = "小小偶奇【" + fm.small + "," + fm.pairOdd + "】";
                this.dataGridView3[4, 3].Value = "小小偶偶【" + fm.small + "," + fm.pair + "】";

                Common.SetDgvBackColorStyle1(this.dataGridView3, 0, 1, Color.LightSteelBlue, 11);
                Common.SetDgvBackColorStyle1(this.dataGridView3, 0, 2, Color.LightSteelBlue, 11);
                Common.SetDgvBackColorStyle1(this.dataGridView3, 0, 3, Color.LightSteelBlue, 11);
                Common.SetDgvBackColorStyle1(this.dataGridView3, 0, 4, Color.LightSteelBlue, 11);

                Common.SetDgvBackColorStyle1(this.dataGridView3, 1, 1, Color.LightSteelBlue, 11);
                Common.SetDgvBackColorStyle1(this.dataGridView3, 1, 4, Color.LightSteelBlue, 11);
                Common.SetDgvBackColorStyle1(this.dataGridView3, 2, 1, Color.LightSteelBlue, 11);
                Common.SetDgvBackColorStyle1(this.dataGridView3, 2, 4, Color.LightSteelBlue, 11);

                Common.SetDgvBackColorStyle1(this.dataGridView3, 3, 1, Color.LightSteelBlue, 11);
                Common.SetDgvBackColorStyle1(this.dataGridView3, 3, 2, Color.LightSteelBlue, 11);
                Common.SetDgvBackColorStyle1(this.dataGridView3, 3, 3, Color.LightSteelBlue, 11);
                Common.SetDgvBackColorStyle1(this.dataGridView3, 3, 4, Color.LightSteelBlue, 11);
            }
        }

        /// <summary>
        /// 统计组合的开奖次数
        /// </summary>
        private void statisticsSum()
        {
            initCalculatorStatistics();//计数
            initDgvStatistics();//
        }

        private void initCalculatorStatistics()
        {
            Lt_Statistics.Clear();
            FreqModel curfm;
            FreqModel nextfm;

            for (int i = Lt_Freq.Count - 1; i >= 1; i--)
            {
                curfm = Lt_Freq[i];
                if (i - 1 > 0)
                {
                    nextfm = Lt_Freq[i - 1];
                    addStatisticsValue(nextfm, curfm);
                }
            }

            //for (int i = 1; i < Lt_Freq.Count; i++)
            //{
            //    curfm = Lt_Freq[i];
            //    if (i + 1 < Lt_Freq.Count)
            //    {
            //        nextfm = Lt_Freq[i + 1];
            //        if (nextfm.dbl != 0)
            //            addStatisticsValue(curfm, nextfm);
            //    }
            //}
        }

        /// <summary>
        /// 给统计表添加数据
        /// </summary>
        /// <param name="curFm"></param>
        /// <param name="nextFm"></param>
        private void addStatisticsValue(FreqModel curfm, FreqModel nextfm)
        {
            StatisticsModel sm = new StatisticsModel();
            if (nextfm.dbl == 0)
            {
                sm.bigSmallValue = curfm.big;
                sm.unitName = "重重";
            }
            else if (nextfm.big == 0)
            {
                sm.bigSmallValue = curfm.big;
                sm.unitName = "大大";
            }
            else if (nextfm.bigSmall == 0)
            {
                sm.bigSmallValue = curfm.bigSmall;
                sm.unitName = "大小";
            }
            else if (nextfm.smallBig == 0)
            {
                sm.bigSmallValue = curfm.smallBig;
                sm.unitName = "小大";
            }
            else if (nextfm.small == 0)
            {
                sm.bigSmallValue = curfm.small;
                sm.unitName = "小小";
            }

            if (nextfm.dbl == 0)
            {
                sm.bigSmallValue = curfm.big;
                sm.unitName += "重重";
            }
            else if (nextfm.odd == 0)
            {
                sm.oddPairValue = curfm.odd;
                sm.unitName += "奇奇";
            }
            else if (nextfm.oddPair == 0)
            {
                sm.oddPairValue = curfm.oddPair;
                sm.unitName += "奇偶";
            }
            else if (nextfm.pairOdd == 0)
            {
                sm.oddPairValue = curfm.pairOdd;
                sm.unitName += "偶奇";
            }
            else if (nextfm.pair == 0)
            {
                sm.oddPairValue = curfm.pair;
                sm.unitName += "偶奇";
            }

            var vs = Lt_Statistics.Where(lt => lt.unitName == sm.unitName && lt.bigSmallValue == sm.bigSmallValue && lt.oddPairValue == sm.oddPairValue).ToList();
            if (vs.Count == 0)
            {
                sm.sum = 1;
                Lt_Statistics.Add(sm);
            }
            else
            {
                vs.ForEach(l => l.sum += 1);
            }
        }

        private void initDgvStatistics()
        {
            this.dataGridView4.Rows.Clear();
            //var vs = Lt_Statistics.OrderBy(lt => lt.bigSmallValue).ThenBy(lt => lt.oddPairValue).ToList();
            for (int i = 0; i < Lt_Statistics.Count; i++)
            {
                this.dataGridView4.Rows.Add();
            }

            int sum = 0;
            for (int i = 0; i < Lt_Statistics.Count; i++)
            {
                this.dataGridView4[0, i].Value = i + 1;
                this.dataGridView4[1, i].Value = Lt_Statistics[i].unitName + "【" + Lt_Statistics[i].bigSmallValue + "," + Lt_Statistics[i].oddPairValue + "】";
                this.dataGridView4[2, i].Value = Lt_Statistics[i].sum;
                if (!Lt_Statistics[i].unitName.Contains("重"))
                    sum += Lt_Statistics[i].sum;
            }

            this.label12.Text = sum.ToString();
        }

        /// <summary>
        /// 预测数据
        /// </summary>
        private void forecastData()
        {
            Lt_Forecast.Clear();
            this.dataGridView5.Rows.Clear();
            if (Lt_Freq.Count > 0)
            {
                FreqModel fm = Lt_Freq[Lt_Freq.Count - 1];
                initForecast(fm);
                initDgvForecast();
            }
        }

        /// <summary>
        /// 初始化预测值
        /// </summary>
        /// <param name="fm"></param>
        private void initForecast(FreqModel fm)
        {
            Lt_Forecast.Add(new ForecastModel()
            {
                unitName = "大大",
                sum = fm.big * 4 + fm.odd + fm.oddPair + fm.pairOdd + fm.pair,
                diff = Math.Abs(fm.big - fm.odd) + Math.Abs(fm.big - fm.oddPair) + Math.Abs(fm.big - fm.pairOdd) + Math.Abs(fm.big - fm.pair)
            });

            Lt_Forecast.Add(new ForecastModel()
            {
                unitName = "大小",
                sum = fm.bigSmall * 4 + fm.odd + fm.oddPair + fm.pairOdd + fm.pair,
                diff = Math.Abs(fm.bigSmall - fm.odd) + Math.Abs(fm.bigSmall - fm.oddPair) + Math.Abs(fm.bigSmall - fm.pairOdd) + Math.Abs(fm.bigSmall - fm.pair)
            });

            Lt_Forecast.Add(new ForecastModel()
            {
                unitName = "小大",
                sum = fm.smallBig * 4 + fm.odd + fm.oddPair + fm.pairOdd + fm.pair,
                diff = Math.Abs(fm.smallBig - fm.odd) + Math.Abs(fm.smallBig - fm.oddPair) + Math.Abs(fm.smallBig - fm.pairOdd) + Math.Abs(fm.smallBig - fm.pair)
            });

            Lt_Forecast.Add(new ForecastModel()
            {
                unitName = "小小",
                sum = fm.small * 4 + fm.odd + fm.oddPair + fm.pairOdd + fm.pair,
                diff = Math.Abs(fm.small - fm.odd) + Math.Abs(fm.small - fm.oddPair) + Math.Abs(fm.small - fm.pairOdd) + Math.Abs(fm.small - fm.pair)
            });


            Lt_Forecast.Add(new ForecastModel()
            {
                unitName = "奇奇",
                sum = fm.odd * 4 + fm.big + fm.bigSmall + fm.smallBig + fm.small,
                diff = Math.Abs(fm.odd - fm.big) + Math.Abs(fm.odd - fm.bigSmall) + Math.Abs(fm.odd - fm.smallBig) + Math.Abs(fm.odd - fm.small)
            });

            Lt_Forecast.Add(new ForecastModel()
            {
                unitName = "奇偶",
                sum = fm.oddPair * 4 + fm.big + fm.bigSmall + fm.smallBig + fm.small,
                diff = Math.Abs(fm.oddPair - fm.big) + Math.Abs(fm.oddPair - fm.bigSmall) + Math.Abs(fm.oddPair - fm.smallBig) + Math.Abs(fm.oddPair - fm.small)
            });

            Lt_Forecast.Add(new ForecastModel()
            {
                unitName = "偶奇",
                sum = fm.pairOdd * 4 + fm.big + fm.bigSmall + fm.smallBig + fm.small,
                diff = Math.Abs(fm.pairOdd - fm.big) + Math.Abs(fm.pairOdd - fm.bigSmall) + Math.Abs(fm.pairOdd - fm.smallBig) + Math.Abs(fm.pairOdd - fm.small)
            });

            Lt_Forecast.Add(new ForecastModel()
            {
                unitName = "偶偶",
                sum = fm.pair * 4 + fm.big + fm.bigSmall + fm.smallBig + fm.small,
                diff = Math.Abs(fm.pair - fm.big) + Math.Abs(fm.pair - fm.bigSmall) + Math.Abs(fm.pair - fm.smallBig) + Math.Abs(fm.pair - fm.small)
            });
        }

        /// <summary>
        /// dgv赋值
        /// </summary>
        private void initDgvForecast()
        {
            for (int i = 0; i < Lt_Forecast.Count; i++)
            {
                this.dataGridView5.Rows.Add();
            }

            var vs = Lt_Forecast.OrderBy(l => l.diff).ToList();
            for (int i = 0; i < vs.Count; i++)
            {
                this.dataGridView5[0, i].Value = i + 1;
                this.dataGridView5[1, i].Value = vs[i].unitName;
                this.dataGridView5[2, i].Value = vs[i].sum;
                this.dataGridView5[3, i].Value = vs[i].diff;
                this.dataGridView5[4, i].Value = vs[i].sum - vs[i].diff;
            }
        }

        #endregion


        private void initDgv()
        {
            this.dataGridView1.Rows.Clear();
            for (int i = 0; i < Lt_Freq.Count; i++)
            {
                this.dataGridView1.Rows.Add();
            }

            for (int i = Lt_Freq.Count - 1, j = 0; i >= 0; i--)
            {
                this.dataGridView1[0, j].Value = j + 1;//局数
                this.dataGridView1[1, j].Value = Lt_Freq[i].big;//大
                this.dataGridView1[2, j].Value = Lt_Freq[i].small;//小
                this.dataGridView1[3, j].Value = Lt_Freq[i].odd;//奇
                this.dataGridView1[4, j].Value = Lt_Freq[i].pair;//偶
                this.dataGridView1[5, j].Value = Lt_Freq[i].dbl;//重
                j++;
            }

            this.dataGridView2.Rows.Clear();
            for (int i = 0; i < Lt_Freq.Count; i++)
            {
                this.dataGridView2.Rows.Add();
            }

            for (int i = Lt_Freq.Count - 1, j = 0; i >= 0; i--)
            {
                this.dataGridView2[0, j].Value = j + 1;//局数
                this.dataGridView2[1, j].Value = Lt_Freq[i].bigSmall;//大小
                this.dataGridView2[2, j].Value = Lt_Freq[i].smallBig;//小大
                this.dataGridView2[3, j].Value = Lt_Freq[i].oddPair;//奇偶
                this.dataGridView2[4, j].Value = Lt_Freq[i].pairOdd;//偶奇
                this.dataGridView2[5, j].Value = Lt_Freq[i].dbl;//重
                j++;
            }
        }

        private void refreshDgvColor(DataGridView dgv)
        {
            int value = 0;
            for (int i = 0; i < dgv.Rows.Count; i++)
            {
                for (int j = 1; j < dgv.Columns.Count - 1; j++)
                {
                    value = Convert.ToInt32(dgv[j, i].Value.ToString());
                    if (value >= Lt_Warning[0].value)
                    {
                        Color color = getWarningColor(value);
                        Common.SetDgvBackColorStyle(dgv, i, j, color, 11);
                    }
                }
            }
        }

        /// <summary>
        /// 分析值的大小，确定颜色
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private Color getWarningColor(int value)
        {
            Color color;
            if (value >= Lt_Warning[2].value)
                color = this.textBox3.BackColor;//Color.Red;
            else if (value >= Lt_Warning[1].value)
                color = this.textBox2.BackColor;// Color.Yellow;
            else if (value >= Lt_Warning[0].value)
                color = this.textBox1.BackColor;// Color.Lime;
            else color = Color.Black;
            return color;
        }

        /// <summary>
        /// 分析彩票
        /// </summary>
        private void analyzeXscp()
        {
            FreqModel freq = new FreqModel();
            freq.big = bigNum(0);
            freq.small = smallNum(0);
            freq.odd = oddPairNum(1, 0);
            freq.pair = oddPairNum(0, 0);
            freq.dbl = doubleNum(0);

            //大
            this.label10.Text = freq.big.ToString();

            //小
            this.label9.Text = freq.small.ToString();

            //奇数
            this.label8.Text = freq.odd.ToString();

            //偶数
            this.label7.Text = freq.pair.ToString();

            //偶数
            this.label6.Text = freq.dbl.ToString();
        }

        private void analyzeWarning()
        {
            FreqModel freq;
            Lt_Freq.Clear();
            for (int i = Lt_Digit.Count - 1; i >= 0; i--)
            {
                freq = new FreqModel();
                freq.big = bigNum(i);
                freq.small = smallNum(i);
                freq.odd = oddPairNum(1, i);
                freq.pair = oddPairNum(0, i);
                freq.dbl = doubleNum(i);

                freq.bigSmall = bigSmallNum(i);
                freq.smallBig = smallBigNum(i);
                freq.oddPair = oddPairNum(i, 1, 0);
                freq.pairOdd = oddPairNum(i, 0, 1);
                freq.dbl = doubleNum(i);
                Lt_Freq.Add(freq);//添加
            }
        }

        /// <summary>
        /// 求上次出现大数的 离现在有几次了
        /// </summary>
        /// <returns></returns>
        private int bigNum(int index)
        {
            int curIndex = Lt_Digit[index].sno;//当前序号
            int preSno = 0;
            for (int i = index; i < Lt_Digit.Count; i++)
            {
                if (Lt_Digit[i].num4 > 4 && Lt_Digit[i].num5 > 4 && Lt_Digit[i].num4 != Lt_Digit[i].num5)
                {
                    preSno = Lt_Digit[i].sno;
                    break;
                }
            }
            return curIndex - preSno;
        }

        /// <summary>
        /// 求上次出现大小数的 离现在有几次了
        /// </summary>
        /// <returns></returns>
        private int bigSmallNum(int index)
        {
            int curIndex = Lt_Digit[index].sno;//当前序号
            int preSno = 0;
            for (int i = index; i < Lt_Digit.Count; i++)
            {
                if (Lt_Digit[i].num4 > 4 && Lt_Digit[i].num5 < 5 && Lt_Digit[i].num4 != Lt_Digit[i].num5)
                {
                    preSno = Lt_Digit[i].sno;
                    break;
                }
            }
            return curIndex - preSno;
        }

        /// <summary>
        /// 求上次出现小大数的 离现在有几次了
        /// </summary>
        /// <returns></returns>
        private int smallBigNum(int index)
        {
            int curIndex = Lt_Digit[index].sno;//当前序号
            int preSno = 0;
            for (int i = index; i < Lt_Digit.Count; i++)
            {
                if (Lt_Digit[i].num4 < 5 && Lt_Digit[i].num5 > 4 && Lt_Digit[i].num4 != Lt_Digit[i].num5)
                {
                    preSno = Lt_Digit[i].sno;
                    break;
                }
            }
            return curIndex - preSno;
        }

        /// <summary>
        /// 求上次出现小数的 离现在有几次了
        /// </summary>
        /// <returns></returns>
        private int smallNum(int index)
        {
            int curIndex = Lt_Digit[index].sno;//当前序号
            int preSno = 0;
            for (int i = index; i < Lt_Digit.Count; i++)
            {
                if (Lt_Digit[i].num4 < 5 && Lt_Digit[i].num5 < 5 && Lt_Digit[i].num4 != Lt_Digit[i].num5)
                {
                    preSno = Lt_Digit[i].sno;
                    break;
                }
            }
            return curIndex - preSno;
        }


        /// <summary>
        /// 求上次出现奇偶数的 离现在有几次了
        /// </summary>
        /// <returns></returns>
        private int oddPairNum(int op, int index)
        {
            int curIndex = Lt_Digit[index].sno;//当前序号
            int preSno = 0;
            for (int i = index; i < Lt_Digit.Count; i++)
            {
                if (Lt_Digit[i].num4 % 2 == op && Lt_Digit[i].num5 % 2 == op && Lt_Digit[i].num4 != Lt_Digit[i].num5)
                {
                    preSno = Lt_Digit[i].sno;
                    break;
                }
            }
            return curIndex - preSno;
        }

        /// <summary>
        /// 求上次出现奇偶数的 离现在有几次了
        /// </summary>
        /// <returns></returns>
        private int oddPairNum(int index, int number1, int number2)
        {
            int curIndex = Lt_Digit[index].sno;//当前序号
            int preSno = 0;
            for (int i = index; i < Lt_Digit.Count; i++)
            {
                if (Lt_Digit[i].num4 % 2 == number1 && Lt_Digit[i].num5 % 2 == number2 && Lt_Digit[i].num4 != Lt_Digit[i].num5)
                {
                    preSno = Lt_Digit[i].sno;
                    break;
                }
            }
            return curIndex - preSno;
        }

        /// <summary>
        /// 求上次出现重数的 离现在有几次了
        /// </summary>
        /// <returns></returns>
        private int doubleNum(int index)
        {
            int curIndex = Lt_Digit[index].sno;//当前序号
            int preSno = 0;
            for (int i = index; i < Lt_Digit.Count; i++)
            {
                if (Lt_Digit[i].num4 == Lt_Digit[i].num5)
                {
                    preSno = Lt_Digit[i].sno;
                    break;
                }
            }
            return curIndex - preSno;
        }

        /// <summary>
        /// 读取修改后的文件
        /// </summary>
        private void readFile()
        {
            StreamReader sr = new StreamReader(filePath, Encoding.Default);
            String line;
            Lt_Digit.Clear();// 清空
            while ((line = sr.ReadLine()) != null)
            {
                if (string.IsNullOrEmpty(line)) continue;

                string[] array = line.Replace("，", ",").Split(',');
                int[] arrayint = new int[array.Length];
                for (int i = 0; i < array.Length; i++)
                {
                    arrayint[i] = Convert.ToInt32(array[i]);
                }

                Lt_Digit.Add(new DigitModel()
                {
                    num1 = arrayint[0],
                    num2 = arrayint[1],
                    num3 = arrayint[2],
                    num4 = arrayint[3],
                    num5 = arrayint[4]
                });
            }
            sr.Close();

            for (int i = Lt_Digit.Count - 1, j = 0; i >= 0; i--)
            {
                Lt_Digit[i].sno = j;
                j++;
            }
        }

        /// <summary>
        /// 检验预警值的 输入的正确性
        /// </summary>
        /// <returns></returns>
        private bool checkWarmValue(TextBox textBox, EnumWarning waring)
        {
            try
            {
                if (string.IsNullOrEmpty(textBox.Text))
                {
                    MessageBox.Show("请填写：预警值！");
                    return false;
                }

                int num = Convert.ToInt32(textBox.Text);
                Lt_Warning.Add(new WaringModel()
                {
                    WarningType = waring,
                    value = num
                });

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

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            filePath = fileBase + "xscp" + this.dateTimePicker1.Value.ToString("yyyyMMdd") + ".txt";
            if (!File.Exists(filePath))
            {
                this.dataGridView1.Rows.Clear();
                this.dataGridView2.Rows.Clear();
                this.dataGridView3.Rows.Clear();
                this.dataGridView4.Rows.Clear();
            }
        }
    }

    /// <summary>
    /// 奖号的每个位数的模型
    /// </summary>
    public class DigitModel
    {
        public int sno;
        public int num1;
        public int num2;
        public int num3;
        public int num4;
        public int num5;
    }

    /// <summary>
    /// 奖号的每次出现情况的模型
    /// </summary>
    public class FreqModel
    {
        public int big;//大数
        public int small;//小数
        public int odd;//奇数
        public int pair;//偶数

        public int bigSmall;//大小数
        public int smallBig;//小大数
        public int oddPair;//奇偶数
        public int pairOdd;//偶奇数
        public int dbl;//重数
    }

    /// <summary>
    /// 预警模型
    /// </summary>
    public class WaringModel
    {
        public EnumWarning WarningType;
        public int value;
    }

    /// <summary>
    /// 统计模型
    /// </summary>
    public class StatisticsModel
    {
        public string unitName;
        public int bigSmallValue;
        public int oddPairValue;
        public int sum;
    }

    /// <summary>
    /// 预测模型
    /// </summary>
    public class ForecastModel
    {
        public string unitName;
        public int sum;
        public int diff;
    }

    /// <summary>
    /// 颜色提醒
    /// </summary>
    public enum EnumWarning
    {
        绿 = 1,
        黄 = 2,
        红 = 3
    }
}
