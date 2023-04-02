namespace TetrisCompact
{
	partial class SpeedDialog
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		private System.Windows.Forms.MainMenu mainMenu1;

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
			this.mainMenu1 = new System.Windows.Forms.MainMenu();
			this.label1 = new System.Windows.Forms.Label();
			this.radioSlow = new System.Windows.Forms.RadioButton();
			this.radioMedium = new System.Windows.Forms.RadioButton();
			this.radioFast = new System.Windows.Forms.RadioButton();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(3, 19);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(183, 20);
			this.label1.Text = "Choose your preffered speed:";
			// 
			// radioSlow
			// 
			this.radioSlow.Checked = true;
			this.radioSlow.Location = new System.Drawing.Point(15, 62);
			this.radioSlow.Name = "radioSlow";
			this.radioSlow.Size = new System.Drawing.Size(100, 20);
			this.radioSlow.TabIndex = 1;
			this.radioSlow.Text = "Slow";
			// 
			// radioMedium
			// 
			this.radioMedium.Location = new System.Drawing.Point(15, 89);
			this.radioMedium.Name = "radioMedium";
			this.radioMedium.Size = new System.Drawing.Size(100, 20);
			this.radioMedium.TabIndex = 2;
			this.radioMedium.Text = "Medium";
			// 
			// radioFast
			// 
			this.radioFast.Location = new System.Drawing.Point(15, 116);
			this.radioFast.Name = "radioFast";
			this.radioFast.Size = new System.Drawing.Size(100, 20);
			this.radioFast.TabIndex = 3;
			this.radioFast.Text = "Fast";
			// 
			// SpeedDialog
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
			this.AutoScroll = true;
			this.ClientSize = new System.Drawing.Size(240, 294);
			this.Controls.Add(this.radioFast);
			this.Controls.Add(this.radioMedium);
			this.Controls.Add(this.radioSlow);
			this.Controls.Add(this.label1);
			this.MinimizeBox = false;
			this.Name = "SpeedDialog";
			this.Text = "Choose speed...";
			this.Closed += new System.EventHandler(this.SpeedDialog_Closed);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.RadioButton radioSlow;
		private System.Windows.Forms.RadioButton radioMedium;
		private System.Windows.Forms.RadioButton radioFast;
	}
}