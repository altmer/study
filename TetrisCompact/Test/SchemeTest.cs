using Tetris;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace Test
{
    /// <summary>
    ///This is a test class for SchemeTest and is intended
    ///to contain all SchemeTest Unit Tests
    ///</summary>
	[TestClass()]
	public class SchemeTest
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


		/// <summary>
		///A test for Equals
		///</summary>
		[TestMethod()]
		public void EqualsTest() {
			string[] modelTarget = {"##", ".#", ".#"};
			string[] modelObj = { "##", ".#", ".#" };
			var target = new Scheme(modelTarget);
			var obj = new Scheme(modelObj);
			Assert.IsTrue( target.Equals(obj) );

			modelTarget = new[] { "#" };
			modelObj = new[] { "#" };
			target = new Scheme(modelTarget);
			obj = new Scheme(modelObj);
			Assert.IsTrue(target.Equals(obj));

			modelTarget = new[] { "#", "#", "#", "#" };
			modelObj = new[] { "#", "#", "#", "#" };
			target = new Scheme(modelTarget);
			obj = new Scheme(modelObj);
			Assert.IsTrue(target.Equals(obj));
		}

		/// <summary>
		///A test for GetWidth
		///</summary>
		[TestMethod()]
		public void GetWidthTest() {
			string[] model = {".#", "##", "#."};
			var target = new Scheme(model);
			Assert.AreEqual(2, target.GetWidth());

			model = new[]{"####"};
			target = new Scheme(model);
			Assert.AreEqual(4, target.GetWidth());

			model = new[] { "#", "#", "#", "#" };
			target = new Scheme(model);
			Assert.AreEqual(1, target.GetWidth());
		}
		/// <summary>
		///A test for GetHeight
		///</summary>
		[TestMethod()]
		public void GetHeightTest() {
			string[] model = { ".#", "##", "#." };
			var target = new Scheme(model);
			Assert.AreEqual(3, target.GetHeight());

			model = new[] { "####" };
			target = new Scheme(model);
			Assert.AreEqual(1, target.GetHeight());

			model = new[] { "#", "#", "#", "#" };
			target = new Scheme(model);
			Assert.AreEqual(4, target.GetHeight());
		}

	}

}
