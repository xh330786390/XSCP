namespace XSCP.Forecast
{
    partial class FormMain
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle56 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle57 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle58 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle59 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle60 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle61 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle62 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle63 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle64 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle65 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle66 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tsXscp = new System.Windows.Forms.ToolStrip();
            this.tsMenuStart = new System.Windows.Forms.ToolStripButton();
            this.tsMenuStop = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsMenuLottery = new System.Windows.Forms.ToolStripButton();
            this.tsMenuTest = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.dtp = new System.Windows.Forms.DateTimePicker();
            this.label14 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblWarning = new System.Windows.Forms.Label();
            this.lblFF2 = new System.Windows.Forms.Label();
            this.lblFF3 = new System.Windows.Forms.Label();
            this.lblFF4 = new System.Windows.Forms.Label();
            this.lblFF5 = new System.Windows.Forms.Label();
            this.lblFF1 = new System.Windows.Forms.Label();
            this.lblFFTime = new System.Windows.Forms.Label();
            this.lblFFSno = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.dgvFF = new System.Windows.Forms.DataGridView();
            this.Sno = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Big = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Small = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BigSmall = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SmallBig = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Odd = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Pair = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OddPair = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PairOdd = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Dbl = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Tendency2 = new System.Windows.Forms.DataGridViewButtonColumn();
            this.Statistics = new System.Windows.Forms.DataGridViewButtonColumn();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.lblTime = new System.Windows.Forms.Label();
            this.tsXscp.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFF)).BeginInit();
            this.SuspendLayout();
            // 
            // tsXscp
            // 
            this.tsXscp.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsMenuStart,
            this.tsMenuStop,
            this.toolStripSeparator1,
            this.tsMenuLottery,
            this.tsMenuTest,
            this.toolStripSeparator2});
            this.tsXscp.Location = new System.Drawing.Point(0, 0);
            this.tsXscp.Name = "tsXscp";
            this.tsXscp.Size = new System.Drawing.Size(642, 25);
            this.tsXscp.TabIndex = 0;
            this.tsXscp.Text = "toolStrip1";
            // 
            // tsMenuStart
            // 
            this.tsMenuStart.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsMenuStart.Image = ((System.Drawing.Image)(resources.GetObject("tsMenuStart.Image")));
            this.tsMenuStart.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsMenuStart.Name = "tsMenuStart";
            this.tsMenuStart.Size = new System.Drawing.Size(36, 22);
            this.tsMenuStart.Text = "启动";
            this.tsMenuStart.Click += new System.EventHandler(this.tsMenuStart_Click);
            // 
            // tsMenuStop
            // 
            this.tsMenuStop.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsMenuStop.Image = ((System.Drawing.Image)(resources.GetObject("tsMenuStop.Image")));
            this.tsMenuStop.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsMenuStop.Name = "tsMenuStop";
            this.tsMenuStop.Size = new System.Drawing.Size(36, 22);
            this.tsMenuStop.Text = "停止";
            this.tsMenuStop.Click += new System.EventHandler(this.tsMenuStop_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // tsMenuLottery
            // 
            this.tsMenuLottery.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsMenuLottery.Image = ((System.Drawing.Image)(resources.GetObject("tsMenuLottery.Image")));
            this.tsMenuLottery.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsMenuLottery.Name = "tsMenuLottery";
            this.tsMenuLottery.Size = new System.Drawing.Size(60, 22);
            this.tsMenuLottery.Text = "奖号更新";
            this.tsMenuLottery.Click += new System.EventHandler(this.tsMenuLottery_Click);
            // 
            // tsMenuTest
            // 
            this.tsMenuTest.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsMenuTest.Image = ((System.Drawing.Image)(resources.GetObject("tsMenuTest.Image")));
            this.tsMenuTest.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsMenuTest.Name = "tsMenuTest";
            this.tsMenuTest.Size = new System.Drawing.Size(60, 22);
            this.tsMenuTest.Text = "异常检测";
            this.tsMenuTest.Click += new System.EventHandler(this.tsMenuTest_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.textBox4);
            this.groupBox2.Controls.Add(this.label13);
            this.groupBox2.Controls.Add(this.textBox3);
            this.groupBox2.Controls.Add(this.textBox2);
            this.groupBox2.Controls.Add(this.textBox1);
            this.groupBox2.Controls.Add(this.dtp);
            this.groupBox2.Controls.Add(this.label14);
            this.groupBox2.Location = new System.Drawing.Point(3, 23);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(634, 48);
            this.groupBox2.TabIndex = 48;
            this.groupBox2.TabStop = false;
            // 
            // textBox4
            // 
            this.textBox4.BackColor = System.Drawing.Color.DarkViolet;
            this.textBox4.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBox4.Location = new System.Drawing.Point(539, 14);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(40, 26);
            this.textBox4.TabIndex = 39;
            this.textBox4.Text = "35";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label13.ForeColor = System.Drawing.Color.Black;
            this.label13.Location = new System.Drawing.Point(300, 18);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(64, 16);
            this.label13.TabIndex = 38;
            this.label13.Text = "预警色:";
            // 
            // textBox3
            // 
            this.textBox3.BackColor = System.Drawing.Color.Red;
            this.textBox3.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBox3.Location = new System.Drawing.Point(485, 14);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(40, 26);
            this.textBox3.TabIndex = 37;
            this.textBox3.Text = "25";
            // 
            // textBox2
            // 
            this.textBox2.BackColor = System.Drawing.Color.Yellow;
            this.textBox2.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBox2.Location = new System.Drawing.Point(427, 14);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(40, 26);
            this.textBox2.TabIndex = 36;
            this.textBox2.Text = "20";
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.Color.Lime;
            this.textBox1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBox1.Location = new System.Drawing.Point(372, 14);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(40, 26);
            this.textBox1.TabIndex = 35;
            this.textBox1.Text = "10";
            // 
            // dtp
            // 
            this.dtp.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dtp.Location = new System.Drawing.Point(102, 13);
            this.dtp.Name = "dtp";
            this.dtp.Size = new System.Drawing.Size(138, 26);
            this.dtp.TabIndex = 34;
            this.dtp.ValueChanged += new System.EventHandler(this.dtp_ValueChanged);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label14.ForeColor = System.Drawing.Color.Black;
            this.label14.Location = new System.Drawing.Point(6, 18);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(80, 16);
            this.label14.TabIndex = 33;
            this.label14.Text = "开奖日期:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblTime);
            this.groupBox1.Controls.Add(this.lblWarning);
            this.groupBox1.Controls.Add(this.lblFF2);
            this.groupBox1.Controls.Add(this.lblFF3);
            this.groupBox1.Controls.Add(this.lblFF4);
            this.groupBox1.Controls.Add(this.lblFF5);
            this.groupBox1.Controls.Add(this.lblFF1);
            this.groupBox1.Controls.Add(this.lblFFTime);
            this.groupBox1.Controls.Add(this.lblFFSno);
            this.groupBox1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox1.Location = new System.Drawing.Point(3, 70);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(634, 48);
            this.groupBox1.TabIndex = 50;
            this.groupBox1.TabStop = false;
            // 
            // lblWarning
            // 
            this.lblWarning.AutoSize = true;
            this.lblWarning.BackColor = System.Drawing.SystemColors.Control;
            this.lblWarning.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblWarning.ForeColor = System.Drawing.Color.Blue;
            this.lblWarning.Location = new System.Drawing.Point(449, 17);
            this.lblWarning.Name = "lblWarning";
            this.lblWarning.Size = new System.Drawing.Size(185, 24);
            this.lblWarning.TabIndex = 50;
            this.lblWarning.Text = "杜绝长线。。。";
            // 
            // lblFF2
            // 
            this.lblFF2.AutoSize = true;
            this.lblFF2.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblFF2.ForeColor = System.Drawing.Color.Blue;
            this.lblFF2.Location = new System.Drawing.Point(149, 17);
            this.lblFF2.Name = "lblFF2";
            this.lblFF2.Size = new System.Drawing.Size(22, 21);
            this.lblFF2.TabIndex = 49;
            this.lblFF2.Text = "X";
            // 
            // lblFF3
            // 
            this.lblFF3.AutoSize = true;
            this.lblFF3.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblFF3.ForeColor = System.Drawing.Color.Blue;
            this.lblFF3.Location = new System.Drawing.Point(186, 17);
            this.lblFF3.Name = "lblFF3";
            this.lblFF3.Size = new System.Drawing.Size(22, 21);
            this.lblFF3.TabIndex = 48;
            this.lblFF3.Text = "X";
            // 
            // lblFF4
            // 
            this.lblFF4.AutoSize = true;
            this.lblFF4.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblFF4.ForeColor = System.Drawing.Color.Blue;
            this.lblFF4.Location = new System.Drawing.Point(218, 17);
            this.lblFF4.Name = "lblFF4";
            this.lblFF4.Size = new System.Drawing.Size(22, 21);
            this.lblFF4.TabIndex = 47;
            this.lblFF4.Text = "X";
            // 
            // lblFF5
            // 
            this.lblFF5.AutoSize = true;
            this.lblFF5.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblFF5.ForeColor = System.Drawing.Color.Blue;
            this.lblFF5.Location = new System.Drawing.Point(253, 17);
            this.lblFF5.Name = "lblFF5";
            this.lblFF5.Size = new System.Drawing.Size(22, 21);
            this.lblFF5.TabIndex = 46;
            this.lblFF5.Text = "X";
            // 
            // lblFF1
            // 
            this.lblFF1.AutoSize = true;
            this.lblFF1.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblFF1.ForeColor = System.Drawing.Color.Blue;
            this.lblFF1.Location = new System.Drawing.Point(115, 17);
            this.lblFF1.Name = "lblFF1";
            this.lblFF1.Size = new System.Drawing.Size(22, 21);
            this.lblFF1.TabIndex = 45;
            this.lblFF1.Text = "X";
            // 
            // lblFFTime
            // 
            this.lblFFTime.AutoSize = true;
            this.lblFFTime.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblFFTime.ForeColor = System.Drawing.Color.Blue;
            this.lblFFTime.Location = new System.Drawing.Point(294, 17);
            this.lblFFTime.Name = "lblFFTime";
            this.lblFFTime.Size = new System.Drawing.Size(64, 20);
            this.lblFFTime.TabIndex = 44;
            this.lblFFTime.Text = "11:30";
            // 
            // lblFFSno
            // 
            this.lblFFSno.AutoSize = true;
            this.lblFFSno.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblFFSno.ForeColor = System.Drawing.Color.Black;
            this.lblFFSno.Location = new System.Drawing.Point(25, 18);
            this.lblFFSno.Name = "lblFFSno";
            this.lblFFSno.Size = new System.Drawing.Size(84, 19);
            this.lblFFSno.TabIndex = 32;
            this.lblFFSno.Text = "1234期:";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.dgvFF);
            this.groupBox4.Location = new System.Drawing.Point(3, 121);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(635, 227);
            this.groupBox4.TabIndex = 54;
            this.groupBox4.TabStop = false;
            // 
            // dgvFF
            // 
            this.dgvFF.AllowUserToAddRows = false;
            this.dgvFF.AllowUserToDeleteRows = false;
            this.dgvFF.AllowUserToResizeColumns = false;
            this.dgvFF.AllowUserToResizeRows = false;
            dataGridViewCellStyle56.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle56.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle56.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle56.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle56.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle56.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle56.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvFF.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle56;
            this.dgvFF.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvFF.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Sno,
            this.Big,
            this.Small,
            this.BigSmall,
            this.SmallBig,
            this.dataGridViewTextBoxColumn6,
            this.Odd,
            this.Pair,
            this.OddPair,
            this.PairOdd,
            this.Dbl,
            this.Tendency2,
            this.Statistics});
            this.dgvFF.Location = new System.Drawing.Point(4, 12);
            this.dgvFF.Name = "dgvFF";
            this.dgvFF.ReadOnly = true;
            this.dgvFF.RowHeadersVisible = false;
            this.dgvFF.RowTemplate.Height = 30;
            this.dgvFF.Size = new System.Drawing.Size(624, 209);
            this.dgvFF.TabIndex = 51;
            this.dgvFF.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvFF_CellClick);
            // 
            // Sno
            // 
            this.Sno.DataPropertyName = "Sno";
            dataGridViewCellStyle57.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle57.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Sno.DefaultCellStyle = dataGridViewCellStyle57;
            this.Sno.HeaderText = "类型";
            this.Sno.Name = "Sno";
            this.Sno.ReadOnly = true;
            this.Sno.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Sno.Width = 52;
            // 
            // Big
            // 
            this.Big.DataPropertyName = "Big";
            dataGridViewCellStyle58.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle58.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Big.DefaultCellStyle = dataGridViewCellStyle58;
            this.Big.HeaderText = "大大";
            this.Big.Name = "Big";
            this.Big.ReadOnly = true;
            this.Big.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Big.Width = 52;
            // 
            // Small
            // 
            this.Small.DataPropertyName = "Small";
            dataGridViewCellStyle59.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle59.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Small.DefaultCellStyle = dataGridViewCellStyle59;
            this.Small.HeaderText = "小小";
            this.Small.Name = "Small";
            this.Small.ReadOnly = true;
            this.Small.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Small.Width = 52;
            // 
            // BigSmall
            // 
            this.BigSmall.DataPropertyName = "BigSmall";
            dataGridViewCellStyle60.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle60.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.BigSmall.DefaultCellStyle = dataGridViewCellStyle60;
            this.BigSmall.HeaderText = "大小";
            this.BigSmall.Name = "BigSmall";
            this.BigSmall.ReadOnly = true;
            this.BigSmall.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.BigSmall.Width = 52;
            // 
            // SmallBig
            // 
            this.SmallBig.DataPropertyName = "SmallBig";
            dataGridViewCellStyle61.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle61.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.SmallBig.DefaultCellStyle = dataGridViewCellStyle61;
            this.SmallBig.HeaderText = "小大";
            this.SmallBig.Name = "SmallBig";
            this.SmallBig.ReadOnly = true;
            this.SmallBig.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.SmallBig.Width = 52;
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.HeaderText = "";
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            this.dataGridViewTextBoxColumn6.ReadOnly = true;
            this.dataGridViewTextBoxColumn6.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn6.Width = 20;
            // 
            // Odd
            // 
            this.Odd.DataPropertyName = "Odd";
            dataGridViewCellStyle62.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle62.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Odd.DefaultCellStyle = dataGridViewCellStyle62;
            this.Odd.HeaderText = "奇奇";
            this.Odd.Name = "Odd";
            this.Odd.ReadOnly = true;
            this.Odd.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Odd.Width = 52;
            // 
            // Pair
            // 
            this.Pair.DataPropertyName = "Pair";
            dataGridViewCellStyle63.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle63.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Pair.DefaultCellStyle = dataGridViewCellStyle63;
            this.Pair.HeaderText = "偶偶";
            this.Pair.Name = "Pair";
            this.Pair.ReadOnly = true;
            this.Pair.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Pair.Width = 52;
            // 
            // OddPair
            // 
            this.OddPair.DataPropertyName = "OddPair";
            dataGridViewCellStyle64.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle64.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.OddPair.DefaultCellStyle = dataGridViewCellStyle64;
            this.OddPair.HeaderText = "奇偶";
            this.OddPair.Name = "OddPair";
            this.OddPair.ReadOnly = true;
            this.OddPair.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.OddPair.Width = 52;
            // 
            // PairOdd
            // 
            this.PairOdd.DataPropertyName = "PairOdd";
            dataGridViewCellStyle65.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle65.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.PairOdd.DefaultCellStyle = dataGridViewCellStyle65;
            this.PairOdd.HeaderText = "偶奇";
            this.PairOdd.Name = "PairOdd";
            this.PairOdd.ReadOnly = true;
            this.PairOdd.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.PairOdd.Width = 52;
            // 
            // Dbl
            // 
            this.Dbl.DataPropertyName = "Dbl";
            dataGridViewCellStyle66.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle66.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Dbl.DefaultCellStyle = dataGridViewCellStyle66;
            this.Dbl.HeaderText = "重";
            this.Dbl.Name = "Dbl";
            this.Dbl.ReadOnly = true;
            this.Dbl.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Dbl.Width = 40;
            // 
            // Tendency2
            // 
            this.Tendency2.DataPropertyName = "Tendency2";
            this.Tendency2.HeaderText = "";
            this.Tendency2.Name = "Tendency2";
            this.Tendency2.ReadOnly = true;
            this.Tendency2.Width = 45;
            // 
            // Statistics
            // 
            this.Statistics.DataPropertyName = "Statistics";
            this.Statistics.HeaderText = "";
            this.Statistics.Name = "Statistics";
            this.Statistics.ReadOnly = true;
            this.Statistics.Width = 45;
            // 
            // timer
            // 
            this.timer.Interval = 500;
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // lblTime
            // 
            this.lblTime.AutoSize = true;
            this.lblTime.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTime.ForeColor = System.Drawing.Color.Red;
            this.lblTime.Location = new System.Drawing.Point(367, 14);
            this.lblTime.Name = "lblTime";
            this.lblTime.Size = new System.Drawing.Size(75, 24);
            this.lblTime.TabIndex = 53;
            this.lblTime.Text = "00:59";
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(642, 353);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.tsXscp);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FormMain";
            this.Text = "新生服务-分分彩";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormMain_FormClosed);
            this.Load += new System.EventHandler(this.FormMainDigit_Load);
            this.tsXscp.ResumeLayout(false);
            this.tsXscp.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvFF)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip tsXscp;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.DateTimePicker dtp;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblFF2;
        private System.Windows.Forms.Label lblFF3;
        private System.Windows.Forms.Label lblFF4;
        private System.Windows.Forms.Label lblFF5;
        private System.Windows.Forms.Label lblFF1;
        private System.Windows.Forms.Label lblFFTime;
        private System.Windows.Forms.Label lblFFSno;
        private System.Windows.Forms.ToolStripButton tsMenuStart;
        private System.Windows.Forms.ToolStripButton tsMenuStop;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.DataGridView dgvFF;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton tsMenuLottery;
        private System.Windows.Forms.Label lblWarning;
        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.ToolStripButton tsMenuTest;
        private System.Windows.Forms.DataGridViewTextBoxColumn Sno;
        private System.Windows.Forms.DataGridViewTextBoxColumn Big;
        private System.Windows.Forms.DataGridViewTextBoxColumn Small;
        private System.Windows.Forms.DataGridViewTextBoxColumn BigSmall;
        private System.Windows.Forms.DataGridViewTextBoxColumn SmallBig;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private System.Windows.Forms.DataGridViewTextBoxColumn Odd;
        private System.Windows.Forms.DataGridViewTextBoxColumn Pair;
        private System.Windows.Forms.DataGridViewTextBoxColumn OddPair;
        private System.Windows.Forms.DataGridViewTextBoxColumn PairOdd;
        private System.Windows.Forms.DataGridViewTextBoxColumn Dbl;
        private System.Windows.Forms.DataGridViewButtonColumn Tendency2;
        private System.Windows.Forms.DataGridViewButtonColumn Statistics;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.Label lblTime;

    }
}

