using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace PaymentManagerView
{
	public partial class RateDialog : Form
	{
		public RateDialog(decimal rate) {
			InitializeComponent();

			textBox1.Text = rate.ToString("F2");
		}

		public decimal? Rate{
			get{
				decimal ret;
				if (Decimal.TryParse ( textBox1.Text, out ret )){
					return ret;
				}
				return null;
			}
		}
	}
}
