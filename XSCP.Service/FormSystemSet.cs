using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using XSCP.Core;

namespace XSCP.Service
{
    public partial class FormSystemSet : Form
    {
        public FormSystemSet()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SysConfig.Config.Items.Cookie1 = this.textBox1.Text;
            SysConfig.Config.Items.Cookie2 = this.textBox2.Text;
            new XmlOperate<Model.Config>().SaveConfig<Model.Config>(SysConfig.ConfigPath, SysConfig.Config);
            this.Close();
            try
            {
                Common.HttpDataRequest.SetCookieValues(this.textBox1.Text, this.textBox2.Text);
            }
            catch (Exception er)
            {

            }
        }

        private void FormSystemSet_Load(object sender, EventArgs e)
        {
            this.textBox1.Text = SysConfig.Config.Items.Cookie1;
            this.textBox2.Text = SysConfig.Config.Items.Cookie2;
        }
    }
}
