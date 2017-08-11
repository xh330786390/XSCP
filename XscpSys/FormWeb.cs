using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Windows.Forms;
using Web = System.Windows.Browser;

namespace XscpSys
{
    public partial class FormWeb : Form
    {

       


        public Dictionary<string, string> dictType = new Dictionary<string, string>();
        public FormWeb()
        {
            InitializeComponent();
            addCpType();


        }



        private void addCpType()
        {
            dictType.Add("分分彩", "15");
            dictType.Add("3D彩", "17");
        }



        private void button1_Click(object sender, EventArgs e)
        {
            //string url = @"http://www.kaixin001.com/";
            Uri uri = new Uri(@"http://www.kaixin001.com/");
            CookieContainer cookies = Cookies.GetUriCookieContainer(uri);
            Cookies.PrintCookies(cookies, uri);


            ////string cookie = CookieHelper.GetCookies("http://www.xs6868.com");
            //MessageBox.Show(cookie);
            return;
            //WebHelper.SetCookies(this.txtCookie.Text, this.txtSession.Text, this.txtUrl.Text.Replace("http://",""));
            WebHelper wh = new WebHelper();
            string url = this.txtUrl.Text + "/page/WORecord.shtml";
            Dictionary<string, string> param = new Dictionary<string, string>();
            param["id"] = dictType[this.comboBox1.Text];
            param["num"] = this.txtNum.Text;
            string result = wh.Get(url,this.txtCookie.Text, this.txtSession.Text, param);
            MessageBox.Show(result);
        }

        private void FormWeb_Load(object sender, EventArgs e)
        {
            
            return;
            dictType.ToList().ForEach(l => { this.comboBox1.Items.Add(l.Key); });
            this.comboBox1.SelectedIndex = 0;
        }
    }
}
