using System;
using System.Collections.Generic;
using PaymentManagerController;
using PaymentManagerModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ModelTest
{
	/// <summary>
	/// Summary description for FilteringTest
	/// </summary>
	[TestClass]
	public class FilteringTest
	{

		private TestContext testContextInstance;

		/// <summary>
		///Gets or sets the test context which provides
		///information about and functionality for the current test run.
		///</summary>
		public TestContext TestContext {
			get {
				return testContextInstance;
			}
			set {
				testContextInstance = value;
			}
		}

		#region Additional test attributes
		//
		// You can use the following additional attributes as you write your tests:
		//
		// Use ClassInitialize to run code before running the first test in the class
		// [ClassInitialize()]
		// public static void MyClassInitialize(TestContext testContext) { }
		//
		// Use ClassCleanup to run code after all tests in a class have run
		// [ClassCleanup()]
		// public static void MyClassCleanup() { }
		//
		// Use TestInitialize to run code before running each test 
		// [TestInitialize()]
		// public void MyTestInitialize() { }
		//
		// Use TestCleanup to run code after each test has run
		// [TestCleanup()]
		// public void MyTestCleanup() { }
		//
		#endregion

		[TestMethod]
		public void PaymentFilterTest() {
			PersistenceManager pm = PersistenceManager.GetInstance();
			pm.DeleteAll();
			PaymentController control = PaymentController.GetInstance();

			Customer cust1 = new Customer{	FirstName = "ffd", 
											LastName = "dsf", 
											MiddleName = "ds", 
											Number = "1"
			};

			Customer cust2 = new Customer {
				FirstName = "dsaf",
				LastName = "fsdg",
				MiddleName = "rewr",
				Number = "2"
			};

			Customer cust3 = new Customer {
				FirstName = "gjnhgk",
				LastName = "yiuio",
				MiddleName = "m,.,m.",
				Number = "3"
			};

			Assert.IsTrue(pm.AddCustomer(cust1));
			Assert.IsTrue(pm.AddCustomer(cust2));
			Assert.IsTrue(pm.AddCustomer(cust3));

			Payment p1 = new Payment{
			    Amount = (decimal) 1.34,
			    CustomerID = cust1.ID,
			    MonthPaid = new DateTime(2000, 2, 1),
				PaymentDate = new DateTime(2009, 2, 1),
			    Rate = (decimal) 10.435
			};

			Payment p2 = new Payment {
				Amount = (decimal)2.435,
				CustomerID = cust2.ID,
				MonthPaid = new DateTime(2000, 3, 1),
				PaymentDate = new DateTime(2008, 2, 1),
				Rate = (decimal)9.5445435
			};

			Payment p3 = new Payment { 
				Amount = (decimal)3.545,
				CustomerID = cust3.ID,
				MonthPaid = new DateTime(2000, 4, 1),
				PaymentDate = new DateTime(2007, 2, 1),
				Rate = (decimal)8.4345435
			};

			Payment p4 = new Payment {
				Amount = (decimal)4.3235,
				CustomerID = cust1.ID,
				MonthPaid = new DateTime(2001, 12, 1),
				PaymentDate = new DateTime(2006, 2, 1),
				Rate = (decimal)7.321432
			};

			Payment p5 = new Payment {
				Amount = (decimal)5.3243,
				CustomerID = cust2.ID,
				MonthPaid = new DateTime(2002, 1, 1),
				PaymentDate = new DateTime(2005, 2, 1),
				Rate = (decimal)6.432435
			};

			Payment p6 = new Payment {
				Amount = (decimal)6.43534,
				CustomerID = cust3.ID,
				MonthPaid = new DateTime(2002, 3, 1),
				PaymentDate = new DateTime(2004, 2, 1),
				Rate = (decimal)5.3232135
			};

			Payment p7 = new Payment {
				Amount = (decimal)7.4325,
				CustomerID = cust1.ID,
				MonthPaid = new DateTime(2002, 4, 1),
				PaymentDate = new DateTime(2003, 2, 1),
				Rate = (decimal)4.3214325
			};

			Payment p8 = new Payment {
				Amount = (decimal)8.432543,
				CustomerID = cust2.ID,
				MonthPaid = new DateTime(2002, 11, 1),
				PaymentDate = new DateTime(2002, 2, 1),
				Rate = (decimal)3.43435
			};

			Payment p9 = new Payment {
				Amount = (decimal)9.3436546545,
				CustomerID = cust3.ID,
				MonthPaid = new DateTime(2010, 1, 1),
				PaymentDate = new DateTime(2001, 2, 1),
				Rate = (decimal)2.44353435
			};

			Payment p10 = new Payment {
				Amount = (decimal)10.43545,
				CustomerID = cust1.ID,
				MonthPaid = new DateTime(2010, 12, 1),
				PaymentDate = new DateTime(2000, 2, 1),
				Rate = (decimal)1.435
			};

			Assert.IsTrue(pm.AddPayment(p1));
			Assert.IsTrue(pm.AddPayment(p2));
			Assert.IsTrue(pm.AddPayment(p3));
			Assert.IsTrue(pm.AddPayment(p4));
			Assert.IsTrue(pm.AddPayment(p5));
			Assert.IsTrue(pm.AddPayment(p6));
			Assert.IsTrue(pm.AddPayment(p7));
			Assert.IsTrue(pm.AddPayment(p8));
			Assert.IsTrue(pm.AddPayment(p9));
			Assert.IsTrue(pm.AddPayment(p10));

			PaymentFilter filter = new PaymentFilter();
			filter.From.Amount = 1;
			filter.To.Amount = 3;
			
			control.ApplyFilter (filter);
			List <Payment> list = control.GetFilteredPayments();

			Assert.AreEqual ( 2,  list.Count);
			Assert.IsTrue ( list.Contains ( p1 ) );
			Assert.IsTrue ( list.Contains ( p2 ) );

			filter.From.Amount = (decimal) 1.34;
			filter.To.Amount = (decimal) 1.34;

			control.ApplyFilter ( filter );
			list = control.GetFilteredPayments();

			Assert.AreEqual(1, list.Count);
			Assert.IsTrue(list[0].Equals ( p1 ));

			filter = new PaymentFilter();

			filter.From.CustomerID = cust1.ID;
			filter.To.CustomerID = cust1.ID;

			control.ApplyFilter ( filter );
			list = control.GetFilteredPayments();

			Assert.AreEqual(4, list.Count);
			Assert.IsTrue(list.Contains(p1));
			Assert.IsTrue(list.Contains(p4));
			Assert.IsTrue(list.Contains(p10));
			Assert.IsTrue(list.Contains(p7));

			filter.From.CustomerID = cust2.ID;
			filter.To.CustomerID = cust2.ID;

			control.ApplyFilter(filter);
			list = control.GetFilteredPayments();

			Assert.AreEqual(3, list.Count);
			Assert.IsTrue(list.Contains(p2));
			Assert.IsTrue(list.Contains(p5));
            Assert.IsTrue(list.Contains(p8));
	
			filter = new PaymentFilter();

			filter.From.MonthPaid = new DateTime(2002, 1, 1);
			filter.To.MonthPaid = new DateTime(2010, 2, 1);

			control.ApplyFilter(filter);
			list = control.GetFilteredPayments();

			Assert.AreEqual(5, list.Count);
			Assert.IsTrue(list.Contains(p5));
			Assert.IsTrue(list.Contains(p6));
			Assert.IsTrue(list.Contains(p7));
			Assert.IsTrue(list.Contains(p8));
			Assert.IsTrue(list.Contains(p9));

			filter.From.MonthPaid = new DateTime(2000, 2, 1);
			filter.To.MonthPaid = new DateTime(2000, 2, 1);

			control.ApplyFilter(filter);
			list = control.GetFilteredPayments();

			Assert.AreEqual(1, list.Count);
			Assert.IsTrue(list[0].Equals ( p1 ));

			filter = new PaymentFilter();

			filter.From.PaymentDate = new DateTime(2005, 12, 1);
			filter.To.PaymentDate = new DateTime(2009, 1, 1);

			control.ApplyFilter(filter);
			list = control.GetFilteredPayments();

			Assert.AreEqual(3, list.Count);
			Assert.IsTrue(list.Contains(p2));
			Assert.IsTrue(list.Contains(p3));
			Assert.IsTrue(list.Contains(p4));

			filter.From.PaymentDate = new DateTime(2002, 2, 1);
			filter.To.PaymentDate = new DateTime(2002, 2, 1);

			control.ApplyFilter(filter);
			list = control.GetFilteredPayments();

			Assert.AreEqual(1, list.Count);
			Assert.IsTrue(list.Contains(p8));

			filter = new PaymentFilter();

			filter.From.Rate = 5;
			filter.To.Rate = (decimal)9.53;

			control.ApplyFilter(filter);
			list = control.GetFilteredPayments();

			Assert.AreEqual(4, list.Count);
			Assert.IsTrue(list.Contains(p3));
			Assert.IsTrue(list.Contains(p6));
			Assert.IsTrue(list.Contains(p4));
			Assert.IsTrue(list.Contains(p5));

			filter.From.Rate = (decimal)1.435;
			filter.To.Rate = (decimal)1.435;

			control.ApplyFilter(filter);
			list = control.GetFilteredPayments();

			Assert.AreEqual(1, list.Count);
			Assert.IsTrue(list.Contains(p10));

			filter = new PaymentFilter();
            
			filter.SumFrom = (decimal) 29.9;
			filter.SumTo = (decimal)39.9;

			control.ApplyFilter(filter);
			list = control.GetFilteredPayments();

			Assert.AreEqual(5, list.Count);
			Assert.IsTrue(list.Contains(p3));
			Assert.IsTrue(list.Contains(p4));
			Assert.IsTrue(list.Contains(p5));
			Assert.IsTrue(list.Contains(p6));
			Assert.IsTrue(list.Contains(p7));

			filter = new PaymentFilter();

			filter.From.Amount = 1;
			filter.To.Amount = 10;
			filter.From.MonthPaid =  new DateTime(2000, 3, 1);
			filter.To.MonthPaid = new DateTime(2012, 3, 1);
			filter.From.PaymentDate =  new DateTime(2002, 1, 1);
			filter.To.PaymentDate = new DateTime(2008, 1, 1);
			filter.From.Rate = 0;
			filter.To.Rate = 8;
			filter.From.CustomerID = cust1.ID;
			filter.To.CustomerID = cust1.ID;

			control.ApplyFilter(filter);
			list = control.GetFilteredPayments();

			Assert.AreEqual(2, list.Count);
			Assert.IsTrue(list.Contains(p4));
			Assert.IsTrue(list.Contains(p7));
		}

		[TestMethod]
		public void CustomerFilterTest(){
			PersistenceManager pm = PersistenceManager.GetInstance();
			pm.DeleteAll();
			PaymentController control = PaymentController.GetInstance();

			Customer cust1 = new Customer {
				FirstName = "abcd",
				LastName = "abcd",
				MiddleName = "abcd",
				Number = "A341B"
			};

			Customer cust2 = new Customer {
				FirstName = "kiyre",
				LastName = "fjjruei",
				MiddleName = "kfuenv",
				Number = "754438"
			};

			Customer cust3 = new Customer {
				FirstName = "fdsabcdgj",
				LastName = "gfdhabcdfgg",
				MiddleName = "ghfabcddsa",
				Number = "1A341B"
			};

			Customer cust4 = new Customer {
				FirstName = "парпнкг",
				LastName = "аопку",
				MiddleName = "памвопшк",
				Number = "4авп"
			};

			Customer cust5 = new Customer {
				FirstName = "abcdefg",
				LastName = "abcdefg",
				MiddleName = "abcdefg",
				Number = "abcdefg"
			};

			Assert.IsTrue(pm.AddCustomer(cust1));
			Assert.IsTrue(pm.AddCustomer(cust2));
			Assert.IsTrue(pm.AddCustomer(cust3));
			Assert.IsTrue(pm.AddCustomer(cust4));
			Assert.IsTrue(pm.AddCustomer(cust5));

			CustomerFilter filter = new CustomerFilter();

			filter.Value.FirstName = "kiyre";

			control.ApplyFilter ( filter );
			List <Customer> list = control.GetFilteredCustomers();

			Assert.AreEqual ( 1, list.Count );
			Assert.IsTrue ( list.Contains ( cust2 ) );

			filter.Value.FirstName = "парпнкг";

			control.ApplyFilter(filter);
			list = control.GetFilteredCustomers();

			Assert.AreEqual(1, list.Count);
			Assert.IsTrue(list.Contains(cust4));

			filter.Value.FirstName = "abcd";

			control.ApplyFilter(filter);
			list = control.GetFilteredCustomers();

			Assert.AreEqual(3, list.Count);
			Assert.IsTrue(list.Contains(cust1));
			Assert.IsTrue(list.Contains(cust3));
			Assert.IsTrue(list.Contains(cust5));

			filter = new CustomerFilter();

			filter.Value.MiddleName = "ghfabcddsa";

			control.ApplyFilter(filter);
			list = control.GetFilteredCustomers();

			Assert.AreEqual(1, list.Count);
			Assert.IsTrue(list.Contains(cust3));

			filter.Value.MiddleName = "памвопшк";

			control.ApplyFilter(filter);
			list = control.GetFilteredCustomers();

			Assert.AreEqual(1, list.Count);
			Assert.IsTrue(list.Contains(cust4));

			filter.Value.MiddleName = "abcd";

			control.ApplyFilter(filter);
			list = control.GetFilteredCustomers();

			Assert.AreEqual(3, list.Count);
			Assert.IsTrue(list.Contains(cust1));
			Assert.IsTrue(list.Contains(cust3));
			Assert.IsTrue(list.Contains(cust5));

			filter = new CustomerFilter();

			filter.Value.LastName = "аопку";

			control.ApplyFilter(filter);
			list = control.GetFilteredCustomers();

			Assert.AreEqual(1, list.Count);
			Assert.IsTrue(list.Contains(cust4));

			filter.Value.LastName = "abcd";

			control.ApplyFilter(filter);
			list = control.GetFilteredCustomers();

			Assert.AreEqual(3, list.Count);
			Assert.IsTrue(list.Contains(cust1));
			Assert.IsTrue(list.Contains(cust3));
			Assert.IsTrue(list.Contains(cust5));

			filter = new CustomerFilter();

			filter.Value.Number = "4авп";

			control.ApplyFilter(filter);
			list = control.GetFilteredCustomers();

			Assert.AreEqual(1, list.Count);
			Assert.IsTrue(list.Contains(cust4));

			filter.Value.Number = "A341B";

			control.ApplyFilter(filter);
			list = control.GetFilteredCustomers();

			Assert.AreEqual(2, list.Count);
			Assert.IsTrue(list.Contains(cust1));
			Assert.IsTrue(list.Contains(cust3));

			filter = new CustomerFilter();

			filter.Value.FirstName = "ab";
			filter.Value.LastName = "fg";
			filter.Value.MiddleName = "abcd";

			control.ApplyFilter(filter);
			list = control.GetFilteredCustomers();

			Assert.AreEqual(2, list.Count);
			Assert.IsTrue(list.Contains(cust3));
			Assert.IsTrue(list.Contains(cust5));

			filter.Value.Number = "1";

			control.ApplyFilter(filter);
			list = control.GetFilteredCustomers();

			Assert.AreEqual(1, list.Count);
			Assert.IsTrue(list.Contains(cust3));
		}

		[TestMethod]
		public void DebtCustomerFilterTest(){
			PersistenceManager pm = PersistenceManager.GetInstance();
			pm.DeleteAll();
			PaymentController control = PaymentController.GetInstance();
            
			Customer cust1 = new Customer {
				FirstName = "A",
				LastName = "A",
				MiddleName = "A",
				Number = "1"
			};

			Customer cust2 = new Customer {
				FirstName = "B",
				LastName = "B",
				MiddleName = "B",
				Number = "2"
			};

			Customer cust3 = new Customer {
				FirstName = "C",
				LastName = "C",
				MiddleName = "C",
				Number = "3"
			};

			Customer cust4 = new Customer {
				FirstName = "D",
				LastName = "D",
				MiddleName = "D",
				Number = "4"
			};

			Customer cust5 = new Customer {
				FirstName = "E",
				LastName = "E",
				MiddleName = "E",
				Number = "5"
			};

			Assert.IsTrue(pm.AddCustomer(cust1));
			Assert.IsTrue(pm.AddCustomer(cust2));
			Assert.IsTrue(pm.AddCustomer(cust3));
			Assert.IsTrue(pm.AddCustomer(cust4));
			Assert.IsTrue(pm.AddCustomer(cust5));

			Payment p1 = new Payment {
				Amount = (decimal)1.34,
				CustomerID = cust1.ID,
				MonthPaid = new DateTime(2000, 2, 1),
				PaymentDate = new DateTime(2009, 2, 1),
				Rate = (decimal)10.435
			};
			Payment p2 = new Payment {
				Amount = (decimal)1.34,
				CustomerID = cust2.ID,
				MonthPaid = new DateTime(2000, 2, 1),
				PaymentDate = new DateTime(2009, 2, 1),
				Rate = (decimal)10.435
			};
			Payment p3 = new Payment {
				Amount = (decimal)1.34,
				CustomerID = cust3.ID,
				MonthPaid = new DateTime(2000, 2, 1),
				PaymentDate = new DateTime(2009, 2, 1),
				Rate = (decimal)10.435
			};
			Payment p4 = new Payment {
				Amount = (decimal)1.34,
				CustomerID = cust4.ID,
				MonthPaid = new DateTime(2000, 2, 1),
				PaymentDate = new DateTime(2009, 2, 1),
				Rate = (decimal)10.435
			};
			Payment p5 = new Payment {
				Amount = (decimal)1.34,
				CustomerID = cust5.ID,
				MonthPaid = new DateTime(2000, 2, 1),
				PaymentDate = new DateTime(2009, 2, 1),
				Rate = (decimal)10.435
			};
			Payment p6 = new Payment {
				Amount = (decimal)1.34,
				CustomerID = cust1.ID,
				MonthPaid = new DateTime(1800, 7, 1),
				PaymentDate = new DateTime(2009, 2, 1),
				Rate = (decimal)10.435
			};
			Payment p7 = new Payment {
				Amount = (decimal)1.34,
				CustomerID = cust2.ID,
				MonthPaid = new DateTime(1800, 7, 1),
				PaymentDate = new DateTime(2009, 2, 1),
				Rate = (decimal)10.435
			};
			Payment p8 = new Payment {
				Amount = (decimal)1.34,
				CustomerID = cust3.ID,
				MonthPaid = new DateTime(1800, 7, 1),
				PaymentDate = new DateTime(2009, 2, 1),
				Rate = (decimal)10.435
			};
			Payment p9 = new Payment {
				Amount = (decimal)1.34,
				CustomerID = cust5.ID,
				MonthPaid = new DateTime(1800, 7, 1),
				PaymentDate = new DateTime(2009, 2, 1),
				Rate = (decimal)10.435
			};
			Payment p10 = new Payment {
				Amount = (decimal)1.34,
				CustomerID = cust4.ID,
				MonthPaid = new DateTime(1900, 8, 1),
				PaymentDate = new DateTime(2009, 2, 1),
				Rate = (decimal)10.435
			};
			Payment p11 = new Payment {
				Amount = (decimal)1.34,
				CustomerID = cust2.ID,
				MonthPaid = new DateTime(1950, 12, 1),
				PaymentDate = new DateTime(2009, 2, 1),
				Rate = (decimal)10.435
			};
			Payment p12 = new Payment {
				Amount = (decimal)1.34,
				CustomerID = cust3.ID,
				MonthPaid = new DateTime(1950, 12, 1),
				PaymentDate = new DateTime(2009, 2, 1),
				Rate = (decimal)10.435
			};

			Assert.IsTrue(pm.AddPayment(p1));
			Assert.IsTrue(pm.AddPayment(p2));
			Assert.IsTrue(pm.AddPayment(p3));
			Assert.IsTrue(pm.AddPayment(p4));
			Assert.IsTrue(pm.AddPayment(p5));
			Assert.IsTrue(pm.AddPayment(p6));
			Assert.IsTrue(pm.AddPayment(p7));
			Assert.IsTrue(pm.AddPayment(p8));
			Assert.IsTrue(pm.AddPayment(p9));
			Assert.IsTrue(pm.AddPayment(p10));
			Assert.IsTrue(pm.AddPayment(p11));
			Assert.IsTrue(pm.AddPayment(p12));

			control.ApplyDebtCustomersFilter ( new DateTime(2000, 2, 1) );
			List <Customer> list = control.GetFilteredCustomers();

			Assert.AreEqual(0, list.Count);

			control.ApplyDebtCustomersFilter(new DateTime(2010, 2, 1));
			list = control.GetFilteredCustomers();

			Assert.AreEqual(5, list.Count);
			Assert.IsTrue(list.Contains(cust1));
			Assert.IsTrue(list.Contains(cust2));
			Assert.IsTrue(list.Contains(cust3));
			Assert.IsTrue(list.Contains(cust4));
			Assert.IsTrue(list.Contains(cust5));

			control.ApplyDebtCustomersFilter(new DateTime(1800, 7, 1));
			list = control.GetFilteredCustomers();

			Assert.AreEqual(1, list.Count);
			Assert.IsTrue(list.Contains(cust4));

			control.ApplyDebtCustomersFilter(new DateTime(1900, 8, 1));
			list = control.GetFilteredCustomers();

			Assert.AreEqual(4, list.Count);
			Assert.IsTrue(list.Contains(cust1));
			Assert.IsTrue(list.Contains(cust2));
			Assert.IsTrue(list.Contains(cust3));
			Assert.IsTrue(list.Contains(cust5));

			control.ApplyDebtCustomersFilter(new DateTime(1950, 12, 1));
			list = control.GetFilteredCustomers();

			Assert.AreEqual(3, list.Count);
			Assert.IsTrue(list.Contains(cust1));
			Assert.IsTrue(list.Contains(cust4));
			Assert.IsTrue(list.Contains(cust5));
		}

	}
}
