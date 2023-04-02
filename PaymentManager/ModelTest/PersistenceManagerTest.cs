using System;
using PaymentManagerModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ModelTest
{
    
    
    /// <summary>
    ///This is a test class for PersistenceManagerTest and is intended
    ///to contain all PersistenceManagerTest Unit Tests
    ///</summary>
	[TestClass()]
	public class PersistenceManagerTest
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
		//You can use the following additional attributes as you write your tests:
		//
		//Use ClassInitialize to run code before running the first test in the class
		//[ClassInitialize()]
		//public static void MyClassInitialize(TestContext testContext)
		//{
		//}
		//
		//Use ClassCleanup to run code after all tests in a class have run
		//[ClassCleanup()]
		//public static void MyClassCleanup()
		//{
		//}
		//
		//Use TestInitialize to run code before running each test
		//[TestInitialize()]
		//public void MyTestInitialize()
		//{
		//}
		//
		//Use TestCleanup to run code after each test has run
		//[TestCleanup()]
		//public void MyTestCleanup()
		//{
		//}
		//
		#endregion


		[TestMethod()]
		public void CustomerTest() {
			PersistenceManager pm = PersistenceManager.GetInstance();
			pm.DeleteAll();

			Customer cust = new Customer{
			                            	FirstName = ( "FirstName1" ),
			                            	MiddleName = ( "MiddleName1" ),
			                            	LastName = ( "LastName1" ),
			                            	Number = ( "1" )
			                            };
			Assert.IsTrue ( pm.AddCustomer ( cust ) );
			Assert.IsTrue ( pm.IsExistCustomerWithNumber ( "1" ) );
			Customer cust1 = new Customer{
			                             	FirstName = ( "FirstName2" ),
			                             	MiddleName = ( "MiddleName2" ),
			                             	LastName = ( "LastName2" ),
			                             	Number = ( "2" )
			                             };
			Assert.IsTrue ( pm.AddCustomer(cust1) );
			Assert.IsTrue ( pm.IsExistCustomerWithNumber ( "2" ) );

			Customer custReal = pm.GetCustomer ( cust.ID );
			Assert.IsTrue ( cust.Equals ( custReal ) );
			Customer cust1Real = pm.GetCustomer(cust1.ID);
			Assert.IsTrue(cust1.Equals(cust1Real));

			Assert.IsFalse ( pm.AddCustomer ( cust ) );

			Assert.IsNull ( pm.GetCustomer ( Guid.NewGuid() ) );

			Customer cust3 = new Customer {
				FirstName = ("FirstName3"),
				MiddleName = ("MiddleName3"),
				LastName = ("LastName3"),
				Number = ("3")
			};
			Assert.IsTrue(pm.AddCustomer(cust3));
			Assert.IsTrue ( pm.IsExistCustomerWithNumber ( "3" ) );
			Assert.IsFalse ( pm.IsExistCustomerWithNumber ( "4" ) );
			Assert.IsFalse ( pm.IsExistCustomerWithNumber ( "vjkdhsfjk" ) );

			Assert.IsTrue ( pm.RemoveCustomer ( cust.ID ) );
			Assert.IsNull ( pm.GetCustomer ( cust.ID ) );
			Assert.IsFalse ( pm.RemoveCustomer ( cust.ID ) );
		}

		[TestMethod()]
		public void PaymentTest(){
			PersistenceManager pm = PersistenceManager.GetInstance();
			pm.DeleteAll();

			Customer sample = new Customer{
			                              	FirstName = ( "name" ),
			                              	LastName = ( "name" ),
			                              	MiddleName = ( "fdsfg" ),
			                              	Number = ( "fsdgdfg" )
			                              };

			Assert.IsTrue ( pm.AddCustomer ( sample ) );

			Payment payment = new Payment{
			                             	Amount = ( (decimal) 3567.3254 ),
			                             	CustomerID = ( sample.ID ),
			                             	MonthPaid = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1 ),
			                             	PaymentDate = ( new DateTime ( 2009, 12, 31 ) ),
			                             	Rate = ( (decimal) 3546.435 )
			                             };

			Assert.IsTrue ( pm.AddPayment ( payment ) );
			Assert.IsTrue ( pm.IsExistPaymentForCustomerAndMonth ( sample.ID,
			                                                       new DateTime ( DateTime.Today.Year, DateTime.Today.Month, 1 ) ) );

			Assert.IsFalse(pm.IsExistPaymentForCustomerAndMonth(sample.ID,
																   new DateTime(1900, DateTime.Today.Month, 1)));

			Payment payReal = pm.GetPayment ( payment.ID );
			Assert.IsTrue ( payReal.Equals ( payment ) );

			payment = new Payment{
			                     	Amount = ( (decimal) 3567.3254 ),
			                     	CustomerID = ( Guid.NewGuid() ),
									MonthPaid = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1),
			                     	PaymentDate = ( new DateTime ( 2009, 12, 31 ) ),
			                     	Rate = ( (decimal) 3546.435 )
			                     };

			Assert.IsFalse ( pm.AddPayment ( payment ) );
			Assert.IsFalse ( pm.AddPayment ( payReal ) );
			Assert.IsNull ( pm.GetPayment ( Guid.NewGuid() ) );

			Assert.IsTrue ( pm.RemovePayment ( payReal.ID ) );
			Assert.IsNull ( pm.GetPayment ( payReal.ID ) );
			Assert.IsFalse ( pm.RemovePayment ( payReal.ID ) );
		}

	}
}
