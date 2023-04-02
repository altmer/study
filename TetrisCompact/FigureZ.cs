using Tetris;

namespace TetrisCompact
{
	public class FigureZ : AbstractFigure
	{
		public FigureZ() {
			_color = Const.Z;

			_schemes = new Scheme[2];

			_schemes[0] = new Scheme(new[] { ".#", 
			                                 "##", 
			                                 "#." });

			_schemes[1] = new Scheme(new[] { "##.", 
			                                 ".##" });
		}

		public override AbstractFigure Clone(){
			return new FigureZ { _state = _state };
		}
	}
}