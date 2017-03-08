using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Rlc.Cron.Tests
{
	/// <summary>
	/// Summary description for UnitTest1
	/// </summary>
	[TestClass]
	public class CronScheduleTests
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
		[ExpectedException(typeof(ArgumentException))]
		public void Ctor_ThrowsException_ExpressionIsNull()
		{
			CronSchedule.Parse(null);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentException))]
		public void Ctor_ThrowsException_ExpressionIsEmpty()
		{
			CronSchedule.Parse(string.Empty);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentException))]
		public void Ctor_ThrowsException_ExpressionDoesNotContain5Parts()
		{
			CronSchedule.Parse("* * *");
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentException))]
		public void Ctor_ThrowsException_IfMinuteParameterIsNull()
		{
			CronSchedule.Parse(null, "*", "*", "*", "*");
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentException))]
		public void Ctor_ThrowsException_IfHourParameterIsNull()
		{
			CronSchedule.Parse("*", null, "*", "*", "*");
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentException))]
		public void Ctor_ThrowsException_IfDayParameterIsNull()
		{
			CronSchedule.Parse("*", "*", null, "*", "*");
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentException))]
		public void Ctor_ThrowsException_IfMonthParameterIsNull()
		{
			CronSchedule.Parse("*", "*", "*", null, "*");
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentException))]
		public void Ctor_ThrowsException_IfDayOfWeekParameterIsNull()
		{
			CronSchedule.Parse("*", "*", "*", "*", null);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentException))]
		public void Ctor_ThrowsException_IfMinuteParameterIsEmpty()
		{
			CronSchedule.Parse(string.Empty, "*", "*", "*", "*");
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentException))]
		public void Ctor_ThrowsException_IfHourParameterIsEmpty()
		{
			CronSchedule.Parse("*", string.Empty, "*", "*", "*");
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentException))]
		public void Ctor_ThrowsException_IfDayParameterIsEmpty()
		{
			CronSchedule.Parse("*", "*", string.Empty, "*", "*");
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentException))]
		public void Ctor_ThrowsException_IfMonthParameterIsEmpty()
		{
			CronSchedule.Parse("*", "*", "*", string.Empty, "*");
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentException))]
		public void Ctor_ThrowsException_IfDayOfWeekParameterIsEmpty()
		{
			CronSchedule.Parse("*", "*", "*", "*", string.Empty);
		}

		[TestMethod]
		public void ToString_ReturnsExpectedFormat()
		{
			CronSchedule cron = CronSchedule.Parse("*", "5", "1-2", "*/2", "4,6");
			const string expect = "* 5 1-2 */2 4,6";
			Assert.IsTrue(cron.ToString() == expect);
		}

		[TestMethod]
		public void GetNext_ReturnsExpected_SimpleMinutesCase_UsingSingleParamterCtor()
		{
			CronSchedule cronSchedule = CronSchedule.Parse("* * * * *");
			DateTime start = DateTime.Now;
			for (int i = 0; i < 10; i++)
			{
				int minsToAdd = _random.Next(0, 1000);
				start = start.AddMinutes(minsToAdd);

				DateTime expected = start.AddMinutes(1.0);

				DateTime result;
				Assert.IsTrue(cronSchedule.GetNext(start, out result));
				Assert.IsTrue(CompareDates(result, expected));
			}
		}

		[TestMethod]
		public void GetNext_ReturnsExpected_SimpleMinutesCase()
		{
			DateTime start = DateTime.Now;
			for (int i = 0; i < 10; i++)
			{
				int minsToAdd = _random.Next(0, 1000);
				start = start.AddMinutes(minsToAdd);

				DateTime expected = start.AddMinutes(1.0);

				Assert.IsTrue(CronTest("* * * * *", start, expected));
			}
		}

		[TestMethod]
		public void GetNext_ReturnsExpected_SimpleMinutesRollOver()
		{
			DateTime start = new DateTime(2009, 1, 1, 1, 59, 0);
			DateTime expected = start.AddMinutes(1.0);
			Assert.IsTrue(CronTest("* * * * *", start, expected));
		}

		[TestMethod]
		public void GetNext_ReturnsExpected_SimpleHoursCase()
		{
			DateTime start = DateTime.Now;
			for (int i = 0; i < 10; i++)
			{
				int minsToAdd = _random.Next(0, 1000);
				start = start.AddMinutes(minsToAdd);

				DateTime expected = new DateTime(start.Year, start.Month, start.Day, start.Hour, 0, 0).AddHours(1.0);

				Assert.IsTrue(CronTest("0 * * * *", start, expected));
			}
		}

		[TestMethod]
		public void GetNext_ReturnsExpected_SimpleHoursRollOver()
		{
			DateTime start = new DateTime(2009, 1, 1, 23, 59, 0);
			DateTime expected = new DateTime(start.Year, start.Month, start.Day, start.Hour, 0, 0).AddHours(1.0);
			Assert.IsTrue(CronTest("0 * * * *", start, expected));
		}

		[TestMethod]
		public void GetNext_ReturnsExpected_SimpleDaysCase()
		{
			DateTime start = DateTime.Now;
			for (int i = 0; i < 10; i++)
			{
				int daysToAdd = _random.Next(0, 1000);
				start = start.AddDays(daysToAdd);

				DateTime expected = new DateTime(start.Year, start.Month, start.Day, 0, 0, 0).AddDays(1.0);

				Assert.IsTrue(CronTest("0 0 * * *", start, expected));
			}
		}

		[TestMethod]
		public void GetNext_ReturnsExpected_SimpleDaysRollOver_31Days()
		{
			DateTime start = new DateTime(2009, 1, 31, 23, 59, 0);
			DateTime expected = new DateTime(start.Year, start.Month, start.Day, 0, 0, 0).AddDays(1.0);
			Assert.IsTrue(CronTest("0 0 * * *", start, expected));
		}

		[TestMethod]
		public void GetNext_ReturnsExpected_SimpleDaysRollOver_28Days()
		{
			DateTime start = new DateTime(2009, 2, 28, 23, 59, 0);
			DateTime expected = new DateTime(start.Year, start.Month, start.Day, 0, 0, 0).AddDays(1.0);
			Assert.IsTrue(CronTest("0 0 * * *", start, expected));
		}

		[TestMethod]
		public void GetNext_ReturnsExpected_SimpleDaysRollOver_30Days()
		{
			DateTime start = new DateTime(2009, 4, 30, 23, 59, 0);
			DateTime expected = new DateTime(start.Year, start.Month, start.Day, 0, 0, 0).AddDays(1.0);
			Assert.IsTrue(CronTest("0 0 * * *", start, expected));
		}

		[TestMethod]
		public void GetNext_ReturnsExpected_SimpleDaysRollOver_LeapYear()
		{
			DateTime start = new DateTime(2000, 2, 28, 23, 59, 0);
			DateTime expected = new DateTime(2000, 2, 29, 0, 0, 0);
			Assert.IsTrue(CronTest("0 0 * * *", start, expected));
		}

		[TestMethod]
		public void GetNext_ReturnsExpected_SimpleMonthsCase()
		{
			DateTime start = DateTime.Now;
			for (int i = 0; i < 10; i++)
			{
				int daysToAdd = _random.Next(0, 1000);
				start = start.AddDays(daysToAdd);

				DateTime expected = new DateTime(start.Year, start.Month, 1, 0, 0, 0).AddMonths(1);

				Assert.IsTrue(CronTest("0 0 1 * *", start, expected));
			}
		}

		[TestMethod]
		public void GetNext_ReturnsExpected_SimpleMonthsRollOver()
		{
			DateTime start = new DateTime(2009, 12, 31, 23, 59, 0);
			DateTime expected = new DateTime(start.Year, start.Month, 1, 0, 0, 0).AddMonths(1);
			Assert.IsTrue(CronTest("0 0 1 * *", start, expected));
		}

		[TestMethod]
		public void GetNext_ReturnsExpected_SimpleDayOfWeekCase()
		{
			int dayOfWeek = _random.Next(0, 6);

			DateTime start = DateTime.Now;
			for (int i = 0; i < 10; i++)
			{
				int daysToAdd = _random.Next(0, 1000);
				start = start.AddDays(daysToAdd);

				DateTime expected = new DateTime(start.Year, start.Month, start.Day + 1, 0, 0, 0);
				while ((int)expected.DayOfWeek != dayOfWeek)
				{
					expected = expected.AddDays(1.0);
				}

				Assert.IsTrue(CronTest("0 0 * * " + dayOfWeek, start, expected));
			}
		}

		[TestMethod]
		public void GetNext_ReturnsFalse_IfBeyondMaxDate()
		{
			const string cronExpression = "0 0 1 * *";
			string[] parts = cronExpression.Split(' ');
			CronSchedule cron = CronSchedule.Parse(parts[0], parts[1], parts[2], parts[3], parts[4]);
			DateTime start = new DateTime(2009, 12, 31, 23, 59, 0);
			DateTime next;
			bool result = cron.GetNext(start, start, out next);
			Assert.IsFalse(result);
		}

		[TestMethod]
		public void GetAll_ReturnsExpected()
		{
			const string cronExpression = "0 * 1 1 *";
			string[] parts = cronExpression.Split(' ');
			CronSchedule cron = CronSchedule.Parse(parts[0], parts[1], parts[2], parts[3], parts[4]);
			DateTime start = new DateTime(2008, 12, 1, 0, 0, 0);
			DateTime end = new DateTime(2009, 3, 1, 0, 0, 0);
			List<DateTime> dates = cron.GetAll(start, end);
			
			Assert.IsTrue(dates.Count == 24);
			DateTime expected = new DateTime(2009, 1, 1, 0, 0, 0);
			for(int hour=0;hour<24;hour++)
			{
				Assert.IsTrue(dates[hour] == expected);

				// Get the next expected.
				expected = expected.AddHours(1.0);
			}
		}

		[TestMethod]
		public void Various_Tests()
		{
			// These tests where ported directly from the NCrontab project
			//	tests (http://code.google.com/p/ncrontab/)
			//
			CronCall("01/01/2003 00:01", "* * * * *", "01/01/2003 00:02");
			CronCall("01/01/2003 00:02", "* * * * *", "01/01/2003 00:03");
			CronCall("01/01/2003 00:59", "* * * * *", "01/01/2003 01:00");
			CronCall("01/01/2003 01:59", "* * * * *", "01/01/2003 02:00");
			CronCall("01/01/2003 23:59", "* * * * *", "02/01/2003 00:00");
			CronCall("31/12/2003 23:59", "* * * * *", "01/01/2004 00:00");

			CronCall("28/02/2003 23:59", "* * * * *", "01/03/2003 00:00");
			CronCall("28/02/2004 23:59", "* * * * *", "29/02/2004 00:00");

			// Minute tests

			CronCall("01/01/2003 00:00", "45 * * * *", "01/01/2003 00:45");

			CronCall("01/01/2003 00:00", "45-47,48,49 * * * *", "01/01/2003 00:45");
			CronCall("01/01/2003 00:45", "45-47,48,49 * * * *", "01/01/2003 00:46");
			CronCall("01/01/2003 00:46", "45-47,48,49 * * * *", "01/01/2003 00:47");
			CronCall("01/01/2003 00:47", "45-47,48,49 * * * *", "01/01/2003 00:48");
			CronCall("01/01/2003 00:48", "45-47,48,49 * * * *", "01/01/2003 00:49");
			CronCall("01/01/2003 00:49", "45-47,48,49 * * * *", "01/01/2003 01:45");

			CronCall("01/01/2003 00:00", "2/5 * * * *", "01/01/2003 00:02");
			CronCall("01/01/2003 00:02", "2/5 * * * *", "01/01/2003 00:07");
			CronCall("01/01/2003 00:50", "2/5 * * * *", "01/01/2003 00:52");
			CronCall("01/01/2003 00:52", "2/5 * * * *", "01/01/2003 00:57");
			CronCall("01/01/2003 00:57", "2/5 * * * *", "01/01/2003 01:02");

			// Hour tests

			CronCall("20/12/2003 10:00", "* 3/4 * * *", "20/12/2003 11:00");
			CronCall("20/12/2003 00:30", "* 3 * * *", "20/12/2003 03:00");
			CronCall("20/12/2003 01:45", "30 3 * * *", "20/12/2003 03:30");

			// Day of month tests

			CronCall("07/01/2003 00:00", "30 * 1 * *", "01/02/2003 00:30");
			CronCall("01/02/2003 00:30", "30 * 1 * *", "01/02/2003 01:30");

			CronCall("01/01/2003 00:00", "10 * 22 * *", "22/01/2003 00:10");
			CronCall("01/01/2003 00:00", "30 23 19 * *", "19/01/2003 23:30");
			CronCall("01/01/2003 00:00", "30 23 21 * *", "21/01/2003 23:30");
			CronCall("01/01/2003 00:01", "* * 21 * *", "21/01/2003 00:00");
			CronCall("10/07/2003 00:00", "* * 30,31 * *", "30/07/2003 00:00");

			// Test month rollovers for months with 28,29,30 and 31 days

			CronCall("28/02/2002 23:59", "* * * 3 *", "01/03/2002 00:00");
			CronCall("29/02/2004 23:59", "* * * 3 *", "01/03/2004 00:00");
			CronCall("31/03/2002 23:59", "* * * 4 *", "01/04/2002 00:00");
			CronCall("30/04/2002 23:59", "* * * 5 *", "01/05/2002 00:00");

			// Test month 30,31 days

			CronCall("01/01/2000 00:00", "0 0 15,30,31 * *", "15/01/2000 00:00");
			CronCall("15/01/2000 00:00", "0 0 15,30,31 * *", "30/01/2000 00:00");
			CronCall("30/01/2000 00:00", "0 0 15,30,31 * *", "31/01/2000 00:00");
			CronCall("31/01/2000 00:00", "0 0 15,30,31 * *", "15/02/2000 00:00");

			CronCall("15/02/2000 00:00", "0 0 15,30,31 * *", "15/03/2000 00:00");

			CronCall("15/03/2000 00:00", "0 0 15,30,31 * *", "30/03/2000 00:00");
			CronCall("30/03/2000 00:00", "0 0 15,30,31 * *", "31/03/2000 00:00");
			CronCall("31/03/2000 00:00", "0 0 15,30,31 * *", "15/04/2000 00:00");

			CronCall("15/04/2000 00:00", "0 0 15,30,31 * *", "30/04/2000 00:00");
			CronCall("30/04/2000 00:00", "0 0 15,30,31 * *", "15/05/2000 00:00");

			CronCall("15/05/2000 00:00", "0 0 15,30,31 * *", "30/05/2000 00:00");
			CronCall("30/05/2000 00:00", "0 0 15,30,31 * *", "31/05/2000 00:00");
			CronCall("31/05/2000 00:00", "0 0 15,30,31 * *", "15/06/2000 00:00");

			CronCall("15/06/2000 00:00", "0 0 15,30,31 * *", "30/06/2000 00:00");
			CronCall("30/06/2000 00:00", "0 0 15,30,31 * *", "15/07/2000 00:00");

			CronCall("15/07/2000 00:00", "0 0 15,30,31 * *", "30/07/2000 00:00");
			CronCall("30/07/2000 00:00", "0 0 15,30,31 * *", "31/07/2000 00:00");
			CronCall("31/07/2000 00:00", "0 0 15,30,31 * *", "15/08/2000 00:00");

			CronCall("15/08/2000 00:00", "0 0 15,30,31 * *", "30/08/2000 00:00");
			CronCall("30/08/2000 00:00", "0 0 15,30,31 * *", "31/08/2000 00:00");
			CronCall("31/08/2000 00:00", "0 0 15,30,31 * *", "15/09/2000 00:00");

			CronCall("15/09/2000 00:00", "0 0 15,30,31 * *", "30/09/2000 00:00");
			CronCall("30/09/2000 00:00", "0 0 15,30,31 * *", "15/10/2000 00:00");

			CronCall("15/10/2000 00:00", "0 0 15,30,31 * *", "30/10/2000 00:00");
			CronCall("30/10/2000 00:00", "0 0 15,30,31 * *", "31/10/2000 00:00");
			CronCall("31/10/2000 00:00", "0 0 15,30,31 * *", "15/11/2000 00:00");

			CronCall("15/11/2000 00:00", "0 0 15,30,31 * *", "30/11/2000 00:00");
			CronCall("30/11/2000 00:00", "0 0 15,30,31 * *", "15/12/2000 00:00");

			CronCall("15/12/2000 00:00", "0 0 15,30,31 * *", "30/12/2000 00:00");
			CronCall("30/12/2000 00:00", "0 0 15,30,31 * *", "31/12/2000 00:00");
			CronCall("31/12/2000 00:00", "0 0 15,30,31 * *", "15/01/2001 00:00");

			// Other month tests (including year rollover)

			CronCall("01/12/2003 05:00", "10 * * 6 *", "01/06/2004 00:10");
			CronCall("04/01/2003 00:00", "1 2 3 * *", "03/02/2003 02:01");
			CronCall("01/01/2003 00:00", "0 12 1 6 *", "01/06/2003 12:00");
			CronCall("11/09/1988 14:23", "* 12 1 6 *", "01/06/1989 12:00");
			CronCall("11/03/1988 14:23", "* 12 1 6 *", "01/06/1988 12:00");
			CronCall("11/03/1988 14:23", "* 2,4-8,15 * 6 *", "01/06/1988 02:00");

			// Day of week tests

			CronCall("26/06/2003 10:00", "30 6 * * 0", "29/06/2003 06:30");
			CronCall("19/06/2003 00:00", "1 12 * * 2", "24/06/2003 12:01");
			CronCall("24/06/2003 12:01", "1 12 * * 2", "01/07/2003 12:01");

			CronCall("01/06/2003 14:55", "15 18 * * 1", "02/06/2003 18:15");
			CronCall("02/06/2003 18:15", "15 18 * * 1", "09/06/2003 18:15");
			CronCall("09/06/2003 18:15", "15 18 * * 1", "16/06/2003 18:15");
			CronCall("16/06/2003 18:15", "15 18 * * 1", "23/06/2003 18:15");
			CronCall("23/06/2003 18:15", "15 18 * * 1", "30/06/2003 18:15");
			CronCall("30/06/2003 18:15", "15 18 * * 1", "07/07/2003 18:15");

			CronCall("01/01/2003 00:00", "* * * * 1", "06/01/2003 00:00");
			CronCall("01/01/2003 12:00", "45 16 1 * 1", "01/09/2003 16:45");
			CronCall("01/09/2003 23:45", "45 16 1 * 1", "01/12/2003 16:45");

			// Leap year tests

			CronCall("01/01/2000 12:00", "1 12 29 2 *", "29/02/2000 12:01");
			CronCall("29/02/2000 12:01", "1 12 29 2 *", "29/02/2004 12:01");
			CronCall("29/02/2004 12:01", "1 12 29 2 *", "29/02/2008 12:01");

			// Non-leap year tests

			CronCall("01/01/2000 12:00", "1 12 28 2 *", "28/02/2000 12:01");
			CronCall("28/02/2000 12:01", "1 12 28 2 *", "28/02/2001 12:01");
			CronCall("28/02/2001 12:01", "1 12 28 2 *", "28/02/2002 12:01");
			CronCall("28/02/2002 12:01", "1 12 28 2 *", "28/02/2003 12:01");
			CronCall("28/02/2003 12:01", "1 12 28 2 *", "28/02/2004 12:01");
			CronCall("29/02/2004 12:01", "1 12 28 2 *", "28/02/2005 12:01");
		}

		#region Helper Methods

		private readonly Random _random = new Random();

		private static void CronCall(string startTimeString, string cronExpression, string nextTimeString)
		{
			DateTime start = Time(startTimeString);

			string[] parts = cronExpression.Split(' ');
			CronSchedule schedule = CronSchedule.Parse(parts[0], parts[1], parts[2], parts[3], parts[4]);

			DateTime next;
			schedule.GetNext(start, out next);

			Assert.AreEqual(nextTimeString, TimeString(next),
			                "Occurrence of <{0}> after <{1}>.", cronExpression, startTimeString);
		}

		private const string TimeFormat = "dd/MM/yyyy HH:mm";

		private static string TimeString(IFormattable time)
		{
			return time.ToString(TimeFormat, CultureInfo.InvariantCulture);
		}

		private static DateTime Time(string str)
		{
			return DateTime.ParseExact(str, TimeFormat, CultureInfo.InvariantCulture);
		}

		private static bool CronTest(string cronExpression, DateTime start, DateTime expectedNext)
		{
			string[] parts = cronExpression.Split(' ');
			CronSchedule cron = CronSchedule.Parse(parts[0], parts[1], parts[2], parts[3], parts[4]);

			DateTime result;
			if(cron.GetNext(start, out result))
			{
				return CompareDates(result, expectedNext);
			}
			return false;
		}

		private static bool CompareDates(DateTime cronNextDate, DateTime expectedResult)
		{
			if (cronNextDate.Minute == expectedResult.Minute &&
			    cronNextDate.Hour == expectedResult.Hour &&
			    cronNextDate.Day == expectedResult.Day &&
			    cronNextDate.Month == expectedResult.Month &&
			    cronNextDate.Year == expectedResult.Year &&
			    cronNextDate.Second == 0 &&
			    cronNextDate.Millisecond == 0)
			{
				return true;
			}
			return false;
		}

		#endregion
	}
}