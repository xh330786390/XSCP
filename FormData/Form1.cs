using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FormData
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string result = this.richTextBox1.Text;
            if (string.IsNullOrEmpty(result)) return;

            int index = result.IndexOf("<div id=\"ewinnumber\">");
            result = result.Substring(index);
            index = result.IndexOf("</div>");
            result = result.Substring(0, index);
            result = result.Replace("<div id=\"ewinnumber\">\n", "").Replace("<dl class=\"num_dl01 num_dl02\"><dt>", "").Replace("</dd></dl>", "").Replace("</dd></dl>", "").Replace("&#26399;</dt><dd>", ",").Replace("	    ", "");
            index = result.LastIndexOf('\n');
            result = result.Substring(0, index);
            richTextBox2.Text = result;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.richTextBox1.Text = "";
            this.richTextBox2.Text = "";
        }
    }
}
