using Tetris;

namespace TetrisCompact
{
	public class FigureJ : AbstractFigure
	{
		public FigureJ(){
			_color = Const.J;

			_schemes = new Scheme[4];

			_schemes[0]	= new Scheme(new[] { ".#", 
			           	                     ".#", 
			           	                     "##" });

			_schemes[1] = new Scheme(new[] { "#..", 
			                                 "###" });

			_schemes[2] = new Scheme(new[] { "##", 
			                                 "#.", 
			                                 "#." });

			_schemes[3] = new Scheme(new[] { "###", 
			                                 "..#" });
		}

		public override AbstractFigure Clone(){
			return new FigureJ{ _state = _state};
		}
	}
}