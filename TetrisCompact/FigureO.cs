using Tetris;

namespace TetrisCompact
{
	public class FigureO : AbstractFigure
	{
		public FigureO(){
			_color = Const.O;

			_schemes = new Scheme[1];

			_schemes[0] = new Scheme(new[]{"##", 
			                               "##"});
		}

		public override AbstractFigure Clone(){
			return new FigureO{ _state = _state };
		}
	}
}