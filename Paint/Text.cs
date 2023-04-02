using System;
using System.Drawing;

namespace Kamikaze
{
    [Serializable]
	class Text : Figure
	{
		private string text;
		private Font font;

		public string TextToDraw{
			set{
				text = value;
			}
			get{
				return text;
			}
		}

    	public Font Font{
    		get{
    			return font;
    		}
			set{
				font = value;
			}
    	}

		public Text(Point p1, Point p2, Color c, Color h, int w, bool f, Font font) : base( p1, p2, c, h, w, f ){
			this.font = font;
		}

		public override void Draw(Graphics g, int shiftX, int shiftY){
			int x1 = startPoint.X + shiftX, x2 = endPoint.X + shiftX, y1 = startPoint.Y + shiftY, y2 = endPoint.Y + shiftY;
			Normalize(ref x1, ref x2, ref y1, ref y2);

			if(fill)
				g.FillRectangle(new SolidBrush(colorBackground), Rectangle.FromLTRB(x1, y1, x2, y2));

			if(selected) {
				Pen pen = new Pen(Color.Black, 1);
				pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
				g.DrawRectangle(pen, Rectangle.FromLTRB(x1, y1, x2, y2));
			}
			if(edited)
				DrawMarkers(g, shiftX, shiftY);

			Normalize(ref x1, ref x2, ref y1, ref y2);

			g.DrawString(text, font, new Pen(color, width).Brush, Rectangle.FromLTRB(x1, y1, x2, y2));
		}
		public override void DrawDash(Graphics g, int shiftX, int shiftY){
			int x1 = startPoint.X + shiftX, x2 = endPoint.X + shiftX, y1 = startPoint.Y + shiftY, y2 = endPoint.Y + shiftY;
			Normalize(ref x1, ref x2, ref y1, ref y2);

			Pen pen = new Pen(color, width);
			pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
			g.DrawRectangle(pen, Rectangle.FromLTRB(x1, y1, x2, y2));
		}

		public override void DrawDash(Graphics g, int x1, int y1, int x2, int y2, int shiftX, int shiftY) {
			Normalize(ref x1, ref x2, ref y1, ref y2);
			Pen pen = new Pen(color, width);
			pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
			g.DrawRectangle(pen, Rectangle.FromLTRB(x1, y1, x2, y2));
    	}

    	public override object Clone(){
    		return new Text ( startPoint, endPoint, color, colorBackground, width, fill, font ){text = text};
    	}
	}
}
