using Tetris;

namespace TetrisCompact
{
	public abstract class AbstractFigure
	{
		protected Scheme[] _schemes;
		protected int _state;
		protected int _color;

		/// <summary>
		/// ��������� ������ �� ������� �������.  
		/// </summary>
		public void Rotate(){
			_state = (_state + 1) % _schemes.Length;
		}


		/// <summary>
		/// ������ ������.  
		/// </summary>
		/// <returns></returns>
		public int GetWidth(){
			return _schemes[ _state ].GetWidth();
		}

		/// <summary>
		/// ������ ������. 
		/// </summary>
		/// <returns></returns>
		public int GetHeight(){
			return _schemes[ _state ].GetHeight();
		}

		/// <summary>
		/// �������� ������ ������ � ���� ���������� �������.
		/// </summary>
		/// <returns></returns>
		public bool[][] GetModel(){
			return _schemes[ _state ].GetModel();
		}

		/// <summary>
		/// �������� ���� ������.
		/// </summary>
		/// <returns>���� ������</returns>
		public int GetColor(){
			return _color;
		}

		/// <summary>
		/// ������� ����� ������ ������. 
		/// </summary>
		/// <returns></returns>
		public abstract AbstractFigure Clone();
        
	}
}