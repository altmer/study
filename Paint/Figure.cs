using System;
using System.Drawing;

namespace Kamikaze
{
	public enum Marker
	{
		LEFTTOP,
		RIGHTTOP,
		LEFTBOT,
		RIGHTBOT,
		TOP,
		BOT,
		LEFT,
		RIGHT,
		NONE
	};

	[Serializable]
	public abstract class Figure : ICloneable
	{
		private const int markerHeight = 6;
		private const int markerWidth = 6;

		protected Point startPoint, endPoint;
		protected Color color, colorBackground;
		protected int width;
		protected bool fill;
		protected bool selected;
		protected bool edited;

		protected Figure(Point p1, Point p2, Color c, Color h, int w, bool f){
			startPoint = p1; endPoint=p2; color = c; width = w; colorBackground = h; fill = f;
		}

		public Color LineColor{
			get{
				return color;
			}
			set{
				color = value;
			}
		}

		public Color BackgroundColor{
			get{
				return colorBackground;
			}
			set{
				colorBackground = value;
			}
		}

		public bool Selected{
			get{
				return selected;
			}
			set{
				selected = value;
			}
		}

		public bool Edited{
			get{
				return edited;
			}
			set{
				selected = edited = value;
			}
		}

		public int PenWidth{
			set{
				width = value;
			}
		}

		public Point GetLocatoin(int shiftX, int shiftY){
			int x1 = startPoint.X + shiftX, x2 = endPoint.X + shiftX, y1 = startPoint.Y + shiftY, y2 = endPoint.Y + shiftY;
			Normalize(ref x1, ref x2, ref y1, ref y2);
			return new Point(x1, y1);
		}

		public Size GetSize(int shiftX, int shiftY) {
			int x1 = startPoint.X + shiftX, x2 = endPoint.X + shiftX, y1 = startPoint.Y + shiftY, y2 = endPoint.Y + shiftY;
			Normalize(ref x1, ref x2, ref y1, ref y2);
			return new Size(x2 - x1, y2 - y1);
		}

		public abstract void Draw (Graphics g, int shiftX, int shiftY);
		public abstract void DrawDash(Graphics g, int shiftX, int shiftY);
		public abstract void DrawDash(Graphics g, int x1, int y1, int x2, int y2, int shiftX, int shiftY);
		public abstract object Clone();

		protected static void Normalize(ref int x1, ref int x2, ref int y1, ref int y2){
			int nx1 = Math.Min(x1, x2);
			int nx2 = Math.Max(x1, x2);
			int ny1 = Math.Min(y1, y2);
			int ny2 = Math.Max(y1, y2);
			x1 = nx1; y1 = ny1; x2 = nx2; y2 = ny2;
		}

		public virtual void PaintAndDrawDash(Graphics g, int x, int y, int shiftX, int shiftY) {
			endPoint.X = x;
			endPoint.Y = y;
			DrawDash(g, shiftX, shiftY);
		}

		public virtual void ResizeAndDrawDash(Graphics g, int x, int y, int shiftX, int shiftY, Marker direction){
			int x1 = startPoint.X + shiftX, x2 = endPoint.X + shiftX, y1 = startPoint.Y + shiftY, y2 = endPoint.Y + shiftY;
			if(direction == Marker.LEFT || direction == Marker.LEFTBOT || direction == Marker.LEFTTOP)
				if(x1 < x2)
					x1 += x;
				else
					x2 += x;

			if(direction == Marker.RIGHT || direction == Marker.RIGHTBOT || direction == Marker.RIGHTTOP)
				if(x1 > x2)
					x1 += x;
				else
					x2 += x;

			if(direction == Marker.TOP || direction == Marker.LEFTTOP || direction == Marker.RIGHTTOP)
				if(y1 < y2)
					y1 += y;
				else
					y2 += y;

			if(direction == Marker.BOT || direction == Marker.LEFTBOT || direction == Marker.RIGHTBOT)
				if(y1 > y2)
					y1 += y;
				else
					y2 += y;

			DrawDash(g, x1, y1, x2, y2, shiftX, shiftY);
		}

		protected static bool insideX(int x, Size imageSize){
			return x >= 0 && x < imageSize.Width;
		}

		protected static bool insideY(int y, Size imageSize) {
			return y >= 0 && y < imageSize.Height;
		}

		protected virtual bool checkResize(int shiftX, int shiftY, Marker direction, Size imageSize){

			bool res = true;

			if(direction == Marker.LEFT || direction == Marker.LEFTBOT || direction == Marker.LEFTTOP)
				res = res && startPoint.X < endPoint.X
					       	? insideX(startPoint.X + shiftX, imageSize)
					       	: insideX(endPoint.X + shiftX, imageSize);

			if(direction == Marker.RIGHT || direction == Marker.RIGHTBOT || direction == Marker.RIGHTTOP)
				res = res && startPoint.X > endPoint.X
					       	? insideX(startPoint.X + shiftX, imageSize)
					       	: insideX(endPoint.X + shiftX, imageSize);

			if(direction == Marker.TOP || direction == Marker.LEFTTOP || direction == Marker.RIGHTTOP)
				res = res && ( startPoint.Y < endPoint.Y
				               	? insideY ( startPoint.Y + shiftY, imageSize )
				               	: insideY ( endPoint.Y + shiftY, imageSize ) );

			if(direction == Marker.BOT || direction == Marker.LEFTBOT || direction == Marker.RIGHTBOT)
				res = res && ( startPoint.Y > endPoint.Y
				               	? insideY ( startPoint.Y + shiftY, imageSize )
				               	: insideY ( endPoint.Y + shiftY, imageSize ) );

			return res;
		}

		public virtual void Resize(int shiftX, int shiftY, Marker direction, Size imageSize){
			if(!checkResize(shiftX, shiftY, direction, imageSize)) return;

			if(direction == Marker.LEFT || direction == Marker.LEFTBOT || direction == Marker.LEFTTOP)
				if(startPoint.X < endPoint.X)
					startPoint.X += shiftX;
				else
                    endPoint.X += shiftX;

			if(direction == Marker.RIGHT || direction == Marker.RIGHTBOT || direction == Marker.RIGHTTOP)
				if(startPoint.X > endPoint.X)
					startPoint.X += shiftX;
				else
					endPoint.X += shiftX;

			if(direction == Marker.TOP || direction == Marker.LEFTTOP || direction == Marker.RIGHTTOP)
				if(startPoint.Y < endPoint.Y)
				    startPoint.Y += shiftY;
				else
				    endPoint.Y += shiftY;

			if(direction == Marker.BOT || direction == Marker.LEFTBOT || direction == Marker.RIGHTBOT)
				if(startPoint.Y > endPoint.Y)
                    startPoint.Y += shiftY;
				else
				    endPoint.Y += shiftY;
		}

		public virtual void Move(int shiftX, int shiftY){
			startPoint.X += shiftX;
			startPoint.Y += shiftY;
			endPoint.X += shiftX;
			endPoint.Y += shiftY;
		}

		public bool IsInside(Size size) {
			return startPoint.X >= 0 && startPoint.X <= size.Width && startPoint.Y >= 0 && 
				startPoint.Y <= size.Height && endPoint.X >= 0 && endPoint.X <= size.Width && 
					endPoint.Y >= 0 && endPoint.Y <= size.Height;
		}

		public bool IsInside(Size size, int shiftX, int shiftY) {
			return startPoint.X + shiftX >= 0 && startPoint.X + shiftX <= size.Width && startPoint.Y + shiftY >= 0 && 
				startPoint.Y + shiftY <= size.Height && endPoint.X + shiftX >= 0 && endPoint.X + shiftX <= size.Width && 
					endPoint.Y + shiftY >= 0 && endPoint.Y + shiftY <= size.Height;
		}

		protected Rectangle GetFigureRectangleNoShift() {
			int x1 = startPoint.X, x2 = endPoint.X, y1 = startPoint.Y, y2 = endPoint.Y;
			Normalize(ref x1, ref x2, ref y1, ref y2);
			return Rectangle.FromLTRB(x1, y1, x2, y2);
		}

		public bool Contains (Point point){
			Rectangle testRect = Rectangle.FromLTRB ( point.X, point.Y, point.X, point.Y );
			Rectangle figureRect = GetFigureRectangleNoShift();
			return figureRect.IntersectsWith ( testRect );
		}

		public bool Intersects (Rectangle rect){
			Rectangle figureRect = GetFigureRectangleNoShift();
			return figureRect.IntersectsWith(rect);
		}

		protected static Point Drag(ref Point p, int step, Size size){
			int nx = p.X / step;
			int ny = p.Y / step;
			if((nx + 1) * step - p.X < p.X - nx * step) {
				nx++;
			}

			if((ny + 1) * step - p.Y < p.Y - ny * step) {
				ny++;
			}

			//if(nx * step >= 0 && nx * step <= size.Width && ny * step <= size.Height && ny * step >=0){
				Point ret = new Point(0, 0){X = nx * step - p.X, Y = ny * step - p.Y};
				p.X = nx * step;
				p.Y = ny * step;
				return ret;
			//}
			//return new Point(0, 0);
		}

		public virtual void DragToGrid(int step, Size size){
            Point tmpStart = startPoint;
            Point tmpEnd = endPoint;

			Point shift = Drag ( ref tmpStart, step, size );

			tmpEnd.X += shift.X;
			tmpEnd.Y += shift.Y;

			Drag ( ref tmpEnd, step, size);

            if (tmpEnd.X > size.Width || tmpStart.X > size.Width)
            {
				if (tmpStart.X < step || tmpEnd.X < step){
					Resize ( -step, 0, Marker.RIGHT, size );
					DragToGrid ( step, size );
					return;
				}

                tmpStart.X -= step;
                tmpEnd.X -= step;
            }

			if(tmpEnd.Y > size.Height || tmpStart.Y > size.Height)
            {
				if(tmpStart.Y < step ||  tmpEnd.Y < step) {
					Resize(0, -step, Marker.BOT, size);
					DragToGrid(step, size);
					return;
				}
				tmpStart.Y -= step;
                tmpEnd.Y -= step;
            }

            startPoint = tmpStart;
            endPoint = tmpEnd;
		}

		protected void DrawMarkers (Graphics g, int shiftX, int shiftY){
			Rectangle rect = GetFigureRectangleNoShift();
			rect.Location = new Point(rect.Location.X + shiftX, rect.Location.Y + shiftY);

			Pen p = new Pen ( Color.Black, 1 );
			g.DrawRectangle (p, rect.Left, rect.Top, markerWidth, markerHeight);
			g.DrawRectangle (p, rect.Left, rect.Bottom - markerHeight, markerWidth, markerHeight);
			g.DrawRectangle (p, rect.Right - markerWidth, rect.Top, markerWidth, markerHeight );
			g.DrawRectangle (p, rect.Right - markerWidth, rect.Bottom - markerHeight, markerWidth, markerHeight);

			g.DrawRectangle(p,(rect.Left + rect.Right) / 2 - markerWidth / 2, rect.Top, markerWidth, markerHeight);
			g.DrawRectangle(p, (rect.Left + rect.Right) / 2 - markerWidth / 2, rect.Bottom - markerHeight, markerWidth, markerHeight);
			g.DrawRectangle(p, rect.Left, (rect.Top + rect.Bottom) /2 - markerHeight / 2, markerWidth, markerHeight);
			g.DrawRectangle(p, rect.Right - markerWidth, (rect.Top + rect.Bottom) /2 - markerHeight / 2, markerWidth, markerHeight);

		}

		public Marker MarkersContain(Point p){
			Rectangle rect = GetFigureRectangleNoShift();

			if (Math.Abs(p.X - rect.Left) <= markerWidth && Math.Abs(p.Y - rect.Top) <= markerWidth)
				return Marker.LEFTTOP;

			if(Math.Abs(p.X - rect.Left) <= markerWidth && Math.Abs(p.Y - rect.Bottom) <= markerWidth)
				return Marker.LEFTBOT;

			if(Math.Abs(p.X - rect.Right) <= markerWidth && Math.Abs(p.Y - rect.Top) <= markerWidth)
				return Marker.RIGHTTOP;

			if(Math.Abs(p.X - rect.Right) <= markerWidth && Math.Abs(p.Y - rect.Bottom) <= markerWidth)
				return Marker.RIGHTBOT;

			if(Math.Abs(p.X - rect.Right) <= markerWidth && Math.Abs(p.Y - (rect.Bottom + rect.Top) / 2) <= markerWidth/2)
				return Marker.RIGHT;

			if(Math.Abs(p.X - rect.Left) <= markerWidth && Math.Abs(p.Y - (rect.Bottom + rect.Top) / 2) <= markerWidth/2)
				return Marker.LEFT;

			if(Math.Abs(p.Y - rect.Top) <= markerWidth && Math.Abs(p.X - (rect.Left + rect.Right) / 2) <= markerWidth/2)
				return Marker.TOP;

			if(Math.Abs(p.Y - rect.Bottom) <= markerWidth && Math.Abs(p.X - (rect.Left + rect.Right) / 2) <= markerWidth/2)
				return Marker.BOT;

			return Marker.NONE; 
		}

	}
}
