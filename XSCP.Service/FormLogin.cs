using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Reflection;
using System.Threading;
using System.Collections;
using System.Security;
using System.Security.Principal;
using DevComponents.DotNetBar;
using System.Net;
using XSCP.Service.Common;

namespace XSCP.Service
{
    public partial class FormLogin : Office2007Form
    {
        public LotteryServiceDelegate LotteryServiceDelegate;

        public FormLogin()
        {
            InitializeComponent();
        }

        #region 窗体事件处理
        private void login_Load(object sender, EventArgs e)
        {
            try
            {
                new Common.TransService().Logout();
            }
            catch
            {
            }
            finally
            {
                Param.Count = 0;
            }
        }

        private void autoLogin_CheckedChanged(object sender, EventArgs e)
        {
            if (this.autoLogin.Checked)
            {
                this.chkPwd.Checked = true;
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void login_FormClosed(object sender, FormClosedEventArgs e)
        {

        }
        //设置
        private void btnSet_Click(object sender, EventArgs e)
        {

        }
        #endregion

        #region 登录
        private void btnLogin_Click(object sender, EventArgs e)
        {
            TransService ts = new TransService();
            string username = this.txtUserName.Text;
            string password = this.txtPassword.Text;

            try
            {
                string result = ts.Login(username, password);
                if (LotteryServiceDelegate != null) LotteryServiceDelegate(true);
            }
            catch
            {
            }
            finally
            {
                Param.Count = 1;
            }

            this.Close();
        }


        private void theout(object source, System.Timers.ElapsedEventArgs e)
        {

        }
        #endregion

        #region 初始化用户信息
        private void txtUserName_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void txtUserName_Leave(object sender, EventArgs e)
        {

        }

        #endregion

        #region 点击回车键时出发登录事件
        private void txtPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                btnLogin.PerformClick();
            }
        }

        private void chkPwd_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                btnLogin.PerformClick();
            }
        }

        private void autoLogin_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                btnLogin.PerformClick();
            }
        }

        private void txtUserName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                btnLogin.PerformClick();
            }
        }
        #endregion
    }
}
