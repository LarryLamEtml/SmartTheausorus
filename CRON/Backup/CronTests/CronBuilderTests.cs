using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Rlc.Cron.Tests
{
	/// <summary>
	/// Summary description for CronBuilderTests
	/// </summary>
	[TestClass]
	public class CronBuilderTests
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


		#region Minutely Tests

		[TestMethod]
		public void CreateMinutelyTrigger_Returns_Expected_Expression_Default()
		{
			CronExpression expression = CronBuilder.CreateMinutelyTrigger();
			Assert.IsTrue(expression.ToString() == "* * * * *");
		}

		#endregion

		#region Hourly Tests

		[TestMethod]
		public void CreateHourlyTrigger_Returns_Expected_Expression_Default()
		{
			CronExpression expression = CronBuilder.CreateHourlyTrigger();
			Assert.IsTrue(expression.ToString() == "0 * * * *");
		}

		[TestMethod]
		public void CreateHourlyTrigger_Returns_Expected_Expression_Single()
		{
			CronExpression expression = CronBuilder.CreateHourlyTrigger(5);
			Assert.IsTrue(expression.ToString() == "5 * * * *");
		}

		[TestMethod]
		public void CreateHourlyTrigger_Returns_Expected_Expression_List()
		{
			CronExpression expression = CronBuilder.CreateHourlyTrigger(new[]{3,9,12});
			Assert.IsTrue(expression.ToString() == "3,9,12 * * * *");
		}

		[TestMethod]
		public void CreateHourlyTrigger_Returns_Expected_Expression_Range()
		{
			CronExpression expression = CronBuilder.CreateHourlyTrigger(5,9);
			Assert.IsTrue(expression.ToString() == "5-9 * * * *");
		}

		[TestMethod]
		public void CreateHourlyTrigger_Returns_Expected_Expression_Range_With_Interval()
		{
			CronExpression expression = CronBuilder.CreateHourlyTrigger(4, 11, 3);
			Assert.IsTrue(expression.ToString() == "4-11/3 * * * *");
		}

		#endregion

		#region Daily Tests

		[TestMethod]
		public void CreateDailyTrigger_Returns_Expected_Expression_Default()
		{
			CronExpression expression = CronBuilder.CreateDailyTrigger();
			Assert.IsTrue(expression.ToString() == "0 0 * * *");
		}

		[TestMethod]
		public void CreateDailyTrigger_Returns_Expected_Expression_Single()
		{
			CronExpression expression = CronBuilder.CreateDailyTrigger(5);
			Assert.IsTrue(expression.ToString() == "0 5 * * *");
		}

		[TestMethod]
		public void CreateDailyTrigger_Returns_Expected_Expression_List()
		{
			CronExpression expression = CronBuilder.CreateDailyTrigger(new[] { 3, 9, 12 });
			Assert.IsTrue(expression.ToString() == "0 3,9,12 * * *");
		}

		[TestMethod]
		public void CreateDailyTrigger_Returns_Expected_Expression_Range()
		{
			CronExpression expression = CronBuilder.CreateDailyTrigger(5, 9);
			Assert.IsTrue(expression.ToString() == "0 5-9 * * *");
		}

		[TestMethod]
		public void CreateDailyTrigger_Returns_Expected_Expression_Range_With_Interval()
		{
			CronExpression expression = CronBuilder.CreateDailyTrigger(4, 11, 3);
			Assert.IsTrue(expression.ToString() == "0 4-11/3 * * *");
		}

		[TestMethod]
		public void CreateDailyTrigger_Returns_Expected_Expression_Default_Filtered()
		{
			CronBuilder.DayOfWeek[] days = new[]{CronBuilder.DayOfWeek.Tuesday, CronBuilder.DayOfWeek.Thursday};
			CronExpression expression = CronBuilder.CreateDailyTrigger(days);
			Assert.IsTrue(expression.ToString() == "0 0 * * 2,4");
		}

		[TestMethod]
		public void CreateDailyTrigger_Returns_Expected_Expression_Single_Filtered()
		{
			CronBuilder.DayOfWeek[] days = new[] { CronBuilder.DayOfWeek.Tuesday, CronBuilder.DayOfWeek.Thursday };
			CronExpression expression = CronBuilder.CreateDailyTrigger(5, days);
			Assert.IsTrue(expression.ToString() == "0 5 * * 2,4");
		}

		[TestMethod]
		public void CreateDailyTrigger_Returns_Expected_Expression_List_Filtered()
		{
			CronBuilder.DayOfWeek[] days = new[] { CronBuilder.DayOfWeek.Tuesday, CronBuilder.DayOfWeek.Thursday };
			CronExpression expression = CronBuilder.CreateDailyTrigger(new[] { 3, 9, 12 }, days);
			Assert.IsTrue(expression.ToString() == "0 3,9,12 * * 2,4");
		}

		[TestMethod]
		public void CreateDailyTrigger_Returns_Expected_Expression_Range_Filtered()
		{
			CronBuilder.DayOfWeek[] days = new[] { CronBuilder.DayOfWeek.Tuesday, CronBuilder.DayOfWeek.Thursday };
			CronExpression expression = CronBuilder.CreateDailyTrigger(5, 9, days);
			Assert.IsTrue(expression.ToString() == "0 5-9 * * 2,4");
		}

		[TestMethod]
		public void CreateDailyTrigger_Returns_Expected_Expression_Range_With_Interval_Filtered()
		{
			CronBuilder.DayOfWeek[] days = new[] { CronBuilder.DayOfWeek.Tuesday, CronBuilder.DayOfWeek.Thursday };
			CronExpression expression = CronBuilder.CreateDailyTrigger(4, 11, 3, days);
			Assert.IsTrue(expression.ToString() == "0 4-11/3 * * 2,4");
		}

		[TestMethod]
		public void CreateDailyTrigger_Returns_Expected_Expression_Default_Only_Week_Days()
		{
			CronExpression expression = CronBuilder.CreateDailyOnlyWeekDayTrigger();
			Assert.IsTrue(expression.ToString() == "0 0 * * 1,2,3,4,5");
		}

		[TestMethod]
		public void CreateDailyTrigger_Returns_Expected_Expression_Single_Only_Week_Days()
		{
			CronExpression expression = CronBuilder.CreateDailyOnlyWeekDayTrigger(5);
			Assert.IsTrue(expression.ToString() == "0 5 * * 1,2,3,4,5");
		}

		[TestMethod]
		public void CreateDailyTrigger_Returns_Expected_Expression_List_Only_Week_Days()
		{
			CronExpression expression = CronBuilder.CreateDailyOnlyWeekDayTrigger(new[] { 3, 9, 12 });
			Assert.IsTrue(expression.ToString() == "0 3,9,12 * * 1,2,3,4,5");
		}

		[TestMethod]
		public void CreateDailyTrigger_Returns_Expected_Expression_Range_Only_Week_Days()
		{
			CronExpression expression = CronBuilder.CreateDailyOnlyWeekDayTrigger(5, 9);
			Assert.IsTrue(expression.ToString() == "0 5-9 * * 1,2,3,4,5");
		}

		[TestMethod]
		public void CreateDailyTrigger_Returns_Expected_Expression_Range_With_Interval_Only_Week_Days()
		{
			CronExpression expression = CronBuilder.CreateDailyOnlyWeekDayTrigger(4, 11, 3);
			Assert.IsTrue(expression.ToString() == "0 4-11/3 * * 1,2,3,4,5");
		}

		[TestMethod]
		public void CreateDailyTrigger_Returns_Expected_Expression_Default_Only_Week_Ends()
		{
			CronExpression expression = CronBuilder.CreateDailyOnlyWeekEndTrigger();
			Assert.IsTrue(expression.ToString() == "0 0 * * 0,6");
		}

		[TestMethod]
		public void CreateDailyTrigger_Returns_Expected_Expression_Single_Only_Week_Ends()
		{
			CronExpression expression = CronBuilder.CreateDailyOnlyWeekEndTrigger(5);
			Assert.IsTrue(expression.ToString() == "0 5 * * 0,6");
		}

		[TestMethod]
		public void CreateDailyTrigger_Returns_Expected_Expression_List_Only_Week_Ends()
		{
			CronExpression expression = CronBuilder.CreateDailyOnlyWeekEndTrigger(new[] { 3, 9, 12 });
			Assert.IsTrue(expression.ToString() == "0 3,9,12 * * 0,6");
		}

		[TestMethod]
		public void CreateDailyTrigger_Returns_Expected_Expression_Range_Only_Week_Ends()
		{
			CronExpression expression = CronBuilder.CreateDailyOnlyWeekEndTrigger(5, 9);
			Assert.IsTrue(expression.ToString() == "0 5-9 * * 0,6");
		}

		[TestMethod]
		public void CreateDailyTrigger_Returns_Expected_Expression_Range_With_Interval_Only_Week_Ends()
		{
			CronExpression expression = CronBuilder.CreateDailyOnlyWeekEndTrigger(4, 11, 3);
			Assert.IsTrue(expression.ToString() == "0 4-11/3 * * 0,6");
		}	
		#endregion

		#region Monthly Tests

		[TestMethod]
		public void CreateMonthlyTrigger_Returns_Expected_Expression_Default()
		{
			CronExpression expression = CronBuilder.CreateMonthlyTrigger();
			Assert.IsTrue(expression.ToString() == "0 0 0 * *");
		}

		[TestMethod]
		public void CreateMonthlyTrigger_Returns_Expected_Expression_Single()
		{
			CronExpression expression = CronBuilder.CreateMonthlyTrigger(5);
			Assert.IsTrue(expression.ToString() == "0 0 5 * *");
		}

		[TestMethod]
		public void CreateMonthlyTrigger_Returns_Expected_Expression_List()
		{
			CronExpression expression = CronBuilder.CreateMonthlyTrigger(new[] { 3, 9, 12 });
			Assert.IsTrue(expression.ToString() == "0 0 3,9,12 * *");
		}

		[TestMethod]
		public void CreateMonthlyTrigger_Returns_Expected_Expression_Range()
		{
			CronExpression expression = CronBuilder.CreateMonthlyTrigger(5, 9);
			Assert.IsTrue(expression.ToString() == "0 0 5-9 * *");
		}

		[TestMethod]
		public void CreateMonthlyTrigger_Returns_Expected_Expression_Range_With_Interval()
		{
			CronExpression expression = CronBuilder.CreateMonthlyTrigger(4, 11, 3);
			Assert.IsTrue(expression.ToString() == "0 0 4-11/3 * *");
		}

		#endregion

		#region Yearly Tests

		[TestMethod]
		public void CreateYearlyTrigger_Returns_Expected_Expression_Default()
		{
			CronExpression expression = CronBuilder.CreateYearlyTrigger();
			Assert.IsTrue(expression.ToString() == "0 0 0 0 *");
		}

		[TestMethod]
		public void CreateYearlyTrigger_Returns_Expected_Expression_Single()
		{
			CronExpression expression = CronBuilder.CreateYearlyTrigger(5);
			Assert.IsTrue(expression.ToString() == "0 0 0 5 *");
		}

		[TestMethod]
		public void CreateYearlyTrigger_Returns_Expected_Expression_List()
		{
			CronExpression expression = CronBuilder.CreateYearlyTrigger(new[] { 3, 9, 12 });
			Assert.IsTrue(expression.ToString() == "0 0 0 3,9,12 *");
		}

		[TestMethod]
		public void CreateYearlyTrigger_Returns_Expected_Expression_Range()
		{
			CronExpression expression = CronBuilder.CreateYearlyTrigger(5, 9);
			Assert.IsTrue(expression.ToString() == "0 0 0 5-9 *");
		}

		[TestMethod]
		public void CreateYearlyTrigger_Returns_Expected_Expression_Range_With_Interval()
		{
			CronExpression expression = CronBuilder.CreateYearlyTrigger(4, 11, 3);
			Assert.IsTrue(expression.ToString() == "0 0 0 4-11/3 *");
		}

		#endregion
	}
}
