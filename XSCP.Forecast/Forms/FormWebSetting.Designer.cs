namespace  XSCP.Forecast
{
    partial class FormWebSetting
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormWebSetting));
            this.btnConnet = new System.Windows.Forms.Button();
            this.dgvDigit = new System.Windows.Forms.DataGridView();
            this.DomainName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CookieName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CookieValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CookiePath = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtNum = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtPwd = new System.Windows.Forms.TextBox();
            this.comDomain = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.comType = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtUser = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtUrl = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDigit)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnConnet
            // 
            this.btnConnet.Location = new System.Drawing.Point(451, 266);
            this.btnConnet.Name = "btnConnet";
            this.btnConnet.Size = new System.Drawing.Size(75, 23);
            this.btnConnet.TabIndex = 9;
            this.btnConnet.Text = "连接";
            this.btnConnet.UseVisualStyleBackColor = true;
            this.btnConnet.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // dgvDigit
            // 
            this.dgvDigit.AllowUserToAddRows = false;
            this.dgvDigit.AllowUserToDeleteRows = false;
            this.dgvDigit.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvDigit.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvDigit.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.DomainName,
            this.CookieName,
            this.CookieValue,
            this.CookiePath});
            this.dgvDigit.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgvDigit.Location = new System.Drawing.Point(8, 107);
            this.dgvDigit.MultiSelect = false;
            this.dgvDigit.Name = "dgvDigit";
            this.dgvDigit.RowHeadersVisible = false;
            this.dgvDigit.RowHeadersWidth = 20;
            this.dgvDigit.RowTemplate.Height = 25;
            this.dgvDigit.Size = new System.Drawing.Size(520, 144);
            this.dgvDigit.TabIndex = 53;
            // 
            // DomainName
            // 
            this.DomainName.DataPropertyName = "DomainName";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.DomainName.DefaultCellStyle = dataGridViewCellStyle2;
            this.DomainName.HeaderText = "域名";
            this.DomainName.Name = "DomainName";
            this.DomainName.ReadOnly = true;
            this.DomainName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // CookieName
            // 
            this.CookieName.DataPropertyName = "CookieName";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.CookieName.DefaultCellStyle = dataGridViewCellStyle3;
            this.CookieName.HeaderText = "名称";
            this.CookieName.Name = "CookieName";
            this.CookieName.ReadOnly = true;
            this.CookieName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.CookieName.Width = 130;
            // 
            // CookieValue
            // 
            this.CookieValue.DataPropertyName = "CookieValue";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.CookieValue.DefaultCellStyle = dataGridViewCellStyle4;
            this.CookieValue.HeaderText = "值";
            this.CookieValue.Name = "CookieValue";
            this.CookieValue.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.CookieValue.Width = 200;
            // 
            // CookiePath
            // 
            this.CookiePath.DataPropertyName = "CookiePath";
            this.CookiePath.HeaderText = "路径";
            this.CookiePath.Name = "CookiePath";
            this.CookiePath.ReadOnly = true;
            this.CookiePath.Width = 60;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.txtNum);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.txtPwd);
            this.groupBox1.Controls.Add(this.comDomain);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.comType);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtUser);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtUrl);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(8, 8);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(520, 84);
            this.groupBox1.TabIndex = 56;
            this.groupBox1.TabStop = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(430, 56);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(41, 12);
            this.label6.TabIndex = 67;
            this.label6.Text = "期数：";
            // 
            // txtNum
            // 
            this.txtNum.Location = new System.Drawing.Point(473, 52);
            this.txtNum.Name = "txtNum";
            this.txtNum.Size = new System.Drawing.Size(42, 21);
            this.txtNum.TabIndex = 66;
            this.txtNum.Text = "10";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(303, 56);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 12);
            this.label5.TabIndex = 65;
            this.label5.Text = "密码：";
            // 
            // txtPwd
            // 
            this.txtPwd.Location = new System.Drawing.Point(347, 52);
            this.txtPwd.Name = "txtPwd";
            this.txtPwd.PasswordChar = '*';
            this.txtPwd.Size = new System.Drawing.Size(72, 21);
            this.txtPwd.TabIndex = 64;
            this.txtPwd.Text = "zxj361226";
            // 
            // comDomain
            // 
            this.comDomain.BackColor = System.Drawing.Color.White;
            this.comDomain.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comDomain.FormattingEnabled = true;
            this.comDomain.Location = new System.Drawing.Point(57, 52);
            this.comDomain.Name = "comDomain";
            this.comDomain.Size = new System.Drawing.Size(97, 20);
            this.comDomain.TabIndex = 63;
            this.comDomain.SelectedIndexChanged += new System.EventHandler(this.comDomain_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(10, 55);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 12);
            this.label4.TabIndex = 62;
            this.label4.Text = "域名：";
            // 
            // comType
            // 
            this.comType.BackColor = System.Drawing.Color.White;
            this.comType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comType.FormattingEnabled = true;
            this.comType.Location = new System.Drawing.Point(57, 19);
            this.comType.Name = "comType";
            this.comType.Size = new System.Drawing.Size(97, 20);
            this.comType.TabIndex = 61;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(167, 56);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 60;
            this.label3.Text = "用户名：";
            // 
            // txtUser
            // 
            this.txtUser.Location = new System.Drawing.Point(222, 52);
            this.txtUser.Name = "txtUser";
            this.txtUser.Size = new System.Drawing.Size(72, 21);
            this.txtUser.TabIndex = 59;
            this.txtUser.Text = "xh168";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 58;
            this.label2.Text = "彩种：";
            // 
            // txtUrl
            // 
            this.txtUrl.Location = new System.Drawing.Point(214, 18);
            this.txtUrl.Name = "txtUrl";
            this.txtUrl.Size = new System.Drawing.Size(300, 21);
            this.txtUrl.TabIndex = 57;
            this.txtUrl.Text = "http://www.xs6868.com";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(167, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 56;
            this.label1.Text = "网址：";
            // 
            // FormWebSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(531, 296);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.dgvDigit);
            this.Controls.Add(this.btnConnet);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormWebSetting";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "连接设置";
            this.Load += new System.EventHandler(this.FormWeb_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDigit)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnConnet;
        private System.Windows.Forms.DataGridView dgvDigit;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox comDomain;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox comType;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtUser;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtUrl;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtNum;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtPwd;
        private System.Windows.Forms.DataGridViewTextBoxColumn DomainName;
        private System.Windows.Forms.DataGridViewTextBoxColumn CookieName;
        private System.Windows.Forms.DataGridViewTextBoxColumn CookieValue;
        private System.Windows.Forms.DataGridViewTextBoxColumn CookiePath;
    }
}