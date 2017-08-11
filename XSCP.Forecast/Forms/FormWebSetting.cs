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
using XSCP.Common.Model;
using XSCP.Common.Extend;
namespace XSCP.Forecast
{
    public partial class FormWebSetting : Form
    {

        private List<string> lt_domain = new List<string>() 
        { 
            "goxs.co",
            "xsgo8.com",
            "xs6868.com",
            "xs8cp.com",
            "xs8cp.info",
            "run88.co",
        };

        public FormWebSetting()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 获取Cookie
        /// </summary>
        /// <returns></returns>
        private List<CookieModel> GetCookies()
        {
            List<CookieModel> lt_cookie = new List<CookieModel>();
            for (int i = 0; i < this.dgvDigit.Rows.Count; i++)
            {
                CookieModel cookie = new CookieModel();
                DataGridViewRow row = this.dgvDigit.Rows[i];
                if (row.Cells["CookieValue"].Value == null || row.Cells["CookieValue"].Value.ToString().Trim() == "") continue;

                cookie.DomainName = row.Cells["DomainName"].Value.ToString();
                cookie.CookieName = row.Cells["CookieName"].Value.ToString();
                cookie.CookieValue = row.Cells["CookieValue"].Value.ToString();
                cookie.CookiePath = row.Cells["CookiePath"].Value == null ? "" : row.Cells["CookiePath"].Value.ToString();
                lt_cookie.Add(cookie);
            }
            return lt_cookie;
        }

        /// <summary>
        /// 连接
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnConnect_Click(object sender, EventArgs e)
        {
            WebHelper.Connected = false;

            List<CookieModel> lt_cookeis = GetCookies();
            WebHelper.Cookie = GetCookies(lt_cookeis);//设置Cookie

            string url = this.txtUrl.Text + "/page/WORecord.shtml";
            Dictionary<string, string> param = new Dictionary<string, string>();
            param["id"] = "15";
            param["num"] = this.txtNum.Text;
            WebHelper.Url = setUrl(url, param);//设置Url

            string result = WebHelper.Get(WebHelper.Url);
            List<string> ltData = result.TransLottery();

            if (ltData == null)
            {
                MessageBox.Show("连接失败，请重新连接！");
                return;
            }

            WebHelper.Connected = true;
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        /// <summary>
        /// 设置Cookie
        /// </summary>
        /// <param name="lt_cookie"></param>
        private CookieContainer GetCookies(List<CookieModel> lt_cookie)
        {
            CookieContainer cookie = new CookieContainer();
            CookieCollection lt_cookies = new CookieCollection();

            lt_cookie.ForEach(l =>
            {
                lt_cookies.Add(new Cookie(l.CookieName, l.CookieValue, l.CookiePath, l.DomainName));
            });

            cookie.Add(lt_cookies);
            return cookie;
        }

        /// <summary>
        /// 设置Url
        /// </summary>
        /// <param name="url"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        private string setUrl(string url, Dictionary<String, String> param)
        {
            if (param != null) //有参数的情况下，拼接url
            {
                url = url + "?";
                foreach (var item in param)
                {
                    url = url + item.Key + "=" + item.Value + "&";
                }
                url = url.Substring(0, url.Length - 1);
            }
            return url;
        }

        private void FormWeb_Load(object sender, EventArgs e)
        {
            List<string> lt_alldomain = XSCP.Common.XscpBLL.QueryCookieDomain();
            List<string> lt_ffdomain = new List<string>();

            lt_alldomain.ForEach(l =>
            {
                lt_domain.ForEach(f =>
                {
                    if (l.Contains(f))
                    {
                        lt_ffdomain.Add(l);
                    }
                });
            });

            lt_ffdomain.ForEach(l =>
            {
                this.comDomain.Items.Add(l);
            });

            if (this.comDomain.Items.Count > 0)
            {
                this.comDomain.SelectedIndex = 0;
            }

            this.comType.Items.Add("分分彩");
            this.comType.SelectedIndex = 0;
        }

        private void comDomain_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<CookieModel> lt_cookie = XSCP.Common.XscpBLL.QueryCookieKeys("'" + this.comDomain.Text + "'");
            this.dgvDigit.DataSource = lt_cookie;
        }
    }
}
