using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tetris;

namespace Test
{
	/// <summary>
	/// Summary description for UnitTest2
	/// </summary>
	[TestClass]
	public class TestFigures
	{
		private AbstractFigure figure;

		public TestFigures() {
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
		public void TestFigureI() {
			figure = new FigureI();
			var schemePhase1 = new Scheme(new[] { "#", "#", "#", "#" });
			var schemePhase2 = new Scheme(new[] { "####" });

			Assert.AreEqual(schemePhase1, figure.GetScheme());
			Assert.AreEqual(1, figure.GetWidth());
			Assert.AreEqual(4, figure.GetHeight());

			figure.Rotate();
			Assert.AreEqual(schemePhase2, figure.GetScheme());
			Assert.AreEqual(4, figure.GetWidth());
			Assert.AreEqual(1, figure.GetHeight());

			figure.Rotate();
			Assert.AreEqual(schemePhase1, figure.GetScheme());
			Assert.AreEqual(1, figure.GetWidth());
			Assert.AreEqual(4, figure.GetHeight());

			figure.Rotate();
			Assert.AreEqual(schemePhase2, figure.GetScheme());
			Assert.AreEqual(4, figure.GetWidth());
			Assert.AreEqual(1, figure.GetHeight());
		}
		
		[TestMethod]
		public void TestFigureJ() {
			figure = new FigureJ();

			var schemePhase1 = new Scheme(new[] {".#", ".#", "##"} );
			var schemePhase2 = new Scheme(new[] { "#..", "###"} );
			var schemePhase3 = new Scheme(new[] { "##", "#.", "#." } );
			var schemePhase4 = new Scheme(new[] { "###", "..#" } );

			Assert.AreEqual(schemePhase1, figure.GetScheme());
			Assert.AreEqual(2, figure.GetWidth());
			Assert.AreEqual(3, figure.GetHeight());

			figure.Rotate();
			Assert.AreEqual(schemePhase2, figure.GetScheme());
			Assert.AreEqual(3, figure.GetWidth());
			Assert.AreEqual(2, figure.GetHeight());

			figure.Rotate();
			Assert.AreEqual(schemePhase3, figure.GetScheme());
			Assert.AreEqual(2, figure.GetWidth());
			Assert.AreEqual(3, figure.GetHeight());

			figure.Rotate();
			Assert.AreEqual(schemePhase4, figure.GetScheme());
			Assert.AreEqual(3, figure.GetWidth());
			Assert.AreEqual(2, figure.GetHeight());

			figure.Rotate();
			Assert.AreEqual(schemePhase1, figure.GetScheme());
			Assert.AreEqual(2, figure.GetWidth());
			Assert.AreEqual(3, figure.GetHeight());
		}

		[TestMethod]
		public void TestFigureL() {
			figure = new FigureL();

			var schemePhase1 = new Scheme(new[] { "#.", "#.", "##" });
			var schemePhase2 = new Scheme(new[] { "###", "#.." });
			var schemePhase3 = new Scheme(new[] { "##", ".#", ".#" });
			var schemePhase4 = new Scheme(new[] { "..#", "###" });

			Assert.AreEqual(schemePhase1, figure.GetScheme());
			Assert.AreEqual(2, figure.GetWidth());
			Assert.AreEqual(3, figure.GetHeight());

			figure.Rotate();
			Assert.AreEqual(schemePhase2, figure.GetScheme());
			Assert.AreEqual(3, figure.GetWidth());
			Assert.AreEqual(2, figure.GetHeight());

			figure.Rotate();
			Assert.AreEqual(schemePhase3, figure.GetScheme());
			Assert.AreEqual(2, figure.GetWidth());
			Assert.AreEqual(3, figure.GetHeight());

			figure.Rotate();
			Assert.AreEqual(schemePhase4, figure.GetScheme());
			Assert.AreEqual(3, figure.GetWidth());
			Assert.AreEqual(2, figure.GetHeight());

			figure.Rotate();
			Assert.AreEqual(schemePhase1, figure.GetScheme());
			Assert.AreEqual(2, figure.GetWidth());
			Assert.AreEqual(3, figure.GetHeight());
		}

		[TestMethod]
		public void TestFigureO() {
			figure = new FigureO();

			var schemePhase1 = new Scheme(new[] { "##", "##"});
			var schemePhase2 = new Scheme(new[] { "##", "##" });
			var schemePhase3 = new Scheme(new[] { "##", "##" });

			Assert.AreEqual(schemePhase1, figure.GetScheme());
			Assert.AreEqual(2, figure.GetWidth());
			Assert.AreEqual(2, figure.GetHeight());

			figure.Rotate();
			Assert.AreEqual(schemePhase2, figure.GetScheme());
			Assert.AreEqual(2, figure.GetWidth());
			Assert.AreEqual(2, figure.GetHeight());

			figure.Rotate();
			Assert.AreEqual(schemePhase3, figure.GetScheme());
			Assert.AreEqual(2, figure.GetWidth());
			Assert.AreEqual(2, figure.GetHeight());

		}

		[TestMethod]
		public void TestFigureS() {
			figure = new FigureS();
			var schemePhase1 = new Scheme(new[] { "#.", "##", ".#"});
			var schemePhase2 = new Scheme(new[] { ".##", "##." });

			Assert.AreEqual(schemePhase1, figure.GetScheme());
			Assert.AreEqual(2, figure.GetWidth());
			Assert.AreEqual(3, figure.GetHeight());

			figure.Rotate();
			Assert.AreEqual(schemePhase2, figure.GetScheme());
			Assert.AreEqual(3, figure.GetWidth());
			Assert.AreEqual(2, figure.GetHeight());

			figure.Rotate();
			Assert.AreEqual(schemePhase1, figure.GetScheme());
			Assert.AreEqual(2, figure.GetWidth());
			Assert.AreEqual(3, figure.GetHeight());

			figure.Rotate();
			Assert.AreEqual(schemePhase2, figure.GetScheme());
			Assert.AreEqual(3, figure.GetWidth());
			Assert.AreEqual(2, figure.GetHeight());
		}

		[TestMethod]
		public void TestFigureZ() {
			figure = new FigureZ();
			var schemePhase1 = new Scheme(new[] { ".#", "##", "#." });
			var schemePhase2 = new Scheme(new[] { "##.", ".##" });

			Assert.AreEqual(schemePhase1, figure.GetScheme());
			Assert.AreEqual(2, figure.GetWidth());
			Assert.AreEqual(3, figure.GetHeight());

			figure.Rotate();
			Assert.AreEqual(schemePhase2, figure.GetScheme());
			Assert.AreEqual(3, figure.GetWidth());
			Assert.AreEqual(2, figure.GetHeight());

			figure.Rotate();
			Assert.AreEqual(schemePhase1, figure.GetScheme());
			Assert.AreEqual(2, figure.GetWidth());
			Assert.AreEqual(3, figure.GetHeight());

			figure.Rotate();
			Assert.AreEqual(schemePhase2, figure.GetScheme());
			Assert.AreEqual(3, figure.GetWidth());
			Assert.AreEqual(2, figure.GetHeight());
		}

		[TestMethod]
		public void TestFigureT() {
			figure = new FigureT();
			var schemePhase1 = new Scheme(new[] { "#.", "##", "#." });
			var schemePhase2 = new Scheme(new[] { "###", ".#." });

			Assert.AreEqual(schemePhase1, figure.GetScheme());
			Assert.AreEqual(2, figure.GetWidth());
			Assert.AreEqual(3, figure.GetHeight());

			figure.Rotate();
			Assert.AreEqual(schemePhase2, figure.GetScheme());
			Assert.AreEqual(3, figure.GetWidth());
			Assert.AreEqual(2, figure.GetHeight());

			figure.Rotate();
			Assert.AreEqual(schemePhase1, figure.GetScheme());
			Assert.AreEqual(2, figure.GetWidth());
			Assert.AreEqual(3, figure.GetHeight());

			figure.Rotate();
			Assert.AreEqual(schemePhase2, figure.GetScheme());
			Assert.AreEqual(3, figure.GetWidth());
			Assert.AreEqual(2, figure.GetHeight());
		}

	}

}
