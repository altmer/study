using System;
using System.Data;

namespace PaymentManagerModel
{
	public class PersistenceManager
	{
		//private const string dataPath = @"D:\MyDocs\Andrey\Study\Programming\Projects\Prof\C#\PaymentManager\TempModel\model.xml";

		private const string dataPath = @".\Data\data.xml";
		private const string backupPath = @".\Backup\";

		private readonly PaymentSet model;

		private static PersistenceManager instance;

		private PersistenceManager(){
			model = new PaymentSet();
			model.ReadXml(dataPath);
		}

		public static PersistenceManager GetInstance(){
			if (instance == null)
				instance = new PersistenceManager();

			return instance;
		}

		public bool AddCustomer (Customer obj){
			try{
				model.Customers.AddCustomersRow ( obj.ID, 
												  obj.FirstName, 
												  obj.LastName, 
												  obj.MiddleName,
				                                  obj.Number );

				Write();
			}catch(Exception){
				return false;
			}
			return true;
		}

		public Customer GetCustomer(Guid id){
			Customer ret;
			try{
				PaymentSet.CustomersRow row = model.Customers.FindById ( id );

				if(row == null) return null;

				ret = new Customer ( row.Id ){
				                             	FirstName = ( row.FirstName ),
				                             	LastName = ( row.LastName ),
				                             	MiddleName = ( row.MiddleName ),
				                             	Number = ( row.Number )
				                             };
			}catch(Exception){
				return null;
			}

			return ret;
		}

		public bool RemoveCustomer(Guid id){
			try{
				PaymentSet.CustomersRow row = model.Customers.FindById ( id );
				if ( row == null ) return false;
				model.Customers.RemoveCustomersRow ( row );
				Write();
			}catch(Exception){
				return false;
			}
			return true;
		}

		public bool AddPayment(Payment obj) {
			try{
				PaymentSet.CustomersRow row = model.Customers.FindById ( obj.CustomerID );
				if ( row == null ) return false;

				model.Payments.AddPaymentsRow ( obj.ID, 
												obj.PaymentDate, 
												obj.MonthPaid, 
												obj.Amount,
				                                obj.Rate, 
												row);

				Write();
			}catch (Exception){
				return false;
			}
			return true;
		}

		public Payment GetPayment(Guid id) {
			Payment ret;
			try{
				PaymentSet.PaymentsRow row = model.Payments.FindById ( id );

				if (row ==null) return null;

				ret = new Payment ( row.Id ){
				                            	Amount = ( row.Amount ),
				                            	CustomerID = ( row.CustomerId ),
				                            	MonthPaid = ( row.MonthPaid ),
				                            	PaymentDate = ( row.PaymentDate ),
				                            	Rate = ( row.Rate )
				                            };
			}catch(Exception){
				return null;
			}

			return ret;
		}

		public bool RemovePayment(Guid id){
			try{
				PaymentSet.PaymentsRow row = model.Payments.FindById ( id );
				if ( row == null ) return false;
				model.Payments.RemovePaymentsRow ( row );
				Write();
			}catch(Exception){
				return false;
			}
			return true;
		}

		public DataView GetCustomersView(){
			return model.Customers.DefaultView;
		}

		public DataView GetPaymentsView(){
			return model.Payments.DefaultView;
		}

		public void DeleteAll(){
			model.Clear();
			Write();
		}

		public void AcceptChanges(){
			model.Customers.AcceptChanges();
			model.Payments.AcceptChanges();
			Write();
		}

		public void RejectChanges(){
			model.Customers.RejectChanges();
			model.Payments.RejectChanges();
		}

		private void Write() {
			model.WriteXml(dataPath);
		}

		public void Backup(){
			model.WriteXml ( backupPath + "data_" + DateTime.Now.ToFileTime()+ ".xml" );
		}

		public bool IsExistCustomerWithNumber(string number){
			return model.Customers.Select ( CustomerColumns.Number + " = '" + number + "'" ).Length > 0;
		}

		public bool IsExistPaymentForCustomerAndMonth(Guid customerID, DateTime monthPaid){
			return model.Payments.Select ( PaymentColumns.CustomerId + " = '" + customerID + "' AND " + PaymentColumns.MonthPaid +
			                               " = '" + monthPaid + "'").Length > 0;
		}

	}
}
