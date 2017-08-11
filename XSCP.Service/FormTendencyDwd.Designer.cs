namespace XSCP.Service
{
    partial class FormTendencyDwd
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormTendencyDwd));
            this.dgv1 = new System.Windows.Forms.DataGridView();
            this.ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SNO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Big = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Small = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BigSmall = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SmallBig = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column12 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Odd = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Pair = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OddPair = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PairOdd = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Dt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgv1)).BeginInit();
            this.SuspendLayout();
            // 
            // dgv1
            // 
            this.dgv1.AllowUserToAddRows = false;
            this.dgv1.AllowUserToDeleteRows = false;
            this.dgv1.AllowUserToResizeColumns = false;
            this.dgv1.AllowUserToResizeRows = false;
            this.dgv1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ID,
            this.SNO,
            this.Column1,
            this.Big,
            this.Small,
            this.BigSmall,
            this.SmallBig,
            this.Column12,
            this.Odd,
            this.Pair,
            this.OddPair,
            this.PairOdd,
            this.Dt});
            this.dgv1.Location = new System.Drawing.Point(4, 3);
            this.dgv1.Name = "dgv1";
            this.dgv1.ReadOnly = true;
            this.dgv1.RowHeadersVisible = false;
            this.dgv1.RowTemplate.Height = 23;
            this.dgv1.Size = new System.Drawing.Size(809, 604);
            this.dgv1.TabIndex = 40;
            // 
            // ID
            // 
            this.ID.DataPropertyName = "ID";
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.ID.DefaultCellStyle = dataGridViewCellStyle1;
            this.ID.HeaderText = "序号";
            this.ID.Name = "ID";
            this.ID.ReadOnly = true;
            this.ID.Width = 52;
            // 
            // SNO
            // 
            this.SNO.DataPropertyName = "SNO";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.SNO.DefaultCellStyle = dataGridViewCellStyle2;
            this.SNO.HeaderText = "开奖期号";
            this.SNO.Name = "SNO";
            this.SNO.ReadOnly = true;
            this.SNO.Width = 77;
            // 
            // Column1
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Column1.DefaultCellStyle = dataGridViewCellStyle3;
            this.Column1.HeaderText = "开奖号码";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Width = 77;
            // 
            // Big
            // 
            this.Big.DataPropertyName = "Big";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Big.DefaultCellStyle = dataGridViewCellStyle4;
            this.Big.HeaderText = "大";
            this.Big.Name = "Big";
            this.Big.ReadOnly = true;
            this.Big.Width = 60;
            // 
            // Small
            // 
            this.Small.DataPropertyName = "Small";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Small.DefaultCellStyle = dataGridViewCellStyle5;
            this.Small.HeaderText = "小";
            this.Small.Name = "Small";
            this.Small.ReadOnly = true;
            this.Small.Width = 60;
            // 
            // BigSmall
            // 
            this.BigSmall.DataPropertyName = "BigSmall";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.BigSmall.DefaultCellStyle = dataGridViewCellStyle6;
            this.BigSmall.HeaderText = "大小";
            this.BigSmall.Name = "BigSmall";
            this.BigSmall.ReadOnly = true;
            this.BigSmall.Width = 60;
            // 
            // SmallBig
            // 
            this.SmallBig.DataPropertyName = "SmallBig";
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.SmallBig.DefaultCellStyle = dataGridViewCellStyle7;
            this.SmallBig.HeaderText = "小大";
            this.SmallBig.Name = "SmallBig";
            this.SmallBig.ReadOnly = true;
            this.SmallBig.Width = 60;
            // 
            // Column12
            // 
            this.Column12.HeaderText = "";
            this.Column12.Name = "Column12";
            this.Column12.ReadOnly = true;
            this.Column12.Width = 20;
            // 
            // Odd
            // 
            this.Odd.DataPropertyName = "Odd";
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Odd.DefaultCellStyle = dataGridViewCellStyle8;
            this.Odd.HeaderText = "奇";
            this.Odd.Name = "Odd";
            this.Odd.ReadOnly = true;
            this.Odd.Width = 60;
            // 
            // Pair
            // 
            this.Pair.DataPropertyName = "Pair";
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Pair.DefaultCellStyle = dataGridViewCellStyle9;
            this.Pair.HeaderText = "偶";
            this.Pair.Name = "Pair";
            this.Pair.ReadOnly = true;
            this.Pair.Width = 60;
            // 
            // OddPair
            // 
            this.OddPair.DataPropertyName = "OddPair";
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.OddPair.DefaultCellStyle = dataGridViewCellStyle10;
            this.OddPair.HeaderText = "奇偶";
            this.OddPair.Name = "OddPair";
            this.OddPair.ReadOnly = true;
            this.OddPair.Width = 60;
            // 
            // PairOdd
            // 
            this.PairOdd.DataPropertyName = "PairOdd";
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.PairOdd.DefaultCellStyle = dataGridViewCellStyle11;
            this.PairOdd.HeaderText = "偶奇";
            this.PairOdd.Name = "PairOdd";
            this.PairOdd.ReadOnly = true;
            this.PairOdd.Width = 60;
            // 
            // Dt
            // 
            this.Dt.DataPropertyName = "Dt";
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Dt.DefaultCellStyle = dataGridViewCellStyle12;
            this.Dt.HeaderText = "开奖时间";
            this.Dt.Name = "Dt";
            this.Dt.ReadOnly = true;
            this.Dt.Width = 80;
            // 
            // FormTendencyDwd
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(817, 610);
            this.Controls.Add(this.dgv1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FormTendencyDwd";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "开奖趋势";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.FormTendency_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgv1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgv1;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn SNO;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Big;
        private System.Windows.Forms.DataGridViewTextBoxColumn Small;
        private System.Windows.Forms.DataGridViewTextBoxColumn BigSmall;
        private System.Windows.Forms.DataGridViewTextBoxColumn SmallBig;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column12;
        private System.Windows.Forms.DataGridViewTextBoxColumn Odd;
        private System.Windows.Forms.DataGridViewTextBoxColumn Pair;
        private System.Windows.Forms.DataGridViewTextBoxColumn OddPair;
        private System.Windows.Forms.DataGridViewTextBoxColumn PairOdd;
        private System.Windows.Forms.DataGridViewTextBoxColumn Dt;
    }
}