using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tetris
{
	/// <summary>
	/// Схема фигурки тетриса.
	/// </summary>
	public class Scheme : ICloneable
	{
		private readonly bool[][] model;

		private Scheme (Scheme original){
			model = (bool[][]) original.model.Clone();
		}

		public Scheme(string[] stringModel){
			int width = 0;
			foreach(var s in stringModel)
				width = Math.Max(width, s.Length);

			model = new bool[width][];
			for(int i = 0; i < model.Length; ++i )
				model[i] = new bool[stringModel.Length];

			for(int i = 0; i < width; ++i)
				for(int j = 0; j < stringModel.Length; ++j)
					model [ i ] [ j ] = ( stringModel [ j ] [ i ] == '#' );
		}

		public int GetWidth(){
			return model.Length;
		}

		public int GetHeight(){
			int ret = 0;
			foreach(var s in model)
				ret = Math.Max(ret, s.Length);

			return ret;
		}

		public override bool Equals(object obj){
			if (obj == null || GetType() != obj.GetType())
				return false;

			var sobj = obj as Scheme;

			return sobj != null && IsModelsEqual(model, sobj.model);
		}

		public override int GetHashCode(){
			return base.GetHashCode();
		}

		public object Clone(){
			return new Scheme( this );
		}

		public bool[][] GetModel(){
			return model.Clone() as bool[][];
		}

		public static bool operator ==(Scheme lhs, Scheme rhs){
			return lhs.Equals(rhs);
		}

		public static bool operator !=(Scheme lhs, Scheme rhs){
			return !(lhs == rhs);
		}

		/// <summary>
		/// Утилита для проверки равенства моделей схем (в C# нет корректного сравнения массивов ! ).  
		/// </summary>
		/// <param name="lhs"></param>
		/// <param name="rhs"></param>
		/// <returns></returns>
		private static bool IsModelsEqual(bool[][] lhs, bool[][] rhs){
			if(lhs.Length != rhs.Length) return false;

			for (int i = 0; i < lhs.Length; ++i ){
				if(lhs[i].Length != rhs[i].Length) return false;

				for(int j = 0; j < lhs[i].Length; ++j )
					if(lhs[i][j] != rhs[i][j]) return false;
			}

			return true;
		}
	}
}
