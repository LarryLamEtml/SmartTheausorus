using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Rlc.Cron.Tests
{
	/// <summary>
	/// Summary description for CronExpressionTest
	/// </summary>
	[TestClass]
	public class CronExpressionTest
	{
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
		[ExpectedException(typeof(ArgumentNullException))]
		public void Ctor_ThrowsException_IfMinutesIsNull()
		{
			new CronExpression(null, "*", "*", "*", "*");
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void Ctor_ThrowsException_IfHoursIsNull()
		{
			new CronExpression("*", null, "*", "*", "*");
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void Ctor_ThrowsException_IfDaysIsNull()
		{
			new CronExpression("*", "*", null, "*", "*");
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void Ctor_ThrowsException_IfMonthsIsNull()
		{
			new CronExpression("*", "*", "*", null, "*");
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void Ctor_ThrowsException_IfDaysOfWeekIsNull()
		{
			new CronExpression("*", "*", "*", "*", null);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void Ctor_ThrowsException_IfMinutesIsEmpty()
		{
			new CronExpression(string.Empty, "*", "*", "*", "*");
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void Ctor_ThrowsException_IfHoursIsEmpty()
		{
			new CronExpression("*", string.Empty, "*", "*", "*");
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void Ctor_ThrowsException_IfDaysIsEmpty()
		{
			new CronExpression("*", "*", string.Empty, "*", "*");
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void Ctor_ThrowsException_IfMonthsIsEmpty()
		{
			new CronExpression("*", "*", "*", string.Empty, "*");
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void Ctor_ThrowsException_IfDaysOfWeekIsEmpty()
		{
			new CronExpression("*", "*", "*", "*", string.Empty);
		}
	}
}
