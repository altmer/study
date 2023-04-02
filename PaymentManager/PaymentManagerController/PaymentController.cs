using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Text;
using System.Windows.Forms;
using PaymentManagerModel;

namespace PaymentManagerController
{
	public class PaymentController
	{
		private static PaymentController instance;

		private readonly PersistenceManager pm;
		private readonly DataView paymentsView;
		private readonly DataView customersView;

		private PaymentController(){
			pm = PersistenceManager.GetInstance();

			paymentsView = pm.GetPaymentsView();
			customersView = pm.GetCustomersView();

			paymentsView.AllowDelete = false;
			paymentsView.AllowEdit = true;
			paymentsView.AllowNew = false;
			customersView.AllowDelete = false;
			customersView.AllowEdit = true;
			customersView.AllowNew = false;
		}

		public static PaymentController GetInstance(){
			if (instance == null)
				instance = new PaymentController();

			return instance;
		}

		public void ApplyFilter(PaymentFilter filter){
			var query = new StringBuilder("");
			CultureInfo US = CultureInfo.GetCultureInfo ( "en-US" );
			
			if (filter.From.Amount != ModelConst.DEFAULT_NUMBER && 
										filter.To.Amount != ModelConst.DEFAULT_NUMBER) {

				if(query.ToString().Length > 0) query.Append(" AND ");
				query.Append (PaymentColumns.Amount + " >= " + filter.From.Amount.ToString(US));
				query.Append ( " AND " );
				query.Append(PaymentColumns.Amount + " <= " + filter.To.Amount.ToString(US));

			}

			if(filter.From.CustomerID != Guid.Empty && filter.To.CustomerID != Guid.Empty) {
				if(query.ToString().Length > 0) query.Append(" AND ");
				query.Append(PaymentColumns.CustomerId + " >= '" + filter.From.CustomerID + "'");
				query.Append(" AND ");
				query.Append(PaymentColumns.CustomerId + " <= '" + filter.To.CustomerID + "'");
			}

			if(filter.From.MonthPaid != ModelConst.DEFAULT_DATE 
										&& filter.To.MonthPaid != ModelConst.DEFAULT_DATE) {
				if(query.ToString().Length > 0) query.Append(" AND ");
				query.Append(PaymentColumns.MonthPaid + " >= '" + filter.From.MonthPaid + "'");
				query.Append(" AND ");
				query.Append(PaymentColumns.MonthPaid + " <= '" + filter.To.MonthPaid + "'");
			}

			if(filter.From.PaymentDate != ModelConst.DEFAULT_DATE 
										&& filter.To.PaymentDate != ModelConst.DEFAULT_DATE) {
				if(query.ToString().Length > 0) query.Append(" AND ");
				query.Append(PaymentColumns.PaymentDate + " >= '" + filter.From.PaymentDate + "'");
				query.Append(" AND ");
				query.Append(PaymentColumns.PaymentDate + " <= '" + filter.To.PaymentDate + "'");
			}

			if(filter.From.Rate != ModelConst.DEFAULT_NUMBER && 
										filter.To.Rate != ModelConst.DEFAULT_NUMBER) {

				if(query.ToString().Length > 0) query.Append(" AND ");
				query.Append(PaymentColumns.Rate + " >= " + filter.From.Rate.ToString(US));
				query.Append(" AND ");
				query.Append(PaymentColumns.Rate + " <= " + filter.To.Rate.ToString(US));

			}

			if (filter.SumFrom != decimal.MinValue && filter.SumTo != decimal.MinValue){
				if(query.ToString().Length > 0) query.Append(" AND ");
				query.Append(PaymentColumns.Rate + " * " + PaymentColumns.Amount + " >= " + filter.SumFrom.ToString(US));
				query.Append(" AND ");
				query.Append(PaymentColumns.Rate + " * " + PaymentColumns.Amount + " <= " + filter.SumTo.ToString(US));
			}

			paymentsView.RowFilter = query.ToString();

		}

		public void ApplyFilter(CustomerFilter filter) {
			var query = new StringBuilder("");

			if (filter.Value.FirstName != null){
				if(query.ToString().Length > 0) query.Append(" AND ");
				query.Append(CustomerColumns.FirstName + " LIKE '*" + filter.Value.FirstName + "*'");
			}

			if(filter.Value.LastName != null) {
				if(query.ToString().Length > 0) query.Append(" AND ");
				query.Append(CustomerColumns.LastName + " LIKE '*" + filter.Value.LastName + "*'");
			}

			if(filter.Value.MiddleName != null) {
				if(query.ToString().Length > 0) query.Append(" AND ");
				query.Append(CustomerColumns.MiddleName + " LIKE '*" + filter.Value.MiddleName + "*'");
			}

			if(filter.Value.Number != null) {
				if(query.ToString().Length > 0) query.Append(" AND ");
				query.Append(CustomerColumns.Number + " LIKE '*" + filter.Value.Number + "*'");
			}

			customersView.RowFilter = query.ToString();
		}

		public void ApplyDebtCustomersFilter(DateTime month){
			customersView.RowFilter = "";
			var query = new StringBuilder(CustomerColumns.Id + " = " + "'Impossible ID!!!'");

			foreach(DataRowView row in customersView){
				DataView payView = row.CreateChildView ( "CustomerIdForeignKey" );
				payView.RowFilter = PaymentColumns.MonthPaid +  " = '" + new DateTime(month.Year,month.Month, 1) + "'";
				if (payView.Count == 0){
					query.Append(" OR ");
					query.Append ( CustomerColumns.Id + " = '" + row [ CustomerColumns.Id.ToString() ]  + "'");
				}
			}

			customersView.RowFilter = query.ToString();
		}
        
		public List<Payment> GetFilteredPayments(){
			var ret = new List <Payment>();
			foreach (DataRowView row in paymentsView){
				ret.Add(new Payment( (Guid)row [ PaymentColumns.Id.ToString() ] ) {
					Amount = (decimal)row [ PaymentColumns.Amount.ToString() ],
					CustomerID = (Guid) row [ PaymentColumns.CustomerId.ToString() ],
					PaymentDate = (DateTime) row [ PaymentColumns.PaymentDate.ToString() ],
					MonthPaid = (DateTime) row [ PaymentColumns.MonthPaid.ToString() ],
					Rate = (decimal) row [ PaymentColumns.Rate.ToString() ]
				} );
			}
			return ret;
		}

		public List<Customer> GetFilteredCustomers() {
			var ret = new List<Customer>();
			foreach(DataRowView row in customersView) {
				ret.Add(new Customer((Guid)row[CustomerColumns.Id.ToString()]) {
					FirstName = (string)row[CustomerColumns.FirstName.ToString()],
					MiddleName = (string)row[CustomerColumns.MiddleName.ToString()],
					LastName = (string)row[CustomerColumns.LastName.ToString()],
					Number = (string)row[CustomerColumns.Number.ToString()],
				});
			}
			return ret;
		}

		public List<Customer> GetAllCustomers() {
			var ret = new List<Customer>();
			foreach(DataRow row in customersView.Table.Rows) {
				ret.Add(new Customer((Guid)row[CustomerColumns.Id.ToString()]) {
					FirstName = (string)row[CustomerColumns.FirstName.ToString()],
					MiddleName = (string)row[CustomerColumns.MiddleName.ToString()],
					LastName = (string)row[CustomerColumns.LastName.ToString()],
					Number = (string)row[CustomerColumns.Number.ToString()],
				});
			}
			return ret;
		}

		public void BindCustomers (BindingSource source){
			source.DataSource = customersView;
		}

		public void BindPayments (BindingSource source){
			source.DataSource = paymentsView;
		}

		public bool AddCustomer(Customer obj){
			if (pm.IsExistCustomerWithNumber ( obj.Number ))
				return false;
			return pm.AddCustomer ( obj );
		}

		public bool AddPayment (Payment obj){
			if (pm.IsExistPaymentForCustomerAndMonth ( obj.CustomerID, obj.MonthPaid ))
				return false;
			return pm.AddPayment ( obj );
		}

		public Customer GetCustomer(Guid id){
			return pm.GetCustomer(id);
		}

		public Payment GetPayment(Guid id){
			return pm.GetPayment(id);
		}

		public bool RemoveCustomer(Guid id) {
			return pm.RemoveCustomer ( id );
		}

		public bool RemovePayment(Guid id) {
			return pm.RemovePayment(id);
		}

		public void Backup(){
			pm.Backup();
		}

		public void AcceptChanges(){
			pm.AcceptChanges();
		}

		public bool IsCustomerUpdateValid(int rowIndex, int columnIndex, object val){

			if(val.Equals(customersView.Table.Rows[rowIndex][columnIndex]))
				return true;

			if (customersView.Table.Columns[columnIndex].ColumnName.Equals ( CustomerColumns.Number.ToString() )){
				return ! pm.IsExistCustomerWithNumber ( val.ToString() );
			}

			return true;
		}

		public bool IsPaymentUpdateValid(int rowIndex, int columnIndex, object val) {

			if(paymentsView.Table.Columns[columnIndex].ColumnName.Equals(PaymentColumns.CustomerId.ToString())) {
				if(val.Equals(paymentsView.Table.Rows[rowIndex][columnIndex]))
					return true;
				return !pm.IsExistPaymentForCustomerAndMonth((Guid)val, (DateTime)paymentsView.Table.Rows[rowIndex][PaymentColumns.MonthPaid.ToString()]);
			}

			if(paymentsView.Table.Columns[columnIndex].ColumnName.Equals(PaymentColumns.MonthPaid.ToString())) {
				DateTime tmp = (DateTime)val;
				tmp = new DateTime(tmp.Year, tmp.Month, 1);
				if(tmp.Equals(paymentsView.Table.Rows[rowIndex][columnIndex]))
					return true;
				return !pm.IsExistPaymentForCustomerAndMonth((Guid)paymentsView.Table.Rows[rowIndex][PaymentColumns.CustomerId.ToString()], 
								tmp);
			}

			return true;
		}
	}
}
