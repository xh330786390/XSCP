using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using XScpStatistics.Common;
using XScpStatistics.Model;
namespace XScpStatistics
{
    public partial class FormTj : Form
    {
        public FormTj()
        {
            InitializeComponent();
        }

        private void FormTj_Load(object sender, EventArgs e)
        {
            Forecast.AddAllUnitNames();
            Forecast.AnalyzeAllForecasts();

            if (Forecast.Lt_AllForecasts.Count > 0)
            {
                initDgv();
                //DgvController.RefreshDgvColor(this.dgv1);
            }
        }


        private void initDgv()
        {
            DgvController.AddRows(this.dgv1, Forecast.Lt_AllForecasts.Count);
            AllForecastMode fcm;
            for (int i = 0, j = Forecast.Lt_AllForecasts.Count - 1; i < Forecast.Lt_AllForecasts.Count; i++)
            {
                fcm = Forecast.Lt_AllForecasts[j];
                this.dgv1[0, i].Value = i + 1;
                this.dgv1[1, i].Value = fcm.num1;
                this.dgv1[2, i].Value = fcm.num2;
                this.dgv1[3, i].Value = fcm.num3;
                this.dgv1[4, i].Value = fcm.num4;
                this.dgv1[5, i].Value = fcm.num5;
                this.dgv1[6, i].Value = fcm.num6;
                this.dgv1[7, i].Value = fcm.num7;
                this.dgv1[8, i].Value = fcm.num8;
                j--;
            }
        }
    }
}
