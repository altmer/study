using Tetris;

namespace TetrisCompact
{
	public class FigureI : AbstractFigure
	{
		
		public FigureI() {
			_color = Const.I;

			_schemes = new Scheme[2];
			_schemes[0] = new Scheme(new[]{"#", 
			                               "#", 
			                               "#", 
			                               "#"});

			_schemes[1] = new Scheme(new[] { "####" });
		}

		public override AbstractFigure Clone(){
			return new FigureI{_state = _state};
		}
	}
}