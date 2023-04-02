using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Forms;
using PaymentManagerController;
using PaymentManagerModel;

namespace PaymentManagerView
{
	class PaymentSumCell : DataGridViewTextBoxCell
	{
		public PaymentSumCell(){
			Style.Format = "F2";
		}

		public override Type ValueType {
			get {
				return typeof(Decimal);
			}
		}
		protected override object GetValue(int rowIndex) {
			return (decimal)DataGridView.Rows[rowIndex].Cells[PaymentColumns.Amount.ToString()].Value 
				* (decimal)DataGridView.Rows[rowIndex].Cells[PaymentColumns.Rate.ToString()].Value;
		}

	}

}
