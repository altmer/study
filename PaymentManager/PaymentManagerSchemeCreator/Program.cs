using System;
using System.IO;
using System.Data;

namespace PaymentManagerSchemeCreator
{
	class Program
	{
		static void Main() {
			DataSet paymentRecord = new DataSet("PaymentSet");

			DataTable customers = paymentRecord.Tables.Add ( "Customers" );
			DataColumn customersId = customers.Columns.Add ( "Id", typeof (Guid) );
			customers.Columns.Add ( "FirstName", typeof (String) );
			customers.Columns.Add("LastName", typeof(String));
			customers.Columns.Add("MiddleName", typeof(String));
			customers.Columns.Add("Number", typeof(String));
			customers.PrimaryKey = new []{customersId};

			DataTable payments = paymentRecord.Tables.Add ( "Payments" );
			DataColumn paymentsId = payments.Columns.Add ( "Id", typeof (Guid) );
			payments.Columns.Add ( "PaymentDate", typeof (DateTime) );
			payments.Columns.Add ( "MonthPaid", typeof (DateTime) );
			payments.Columns.Add ( "Amount", typeof (decimal) );
			payments.Columns.Add ( "Rate", typeof (decimal) );
			DataColumn custIdForeign = payments.Columns.Add ( "CustomerId", typeof (Guid) );
			payments.PrimaryKey = new [] { paymentsId };

			paymentRecord.Relations.Add ( "CustomerIdForeignKey", customersId, custIdForeign );

			StreamWriter writer = new StreamWriter ( "PaymentSet.xsd" );
			paymentRecord.WriteXmlSchema ( writer );
			writer.Close();
		}
	}
}
