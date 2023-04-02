using System;
using System.Drawing;
using System.Windows.Forms;

namespace TetrisCompact
{
	/// <summary>
	/// Класс, представляющий игру. Singleton.
	/// </summary>
	public class Game
	{
		private static readonly int[] SPEED = new[]{1000, 500, 250};
		private static readonly int[] SCORES = new[] { 1, 5, 10 };

		private static Game INSTANCE;

		private Glass glass;

		private readonly Timer timer;

		private int score;
		private int speedLevel;

		private Control panelGame;
		private Panel panelPreview;
		private Label labScore;

		public Control PanelGame{
			set{
				panelGame = value;
			}
		}

		public Panel PanelPreview {
			set {
				panelPreview = value;
			}
		}

		public Label LabScore {
			set {
				labScore = value;
			}
		}

		private Game(){
			speedLevel = Const.SLOW_SPEED;

			timer = new Timer{Interval = SPEED[speedLevel]};
			timer.Tick += OnTimer;
		}

		public static Game GetInstance() {
			if(INSTANCE == null)
				INSTANCE = new Game();
			return INSTANCE;
		}

		public void StartGame(int choosenSpeed) {
			score = 0;

			speedLevel = choosenSpeed;
			timer.Interval = SPEED [ choosenSpeed ];

			glass = new Glass();
			labScore.Text = score.ToString();
			timer.Enabled = true;
		}

		public void FinishGame() {
			glass = null;
			timer.Enabled = false;
			MessageBox.Show("Your score is " + score, "Game Over");
			score = 0;
			labScore.Text = score.ToString();

			panelGame.Refresh();
			panelPreview.Refresh();
		}

		public bool IsRunning() {
			return glass != null;
		}

		public bool IsPaused(){
			return IsRunning() && !timer.Enabled;
		}

		public int GetScore(){
			return score;
		}

		public void Draw(Graphics g, int width, int height) {
			int square = width / Const.NUM_COLS ;

			g.Clear ( Color.White );
			
			Pen pen = new Pen ( Color.Black );

			g.DrawRectangle ( pen, 0, 0, width - 1, height - 1 );

			// вертикальные линии
			for(int i = 1; i < Const.NUM_COLS; ++i){
				g.DrawLine (pen, square * i, 0, square * i,  height );
			}

			// горизонтальные линии
			for(int i = 1; i < Const.NUM_ROWS; ++i) {
				g.DrawLine(pen, 0, square * i, width, square * i);
			}
			
			if(!IsRunning()) return;
            
			glass.Draw ( g , square );
		}

		public void Rotate(){
			if(!IsRunning() || IsPaused()) return;
			glass.Rotate();
		}

		public void StepLeft() {
			if(!IsRunning() || IsPaused()) return;
			glass.StepLeft();
		}
		
		public void StepRight() {
			if(!IsRunning() || IsPaused()) return;
			glass.StepRight();
		}

		public void FallDown() {
			if(!IsRunning() || IsPaused()) return;
			glass.FallDown();
		}

		public void Pause(bool flag){
			if(!IsRunning()) return;

			timer.Enabled = !flag;
		}

		private void OnTimer(object sender, EventArgs e) {
			if( ! IsRunning() ){
				timer.Enabled = false;
				return;
			}

			if (glass.IsFull()){
				FinishGame();
				return;
			}

			if(! glass.IsRunning()){
				score += glass.EatRows() * SCORES[speedLevel];
				labScore.Text = score.ToString();
				glass.AddFigure ( FactoryFigures.NewFigure() );
				panelPreview.Invalidate();
			}else{
				glass.StepDown();
			}

			panelGame.Invalidate();
		}


	}
}