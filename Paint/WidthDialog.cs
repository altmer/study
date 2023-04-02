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
	public partial class WidthDialog : Form
	{
		public WidthDialog() {
			InitializeComponent();
			comboWidth.SelectedIndex=0;
		}
		public string getSelection() {
			return comboWidth.Text;
		}
	}
}
