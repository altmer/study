using System;
using System.Windows.Forms;

namespace TetrisCompact
{
	public partial class SpeedDialog : Form
	{
		private MainForm _parent;

		public SpeedDialog(MainForm parent) {
			InitializeComponent();

			_parent = parent;
		}

		public int GetChoosenSpeed(){
			if(radioSlow.Checked) {
				return Const.SLOW_SPEED;
			}
			if(radioMedium.Checked) {
				return Const.MEDIUM_SPEED;
			}
			if(radioFast.Checked) {
				return Const.FAST_SPEED;
			}
			throw new ApplicationException();
		}

		private void SpeedDialog_Closed(object sender, EventArgs e) {
			_parent.StartGame ( GetChoosenSpeed() );
		}
	}
}