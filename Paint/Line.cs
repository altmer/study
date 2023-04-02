using System;
using System.Drawing;

namespace Kamikaze
{
	[Serializable]
	class Line : Figure
	{
		public Line(Point p1, Point p2, Color colline, Color colback, int width, bool fill) : base(p1, p2, colline, colback, width, fill) { }

		public override void Draw(Graphics g, int shiftX, int shiftY) {
			int x1 = startPoint.X + shiftX, x2 = endPoint.X + shiftX, y1 = startPoint.Y + shiftY, y2 = endPoint.Y + shiftY;
			//Normalize(ref x1, ref x2, ref y1, ref y2);

			if(selected) {
				Pen pen = new Pen(Color.Black, 1);
				pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
				g.DrawRectangle(pen, Rectangle.FromLTRB(x1, y1, x2, y2));
			}
			if(edited)
				DrawMarkers(g, shiftX, shiftY);

			g.DrawLine(new Pen(color, width), x1,y1,x2,y2);

		}

		public override void DrawDash(Graphics g, int shiftX, int shiftY) {
			int x1 = startPoint.X + shiftX, x2 = endPoint.X + shiftX, y1 = startPoint.Y + shiftY, y2 = endPoint.Y + shiftY;
			Pen pen = new Pen(color, width);
			pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
			g.DrawLine(pen, x1, y1, x2, y2);
		}

		public override void DrawDash(Graphics g, int x1, int y1, int x2, int y2, int shiftX, int shiftY) {
			Pen pen = new Pen(color, width);
			pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
			g.DrawLine(pen, x1, y1, x2, y2);
		}

		public override object Clone(){
			return new Line ( startPoint, endPoint, color, colorBackground, width, fill );
		}

	}
}
