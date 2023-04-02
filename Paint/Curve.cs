using System;
using System.Collections.Generic;
using System.Drawing;

namespace Kamikaze
{
	[Serializable]
	class Curve : Figure
	{
		private List<Point> pts;

		public Curve(Point p1, Point p2, Color colline, Color colback, int width, bool fill) : base(p1, p2, colline, colback, width, fill) {
			pts = new List<Point>();
			pts.Add(p1);
			pts.Add(p2);
		}

		Point[] ToArray(int shiftX, int shiftY) {
			Point[] ret = new Point[pts.Count];
			int ind=0;
			foreach(Point p in pts){
				ret[ind++] = new Point(p.X + shiftX, p.Y + shiftY);
			}
			return ret;
		}

        Point[] ToArray(List<Point> tmp, int shiftX, int shiftY)
        {
            Point[] ret = new Point[tmp.Count];
            int ind = 0;
            foreach (Point p in tmp)
            {
                ret[ind++] = new Point(p.X + shiftX, p.Y + shiftY);
            }
            return ret;
        }

		public override void Draw(Graphics g, int shiftX, int shiftY) {
			int x1 = startPoint.X + shiftX, x2 = endPoint.X + shiftX, y1 = startPoint.Y + shiftY, y2 = endPoint.Y + shiftY;
			Normalize(ref x1, ref x2, ref y1, ref y2);

			if(selected) {
				Pen pen = new Pen(Color.Black, 1);
				pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
				g.DrawRectangle(pen, Rectangle.FromLTRB(x1, y1, x2, y2));
			}
			if(edited)
				DrawMarkers(g, shiftX, shiftY);

			g.DrawCurve(new Pen(color, width), ToArray(shiftX, shiftY));
		}

		public override void DrawDash(Graphics g, int shiftX, int shiftY) {
			Pen pen = new Pen(color, width);
			pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
			g.DrawCurve(pen, ToArray( shiftX, shiftY));
		}

        public override void ResizeAndDrawDash(Graphics g, int x, int y, int shiftX, int shiftY, Marker direction)
        {
            int x1 = startPoint.X, x2 = endPoint.X, y1 = startPoint.Y, y2 = endPoint.Y;
            if (direction == Marker.LEFT || direction == Marker.LEFTBOT || direction == Marker.LEFTTOP)
                if (x1 < x2)
                    x1 += x;
                else
                    x2 += x;

            if (direction == Marker.RIGHT || direction == Marker.RIGHTBOT || direction == Marker.RIGHTTOP)
                if (x1 > x2)
                    x1 += x;
                else
                    x2 += x;

            if (direction == Marker.TOP || direction == Marker.LEFTTOP || direction == Marker.RIGHTTOP)
                if (y1 < y2)
                    y1 += y;
                else
                    y2 += y;

            if (direction == Marker.BOT || direction == Marker.LEFTBOT || direction == Marker.RIGHTBOT)
                if (y1 > y2)
                    y1 += y;
                else
                    y2 += y;

            DrawDash(g, x1, y1, x2, y2, shiftX, shiftY);
        }

		public override void DrawDash(Graphics g, int x1, int y1, int x2, int y2, int shiftX, int shiftY){
			if (x2 < x1 || y2 < y1)
				return;

            List<Point> points = new List<Point>(pts);
			double coefX = (double) ( Math.Abs ( x1 - x2 ) ) / Math.Abs ( startPoint.X - endPoint.X );
			double coefY = (double) ( Math.Abs ( y1 - y2 ) ) / Math.Abs ( startPoint.Y - endPoint.Y );

			for (int i = 0; i < points.Count; ++i){
                Point pt = points[i];
				if(x1 != startPoint.X)
					pt.X = (int)(endPoint.X - Math.Abs( endPoint.X - points [ i ].X ) * coefX);

				if(x2 != endPoint.X)
					pt.X = (int)(startPoint.X + Math.Abs(pt.X - startPoint.X) * coefX);

				if (y1 != startPoint.Y)
					pt.Y = (int)(endPoint.Y - Math.Abs(pt.Y - endPoint.Y) * coefY);

				if(y2 != endPoint.Y)
					pt.Y = (int)(startPoint.Y + Math.Abs(pt.Y - startPoint.Y) * coefY);
                points[i] = pt;
			}

            Point[] ptt = ToArray(points,shiftX, shiftY);
			Pen pen = new Pen(color, width);
			pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
			g.DrawCurve(pen, ptt);
		}

		public override void Resize(int shiftX, int shiftY, Marker direction, Size imageSize) {
			if (!checkResize ( shiftX,  shiftY,  direction,  imageSize )) return;

			Point newStart = startPoint;
			Point newEnd = endPoint;

			if(direction == Marker.LEFT || direction == Marker.LEFTBOT || direction == Marker.LEFTTOP)
				newStart.X += shiftX;

			if(direction == Marker.RIGHT || direction == Marker.RIGHTBOT || direction == Marker.RIGHTTOP)
				newEnd.X += shiftX;

			if(direction == Marker.TOP || direction == Marker.LEFTTOP || direction == Marker.RIGHTTOP)
				newStart.Y += shiftY;

			if(direction == Marker.BOT || direction == Marker.LEFTBOT || direction == Marker.RIGHTBOT)
				newEnd.Y += shiftY;

			double coefX = (double) ( Math.Abs ( newStart.X - newEnd.X ) ) / Math.Abs ( startPoint.X - endPoint.X );
			double coefY = (double) ( Math.Abs ( newStart.Y - newEnd.Y ) ) / Math.Abs ( startPoint.Y - endPoint.Y );

			for(int i = 0; i < pts.Count; ++i) {
				Point pt = pts [ i ];

				if(newStart.X != startPoint.X)
					pt.X = (int)(endPoint.X - Math.Abs(endPoint.X - pt.X) * coefX);

				if(newEnd.X != endPoint.X)
					pt.X = (int)(startPoint.X + Math.Abs(pt.X - startPoint.X) * coefX);

				if(newStart.Y != startPoint.Y)
					pt.Y = (int)(endPoint.Y - Math.Abs(pt.Y - endPoint.Y) * coefY);

				if(newEnd.Y != endPoint.Y)
					pt.Y = (int)(startPoint.Y + Math.Abs(pt.Y - startPoint.Y) * coefY);

				pts [ i ] = pt;
			}

			startPoint = newStart;
			endPoint = newEnd;

		}

		protected override bool checkResize(int shiftX, int shiftY, Marker direction, Size imageSize) {
			int x1 = startPoint.X, x2 = endPoint.X, y1 = startPoint.Y, y2 = endPoint.Y;

			if(direction == Marker.LEFT || direction == Marker.LEFTBOT || direction == Marker.LEFTTOP)
				x1 += shiftX;

			if(direction == Marker.RIGHT || direction == Marker.RIGHTBOT || direction == Marker.RIGHTTOP)
				x2 += shiftX;

			if(direction == Marker.TOP || direction == Marker.LEFTTOP || direction == Marker.RIGHTTOP)
				y1 += shiftY;

			if(direction == Marker.BOT || direction == Marker.LEFTBOT || direction == Marker.RIGHTBOT)
				y2 += shiftY; 

			return x2 > x1 && y2 > y1 && insideX ( x1, imageSize ) && insideX ( x2, imageSize ) && insideY ( y1, imageSize ) 
							&& insideY ( y2, imageSize );
		}


		public override void PaintAndDrawDash(Graphics g, int x, int y, int shiftX, int shiftY) {
			startPoint.X = Math.Min(x, startPoint.X);
			startPoint.Y = Math.Min(y, startPoint.Y);
			endPoint.X = Math.Max ( x, endPoint.X );
			endPoint.Y = Math.Max ( y, endPoint.Y );
			pts.Add(new Point(x, y));			
			DrawDash(g, shiftX, shiftY);			
		}

		public override void Move(int shiftX, int shiftY) {
			for (int i = 0; i< pts.Count; ++i){
				Point p = pts [ i ];
				p.X += shiftX;
				p.Y += shiftY;
				pts [ i ] = p;
			}
			checkBounds();
		}

        public void Move(List<Point> tmp, int shiftX, int shiftY)
        {
            for (int i = 0; i < tmp.Count; ++i)
            {
                Point p = tmp[i];
                p.X += shiftX;
                p.Y += shiftY;
                tmp[i] = p;
            }
        }

		public override object Clone(){
			return new Curve(startPoint, endPoint, color, colorBackground, width, fill) { pts = new List<Point>(pts.ToArray()) };
		}

		private void checkBounds(){
			int minX = Int32.MaxValue, maxX = -1, minY = Int32.MaxValue, maxY = -1;

			foreach (Point p in pts){
				minX = Math.Min ( minX, p.X );
				maxX = Math.Max(maxX, p.X);
				minY = Math.Min(minY, p.Y);
				maxY = Math.Max(maxY, p.Y);
			}
			startPoint.X = minX;
			startPoint.Y = minY;
			endPoint.X = maxX;
			endPoint.Y = maxY;
		}

		public override void DragToGrid(int step, Size size){
			List<Point> tmp = new List <Point>(pts);
			while(tmp.Count >= 2 && tmp[0].Equals ( tmp[1] ))
				tmp.RemoveAt ( 0 );

			Point first = tmp[0];
			Point shift = Drag(ref first, step, size);

			Move(tmp, shift.X, shift.Y);

			Point last = tmp[tmp.Count - 1];
			Drag(ref last, step, size);
			Point oldLast = tmp[tmp.Count - 1];

			tmp[0] = first;
			tmp[tmp.Count - 1] = last;

			for(int i = 1; i < tmp.Count - 1; ++i) {
				Point cur = tmp[i];

				double prop = (i) / (double)(tmp.Count);

				cur.X += (int)Math.Round(prop * (last.X - oldLast.X));
				cur.Y += (int)Math.Round(prop * (last.Y - oldLast.Y));

				tmp[i] = cur;
			}


			bool shiftRight = false;
			bool shiftLeft = false;
			bool shiftDown = false;
			bool shiftUp = false;

			foreach(Point p in tmp) {
				if (p.X < 0) shiftRight = true;
				if (p.Y < 0) shiftDown = true;
				if(p.X > size.Width) shiftLeft = true;
				if (p.Y > size.Height) shiftUp = true;
			}

			if (shiftRight){
				foreach(Point p in tmp){
					if(size.Width - p.X < step){
						Resize ( -step, 0, Marker.RIGHT, size );
						DragToGrid ( step, size );
						return;
					}
				}
				Move ( tmp, step, 0 );
			}
			if(shiftLeft) {
				foreach(Point p in tmp) {
					if(p.X < step) {
						Resize(-step, 0, Marker.RIGHT, size);
						DragToGrid(step, size);
						return;
					}
				}
				Move(tmp, -step, 0);
			}
			if(shiftDown) {
				foreach(Point p in tmp) {
					if(size.Height - p.Y < step) {
						Resize(0, -step, Marker.BOT, size);
						DragToGrid(step, size);
						return;
					}
				}
				Move(tmp, 0, step);
			}
			if(shiftUp) {
				foreach(Point p in tmp) {
					if(p.Y < step) {
						Resize(0, -step, Marker.BOT, size);
						DragToGrid(step, size);
						return;
					}
				}
				Move(tmp, 0, -step);
			}

			pts = tmp;

			checkBounds ( );


        }
	}
}
