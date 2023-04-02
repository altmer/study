using System;
using PaymentManagerController;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaymentManagerModel;

namespace ModelTest
{
	/// <summary>
	/// Summary description for PaymentControllerTest
	/// </summary>
	[TestClass]
	public class PaymentControllerTest
	{
		public PaymentControllerTest() {
			//
			// TODO: Add constructor logic here
			//
		}

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
		public void AddCustomerTest() {
			PaymentController control = PaymentController.GetInstance();
			PersistenceManager.GetInstance().DeleteAll();

			Customer cust = new Customer {
				FirstName = ("FirstName1"),
				MiddleName = ("MiddleName1"),
				LastName = ("LastName1"),
				Number = ("1")
			};

			Assert.IsTrue(control.AddCustomer(cust));

			Customer cust1 = new Customer {
				FirstName = ("FirstName2"),
				MiddleName = ("MiddleName2"),
				LastName = ("LastName2"),
				Number = ("2")
			};
			Assert.IsTrue(control.AddCustomer(cust1));

			Customer custReal = control.GetCustomer(cust.ID);
			Assert.IsTrue(cust.Equals(custReal));
			Customer cust1Real = control.GetCustomer(cust1.ID);
			Assert.IsTrue(cust1.Equals(cust1Real));

			Assert.IsFalse(control.AddCustomer(cust));

			Assert.IsNull(control.GetCustomer(Guid.NewGuid()));

			Customer cust3 = new Customer {
				FirstName = ("FirstName3"),
				MiddleName = ("MiddleName3"),
				LastName = ("LastName3"),
				Number = ("3")
			};
			Assert.IsTrue(control.AddCustomer(cust3));

			Assert.IsTrue(control.RemoveCustomer(cust.ID));
			Assert.IsNull(control.GetCustomer(cust.ID));
			Assert.IsFalse(control.RemoveCustomer(cust.ID));

			Customer cust4 = new Customer {
				FirstName = ("FirstName4"),
				MiddleName = ("MiddleName4"),
				LastName = ("LastName4"),
				Number = ("3")
			};

			Assert.IsFalse ( control.AddCustomer ( cust4 ) );
			Assert.IsNull(control.GetCustomer(cust4.ID));

		}

		[TestMethod]
		public void AddPaymentTest() {
			PersistenceManager.GetInstance().DeleteAll();
			PaymentController control = PaymentController.GetInstance();

			Customer sample = new Customer {
				FirstName = ("name"),
				LastName = ("name"),
				MiddleName = ("fdsfg"),
				Number = ("fsdgdfg")
			};

			Assert.IsTrue(control.AddCustomer(sample));

			Payment payment = new Payment {
				Amount = ((decimal)3567.3254),
				CustomerID = (sample.ID),
				MonthPaid = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1),
				PaymentDate = (new DateTime(2009, 12, 31)),
				Rate = ((decimal)3546.435)
			};

			Assert.IsTrue(control.AddPayment(payment));

			Payment paymentSameMonth = new Payment {
				Amount = ((decimal)3567.3254),
				CustomerID = (sample.ID),
				MonthPaid = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1),
				PaymentDate = (new DateTime(2009, 12, 31)),
				Rate = ((decimal)3546.435)
			};

			Assert.IsFalse ( control.AddPayment ( paymentSameMonth ) );

			Payment payReal = control.GetPayment(payment.ID);
			Assert.IsTrue(payReal.Equals(payment));

			payment = new Payment {
				Amount = ((decimal)3567.3254),
				CustomerID = (Guid.NewGuid()),
				MonthPaid = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1),
				PaymentDate = (new DateTime(2009, 12, 31)),
				Rate = ((decimal)3546.435)
			};

			Assert.IsFalse(control.AddPayment(payment));
			Assert.IsFalse(control.AddPayment(payReal));
			Assert.IsNull(control.GetPayment(Guid.NewGuid()));

			Assert.IsTrue(control.RemovePayment(payReal.ID));
			Assert.IsNull(control.GetPayment(payReal.ID));
			Assert.IsFalse(control.RemovePayment(payReal.ID));
		}


	}
}
