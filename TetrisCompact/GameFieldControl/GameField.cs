using System.Drawing;
using System.Windows.Forms;
using TetrisCompact;

namespace GameFieldControl
{
	public partial class GameField : UserControl
	{
		public GameField() {
			InitializeComponent();
		}

		private Bitmap buffer;
		private readonly Game game = Game.GetInstance();

		protected override void OnPaint(PaintEventArgs e) {
			if(buffer == null) buffer = new Bitmap(Width, Height);
			game.Draw(Graphics.FromImage(buffer), Width, Height);
			e.Graphics.DrawImage(buffer, 0, 0);
			base.OnPaint(e);
		}

		protected override void OnPaintBackground(PaintEventArgs e) { }

	}
}