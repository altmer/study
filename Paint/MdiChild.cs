using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Windows.Forms;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace Kamikaze
{
	public partial class MdiChild : Form
	{
		private List<Figure> listFigures;
		private readonly List <Figure> movingFigures;

		private Figure currentFigure;
		private bool isDrawing;
		private BufferedGraphics bufferedGraphics;
		private Graphics graphics;
		private bool isModified;
		private Size imageSize;
		private BufferedGraphicsContext currentContext;

		private bool isSelecting;
		private bool isMoving;
		private Point clickPoint;

		private bool isEditing;
		private Figure editedFigure;
		private bool isEditedResizing;
		private Marker resizingType = Marker.NONE;

		public MdiChild() {
			listFigures = new List<Figure>();
			movingFigures = new List<Figure>();
			InitializeComponent();
		}

		public MdiChild(Size size) : this(){
			imageSize = size;
			AutoScrollMinSize = size;
		}

		public bool Editing{
			get{
				return isEditing;
			}
		}

		// events' handlers

		private void MdiChild_MouseDown(object sender, MouseEventArgs e) {
			clickPoint = new Point(e.X - AutoScrollPosition.X, e.Y - AutoScrollPosition.Y);

			if(e.Button == MouseButtons.Left){
				MainForm par = (MainForm) MdiParent;

				if(isEditing && editedFigure!= null && editedFigure.Contains(clickPoint)) {
					if((resizingType = editedFigure.MarkersContain(clickPoint)) != Marker.NONE)
						isEditedResizing = true;
					else{
						isMoving = true;
						movingFigures.Add ( editedFigure );
					}
				}
				else{
					if ( editedFigure != null ){
						editedFigure.Edited = false;
						editedFigure = null;
					}
					isEditing = isEditedResizing = false;

					if ( !par.SelectMode ){
						isDrawing = true;
						int X = clickPoint.X;
						int Y = clickPoint.Y;
						switch (par.currentFigure){
							case Figures.Rectangle:
								currentFigure = new Rect ( new Point ( X, Y ), new Point ( X, Y ), par.ColorLine, par.ColorBackground,
								                           par.PenWidth, par.Fill );
								break;
							case Figures.Ellipse:
								currentFigure = new Ellipse ( new Point ( X, Y ), new Point ( X, Y ), par.ColorLine, par.ColorBackground,
								                              par.PenWidth, par.Fill );
								break;
							case Figures.Line:
								currentFigure = new Line ( new Point ( X, Y ), new Point ( X, Y ), par.ColorLine, par.ColorBackground,
								                           par.PenWidth, par.Fill );
								break;
							case Figures.Curve:
								currentFigure = new Curve ( new Point ( X, Y ), new Point ( X, Y ), par.ColorLine, par.ColorBackground,
								                            par.PenWidth, par.Fill );
								break;
							case Figures.Text:
								currentFigure = new Text ( new Point ( X, Y ), new Point ( X, Y ), par.ColorLine, par.ColorBackground,
								                           par.PenWidth, par.Fill, par.Font );
								break;
						}
					}
					else{
						bool select = false;
						bool hit = false;
						foreach (Figure figure in listFigures){
							if ( figure.Contains ( clickPoint ) ){
								hit = true;
								if ( !figure.Selected ){
									select = true;
								}
							}
						}

						if ( !hit || select ){
							isSelecting = true;
						}
						if ( hit && !select ){
							isMoving = true;

							// add figures to moving set
							foreach (Figure figure in listFigures){
								if ( figure.Selected ){
									movingFigures.Add ( figure );
								}
							}
						}
					}
				}
			}
			else if(e.Button == MouseButtons.Middle) {
				currentFigure = null;
				listFigures.Clear();
				isDrawing = false;
			}

			Invalidate();
		}


		private void MdiChild_MouseUp(object sender, MouseEventArgs e) {
			MainForm parent = GetParent();
			if(e.Button == MouseButtons.Left) {

				if(isDrawing) {
					bufferedGraphics.Render();
					if(currentFigure.IsInside(imageSize)) {
                        if (currentFigure is Text)
                        {
                            MyTextBox textBox = new MyTextBox ( currentFigure as Text, this , MdiParent as MainForm);
							textBox.Show(AutoScrollPosition.X, AutoScrollPosition.Y);
                        }
						if(parent.GridAutoDrag){
							currentFigure.DragToGrid ( parent.GridStep, imageSize );
						}
						listFigures.Add(currentFigure);
					}
					isModified = true;
				}

				if (isSelecting){

					int X = e.X - AutoScrollPosition.X;
					int Y = e.Y - AutoScrollPosition.Y;

					if (X == clickPoint.X && Y == clickPoint.Y){
						bool select = false;
						foreach (Figure figure in listFigures){
							if(figure.Contains ( clickPoint )){
								select = true;
								figure.Selected = true;
							}
						}

						if(!select)
							UnselectAllFigures();

					}
				}

				if(isMoving){
					int X = e.X - AutoScrollPosition.X;
					int Y = e.Y - AutoScrollPosition.Y;

					int shiftX = X - clickPoint.X;
					int shiftY = Y - clickPoint.Y;

					bool fits = true;
					foreach(Figure figure in movingFigures){
						if (! figure.IsInside ( imageSize, shiftX, shiftY )){
							fits = false;
							break;
						}
					}
					if(fits){
						foreach(Figure figure in movingFigures) {
							figure.Move ( shiftX, shiftY );
							if(parent.GridAutoDrag) {
								figure.DragToGrid(parent.GridStep, imageSize);
							}
						}
						isModified = true;
					}
				}

				if(isEditedResizing){
					int X = e.X - AutoScrollPosition.X;
					int Y = e.Y - AutoScrollPosition.Y;

					int shiftX = X - clickPoint.X;
					int shiftY = Y - clickPoint.Y;
				
					editedFigure.Resize (shiftX, shiftY, resizingType, imageSize );
					isModified = true;

					if(parent.GridAutoDrag)
						editedFigure.DragToGrid ( parent.GridStep, imageSize );
				}

				currentFigure = null;
				movingFigures.Clear();

				isDrawing = false;
				isSelecting = false;
				isMoving = false;
				isEditedResizing = false;

				Invalidate();
			}
		}

		private void MdiChild_MouseMove(object sender, MouseEventArgs e) {
			int X = e.X - AutoScrollPosition.X;
			int Y = e.Y - AutoScrollPosition.Y;

			if(isDrawing) {
				bufferedGraphics.Render();
				graphics = CreateGraphics();
				currentFigure.PaintAndDrawDash(graphics, X, Y, AutoScrollPosition.X, AutoScrollPosition.Y);
			}

			if(isSelecting){
				UnselectAllFigures();

				Pen pen = new Pen ( Color.Black, 1 );
				pen.DashStyle = DashStyle.Dash;
				Rectangle rectDraw = Rectangle.FromLTRB (	Math.Min ( clickPoint.X + AutoScrollPosition.X, e.X ),
															Math.Min(clickPoint.Y + AutoScrollPosition.Y, e.Y),
															Math.Max(clickPoint.X + AutoScrollPosition.X, e.X), 
															Math.Max(clickPoint.Y + AutoScrollPosition.Y, e.Y));

				Rectangle rectCheck = Rectangle.FromLTRB(Math.Min(clickPoint.X, X), Math.Min(clickPoint.Y , Y),
													  Math.Max(clickPoint.X, X), Math.Max(clickPoint.Y, Y));
				foreach(Figure figure in listFigures) {
					if(figure.Intersects(rectCheck)) {
						figure.Selected = true;
					}
				}

				Refresh();
				graphics = CreateGraphics();
				graphics.DrawRectangle(pen, rectDraw);

			}

			if (isMoving){

				int shiftX = X - clickPoint.X;
				int shiftY = Y - clickPoint.Y;

				Refresh();
				graphics = CreateGraphics();

				foreach(Figure figure in movingFigures){
					figure.DrawDash(graphics, shiftX + AutoScrollPosition.X, shiftY + AutoScrollPosition.Y);
				}
			}

			if(isEditedResizing){
				bufferedGraphics.Render();
				graphics = CreateGraphics();
				editedFigure.ResizeAndDrawDash(graphics, X - clickPoint.X, Y - clickPoint.Y, AutoScrollPosition.X, AutoScrollPosition.Y, 
																																resizingType);
			}
			MainForm parent = GetParent();
			if(parent != null) parent.SetCoordinatesStatus(X, Y);
		}

		private void MdiChild_MouseDoubleClick(object sender, MouseEventArgs e) {
			MainForm parent = GetParent();
			if(parent.SelectMode){
				UnselectAllFigures();
				foreach (Figure figure in listFigures){
					if ( figure.Contains ( clickPoint ) ){
						figure.Edited = true;
						editedFigure = figure;
						isEditing = true;
						break;
					}
				}
				if(editedFigure != null && editedFigure is Text) {
					MyTextBox textBox = new MyTextBox(editedFigure as Text, this, MdiParent as MainForm);
					textBox.Show(AutoScrollPosition.X, AutoScrollPosition.Y);
				}
			}
		}
        
		private void MdiChild_Paint(object sender, PaintEventArgs e) {
			MainForm parent = GetParent();
			Graphics g = bufferedGraphics.Graphics;

			g.Clear(Color.Gray);
			g.FillRectangle(new SolidBrush(Color.White), Rectangle.FromLTRB(0, 0, imageSize.Width, imageSize.Height));

			if(parent.Grid) {
				Pen p = new Pen(Color.Gray, 1) { DashStyle = DashStyle.Dot };
				int step = parent.GridStep;

				for (int x = AutoScrollPosition.X; x <= imageSize.Width; x += step){
					g.DrawLine(p, x, AutoScrollPosition.Y, x, imageSize.Height);
				}

				for(int y = AutoScrollPosition.Y; y <= imageSize.Height; y += step) {
					g.DrawLine(p, AutoScrollPosition.X, y, imageSize.Width, y);
				}
			}

			foreach(Figure f in listFigures) {
				f.Draw(g, AutoScrollPosition.X, AutoScrollPosition.Y);
			}
			bufferedGraphics.Render(e.Graphics);
		}

		private void MdiChild_FormClosing(object sender, FormClosingEventArgs e) {
			if(isModified) {
				switch(MessageBox.Show("Документ "+Text+" не сохранён. Хотите сохранить его?", "Вопрос", MessageBoxButtons.YesNoCancel)) {
					case DialogResult.Yes:
						SaveFileDialog dial = new SaveFileDialog();
						dial.AddExtension=true;
						dial.Filter="Kamikaze files (*.kam)|*.kam";
						dial.InitialDirectory = Environment.CurrentDirectory;
						if(dial.ShowDialog() == DialogResult.OK) {
							if(!dial.FileName.EndsWith(".kam")) {
								MessageBox.Show("Wrong extension!", "Oops!");
								return;
							}
							Save(dial.FileName);
						}
						break;
					case DialogResult.Cancel:
						e.Cancel=true;
						return;
				}
			}
			
		}

		public void Save(string path) {
			BinaryFormatter f = new BinaryFormatter();
			try {
				Stream str = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.None);
				f.Serialize(str, listFigures);
				f.Serialize(str, imageSize);
				char[] del = { '\\', '.' };
				string[] p = path.Split(del);
				Text = p[p.Length-2];
				str.Close();
				isModified=false;
			}
			catch(Exception ex) {
				MessageBox.Show("Error! Reason: "+ex.Message, "Oops!");
			}
		}

		public void Open(string path) {
			BinaryFormatter f = new BinaryFormatter();
			Stream str;
			try {
				str = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);
				char[] del = {'\\', '.'};
				string[] p = path.Split(del);
				Text = p[p.Length-2];
				listFigures = (List<Figure>)f.Deserialize(str);
				imageSize = (Size)f.Deserialize(str);
				AutoScrollMinSize = imageSize;
				Invalidate();
				str.Close();
				isModified=false;
			}
			catch(Exception ex) {
				MessageBox.Show("Error opening the file. Reason: " + ex.Message, "Oops!");
			}
		}

		public void UnselectAllFigures(){
			foreach(Figure figure in listFigures){
				figure.Selected = false;
			}

			if(editedFigure != null) {
				editedFigure.Edited = false;
				editedFigure = null;
			}
			isEditing = false;
		}

		private void MdiChild_Load(object sender, EventArgs e) {
			graphics = CreateGraphics();
			currentContext = BufferedGraphicsManager.Current; 
			currentContext.MaximumBuffer = SystemInformation.PrimaryMonitorMaximizedWindowSize;
			bufferedGraphics = currentContext.Allocate(graphics, Rectangle.FromLTRB(0, 0, SystemInformation.PrimaryMonitorMaximizedWindowSize.Width, SystemInformation.PrimaryMonitorMaximizedWindowSize.Height));
		}

		private void MdiChild_FormClosed(object sender, FormClosedEventArgs e) {
			bufferedGraphics.Dispose();
			MainForm parent = GetParent();
			if(parent != null) parent.RefreshMenuOnClose();
		}

		private void MdiChild_MouseLeave(object sender, EventArgs e) {
			MainForm parent = GetParent();
			if(parent != null) parent.SetCoordinatesStatus(0, 0);
		}

		public void Delete(){
			for (int i = listFigures.Count - 1; i >= 0; --i){
				if(listFigures[i].Selected){
					listFigures.RemoveAt ( i );
				}
			}
            movingFigures.Clear();
			Invalidate();
		}
        
		public List<Figure> GetSelectedFigures() {
			List<Figure> ret = new List <Figure>();

			foreach (Figure figure in listFigures){
				if (figure.Selected){
					ret.Add ( figure.Clone() as Figure );
				}
			}

			return ret;
		}

		public void AddFigures(List<Figure> list) {
			foreach(Figure figure in list){
				if(!figure.IsInside(imageSize)){
					MessageBox.Show ( "Размер вставляемого рисунка превышает допустимые размеры.", "Ошибка." );
					return;
				}
			}
			listFigures.AddRange ( list );
			Invalidate();
		}

		public void SelectAll() {
			foreach(Figure figure in listFigures){
				figure.Selected = true;
			}
			Invalidate();
		}

		public bool HasSelectedFigures(){
			foreach(Figure figure in listFigures){
				if(figure.Selected)
					return true;
			}
			return false;
		}

		public Metafile GetMetafile(){
			Graphics g = CreateGraphics();
			IntPtr dc = g.GetHdc();
			Metafile mf = new Metafile (dc , EmfType.EmfOnly );
			Graphics gr = Graphics.FromImage ( mf );

			List <Figure> listSelectedFigures = GetSelectedFigures();

			// Найдём границы блока

			int xl = Int32.MaxValue;
			int yl = Int32.MaxValue;
			int xh = Int32.MinValue;
			int yh = Int32.MinValue;

			foreach (Figure figure in listSelectedFigures){
				figure.Selected = false;
				Point p = figure.GetLocatoin(0, 0);
				Size size = figure.GetSize(0, 0);

				xl = Math.Min ( xl, p.X );
				yl = Math.Min ( yl, p.Y );
				xh = Math.Max ( xh, p.X + size.Width );
				yh = Math.Max ( yh, p.Y + size.Height);
			}

			foreach(Figure figure in listSelectedFigures){
				figure.Move ( -xl, -yl );
			}

			xh -= xl;
			yh -= yl;
            
			gr.FillRectangle(new SolidBrush(Color.White), Rectangle.FromLTRB(0, 0, xh, yh));
			foreach(Figure f in listSelectedFigures) {
				f.Draw(gr, 0, 0);
			}

			g.ReleaseHdc(dc);
			g.Dispose();
			gr.Dispose();
			return mf;
		}

		private MainForm GetParent(){
			return ParentForm as MainForm;
		}

		public void DragToGrid(){
			MainForm parent = GetParent();
			if (parent == null) return;

			foreach (Figure figure in listFigures){
				figure.DragToGrid ( parent.GridStep , imageSize);
			}
		}


		internal void SetEditedFigurePenWidth(int penWidth) {
			if(editedFigure != null)
				editedFigure.PenWidth = penWidth;
			Invalidate();
		}

		internal void SetEditedFigureLineColor(Color color) {
			if(editedFigure != null)
				editedFigure.LineColor = color;
			Invalidate();
		}

		public void SetEditedFigureBackgroundColor (Color color){
			if(editedFigure != null)
				editedFigure.BackgroundColor = color;
			Invalidate();
		}

		public void SetEditedFigureFont (Font font){
			if(editedFigure != null && editedFigure is Text){
				( (Text) editedFigure ).Font = font;
			}
			Invalidate();
		}

        private void contextMenuStrip1_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            contextMenuStrip1.Show();
        }

        
	}

}
