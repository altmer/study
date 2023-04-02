using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaymentManagerModel;

namespace ModelTest
{
	/// <summary>
	/// Summary description for PaymentTest
	/// </summary>
	[TestClass]
	public class PaymentTest
	{
		public PaymentTest() {
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
		public void TestIdUnique() {
			var guids = new List <Guid>();
			for (int i = 0; i < 1000; ++i){
				var payment = new Payment();
				foreach(var g in guids){
					Assert.AreNotEqual ( g, payment.ID );
				}
				guids.Add ( payment.ID );
			}
		}

		[TestMethod]
		public void TestIdUnbreakable(){
			var payment = new Payment();
			var g = payment.ID;
			g = Guid.Empty;
			Assert.AreNotEqual ( payment.ID, Guid.Empty );
		}

		[TestMethod]
		public void TestPaymentBean(){
			var payment = new Payment{
			                         	Amount = ( (decimal) 3.4658 ),
			                         	Rate = ( (decimal) 23.4546 ),
			                         	MonthPaid = ( new DateTime ( 2010, 1, 1 ) ),
			                         	CustomerID = ( Guid.NewGuid() ),
			                         	PaymentDate = ( new DateTime ( 2456, 12, 3 ) )
			                         };

			Assert.AreEqual((double) payment.Amount, 3.4658, 1e-8);
			Assert.AreEqual((double) payment.Rate, 23.4546, 1e-8);
			Assert.AreEqual(payment.MonthPaid, new DateTime(2010, 1, 1) );
			Assert.AreEqual(payment.PaymentDate, new DateTime(2456, 12, 3));
		}
	}
}
