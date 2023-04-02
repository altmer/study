using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace Kamikaze
{
	public enum Figures { Rectangle, Ellipse, Line, Curve, Text };

	public partial class MainForm : Form
	{
		public Figures currentFigure = Figures.Rectangle;
		private Color colorLine = Color.Black;
		private Color colorBackground = Color.White;
		private int penWidth = 1;
		private Size imageSize = new Size(800, 600);
		private Font font;
		private int gridStep = 10;

		public Color ColorLine {
			get {
				return colorLine;
			}
		}

		public Color ColorBackground {
			get {
				return colorBackground;
			}
		}

		public int PenWidth {
			get {
				return penWidth;
			}
		}

        public Font Font{
            get{
                return font;
            }
        }

		public bool SelectMode{
			get{
				return buttonSelectModeMenu.Checked;
			}
		}

		public bool Fill{
			get{
				return buttonFillMenu.Checked;
			}
		}

		public bool Grid{
			get{
				return buttonGridMenu.Checked;
			}
		}

		public int GridStep{
			get{
				return gridStep;
			}
		}

		public bool GridAutoDrag{
			get{
				return buttonDragToGridFlagMenu.Checked;
			}
		}

		public MainForm() {
			InitializeComponent();

			ImageSizeStatus.Text = imageSize.Width + "," + imageSize.Height;

			ColorBackgroundStatus.BackColor = colorBackground;
			ColorLineStatus.BackColor = colorLine;

			WidhtStatus.Text = penWidth.ToString();
			font = new Font ( new FontFamily ( "Times New Roman" ), 12 );
		}

		private void ButtonNew_Click(object sender, EventArgs e) {
			Form frm = new MdiChild(imageSize);

			frm.MdiParent = this;
			frm.Text = "Picture " + MdiChildren.Length;
			frm.Show();

			EnableSaveAndBuffer ( true );
		}

		public void RefreshMenuOnClose() {
			EnableSaveAndBuffer(MdiChildren.Length > 1);
		}

		public void EnableCopyAndCut(bool flag) {
			buttonCopyMetafileMenu.Enabled = buttonCopyOwnFormatMenu.Enabled = buttonCutMenu.Enabled = flag;
		}

		public void EnablePaste(bool flag) {
			buttonPasteMenu.Enabled = flag;
		}

		private void ButtonSave_Click(object sender, EventArgs e) {
			MdiChild child = getActiveChild();
			if (child == null) return;

			child.Save(Environment.CurrentDirectory + "\\"+ActiveMdiChild.Text + ".kam");
		}

		private void ButtonOpen_Click(object sender, EventArgs e) {
			OpenFileDialog dial = new OpenFileDialog();
			dial.InitialDirectory = Environment.CurrentDirectory;
			dial.CheckFileExists = true;
			dial.Filter = "Kamikaze files (*.kam)|*.kam";
			if(dial.ShowDialog() == DialogResult.OK) {
				// new frame
				MdiChild frm = new MdiChild();
				frm.MdiParent = this;
				frm.Text = "Picture " + MdiChildren.Length;
				frm.Show();
				EnableSaveAndBuffer ( true );
				// try to open!
				frm.Open(dial.FileName);
				frm.UnselectAllFigures();
			}
		}

		private void ButtonSaveAs_Click(object sender, EventArgs e) {
			SaveFileDialog dial = new SaveFileDialog();
			dial.AddExtension = true;
			dial.Filter = "Kamikaze files (*.kam)|*.kam";
			dial.InitialDirectory = Environment.CurrentDirectory;
			if(dial.ShowDialog() == DialogResult.OK) {
				if(!dial.FileName.EndsWith(".kam")) {
					MessageBox.Show("Wrong extension!", "Oops!");
					return;
				}
				MdiChild child = getActiveChild();
				if (child == null) return;
				child.Save(dial.FileName);
			}
		}

		private void ButtonPenWidth_Click(object sender, EventArgs e) {
			WidthDialog dial = new WidthDialog();
			MdiChild child = getActiveChild();
			if(dial.ShowDialog(this) == DialogResult.OK) {
				int result = Int32.Parse ( dial.getSelection() );
				if(child != null && child.Editing) {
					child.SetEditedFigurePenWidth ( result );
				}
				else{
					penWidth = result;
					WidhtStatus.Text = penWidth.ToString();
				}
			}
		}

		private void ButtonLineColor_Click(object sender, EventArgs e) {
			ColorDialog dial = new ColorDialog();
			MdiChild child = getActiveChild();
			if(dial.ShowDialog(this) == DialogResult.OK) {
				if(child != null && child.Editing){
					child.SetEditedFigureLineColor (dial.Color);
				}
				else{
					colorLine = dial.Color;
					ColorLineStatus.BackColor = colorLine;
				}
			}
		}

		private void ButtonBackgroundColor_Click(object sender, EventArgs e) {
			ColorDialog dial = new ColorDialog();
			MdiChild child = getActiveChild();
			if(dial.ShowDialog(this) == DialogResult.OK) {
				if(child != null && child.Editing){
					child.SetEditedFigureBackgroundColor ( dial.Color );
				}
				else{
					colorBackground = dial.Color;
					ColorBackgroundStatus.BackColor = colorBackground;
				}
			}
		}

		private void ButtonImageSize_Click(object sender, EventArgs e) {
			SizeDialog dial = new SizeDialog();
			if(dial.ShowDialog() == DialogResult.OK) {
				imageSize = dial.SelectedSize;
				ImageSizeStatus.Text = imageSize.Width + "," + imageSize.Height;
			}
		}

		private void ButtonFont_Click(object sender, EventArgs e) {
			FontDialog dial = new FontDialog();
			MdiChild child = getActiveChild();
			if(dial.ShowDialog() == DialogResult.OK) {
				if(child != null && child.Editing){
					child.SetEditedFigureFont ( dial.Font );
				}
				else{
					font = dial.Font;
				}
			}
		}

		private void UncheckAllFigures(){
			buttonRectangleMenu.Checked = buttonEllipseMenu.Checked = buttonLineMenu.Checked = buttonCurveMenu.Checked = 
				buttonTextMenu.Checked = false;
			buttonRectangleToolBar.Checked = buttonEclipseToolBar.Checked = buttonLineToolBar.Checked = buttonCurveToolBar.Checked = 
				buttonTextToolBar.Checked = false;
		}

		private void ButtonRectangle_Click(object sender, EventArgs e) {
			UncheckAllFigures();
			buttonRectangleMenu.Checked = true;
			buttonRectangleToolBar.Checked = true;
			FigureStatus.Text = "Прямоугольник";
			currentFigure=Figures.Rectangle;
		}

		private void ButtonEllipse_Click(object sender, EventArgs e) {
			UncheckAllFigures();
			buttonEllipseMenu.Checked = true;
			buttonEclipseToolBar.Checked = true;
			FigureStatus.Text = "Эллипс";
			currentFigure=Figures.Ellipse;
		}

		private void ButtonLine_Click(object sender, EventArgs e) {
			UncheckAllFigures(); 
			buttonLineMenu.Checked = true;
			buttonLineToolBar.Checked = true;
			FigureStatus.Text = "Линия";
			currentFigure=Figures.Line;
		}

		private void ButtonCurve_Click(object sender, EventArgs e) {
			UncheckAllFigures();
			buttonCurveMenu.Checked = true;
			buttonCurveToolBar.Checked = true;
			FigureStatus.Text = "Кривая линия";
			currentFigure=Figures.Curve;
		}

        private void buttonTextToolBar_Click(object sender, EventArgs e)
        {
			UncheckAllFigures(); 
			buttonTextMenu.Checked = true;
            buttonTextToolBar.Checked = true;
            FigureStatus.Text = "Текст";
            currentFigure = Figures.Text;

        }

		private void ButtonFill_Click(object sender, EventArgs e) {
			buttonFillToolBar.Checked = !buttonFillToolBar.Checked;
			buttonFillMenu.Checked = !buttonFillMenu.Checked;
		}

		private void ButtonSelectMode_Click(object sender, EventArgs e) {
			buttonSelectModeMenu.Checked = !buttonSelectModeMenu.Checked;
			buttonSelectModeToolBar.Checked = !buttonSelectModeToolBar.Checked;

			if(!SelectMode){
				foreach (Form frm in MdiChildren){
					MdiChild form = frm as MdiChild;
					if(form == null) continue;
					form.UnselectAllFigures();
					form.Invalidate();
				}
			}
		}

		public void SetCoordinatesStatus(int x, int y) {
			CoordinatesStatus.Text = x + ", " + y;
		}

		public void EnableTextStatus(String fontName, String fontSize){
			FontNameStatus.Text = fontName;
			FontSizeStatus.Text = fontSize;
			FontNameLabel.Text = "Шрифт:";
			FontSizeLabel.Text = "Размер:";
		}

		public void DisableTextStatus() {
			FontNameStatus.Text = "";
			FontSizeStatus.Text = "";
			FontNameLabel.Text = "";
			FontSizeLabel.Text = "";
		}

		private void EnableSaveAndBuffer(bool flag) {
			buttonSavaAsMenu.Enabled = buttonSaveMenu.Enabled = buttonSaveToolBar.Enabled  
					= buttonCopyMetafileMenu.Enabled = buttonCopyOwnFormatMenu.Enabled = buttonCutMenu.Enabled = 
						buttonPasteMenu.Enabled = buttonSelectAllMenu.Enabled = flag;
		}

		private void buttonDeleteMenu_Click(object sender, EventArgs e) {
			MdiChild form = getActiveChild();
			if(form == null) return;
			form.Delete();
		}

		private void editMenuOpened(object sender, EventArgs e) {
			MdiChild child = getActiveChild();
			IDataObject data = Clipboard.GetDataObject();

			buttonPasteMenu.Enabled = child != null && data != null &&  data.GetDataPresent ( typeof (MemoryStream) );

			buttonCopyMetafileMenu.Enabled = buttonCopyOwnFormatMenu.Enabled = buttonCutMenu.Enabled =
			                                     child != null && SelectMode && child.HasSelectedFigures();

			buttonSelectAllMenu.Enabled = child != null && SelectMode;

			buttonDragToGridMenu.Enabled = child != null;
		}

		private void buttonCopyOwnFormatMenu_Click(object sender, EventArgs e) {
			MdiChild child = getActiveChild();

			if (child == null) return;

			List <Figure> list = child.GetSelectedFigures();

			BinaryFormatter f = new BinaryFormatter();
			MemoryStream ms = new MemoryStream();

			f.Serialize ( ms, list );

			Clipboard.SetDataObject ( ms, true );
		}

		private void buttonCutMenu_Click(object sender, EventArgs e) {
			MdiChild child = getActiveChild();
			if(child == null) return;

			buttonCopyOwnFormatMenu_Click(sender, e);
			child.Delete();
		}

		private void buttonPasteMenu_Click(object sender, EventArgs e) {
			MdiChild child = getActiveChild();
			if(child == null) return;

			IDataObject data = Clipboard.GetDataObject();
			if(data == null) return;
            if (! data.GetDataPresent ( typeof(MemoryStream) )) return;

			MemoryStream ms = data.GetData ( typeof (MemoryStream) ) as MemoryStream;
			if (ms == null) return;
			BinaryFormatter f = new BinaryFormatter();

			List <Figure> list;
			try{
				list = (List <Figure>)f.Deserialize ( ms );
				// найти левый верхний угол блока
				int xl = Int32.MaxValue;
				int yl = Int32.MaxValue;

				foreach (Figure figure in list){
					figure.Selected = false;
					xl = Math.Min ( xl, figure.GetLocatoin ( 0, 0 ).X );
					yl = Math.Min ( yl, figure.GetLocatoin ( 0, 0 ).Y);
				}

				foreach(Figure figure in list) {
					figure.Move ( -xl, -yl);
				}

				child.AddFigures ( list );

			}catch(Exception ex){
				MessageBox.Show("Ошибка. Причина: " + ex.Message, "Упс!");
			}
		}

		private void buttonSelectAllMenu_Click(object sender, EventArgs e) {
			MdiChild child = getActiveChild();

			if(child == null) return;
			if (!SelectMode) return;

			child.SelectAll();
		}

		private void buttonCopyMetafileMenu_Click(object sender, EventArgs e) {
			MdiChild child = getActiveChild();

			if(child == null) return;

			Metafile mf = child.GetMetafile();

			ClipboardMetafileHelper.PutEnhMetafileOnClipboard ( child.Handle, mf );

		}

		private void buttonGrid_Click(object sender, EventArgs e){
			buttonGridMenu.Checked = !buttonGridMenu.Checked;
			buttonGridToolBar.Checked = !buttonGridToolBar.Checked;

			foreach (Form form in MdiChildren){
				form.Invalidate();
			}

		}

		private void buttonGridStep_Click(object sender, EventArgs e){
			GridStepDialog dial = new GridStepDialog(gridStep);
			if (dial.ShowDialog() == DialogResult.OK){
				gridStep = dial.Value;

				foreach(Form form in MdiChildren) {
					form.Invalidate();
				}
			}
		}

		private MdiChild getActiveChild(){
			return ActiveMdiChild as MdiChild;
		}

		private void buttonDragToGridMenu_Click(object sender, EventArgs e) {
			MdiChild child = getActiveChild();
			if(child == null) return;

			child.DragToGrid();
			child.Invalidate();
		}

		private void buttonDragToGridFlagMenu_Click(object sender, EventArgs e) {
			buttonDragToGridFlagMenu.Checked = !buttonDragToGridFlagMenu.Checked;
		}
	}
}
