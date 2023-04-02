using Tetris;

namespace TetrisCompact
{
	public abstract class AbstractFigure
	{
		protected Scheme[] _schemes;
		protected int _state;
		protected int _color;

		/// <summary>
		/// Повернуть фигуру по часовой стрелке.  
		/// </summary>
		public void Rotate(){
			_state = (_state + 1) % _schemes.Length;
		}


		/// <summary>
		/// Ширина фигуры.  
		/// </summary>
		/// <returns></returns>
		public int GetWidth(){
			return _schemes[ _state ].GetWidth();
		}

		/// <summary>
		/// Высота фигуры. 
		/// </summary>
		/// <returns></returns>
		public int GetHeight(){
			return _schemes[ _state ].GetHeight();
		}

		/// <summary>
		/// Получить модель фигуры в виде двумерного массива.
		/// </summary>
		/// <returns></returns>
		public bool[][] GetModel(){
			return _schemes[ _state ].GetModel();
		}

		/// <summary>
		/// Получить цвет фигуры.
		/// </summary>
		/// <returns>Цвет фигуры</returns>
		public int GetColor(){
			return _color;
		}

		/// <summary>
		/// Создать копию данной фигуры. 
		/// </summary>
		/// <returns></returns>
		public abstract AbstractFigure Clone();
        
	}
}