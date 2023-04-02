using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace PaymentManagerView
{
	public partial class CustomerDialog : Form
	{
		public CustomerDialog() {
			InitializeComponent();
		}

		public string LastName{
			get{
				return lastName.Text.Trim();
			}
		}

		public string MiddleName {
			get {
				return middleName.Text.Trim();
			}
		}

		public string FirstName {
			get {
				return firstName.Text.Trim();
			}
		}

		public string Number {
			get {
				return number.Text.Trim();
			}
		}
	}
}
