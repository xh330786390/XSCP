using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;

namespace XscpSys
{
    public partial class FormData : Form
    {
        private string FileBase = System.AppDomain.CurrentDomain.BaseDirectory + @"data\分分彩\";

        private string lotterys;
        private List<LotteryData> lt_LotteryData = new List<LotteryData>();

        public FormData()
        {
            InitializeComponent();
        }

        private void createFile(string filePath)
        {
            if (!File.Exists(FileBase + filePath)) File.Create(FileBase + filePath).Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string result = this.richTextBox1.Text;
            if (string.IsNullOrEmpty(result)) return;

            transStr(result);//转换

            excute();//执行
        }

        /// <summary>
        /// 执行
        /// </summary>
        private void excute()
        {
            addLotteryRecord();
            writeFile();
        }

        private string readFile(string fileName)
        {
            if (!File.Exists(FileBase + fileName)) { File.Create(FileBase + fileName).Close(); return string.Empty; }
            else return System.IO.File.ReadAllText(FileBase + fileName, Encoding.UTF8);
        }

        /// <summary>
        /// 获取文件的第一行记录
        /// </summary>
        /// <param name="textInfo"></param>
        /// <returns></returns>
        private string getFirstLine(string textInfo)
        {
            string[] array = textInfo.Split('\n');
            foreach (string s in array)
            {
                if (!string.IsNullOrEmpty(s.Trim())) return s;
            }
            return string.Empty;
        }

        /// <summary>
        /// 获取文件的最后一行记录
        /// </summary>
        /// <param name="textInfo"></param>
        /// <returns></returns>
        private string getLastLine(string textInfo)
        {
            string[] array = textInfo.Split('\n');
            for (int i = array.Length - 1; i >= 0; i--)
            {
                if (!string.IsNullOrEmpty(array[i].Trim())) return array[i];
            }
            return string.Empty;
        }

        /// <summary>
        /// 写文件
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="result"></param>
        private void writeFile(string fileName, string result)
        {
            System.IO.File.WriteAllText(FileBase + fileName, result, Encoding.UTF8);
        }

        /// <summary>
        /// 读写文件
        /// </summary>
        private void writeFile()
        {
            if (lt_LotteryData.Count > 0)
            {
                string fileName = "";
                int index = -1;
                string textInfo = "";//文本内容
                string firstLine = "";//文本第一行（有效数据）
                string lastLine = "";//动态数据中最后一行（有效数据）
                string result = "";
                for (int i = 0; i < lt_LotteryData.Count; i++)
                {
                    fileName = "ffcp" + lt_LotteryData[i].Date + ".txt";
                    createFile(fileName);//创建文件
                    result = lt_LotteryData[i].Lotterys.Trim();
                    textInfo = readFile(fileName).Replace("期", ",").Trim();
                    if (string.IsNullOrEmpty(textInfo.Trim())) writeFile(fileName, result);
                    else
                    {
                        firstLine = getFirstLine(textInfo);//获取文件的第一行记录

                        index = result.IndexOf(firstLine);

                        if (index >= 0) result = result.Substring(0, index);//存在
                        else//不存在
                        {
                            int max = Convert.ToInt32(firstLine.Split(',')[0]);//文本中最大开奖期号
                            lastLine = getLastLine(result);//获取文件的最后一行记录
                            if (string.IsNullOrEmpty(lastLine)) return;
                            int min = Convert.ToInt32(lastLine.Split(',')[0]);//最新数据中最小期号

                            int value = min - max;
                            string str;
                            if (value > 0)
                            {
                                str = string.Format("奖号中间相差【{0}】条，", value);
                                if (MessageBox.Show(str + "是否继续!", "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Hand) == System.Windows.Forms.DialogResult.No)
                                {
                                    return;
                                }
                            }
                        }
                        if (result.LastIndexOf("\n") == result.Length - 1) result = result + textInfo;
                        else result = result + "\n" + textInfo;
                        writeFile(fileName, result);
                    }
                }
            }
        }

        /// <summary>
        /// 添加记录
        /// </summary>
        private void addLotteryRecord()
        {
            lt_LotteryData.Clear();
            int length = -1;

            int i = -1;
            if (lotterys.IndexOf("1380") == 0)
            {
                length = Regex.Matches(lotterys, "1380").Count;
                i = 1;
            }
            else
            {
                length = Regex.Matches(lotterys, "1380").Count + 1;
                i = 0;
            }

            string[] array = lotterys.Replace("1380", "AB").Split('A');

            for (int k = i, j = 0; k < array.Length; k++)
            {
                string date = this.dtp.Value.AddDays(-j).ToString("yyyyMMdd");
                string curLottery = array[k].Replace("B", "1380");
                lt_LotteryData.Add(new LotteryData { Date = date, Lotterys = curLottery });
                j++;
            }
        }

        /// <summary>
        /// 转换数据
        /// </summary>
        /// <param name="result"></param>
        private void transStr(string result)
        {
            int index = result.IndexOf("<div id=\"ewinnumber\">");
            result = result.Substring(index);
            index = result.IndexOf("</div>");
            result = result.Substring(0, index);
            result = result.Replace("<div id=\"ewinnumber\">\n", "").Replace("<dl class=\"num_dl01 num_dl02\"><dt>", "").Replace("</dd></dl>", "").Replace("</dd></dl>", "").Replace("&#26399;</dt><dd>", ",").Replace("	    ", "");
            index = result.LastIndexOf('\n');
            result = result.Substring(0, index);
            lotterys = result;
            richTextBox2.Text = result;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.richTextBox1.Text = "";
            this.richTextBox2.Text = "";
        }

        private void FormData_Load(object sender, EventArgs e)
        {
            if (DateTime.Now.Hour < 8 && DateTime.Now.Hour >= 0)
            { this.dtp.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.AddDays(-1).Day); }
        }
    }

    public class LotteryData
    {
        public string Date;
        public string Lotterys;
    }
}
