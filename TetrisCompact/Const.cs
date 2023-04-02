using System.Drawing;

namespace TetrisCompact
{
	/// <summary>
	/// Набор глобальных констант.
	/// </summary>
	public static class Const
	{
		public static readonly Color[] Colors = new[]{Color.White, Color.Red, Color.Orange, Color.Yellow, Color.Green, Color.Blue, Color.DarkBlue, 
		                                              Color.Purple};

		// набор констант для фигур
		// использовать enum ???
		public const int NUM_FIGURES = 7;

		public const int I = 1;
		public const int J = 2;
		public const int L = 3;
		public const int O = 4;
		public const int S = 5;
		public const int T = 6;
		public const int Z = 7;

		public const int NUM_ROWS = 20;
		public const int NUM_COLS = 10;

		public const int UNUSED_COLOR = 0;

		public const int SLOW_SPEED = 0;
        public const int MEDIUM_SPEED = 1;
		public const int FAST_SPEED = 2;
	}
}
