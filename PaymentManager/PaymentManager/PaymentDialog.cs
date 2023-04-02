using System;

using System.Windows.Forms;
using PaymentManagerController;
using PaymentManagerModel;

namespace PaymentManagerView
{
	public partial class PaymentDialog : Form
	{
		public PaymentDialog() {
			InitializeComponent();

			customers.Items.AddRange ( PaymentController.GetInstance().GetAllCustomers().ToArray() );
		}

		public PaymentDialog (Customer to) : this(){
			customers.SelectedItem = to;
		}

		public Customer Customer{
			get{
				return customers.SelectedItem as Customer;
			}
		}

		public DateTime PaymentDate{
			get{
				return new DateTime( paymentDate.Value.Year, paymentDate.Value.Month, paymentDate.Value.Day);
			}
		}

		public DateTime MonthPaid{
			get{
				return new DateTime(monthPaid.Value.Year, monthPaid.Value.Month, 1);
			}
		}

		public decimal? Amount{
			get{
				decimal ret;
				if (Decimal.TryParse ( amount.Text, out ret ))
					return ret;
				return null;
			}
		}

		private void monthPaid_Enter(object sender, EventArgs e) {
			monthPaid.Value = new DateTime(monthPaid.Value.Year, monthPaid.Value.Month,1);
		}

		private void monthPaid_KeyPress(object sender, KeyPressEventArgs e) {
			monthPaid.Value = new DateTime(monthPaid.Value.Year, monthPaid.Value.Month, 1);
		}
	}
}
