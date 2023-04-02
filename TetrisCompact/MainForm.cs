using System;
using System.Drawing;
using System.Windows.Forms;

namespace TetrisCompact
{
	public partial class MainForm : Form
	{
		private readonly Game game;
        private SpeedDialog dial;

		public MainForm() {
			InitializeComponent();

			game = Game.GetInstance();
			game.PanelGame = gameField;
			game.LabScore = labScore;
			game.PanelPreview = panelPreview;

            gameField.Invalidate();
		}

		public void StartGame(int choosenSpeed) {
			game.StartGame(choosenSpeed);
			gameField.Invalidate();
		}

		private void MainForm_KeyPress(object sender, KeyPressEventArgs e) {
			e.Handled = true;
		}

		private void menuStartGame_Click(object sender, EventArgs e) {
			dial = new SpeedDialog(this);
			dial.Visible = true;
		}

		private void menuStopGame_Click(object sender, EventArgs e) {
			game.FinishGame();
			gameField.Invalidate();
		}

		private void MainForm_KeyDown(object sender, KeyEventArgs e) {
			if((e.KeyCode == Keys.Up)) {
				// Up
			}
			if((e.KeyCode == Keys.Down)) {
				game.FallDown();
			}
			if((e.KeyCode == Keys.Left)) {
				game.StepLeft();
			}
			if((e.KeyCode == Keys.Right)) {
				game.StepRight();
			}
			if((e.KeyCode == Keys.Enter)) {
				game.Rotate();
			}

			gameField.Invalidate();
			e.Handled = true;
		}

		private void panelPreview_Paint(object sender, PaintEventArgs e) {
			Graphics g = e.Graphics;
			int width = panelPreview.Width;
			int height = panelPreview.Height;
			int square = width / 4;

			g.Clear ( Color.White );

			Pen pen = new Pen(Color.Black);

			// вертикальные линии
			for(int i = 1; i < 4; ++i) {
				g.DrawLine(pen, square * i, 0, square * i, height);
			}

			// горизонтальные линии
			for(int i = 1; i < 4; ++i) {
				g.DrawLine(pen, 0, square * i, width, square * i);
			}

			if(! game.IsRunning()) return;

			AbstractFigure figure = FactoryFigures.NextFigure;
			bool[][] model = figure.GetModel();

			for (int i = 0; i < model.Length; ++i){
				for (int j = 0; j < model[i].Length; ++j){
					if(model[i][j])
						g.FillRectangle((new SolidBrush(Const.Colors[figure.GetColor()])),
													   new Rectangle(square*i, square*j, square, square));
				}
			}

			g.Dispose();
		}

		private void butPause_Click(object sender, EventArgs e) {
			game.Pause ( ! game.IsPaused() );
			RenewButtonPause();
		}

		private void MainForm_Deactivate(object sender, EventArgs e) {
			game.Pause ( true );
			RenewButtonPause();
		}

		private void RenewButtonPause(){
			butPause.Text = game.IsPaused() ? "Resume" : "Pause";
		}

	}
}