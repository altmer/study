using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Kamikaze
{
	public partial class GridStepDialog : Form
	{
		public int Value{
			get{
				if (textBox1.Text.Length == 0) return 10;
				int res = Int32.Parse ( textBox1.Text );
				return res == 0 ? 10 : res;
			}
		}

		public GridStepDialog(int gridStep) {
			InitializeComponent();
			textBox1.Text = gridStep.ToString();
		}

		private void textBox1_KeyPress(object sender, KeyPressEventArgs e) {
			if (!Char.IsDigit ( e.KeyChar )){
				e.Handled = true;
			}
		}
	}
}
