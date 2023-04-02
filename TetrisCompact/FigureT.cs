using Tetris;

namespace TetrisCompact
{
	public class FigureT : AbstractFigure
	{
		public FigureT() {
			_color = Const.T;

			_schemes = new Scheme[4];

			_schemes[0] = new Scheme(new[] { "#.", 
			                                 "##", 
			                                 "#." });

			_schemes[1] = new Scheme(new[] { "###", 
			                                 ".#." });

			_schemes[2] = new Scheme(new[] { ".#", 
			                                 "##", 
			                                 ".#" });

			_schemes[3] = new Scheme(new[] { ".#.", 
			                                 "###" });
		}

		public override AbstractFigure Clone(){
			return new FigureT{ _state = _state };
		}
	}
}