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
	public partial class SizeDialog : Form
	{
		private Size sz = new Size(0, 0);

		public Size SelectedSize {
			get {
				return sz;
			}
		}

		public SizeDialog() {
			InitializeComponent();
		}

		private void checkBox1_CheckedChanged(object sender, EventArgs e) {
			if(checkBox1.Checked) {
				groupBox1.Enabled=false;
				textBox1.Enabled=true;
				textBox2.Enabled=true;
			}
			else {
				groupBox1.Enabled=true;
				textBox1.Enabled=false;
				textBox2.Enabled=false;
			}
		}

		private void textBox2_KeyPress(object sender, KeyPressEventArgs e) {
			if(!Char.IsDigit(e.KeyChar)) {
				e.Handled=true;
			}
		}

		private void button1_Click(object sender, EventArgs e) {
			if(checkBox1.Checked) {
				if(textBox1.Text.Length==0 || textBox2.Text.Length==0) {
					MessageBox.Show("Неправильно введены значения!", "Error");
					sz.Width=800;
					sz.Height=600;
				}
				else {
					sz.Width=Int32.Parse(textBox1.Text);
					sz.Height=Int32.Parse(textBox2.Text);
				}
			}
			else {
				if(radioButton1.Checked) {
					sz.Width=320;
					sz.Height=240;
				}
				else if(radioButton2.Checked) {
					sz.Width=640;
					sz.Height=480;
				}
				else {
					sz.Width=800;
					sz.Height=600;
				}
			}
		}
	}
}
