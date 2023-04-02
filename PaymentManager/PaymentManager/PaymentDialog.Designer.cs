namespace PaymentManagerView
{
	partial class PaymentDialog
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PaymentDialog));
			this.customers = new System.Windows.Forms.ComboBox();
			this.paymentDate = new System.Windows.Forms.DateTimePicker();
			this.monthPaid = new System.Windows.Forms.DateTimePicker();
			this.amount = new System.Windows.Forms.TextBox();
			this.button1 = new System.Windows.Forms.Button();
			this.button2 = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// customers
			// 
			this.customers.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.customers.FormattingEnabled = true;
			resources.ApplyResources(this.customers, "customers");
			this.customers.Name = "customers";
			// 
			// paymentDate
			// 
			this.paymentDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			resources.ApplyResources(this.paymentDate, "paymentDate");
			this.paymentDate.Name = "paymentDate";
			// 
			// monthPaid
			// 
			resources.ApplyResources(this.monthPaid, "monthPaid");
			this.monthPaid.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			this.monthPaid.Name = "monthPaid";
			this.monthPaid.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.monthPaid_KeyPress);
			this.monthPaid.Enter += new System.EventHandler(this.monthPaid_Enter);
			// 
			// amount
			// 
			resources.ApplyResources(this.amount, "amount");
			this.amount.Name = "amount";
			// 
			// button1
			// 
			this.button1.DialogResult = System.Windows.Forms.DialogResult.OK;
			resources.ApplyResources(this.button1, "button1");
			this.button1.Name = "button1";
			this.button1.UseVisualStyleBackColor = true;
			// 
			// button2
			// 
			this.button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			resources.ApplyResources(this.button2, "button2");
			this.button2.Name = "button2";
			this.button2.UseVisualStyleBackColor = true;
			// 
			// label1
			// 
			resources.ApplyResources(this.label1, "label1");
			this.label1.Name = "label1";
			// 
			// label2
			// 
			resources.ApplyResources(this.label2, "label2");
			this.label2.Name = "label2";
			// 
			// label3
			// 
			resources.ApplyResources(this.label3, "label3");
			this.label3.Name = "label3";
			// 
			// label4
			// 
			resources.ApplyResources(this.label4, "label4");
			this.label4.Name = "label4";
			// 
			// PaymentDialog
			// 
			this.AcceptButton = this.button1;
			resources.ApplyResources(this, "$this");
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.button2;
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.amount);
			this.Controls.Add(this.monthPaid);
			this.Controls.Add(this.paymentDate);
			this.Controls.Add(this.customers);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "PaymentDialog";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ComboBox customers;
		private System.Windows.Forms.DateTimePicker paymentDate;
		private System.Windows.Forms.DateTimePicker monthPaid;
		private System.Windows.Forms.TextBox amount;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
	}
}