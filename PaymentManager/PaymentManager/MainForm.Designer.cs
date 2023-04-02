namespace PaymentManagerView
{
	partial class MainForm
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing) {
			if(disposing && (components != null)) {
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			this.CustomerGrid = new System.Windows.Forms.DataGridView();
			this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.addPAymentMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStrip1 = new System.Windows.Forms.ToolStrip();
			this.addCustomerButton = new System.Windows.Forms.ToolStripButton();
			this.addPaymentButton = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.deleteButton = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.exportToExcelButton = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
			this.rateLabel = new System.Windows.Forms.ToolStripLabel();
			this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
			this.tabControl = new System.Windows.Forms.TabControl();
			this.tabPage1 = new System.Windows.Forms.TabPage();
			this.debtMonth = new System.Windows.Forms.DateTimePicker();
			this.debtCustomersButton = new System.Windows.Forms.RadioButton();
			this.filteredCustomersButton = new System.Windows.Forms.RadioButton();
			this.customerFilterBox = new System.Windows.Forms.GroupBox();
			this.label4 = new System.Windows.Forms.Label();
			this.customerFilterNumberBox = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.customerFilterMiddleNameBox = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.customerFilterFirstNameBox = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.customerFilterLastNameBox = new System.Windows.Forms.TextBox();
			this.allCustomersButton = new System.Windows.Forms.RadioButton();
			this.tabPage2 = new System.Windows.Forms.TabPage();
			this.paymentFilterBox = new System.Windows.Forms.GroupBox();
			this.paymentCustomerID = new System.Windows.Forms.ComboBox();
			this.paymentMonthTo = new System.Windows.Forms.DateTimePicker();
			this.paymentDateTo = new System.Windows.Forms.DateTimePicker();
			this.paymentDateFrom = new System.Windows.Forms.DateTimePicker();
			this.paymentMonthFrom = new System.Windows.Forms.DateTimePicker();
			this.label16 = new System.Windows.Forms.Label();
			this.label13 = new System.Windows.Forms.Label();
			this.label19 = new System.Windows.Forms.Label();
			this.label10 = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.label15 = new System.Windows.Forms.Label();
			this.label12 = new System.Windows.Forms.Label();
			this.label18 = new System.Windows.Forms.Label();
			this.label9 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.paymentSumTo = new System.Windows.Forms.TextBox();
			this.paymentRateTo = new System.Windows.Forms.TextBox();
			this.paymentAmountTo = new System.Windows.Forms.TextBox();
			this.label20 = new System.Windows.Forms.Label();
			this.label14 = new System.Windows.Forms.Label();
			this.label11 = new System.Windows.Forms.Label();
			this.paymentSumFrom = new System.Windows.Forms.TextBox();
			this.label17 = new System.Windows.Forms.Label();
			this.paymentRateFrom = new System.Windows.Forms.TextBox();
			this.label8 = new System.Windows.Forms.Label();
			this.paymentAmountFrom = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.filteredPaymentsButton = new System.Windows.Forms.RadioButton();
			this.allPaymentsButton = new System.Windows.Forms.RadioButton();
			this.PaymentGrid = new System.Windows.Forms.DataGridView();
			this.dialogExcelFile = new System.Windows.Forms.SaveFileDialog();
			((System.ComponentModel.ISupportInitialize)(this.CustomerGrid)).BeginInit();
			this.contextMenuStrip1.SuspendLayout();
			this.toolStrip1.SuspendLayout();
			this.tabControl.SuspendLayout();
			this.tabPage1.SuspendLayout();
			this.customerFilterBox.SuspendLayout();
			this.tabPage2.SuspendLayout();
			this.paymentFilterBox.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.PaymentGrid)).BeginInit();
			this.SuspendLayout();
			// 
			// CustomerGrid
			// 
			this.CustomerGrid.AllowUserToAddRows = false;
			this.CustomerGrid.AllowUserToDeleteRows = false;
			this.CustomerGrid.AllowUserToOrderColumns = true;
			this.CustomerGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.CustomerGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
			this.CustomerGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.CustomerGrid.ContextMenuStrip = this.contextMenuStrip1;
			this.CustomerGrid.Location = new System.Drawing.Point(6, 6);
			this.CustomerGrid.MinimumSize = new System.Drawing.Size(100, 100);
			this.CustomerGrid.Name = "CustomerGrid";
			this.CustomerGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.CustomerGrid.Size = new System.Drawing.Size(516, 483);
			this.CustomerGrid.TabIndex = 0;
			this.CustomerGrid.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.CustomerGrid_CellValidating);
			this.CustomerGrid.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.CustomerGrid_CellEndEdit);
			// 
			// contextMenuStrip1
			// 
			this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addPAymentMenuItem});
			this.contextMenuStrip1.Name = "contextMenuStrip1";
			this.contextMenuStrip1.Size = new System.Drawing.Size(166, 48);
			// 
			// addPAymentMenuItem
			// 
			this.addPAymentMenuItem.Name = "addPAymentMenuItem";
			this.addPAymentMenuItem.Size = new System.Drawing.Size(165, 22);
			this.addPAymentMenuItem.Text = "Добавить платёж";
			this.addPAymentMenuItem.Click += new System.EventHandler(this.addPAymentMenuItem_Click);
			// 
			// toolStrip1
			// 
			this.toolStrip1.ImageScalingSize = new System.Drawing.Size(50, 50);
			this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addCustomerButton,
            this.addPaymentButton,
            this.toolStripSeparator2,
            this.deleteButton,
            this.toolStripSeparator1,
            this.exportToExcelButton,
            this.toolStripSeparator3,
            this.toolStripLabel1,
            this.rateLabel,
            this.toolStripLabel2});
			this.toolStrip1.Location = new System.Drawing.Point(0, 0);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.Size = new System.Drawing.Size(823, 57);
			this.toolStrip1.TabIndex = 1;
			this.toolStrip1.Text = "toolStrip1";
			// 
			// addCustomerButton
			// 
			this.addCustomerButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.addCustomerButton.Image = global::PaymentManager.Properties.Resources._016;
			this.addCustomerButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.addCustomerButton.Name = "addCustomerButton";
			this.addCustomerButton.Size = new System.Drawing.Size(54, 54);
			this.addCustomerButton.Text = "Добавить клиента";
			this.addCustomerButton.Click += new System.EventHandler(this.addCustomerButton_Click);
			// 
			// addPaymentButton
			// 
			this.addPaymentButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.addPaymentButton.Image = global::PaymentManager.Properties.Resources._032;
			this.addPaymentButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.addPaymentButton.Name = "addPaymentButton";
			this.addPaymentButton.Size = new System.Drawing.Size(54, 54);
			this.addPaymentButton.Text = "Добавить платеж";
			this.addPaymentButton.Click += new System.EventHandler(this.addPaymentButton_Click);
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(6, 57);
			// 
			// deleteButton
			// 
			this.deleteButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.deleteButton.Image = global::PaymentManager.Properties.Resources._363;
			this.deleteButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.deleteButton.Name = "deleteButton";
			this.deleteButton.Size = new System.Drawing.Size(54, 54);
			this.deleteButton.Text = "Удалить выделенные записи";
			this.deleteButton.Click += new System.EventHandler(this.deleteButton_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(6, 57);
			// 
			// exportToExcelButton
			// 
			this.exportToExcelButton.AutoSize = false;
			this.exportToExcelButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.exportToExcelButton.Image = global::PaymentManager.Properties.Resources.Excel;
			this.exportToExcelButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.exportToExcelButton.Name = "exportToExcelButton";
			this.exportToExcelButton.Size = new System.Drawing.Size(50, 50);
			this.exportToExcelButton.Text = "Экспорт в Excel";
			this.exportToExcelButton.Click += new System.EventHandler(this.exportToExcelButton_Click);
			// 
			// toolStripSeparator3
			// 
			this.toolStripSeparator3.Name = "toolStripSeparator3";
			this.toolStripSeparator3.Size = new System.Drawing.Size(6, 57);
			// 
			// toolStripLabel1
			// 
			this.toolStripLabel1.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.toolStripLabel1.Name = "toolStripLabel1";
			this.toolStripLabel1.Size = new System.Drawing.Size(82, 54);
			this.toolStripLabel1.Text = "Тариф:";
			// 
			// rateLabel
			// 
			this.rateLabel.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.rateLabel.IsLink = true;
			this.rateLabel.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
			this.rateLabel.Name = "rateLabel";
			this.rateLabel.Size = new System.Drawing.Size(150, 54);
			this.rateLabel.Text = "toolStripLabel2";
			this.rateLabel.Click += new System.EventHandler(this.rateLabel_Click);
			// 
			// toolStripLabel2
			// 
			this.toolStripLabel2.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.toolStripLabel2.Name = "toolStripLabel2";
			this.toolStripLabel2.Size = new System.Drawing.Size(146, 54);
			this.toolStripLabel2.Text = "руб/(кВт*час)";
			// 
			// tabControl
			// 
			this.tabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tabControl.Controls.Add(this.tabPage1);
			this.tabControl.Controls.Add(this.tabPage2);
			this.tabControl.Location = new System.Drawing.Point(12, 60);
			this.tabControl.Name = "tabControl";
			this.tabControl.SelectedIndex = 0;
			this.tabControl.Size = new System.Drawing.Size(790, 521);
			this.tabControl.TabIndex = 2;
			// 
			// tabPage1
			// 
			this.tabPage1.Controls.Add(this.debtMonth);
			this.tabPage1.Controls.Add(this.debtCustomersButton);
			this.tabPage1.Controls.Add(this.filteredCustomersButton);
			this.tabPage1.Controls.Add(this.customerFilterBox);
			this.tabPage1.Controls.Add(this.allCustomersButton);
			this.tabPage1.Controls.Add(this.CustomerGrid);
			this.tabPage1.Location = new System.Drawing.Point(4, 22);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage1.Size = new System.Drawing.Size(782, 495);
			this.tabPage1.TabIndex = 0;
			this.tabPage1.Text = "Клиенты";
			this.tabPage1.UseVisualStyleBackColor = true;
			// 
			// debtMonth
			// 
			this.debtMonth.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.debtMonth.CustomFormat = "MMMM yyyy";
			this.debtMonth.Enabled = false;
			this.debtMonth.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			this.debtMonth.Location = new System.Drawing.Point(642, 208);
			this.debtMonth.Name = "debtMonth";
			this.debtMonth.Size = new System.Drawing.Size(128, 20);
			this.debtMonth.TabIndex = 8;
			this.debtMonth.ValueChanged += new System.EventHandler(this.debtMonth_ValueChanged);
			this.debtMonth.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.debtMonth_KeyPress);
			this.debtMonth.Enter += new System.EventHandler(this.debtMonth_Enter);
			// 
			// debtCustomersButton
			// 
			this.debtCustomersButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.debtCustomersButton.AutoSize = true;
			this.debtCustomersButton.Location = new System.Drawing.Point(529, 210);
			this.debtCustomersButton.Name = "debtCustomersButton";
			this.debtCustomersButton.Size = new System.Drawing.Size(103, 17);
			this.debtCustomersButton.TabIndex = 7;
			this.debtCustomersButton.TabStop = true;
			this.debtCustomersButton.Text = "Задолжники за";
			this.debtCustomersButton.UseVisualStyleBackColor = true;
			this.debtCustomersButton.CheckedChanged += new System.EventHandler(this.debtCustomersButton_CheckedChanged);
			// 
			// filteredCustomersButton
			// 
			this.filteredCustomersButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.filteredCustomersButton.AutoSize = true;
			this.filteredCustomersButton.Location = new System.Drawing.Point(528, 29);
			this.filteredCustomersButton.Name = "filteredCustomersButton";
			this.filteredCustomersButton.Size = new System.Drawing.Size(130, 17);
			this.filteredCustomersButton.TabIndex = 2;
			this.filteredCustomersButton.TabStop = true;
			this.filteredCustomersButton.Text = "Выбранные клиенты";
			this.filteredCustomersButton.UseVisualStyleBackColor = true;
			this.filteredCustomersButton.CheckedChanged += new System.EventHandler(this.filteredCustomersButton_CheckedChanged);
			// 
			// customerFilterBox
			// 
			this.customerFilterBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.customerFilterBox.Controls.Add(this.label4);
			this.customerFilterBox.Controls.Add(this.customerFilterNumberBox);
			this.customerFilterBox.Controls.Add(this.label3);
			this.customerFilterBox.Controls.Add(this.customerFilterMiddleNameBox);
			this.customerFilterBox.Controls.Add(this.label2);
			this.customerFilterBox.Controls.Add(this.customerFilterFirstNameBox);
			this.customerFilterBox.Controls.Add(this.label1);
			this.customerFilterBox.Controls.Add(this.customerFilterLastNameBox);
			this.customerFilterBox.Enabled = false;
			this.customerFilterBox.Location = new System.Drawing.Point(528, 52);
			this.customerFilterBox.Name = "customerFilterBox";
			this.customerFilterBox.Size = new System.Drawing.Size(242, 151);
			this.customerFilterBox.TabIndex = 2;
			this.customerFilterBox.TabStop = false;
			this.customerFilterBox.Text = "Фильтр";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(6, 103);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(86, 13);
			this.label4.TabIndex = 1;
			this.label4.Text = "Номер участка:";
			// 
			// customerFilterNumberBox
			// 
			this.customerFilterNumberBox.Location = new System.Drawing.Point(98, 100);
			this.customerFilterNumberBox.Name = "customerFilterNumberBox";
			this.customerFilterNumberBox.Size = new System.Drawing.Size(138, 20);
			this.customerFilterNumberBox.TabIndex = 6;
			this.customerFilterNumberBox.TextChanged += new System.EventHandler(this.customerFilter_Changed);
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(6, 75);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(57, 13);
			this.label3.TabIndex = 1;
			this.label3.Text = "Отчество:";
			// 
			// customerFilterMiddleNameBox
			// 
			this.customerFilterMiddleNameBox.Location = new System.Drawing.Point(98, 72);
			this.customerFilterMiddleNameBox.Name = "customerFilterMiddleNameBox";
			this.customerFilterMiddleNameBox.Size = new System.Drawing.Size(138, 20);
			this.customerFilterMiddleNameBox.TabIndex = 5;
			this.customerFilterMiddleNameBox.TextChanged += new System.EventHandler(this.customerFilter_Changed);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(6, 48);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(32, 13);
			this.label2.TabIndex = 1;
			this.label2.Text = "Имя:";
			// 
			// customerFilterFirstNameBox
			// 
			this.customerFilterFirstNameBox.Location = new System.Drawing.Point(98, 45);
			this.customerFilterFirstNameBox.Name = "customerFilterFirstNameBox";
			this.customerFilterFirstNameBox.Size = new System.Drawing.Size(138, 20);
			this.customerFilterFirstNameBox.TabIndex = 4;
			this.customerFilterFirstNameBox.TextChanged += new System.EventHandler(this.customerFilter_Changed);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(6, 22);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(59, 13);
			this.label1.TabIndex = 1;
			this.label1.Text = "Фамилия:";
			// 
			// customerFilterLastNameBox
			// 
			this.customerFilterLastNameBox.Location = new System.Drawing.Point(98, 19);
			this.customerFilterLastNameBox.Name = "customerFilterLastNameBox";
			this.customerFilterLastNameBox.Size = new System.Drawing.Size(138, 20);
			this.customerFilterLastNameBox.TabIndex = 3;
			this.customerFilterLastNameBox.TextChanged += new System.EventHandler(this.customerFilter_Changed);
			// 
			// allCustomersButton
			// 
			this.allCustomersButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.allCustomersButton.AutoSize = true;
			this.allCustomersButton.Checked = true;
			this.allCustomersButton.Location = new System.Drawing.Point(528, 6);
			this.allCustomersButton.Name = "allCustomersButton";
			this.allCustomersButton.Size = new System.Drawing.Size(90, 17);
			this.allCustomersButton.TabIndex = 1;
			this.allCustomersButton.TabStop = true;
			this.allCustomersButton.Text = "Все клиенты";
			this.allCustomersButton.UseVisualStyleBackColor = true;
			this.allCustomersButton.CheckedChanged += new System.EventHandler(this.allCustomersButton_CheckedChanged);
			// 
			// tabPage2
			// 
			this.tabPage2.Controls.Add(this.paymentFilterBox);
			this.tabPage2.Controls.Add(this.filteredPaymentsButton);
			this.tabPage2.Controls.Add(this.allPaymentsButton);
			this.tabPage2.Controls.Add(this.PaymentGrid);
			this.tabPage2.Location = new System.Drawing.Point(4, 22);
			this.tabPage2.Name = "tabPage2";
			this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage2.Size = new System.Drawing.Size(782, 495);
			this.tabPage2.TabIndex = 1;
			this.tabPage2.Text = "Платежи";
			this.tabPage2.UseVisualStyleBackColor = true;
			// 
			// paymentFilterBox
			// 
			this.paymentFilterBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.paymentFilterBox.Controls.Add(this.paymentCustomerID);
			this.paymentFilterBox.Controls.Add(this.paymentMonthTo);
			this.paymentFilterBox.Controls.Add(this.paymentDateTo);
			this.paymentFilterBox.Controls.Add(this.paymentDateFrom);
			this.paymentFilterBox.Controls.Add(this.paymentMonthFrom);
			this.paymentFilterBox.Controls.Add(this.label16);
			this.paymentFilterBox.Controls.Add(this.label13);
			this.paymentFilterBox.Controls.Add(this.label19);
			this.paymentFilterBox.Controls.Add(this.label10);
			this.paymentFilterBox.Controls.Add(this.label7);
			this.paymentFilterBox.Controls.Add(this.label15);
			this.paymentFilterBox.Controls.Add(this.label12);
			this.paymentFilterBox.Controls.Add(this.label18);
			this.paymentFilterBox.Controls.Add(this.label9);
			this.paymentFilterBox.Controls.Add(this.label6);
			this.paymentFilterBox.Controls.Add(this.paymentSumTo);
			this.paymentFilterBox.Controls.Add(this.paymentRateTo);
			this.paymentFilterBox.Controls.Add(this.paymentAmountTo);
			this.paymentFilterBox.Controls.Add(this.label20);
			this.paymentFilterBox.Controls.Add(this.label14);
			this.paymentFilterBox.Controls.Add(this.label11);
			this.paymentFilterBox.Controls.Add(this.paymentSumFrom);
			this.paymentFilterBox.Controls.Add(this.label17);
			this.paymentFilterBox.Controls.Add(this.paymentRateFrom);
			this.paymentFilterBox.Controls.Add(this.label8);
			this.paymentFilterBox.Controls.Add(this.paymentAmountFrom);
			this.paymentFilterBox.Controls.Add(this.label5);
			this.paymentFilterBox.Enabled = false;
			this.paymentFilterBox.Location = new System.Drawing.Point(535, 55);
			this.paymentFilterBox.Name = "paymentFilterBox";
			this.paymentFilterBox.Size = new System.Drawing.Size(241, 413);
			this.paymentFilterBox.TabIndex = 4;
			this.paymentFilterBox.TabStop = false;
			this.paymentFilterBox.Text = "Фильтр";
			// 
			// paymentCustomerID
			// 
			this.paymentCustomerID.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.paymentCustomerID.FormattingEnabled = true;
			this.paymentCustomerID.Location = new System.Drawing.Point(10, 375);
			this.paymentCustomerID.Name = "paymentCustomerID";
			this.paymentCustomerID.Size = new System.Drawing.Size(225, 21);
			this.paymentCustomerID.TabIndex = 12;
			this.paymentCustomerID.SelectionChangeCommitted += new System.EventHandler(this.paymentFilter_Changed);
			this.paymentCustomerID.SelectedIndexChanged += new System.EventHandler(this.paymentFilter_Changed);
			this.paymentCustomerID.TextUpdate += new System.EventHandler(this.paymentFilter_Changed);
			// 
			// paymentMonthTo
			// 
			this.paymentMonthTo.CustomFormat = "MMMM yyyy";
			this.paymentMonthTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			this.paymentMonthTo.Location = new System.Drawing.Point(34, 224);
			this.paymentMonthTo.Name = "paymentMonthTo";
			this.paymentMonthTo.Size = new System.Drawing.Size(118, 20);
			this.paymentMonthTo.TabIndex = 9;
			this.paymentMonthTo.ValueChanged += new System.EventHandler(this.paymentFilter_Changed);
			this.paymentMonthTo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.paymentMonthFrom_KeyPress);
			this.paymentMonthTo.Enter += new System.EventHandler(this.paymentMonthFrom_Enter);
			// 
			// paymentDateTo
			// 
			this.paymentDateTo.CustomFormat = "MMMM yyyy";
			this.paymentDateTo.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			this.paymentDateTo.Location = new System.Drawing.Point(31, 309);
			this.paymentDateTo.Name = "paymentDateTo";
			this.paymentDateTo.Size = new System.Drawing.Size(121, 20);
			this.paymentDateTo.TabIndex = 11;
			this.paymentDateTo.ValueChanged += new System.EventHandler(this.paymentFilter_Changed);
			// 
			// paymentDateFrom
			// 
			this.paymentDateFrom.CustomFormat = "MMMM yyyy";
			this.paymentDateFrom.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			this.paymentDateFrom.Location = new System.Drawing.Point(31, 277);
			this.paymentDateFrom.Name = "paymentDateFrom";
			this.paymentDateFrom.Size = new System.Drawing.Size(121, 20);
			this.paymentDateFrom.TabIndex = 10;
			this.paymentDateFrom.ValueChanged += new System.EventHandler(this.paymentFilter_Changed);
			// 
			// paymentMonthFrom
			// 
			this.paymentMonthFrom.CustomFormat = "MMMM yyyy";
			this.paymentMonthFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			this.paymentMonthFrom.Location = new System.Drawing.Point(34, 195);
			this.paymentMonthFrom.Name = "paymentMonthFrom";
			this.paymentMonthFrom.Size = new System.Drawing.Size(118, 20);
			this.paymentMonthFrom.TabIndex = 8;
			this.paymentMonthFrom.Value = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
			this.paymentMonthFrom.ValueChanged += new System.EventHandler(this.paymentFilter_Changed);
			this.paymentMonthFrom.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.paymentMonthFrom_KeyPress);
			this.paymentMonthFrom.Enter += new System.EventHandler(this.paymentMonthFrom_Enter);
			// 
			// label16
			// 
			this.label16.AutoSize = true;
			this.label16.Location = new System.Drawing.Point(7, 281);
			this.label16.Name = "label16";
			this.label16.Size = new System.Drawing.Size(18, 13);
			this.label16.TabIndex = 3;
			this.label16.Text = "от";
			// 
			// label13
			// 
			this.label13.AutoSize = true;
			this.label13.Location = new System.Drawing.Point(8, 195);
			this.label13.Name = "label13";
			this.label13.Size = new System.Drawing.Size(18, 13);
			this.label13.TabIndex = 3;
			this.label13.Text = "от";
			// 
			// label19
			// 
			this.label19.AutoSize = true;
			this.label19.Location = new System.Drawing.Point(9, 141);
			this.label19.Name = "label19";
			this.label19.Size = new System.Drawing.Size(18, 13);
			this.label19.TabIndex = 3;
			this.label19.Text = "от";
			// 
			// label10
			// 
			this.label10.AutoSize = true;
			this.label10.Location = new System.Drawing.Point(9, 90);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(18, 13);
			this.label10.TabIndex = 3;
			this.label10.Text = "от";
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(9, 39);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(18, 13);
			this.label7.TabIndex = 3;
			this.label7.Text = "от";
			// 
			// label15
			// 
			this.label15.AutoSize = true;
			this.label15.Location = new System.Drawing.Point(6, 313);
			this.label15.Name = "label15";
			this.label15.Size = new System.Drawing.Size(19, 13);
			this.label15.TabIndex = 2;
			this.label15.Text = "до";
			// 
			// label12
			// 
			this.label12.AutoSize = true;
			this.label12.Location = new System.Drawing.Point(9, 228);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(19, 13);
			this.label12.TabIndex = 2;
			this.label12.Text = "до";
			// 
			// label18
			// 
			this.label18.AutoSize = true;
			this.label18.Location = new System.Drawing.Point(121, 141);
			this.label18.Name = "label18";
			this.label18.Size = new System.Drawing.Size(19, 13);
			this.label18.TabIndex = 2;
			this.label18.Text = "до";
			// 
			// label9
			// 
			this.label9.AutoSize = true;
			this.label9.Location = new System.Drawing.Point(121, 90);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(19, 13);
			this.label9.TabIndex = 2;
			this.label9.Text = "до";
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(121, 39);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(19, 13);
			this.label6.TabIndex = 2;
			this.label6.Text = "до";
			// 
			// paymentSumTo
			// 
			this.paymentSumTo.Location = new System.Drawing.Point(146, 138);
			this.paymentSumTo.Name = "paymentSumTo";
			this.paymentSumTo.Size = new System.Drawing.Size(89, 20);
			this.paymentSumTo.TabIndex = 7;
			this.paymentSumTo.TextChanged += new System.EventHandler(this.paymentFilter_Changed);
			// 
			// paymentRateTo
			// 
			this.paymentRateTo.Location = new System.Drawing.Point(146, 87);
			this.paymentRateTo.Name = "paymentRateTo";
			this.paymentRateTo.Size = new System.Drawing.Size(89, 20);
			this.paymentRateTo.TabIndex = 5;
			this.paymentRateTo.TextChanged += new System.EventHandler(this.paymentFilter_Changed);
			// 
			// paymentAmountTo
			// 
			this.paymentAmountTo.Location = new System.Drawing.Point(146, 36);
			this.paymentAmountTo.Name = "paymentAmountTo";
			this.paymentAmountTo.Size = new System.Drawing.Size(89, 20);
			this.paymentAmountTo.TabIndex = 3;
			this.paymentAmountTo.TextChanged += new System.EventHandler(this.paymentFilter_Changed);
			// 
			// label20
			// 
			this.label20.AutoSize = true;
			this.label20.Location = new System.Drawing.Point(6, 347);
			this.label20.Name = "label20";
			this.label20.Size = new System.Drawing.Size(46, 13);
			this.label20.TabIndex = 0;
			this.label20.Text = "Клиент:";
			// 
			// label14
			// 
			this.label14.AutoSize = true;
			this.label14.Location = new System.Drawing.Point(6, 258);
			this.label14.Name = "label14";
			this.label14.Size = new System.Drawing.Size(82, 13);
			this.label14.TabIndex = 0;
			this.label14.Text = "Дата платежа:";
			// 
			// label11
			// 
			this.label11.AutoSize = true;
			this.label11.Location = new System.Drawing.Point(6, 176);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(108, 13);
			this.label11.TabIndex = 0;
			this.label11.Text = "Оплаченный месяц:";
			// 
			// paymentSumFrom
			// 
			this.paymentSumFrom.Location = new System.Drawing.Point(34, 138);
			this.paymentSumFrom.Name = "paymentSumFrom";
			this.paymentSumFrom.Size = new System.Drawing.Size(81, 20);
			this.paymentSumFrom.TabIndex = 6;
			this.paymentSumFrom.TextChanged += new System.EventHandler(this.paymentFilter_Changed);
			// 
			// label17
			// 
			this.label17.AutoSize = true;
			this.label17.Location = new System.Drawing.Point(7, 122);
			this.label17.Name = "label17";
			this.label17.Size = new System.Drawing.Size(44, 13);
			this.label17.TabIndex = 0;
			this.label17.Text = "Сумма:";
			// 
			// paymentRateFrom
			// 
			this.paymentRateFrom.Location = new System.Drawing.Point(34, 87);
			this.paymentRateFrom.Name = "paymentRateFrom";
			this.paymentRateFrom.Size = new System.Drawing.Size(81, 20);
			this.paymentRateFrom.TabIndex = 4;
			this.paymentRateFrom.TextChanged += new System.EventHandler(this.paymentFilter_Changed);
			// 
			// label8
			// 
			this.label8.AutoSize = true;
			this.label8.Location = new System.Drawing.Point(7, 71);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(43, 13);
			this.label8.TabIndex = 0;
			this.label8.Text = "Тариф:";
			// 
			// paymentAmountFrom
			// 
			this.paymentAmountFrom.Location = new System.Drawing.Point(34, 36);
			this.paymentAmountFrom.Name = "paymentAmountFrom";
			this.paymentAmountFrom.Size = new System.Drawing.Size(81, 20);
			this.paymentAmountFrom.TabIndex = 2;
			this.paymentAmountFrom.TextChanged += new System.EventHandler(this.paymentFilter_Changed);
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(7, 20);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(129, 13);
			this.label5.TabIndex = 0;
			this.label5.Text = "Кол-во электроэнергии:";
			// 
			// filteredPaymentsButton
			// 
			this.filteredPaymentsButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.filteredPaymentsButton.AutoSize = true;
			this.filteredPaymentsButton.Location = new System.Drawing.Point(535, 31);
			this.filteredPaymentsButton.Name = "filteredPaymentsButton";
			this.filteredPaymentsButton.Size = new System.Drawing.Size(130, 17);
			this.filteredPaymentsButton.TabIndex = 1;
			this.filteredPaymentsButton.TabStop = true;
			this.filteredPaymentsButton.Text = "Выбранные платежи";
			this.filteredPaymentsButton.UseVisualStyleBackColor = true;
			this.filteredPaymentsButton.CheckedChanged += new System.EventHandler(this.filteredPaymentsButton_CheckedChanged);
			// 
			// allPaymentsButton
			// 
			this.allPaymentsButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.allPaymentsButton.AutoSize = true;
			this.allPaymentsButton.Checked = true;
			this.allPaymentsButton.Location = new System.Drawing.Point(535, 7);
			this.allPaymentsButton.Name = "allPaymentsButton";
			this.allPaymentsButton.Size = new System.Drawing.Size(90, 17);
			this.allPaymentsButton.TabIndex = 0;
			this.allPaymentsButton.TabStop = true;
			this.allPaymentsButton.Text = "Все платежи";
			this.allPaymentsButton.UseVisualStyleBackColor = true;
			this.allPaymentsButton.CheckedChanged += new System.EventHandler(this.allPaymentsButton_CheckedChanged);
			// 
			// PaymentGrid
			// 
			this.PaymentGrid.AllowUserToAddRows = false;
			this.PaymentGrid.AllowUserToDeleteRows = false;
			this.PaymentGrid.AllowUserToOrderColumns = true;
			this.PaymentGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.PaymentGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
			this.PaymentGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.PaymentGrid.Location = new System.Drawing.Point(6, 6);
			this.PaymentGrid.MinimumSize = new System.Drawing.Size(100, 100);
			this.PaymentGrid.Name = "PaymentGrid";
			this.PaymentGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.PaymentGrid.Size = new System.Drawing.Size(522, 483);
			this.PaymentGrid.TabIndex = 1;
			this.PaymentGrid.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.PaymentGrid_CellValidating);
			this.PaymentGrid.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.PaymentGrid_CellEndEdit);
			// 
			// dialogExcelFile
			// 
			this.dialogExcelFile.DefaultExt = "xls";
			this.dialogExcelFile.Filter = "Excel files|*.xls";
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(823, 593);
			this.Controls.Add(this.tabControl);
			this.Controls.Add(this.toolStrip1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MinimumSize = new System.Drawing.Size(800, 620);
			this.Name = "MainForm";
			this.Text = "Платежная система";
			this.Load += new System.EventHandler(this.MainForm_Load);
			((System.ComponentModel.ISupportInitialize)(this.CustomerGrid)).EndInit();
			this.contextMenuStrip1.ResumeLayout(false);
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			this.tabControl.ResumeLayout(false);
			this.tabPage1.ResumeLayout(false);
			this.tabPage1.PerformLayout();
			this.customerFilterBox.ResumeLayout(false);
			this.customerFilterBox.PerformLayout();
			this.tabPage2.ResumeLayout(false);
			this.tabPage2.PerformLayout();
			this.paymentFilterBox.ResumeLayout(false);
			this.paymentFilterBox.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.PaymentGrid)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.DataGridView CustomerGrid;
		private System.Windows.Forms.ToolStrip toolStrip1;
		private System.Windows.Forms.ToolStripButton addCustomerButton;
		private System.Windows.Forms.TabControl tabControl;
		private System.Windows.Forms.TabPage tabPage1;
		private System.Windows.Forms.TabPage tabPage2;
		private System.Windows.Forms.DataGridView PaymentGrid;
		private System.Windows.Forms.ToolStripButton addPaymentButton;
		private System.Windows.Forms.ToolStripButton deleteButton;
		private System.Windows.Forms.ToolStripLabel toolStripLabel1;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripLabel rateLabel;
		private System.Windows.Forms.ToolStripLabel toolStripLabel2;
		private System.Windows.Forms.RadioButton allCustomersButton;
		private System.Windows.Forms.GroupBox customerFilterBox;
		private System.Windows.Forms.RadioButton filteredCustomersButton;
		private System.Windows.Forms.RadioButton filteredPaymentsButton;
		private System.Windows.Forms.RadioButton allPaymentsButton;
		private System.Windows.Forms.GroupBox paymentFilterBox;
		private System.Windows.Forms.RadioButton debtCustomersButton;
		private System.Windows.Forms.DateTimePicker debtMonth;
		private System.Windows.Forms.TextBox customerFilterLastNameBox;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox customerFilterNumberBox;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox customerFilterMiddleNameBox;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox customerFilterFirstNameBox;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.TextBox paymentAmountTo;
		private System.Windows.Forms.TextBox paymentAmountFrom;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label16;
		private System.Windows.Forms.Label label13;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.Label label15;
		private System.Windows.Forms.Label label12;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.TextBox paymentRateTo;
		private System.Windows.Forms.Label label14;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.TextBox paymentRateFrom;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Label label19;
		private System.Windows.Forms.Label label18;
		private System.Windows.Forms.TextBox paymentSumTo;
		private System.Windows.Forms.TextBox paymentSumFrom;
		private System.Windows.Forms.Label label17;
		private System.Windows.Forms.DateTimePicker paymentMonthTo;
		private System.Windows.Forms.DateTimePicker paymentMonthFrom;
		private System.Windows.Forms.DateTimePicker paymentDateTo;
		private System.Windows.Forms.DateTimePicker paymentDateFrom;
		private System.Windows.Forms.Label label20;
		private System.Windows.Forms.ComboBox paymentCustomerID;
		private System.Windows.Forms.ToolStripButton exportToExcelButton;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
		private System.Windows.Forms.SaveFileDialog dialogExcelFile;
		private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
		private System.Windows.Forms.ToolStripMenuItem addPAymentMenuItem;
	}
}

