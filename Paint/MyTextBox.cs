using System;
using System.Windows.Forms;

namespace Kamikaze
{
	class MyTextBox : TextBox
	{
		private Text text;
		private MainForm mainForm;

		public MyTextBox(Text t, Form par, MainForm form) : base(){
			text = t;
			Parent = par;
			mainForm = form;
		}

		public void Show(int shiftX, int shiftY){
			Multiline = true;
			Location = text.GetLocatoin ( shiftX, shiftY );
			Size = text.GetSize(shiftX, shiftY);
			Font = text.Font;
			Text = text.TextToDraw;
			ForeColor = text.LineColor;
			KeyDown += new KeyEventHandler(OnKeyDown);
			GotFocus += new EventHandler(OnGotFocus);
			LostFocus += new EventHandler(OnLostFocus);
			Visible = true;
			Focus();

		}

		private void OnLostFocus (object sender, EventArgs e){
			mainForm.DisableTextStatus();
		}

		private void OnGotFocus (object sender, EventArgs e){
			mainForm.EnableTextStatus(Font.Name, Font.Size.ToString());
        }

		private void OnKeyDown(object sender, KeyEventArgs e) {
			if(e.KeyCode == Keys.Enter) {
				mainForm.DisableTextStatus();
				text.TextToDraw = Text;
				Visible = false;
                Parent.Invalidate();
				Dispose();
			}
		}

	}
}
