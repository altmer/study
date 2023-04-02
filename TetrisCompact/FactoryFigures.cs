using System;

namespace TetrisCompact
{
	/// <summary>
	/// Класс, реализующий паттерн AbstractFactory. Возвращает случайную фигуру (AbstractFigure).
	/// </summary>
	public static class FactoryFigures
	{
		private static readonly Random rand = new Random();
		private static AbstractFigure nextFigure = GetFigure();

		public static AbstractFigure NextFigure{
			get{
				return nextFigure;
			}
		}

		public static AbstractFigure NewFigure() {
			AbstractFigure ret = nextFigure;
            nextFigure = GetFigure();
			return ret;
		}

		/// <summary>
		/// Новая фигура. 
		/// </summary>
		/// <returns>Случайная фигура (подкласс AbstractFigure)</returns>
		private static AbstractFigure GetFigure() {
			int index = rand.Next(1, Const.NUM_FIGURES + 1);

			switch(index) {
				case Const.I:
					return new FigureI();

				case Const.J:
					return new FigureJ();

				case Const.L:
					return new FigureL();

				case Const.O:
					return new FigureO();

				case Const.S:
					return new FigureS();

				case Const.T:
					return new FigureT();

				case Const.Z:
					return new FigureZ();
			}
			throw new ApplicationException();
		}


	}
}