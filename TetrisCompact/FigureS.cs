using Tetris;

namespace TetrisCompact
{
	public class FigureS : AbstractFigure
	{
		public FigureS() {
			_color = Const.S;

			_schemes = new Scheme[2];

			_schemes[0] = new Scheme(new[] { "#.", 
			                                 "##", 
			                                 ".#" });

			_schemes[1] = new Scheme(new[] { ".##", 
			                                 "##." });
		}

		public override AbstractFigure Clone(){
			return new FigureS{ _state = _state };
		}
	}
}