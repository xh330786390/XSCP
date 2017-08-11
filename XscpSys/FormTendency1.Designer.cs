namespace XscpSys
{
    partial class FormTendency1
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormTendency1));
            this.dgv1 = new System.Windows.Forms.DataGridView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cbType = new System.Windows.Forms.ComboBox();
            this.btnFind = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.cbCompare = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
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
            this.Dbl = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Dt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgv1)).BeginInit();
            this.groupBox1.SuspendLayout();
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
            this.Dbl,
            this.Dt});
            this.dgv1.Location = new System.Drawing.Point(2, 49);
            this.dgv1.Name = "dgv1";
            this.dgv1.ReadOnly = true;
            this.dgv1.RowHeadersVisible = false;
            this.dgv1.RowTemplate.Height = 23;
            this.dgv1.Size = new System.Drawing.Size(710, 558);
            this.dgv1.TabIndex = 40;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cbType);
            this.groupBox1.Controls.Add(this.btnFind);
            this.groupBox1.Controls.Add(this.textBox1);
            this.groupBox1.Controls.Add(this.cbCompare);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.comboBox1);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(2, 5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(790, 38);
            this.groupBox1.TabIndex = 44;
            this.groupBox1.TabStop = false;
            // 
            // cbType
            // 
            this.cbType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbType.FormattingEnabled = true;
            this.cbType.Items.AddRange(new object[] {
            "大于等于",
            "小于等于",
            "不等于"});
            this.cbType.Location = new System.Drawing.Point(222, 12);
            this.cbType.Name = "cbType";
            this.cbType.Size = new System.Drawing.Size(82, 20);
            this.cbType.TabIndex = 50;
            this.cbType.SelectedIndexChanged += new System.EventHandler(this.cbType_SelectedIndexChanged);
            // 
            // btnFind
            // 
            this.btnFind.Location = new System.Drawing.Point(503, 10);
            this.btnFind.Name = "btnFind";
            this.btnFind.Size = new System.Drawing.Size(62, 23);
            this.btnFind.TabIndex = 49;
            this.btnFind.Text = "查询";
            this.btnFind.UseVisualStyleBackColor = true;
            this.btnFind.Click += new System.EventHandler(this.btnFind_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(423, 12);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(63, 21);
            this.textBox1.TabIndex = 48;
            this.textBox1.Text = "10";
            // 
            // cbCompare
            // 
            this.cbCompare.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbCompare.FormattingEnabled = true;
            this.cbCompare.Items.AddRange(new object[] {
            "大于等于",
            "小于等于",
            "不等于"});
            this.cbCompare.Location = new System.Drawing.Point(322, 12);
            this.cbCompare.Name = "cbCompare";
            this.cbCompare.Size = new System.Drawing.Size(82, 20);
            this.cbCompare.TabIndex = 47;
            this.cbCompare.SelectedIndexChanged += new System.EventHandler(this.cbCompare_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(175, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 46;
            this.label2.Text = "线长：";
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "近 30条",
            "近 50条",
            "近 100条",
            "所有记录"});
            this.comboBox1.Location = new System.Drawing.Point(65, 12);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(83, 20);
            this.comboBox1.TabIndex = 45;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 44;
            this.label1.Text = "记录数：";
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
            this.Big.HeaderText = "0";
            this.Big.Name = "Big";
            this.Big.ReadOnly = true;
            this.Big.Width = 40;
            // 
            // Small
            // 
            this.Small.DataPropertyName = "Small";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Small.DefaultCellStyle = dataGridViewCellStyle5;
            this.Small.HeaderText = "1";
            this.Small.Name = "Small";
            this.Small.ReadOnly = true;
            this.Small.Width = 40;
            // 
            // BigSmall
            // 
            this.BigSmall.DataPropertyName = "BigSmall";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.BigSmall.DefaultCellStyle = dataGridViewCellStyle6;
            this.BigSmall.HeaderText = "2";
            this.BigSmall.Name = "BigSmall";
            this.BigSmall.ReadOnly = true;
            this.BigSmall.Width = 40;
            // 
            // SmallBig
            // 
            this.SmallBig.DataPropertyName = "SmallBig";
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.SmallBig.DefaultCellStyle = dataGridViewCellStyle7;
            this.SmallBig.HeaderText = "3";
            this.SmallBig.Name = "SmallBig";
            this.SmallBig.ReadOnly = true;
            this.SmallBig.Width = 40;
            // 
            // Column12
            // 
            this.Column12.HeaderText = "4";
            this.Column12.Name = "Column12";
            this.Column12.ReadOnly = true;
            this.Column12.Width = 40;
            // 
            // Odd
            // 
            this.Odd.DataPropertyName = "Odd";
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Odd.DefaultCellStyle = dataGridViewCellStyle8;
            this.Odd.HeaderText = "5";
            this.Odd.Name = "Odd";
            this.Odd.ReadOnly = true;
            this.Odd.Width = 40;
            // 
            // Pair
            // 
            this.Pair.DataPropertyName = "Pair";
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Pair.DefaultCellStyle = dataGridViewCellStyle9;
            this.Pair.HeaderText = "6";
            this.Pair.Name = "Pair";
            this.Pair.ReadOnly = true;
            this.Pair.Width = 40;
            // 
            // OddPair
            // 
            this.OddPair.DataPropertyName = "OddPair";
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.OddPair.DefaultCellStyle = dataGridViewCellStyle10;
            this.OddPair.HeaderText = "7";
            this.OddPair.Name = "OddPair";
            this.OddPair.ReadOnly = true;
            this.OddPair.Width = 40;
            // 
            // PairOdd
            // 
            this.PairOdd.DataPropertyName = "PairOdd";
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.PairOdd.DefaultCellStyle = dataGridViewCellStyle11;
            this.PairOdd.HeaderText = "8";
            this.PairOdd.Name = "PairOdd";
            this.PairOdd.ReadOnly = true;
            this.PairOdd.Width = 40;
            // 
            // Dbl
            // 
            this.Dbl.DataPropertyName = "Dbl";
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Dbl.DefaultCellStyle = dataGridViewCellStyle12;
            this.Dbl.HeaderText = "9";
            this.Dbl.Name = "Dbl";
            this.Dbl.ReadOnly = true;
            this.Dbl.Width = 40;
            // 
            // Dt
            // 
            this.Dt.DataPropertyName = "Dt";
            dataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Dt.DefaultCellStyle = dataGridViewCellStyle13;
            this.Dt.HeaderText = "开奖时间";
            this.Dt.Name = "Dt";
            this.Dt.ReadOnly = true;
            this.Dt.Width = 80;
            // 
            // FormTendency1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(714, 610);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.dgv1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FormTendency1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "开奖趋势";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.FormTendency_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgv1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgv1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbCompare;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button btnFind;
        private System.Windows.Forms.ComboBox cbType;
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
        private System.Windows.Forms.DataGridViewTextBoxColumn Dbl;
        private System.Windows.Forms.DataGridViewTextBoxColumn Dt;
    }
}