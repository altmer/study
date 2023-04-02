namespace TetrisCompact
{
	partial class MainForm
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
			this.menuItem1 = new System.Windows.Forms.MenuItem();
			this.menuStartGame = new System.Windows.Forms.MenuItem();
			this.menuStopGame = new System.Windows.Forms.MenuItem();
			this.label1 = new System.Windows.Forms.Label();
			this.labScore = new System.Windows.Forms.Label();
			this.panelPreview = new System.Windows.Forms.Panel();
			this.label2 = new System.Windows.Forms.Label();
			this.butPause = new System.Windows.Forms.Button();
			this.gameField = new GameFieldControl.GameField();
			this.SuspendLayout();
			// 
			// mainMenu1
			// 
			this.mainMenu1.MenuItems.Add(this.menuItem1);
			// 
			// menuItem1
			// 
			this.menuItem1.MenuItems.Add(this.menuStartGame);
			this.menuItem1.MenuItems.Add(this.menuStopGame);
			this.menuItem1.Text = "Game";
			// 
			// menuStartGame
			// 
			this.menuStartGame.Text = "Start Game";
			this.menuStartGame.Click += new System.EventHandler(this.menuStartGame_Click);
			// 
			// menuStopGame
			// 
			this.menuStopGame.Text = "Stop Game";
			this.menuStopGame.Click += new System.EventHandler(this.menuStopGame_Click);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(131, 4);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(45, 20);
			this.label1.Text = "Score:";
			// 
			// labScore
			// 
			this.labScore.Location = new System.Drawing.Point(172, 4);
			this.labScore.Name = "labScore";
			this.labScore.Size = new System.Drawing.Size(51, 20);
			this.labScore.Text = "0";
			// 
			// panelPreview
			// 
			this.panelPreview.Location = new System.Drawing.Point(131, 56);
			this.panelPreview.Name = "panelPreview";
			this.panelPreview.Size = new System.Drawing.Size(100, 100);
			this.panelPreview.Paint += new System.Windows.Forms.PaintEventHandler(this.panelPreview_Paint);
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(131, 28);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(100, 20);
			this.label2.Text = "Next figure:";
			// 
			// butPause
			// 
			this.butPause.Location = new System.Drawing.Point(131, 186);
			this.butPause.Name = "butPause";
			this.butPause.Size = new System.Drawing.Size(72, 20);
			this.butPause.TabIndex = 5;
			this.butPause.Text = "Pause";
			this.butPause.Click += new System.EventHandler(this.butPause_Click);
			// 
			// gameField
			// 
			this.gameField.Location = new System.Drawing.Point(5, 4);
			this.gameField.Name = "gameField";
			this.gameField.Size = new System.Drawing.Size(120, 240);
			this.gameField.TabIndex = 10;
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
			this.AutoScroll = true;
			this.ClientSize = new System.Drawing.Size(240, 268);
			this.Controls.Add(this.gameField);
			this.Controls.Add(this.butPause);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.panelPreview);
			this.Controls.Add(this.labScore);
			this.Controls.Add(this.label1);
			this.KeyPreview = true;
			this.Menu = this.mainMenu1;
			this.Name = "MainForm";
			this.Text = "Tetris";
			this.Deactivate += new System.EventHandler(this.MainForm_Deactivate);
			this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.MainForm_KeyPress);
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MainForm_KeyDown);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.MenuItem menuItem1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label labScore;
		private System.Windows.Forms.MenuItem menuStartGame;
		private System.Windows.Forms.MenuItem menuStopGame;
		private System.Windows.Forms.Panel panelPreview;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button butPause;
		private GameFieldControl.GameField gameField;
	}
}

