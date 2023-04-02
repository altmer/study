using System;
using System.Drawing;

namespace TetrisCompact
{
	public class Glass
	{
		private readonly int[][] field;

		private AbstractFigure currentFigure;
		private int x, y;

		private bool full;
		
		public Glass(){
			field = new int[ Const.NUM_COLS ][];
			for (int i = 0; i < Const.NUM_COLS; ++i)
				field[ i ] = new int[ Const.NUM_ROWS ];
		}

		// Public Methods
		public bool AddFigure(AbstractFigure figure){
			if (IsFull() || IsRunning())
				return false;

			currentFigure = figure;
			x = (Const.NUM_COLS / 2) - 1;
			y = 0;

			if(! canFit(currentFigure, x, y)){
				full = true;
				currentFigure = null;
				return false;
			}

			paintCurrentFigure( true );
			return true;
		}

		public void Rotate() {
			if (! IsRunning()) return;

			paintCurrentFigure(false);

			var rotatedFigure = currentFigure.Clone();
			rotatedFigure.Rotate();

			if ( canFit ( rotatedFigure, x, y ) ) 
				currentFigure.Rotate();

			paintCurrentFigure ( true );
		}

		public void StepRight() {
			if(!IsRunning()) return;

			paintCurrentFigure(false);

			if(canFit(currentFigure, x + 1, y))
				x++;

			paintCurrentFigure(true);
		}

		public void StepLeft() {
			if(!IsRunning()) return;

			paintCurrentFigure(false);

			if(canFit(currentFigure, x - 1, y))
				x--;

			paintCurrentFigure(true);
		}

		public void StepDown(){

			if ( ! IsRunning() ) return;

			paintCurrentFigure ( false );

			if ( canFit ( currentFigure, x, y + 1 ) ){
				y++;
				paintCurrentFigure(true);
			}else{
				paintCurrentFigure(true);
				currentFigure = null;
			}
		}

		public void FallDown(){

			if( ! IsRunning() ) return;

			paintCurrentFigure ( false );

			for (int newY = y + 1; newY <= Const.NUM_ROWS; ++newY){
				if (! canFit ( currentFigure, x, newY )){
					y = newY - 1;
					paintCurrentFigure ( true );
					currentFigure = null;
					return;
				}
			}

			throw new ApplicationException();
		}

		public int EatRows(){
			int ret = 0;
			for(int row = 0; row < Const.NUM_ROWS; ++row ){
				bool eat = true;
				for (int col = 0; col < Const.NUM_COLS; ++col){
					if(field[col][row] == Const.UNUSED_COLOR){
						eat = false;
						break;
					}
				}
				if(eat){
					shiftRows ( row );
					ret++;
				}
			}
			return ret;
		}

		public void Draw(Graphics g, int square) {
			
			for (int i = 0; i < Const.NUM_COLS; ++i){
				for (int j = 0; j < Const.NUM_ROWS; ++j){
					if(field[ i ][ j ] != Const.UNUSED_COLOR){
						g.FillRectangle ( (new SolidBrush ( Const.Colors[ field[ i ][ j ] ] )), 
							                           new Rectangle(square*i, square*j, square, square) );
					}
				}
			}
		}

		public bool IsRunning() {
			return currentFigure != null;
		}

		public bool IsFull(){
			return full;
		}


		// Private Methods
		private void paintCurrentFigure(bool flag){
			bool[][] model = currentFigure.GetModel();
			int w = currentFigure.GetWidth();
			int h = currentFigure.GetHeight();

			for (int i = 0; i < w; ++i){
				for (int j = 0; j < h; ++j){
					if (flag)
						field[x + i][y + j] = (model[i][j] ? currentFigure.GetColor() : field[x + i][y + j]);
					else{
						field[x + i][y + j] = (model[i][j] ? Const.UNUSED_COLOR : field[x + i][y + j]);
					}
				}
			}
		}

		private bool canFit(AbstractFigure figure, int cx, int cy) {
			if(figure == null) return false;

			bool[][] fig = figure.GetModel();
			int w = figure.GetWidth();
			int h = figure.GetHeight();

			if(cx < 0 || cy < 0 || cx + w - 1 >= Const.NUM_COLS || cy + h - 1 >= Const.NUM_ROWS) return false;

			for(int dx = 0; dx < w; ++dx)
				for(int dy = 0; dy < h; ++dy)
					if(fig[dx][dy] && field[cx + dx][cy + dy] != Const.UNUSED_COLOR)
						return false;

			return true;
		}

		private void shiftRows(int row) {
			// обнуляем съевшийся ряд
			for (int col = 0; col < Const.NUM_COLS; ++col){
				field [ col ] [ row ] = Const.UNUSED_COLOR;
			}

			// сдвигаем вышележащие ряды вниз
			for (int nrow = row - 1; nrow >= 0; nrow--){
				for (int col = 0; col < Const.NUM_COLS; ++col){
					field [ col ] [ nrow + 1 ] = field [ col ] [ nrow ];
				}
			}

			// новый ряд
			for(int col = 0; col < Const.NUM_COLS; ++col) {
				field [ col ] [ 0 ] = Const.UNUSED_COLOR;
			}
		}

	}
}