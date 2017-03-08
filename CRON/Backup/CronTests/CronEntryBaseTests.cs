using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Rlc.Cron.Tests
{
	/// <summary>
	/// Summary description for CronInfoTests
	/// </summary>
	[TestClass]
	public class CronEntryTests
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
		[ExpectedException(typeof(CronEntryException))]
		public void MinutesCronEntryCtor_ThrowsException_IfExpressionIsNull()
		{
			new MinutesCronEntry(null);
		}

		[TestMethod]
		[ExpectedException(typeof(CronEntryException))]
		public void MinutesCronEntryCtor_ThrowsException_IfExpressionIsEmpty()
		{
			new MinutesCronEntry(string.Empty);
		}

		[TestMethod]
		[ExpectedException(typeof(CronEntryException))]
		public void HoursCronEntryCtor_ThrowsException_IfExpressionIsNull()
		{
			new HoursCronEntry(null);
		}

		[TestMethod]
		[ExpectedException(typeof(CronEntryException))]
		public void HoursCronEntryCtor_ThrowsException_IfExpressionIsEmpty()
		{
			new HoursCronEntry(string.Empty);
		}

		[TestMethod]
		[ExpectedException(typeof(CronEntryException))]
		public void DaysCronEntryCtor_ThrowsException_IfExpressionIsNull()
		{
			new DaysCronEntry(null);
		}

		[TestMethod]
		[ExpectedException(typeof(CronEntryException))]
		public void DaysCronEntryCtor_ThrowsException_IfExpressionIsEmpty()
		{
			new DaysCronEntry(string.Empty);
		}

		[TestMethod]
		[ExpectedException(typeof(CronEntryException))]
		public void MonthsCronEntryCtor_ThrowsException_IfExpressionIsNull()
		{
			new MonthsCronEntry(null);
		}

		[TestMethod]
		[ExpectedException(typeof(CronEntryException))]
		public void MonthsCronEntryCtor_ThrowsException_IfExpressionIsEmpty()
		{
			new MonthsCronEntry(string.Empty);
		}

		[TestMethod]
		[ExpectedException(typeof(CronEntryException))]
		public void DaysOfWeekCronEntryCtor_ThrowsException_IfExpressionIsNull()
		{
			new DaysOfWeekCronEntry(null);
		}

		[TestMethod]
		[ExpectedException(typeof(CronEntryException))]
		public void DaysOfWeekCronEntryCtor_ThrowsException_IfExpressionIsEmpty()
		{
			new DaysOfWeekCronEntry(string.Empty);
		}

		[TestMethod]
		public void Minutes_ParsesExpressionCorrectly_Star()
		{
			MinutesCronEntry cronInfo = new MinutesCronEntry("*");

			Assert.IsTrue(cronInfo.Values.Count == 60);
			for (int i = 0; i <= 59; i++)
			{
				Assert.IsTrue(cronInfo.Values[i] == i);
			}
		}

		[TestMethod]
		public void Hours_ParsesExpressionCorrectly_Star()
		{
			HoursCronEntry cronInfo = new HoursCronEntry("*");

			Assert.IsTrue(cronInfo.Values.Count == 24);
			for (int i = 0; i <= 23; i++)
			{
				Assert.IsTrue(cronInfo.Values[i] == i);
			}
		}

		[TestMethod]
		public void Days_ParsesExpressionCorrectly_Star()
		{
			DaysCronEntry cronInfo = new DaysCronEntry("*");

			Assert.IsTrue(cronInfo.Values.Count == 31);
			for (int i = 0; i < 31; i++)
			{
				Assert.IsTrue(cronInfo.Values[i] == i + 1);
			}
		}

		[TestMethod]
		public void Months_ParsesExpressionCorrectly_Star()
		{
			MonthsCronEntry cronInfo = new MonthsCronEntry("*");

			Assert.IsTrue(cronInfo.Values.Count == 12);
			for (int i = 0; i < 12; i++)
			{
				Assert.IsTrue(cronInfo.Values[i] == i + 1);
			}
		}

		[TestMethod]
		public void DaysOfWeek_ParsesExpressionCorrectly_Star()
		{
			DaysOfWeekCronEntry cronInfo = new DaysOfWeekCronEntry("*");

			Assert.IsTrue(cronInfo.Values.Count == 7);
			for (int i = 0; i <= 6; i++)
			{
				Assert.IsTrue(cronInfo.Values[i] == i);
			}
		}

		[TestMethod]
		[ExpectedException(typeof(CronEntryException))]
		public void Parser_ThrowsException_IfSingleNumberIsGreaterThanExpected()
		{
			new DaysOfWeekCronEntry("10");
		}

		[TestMethod]
		[ExpectedException(typeof(CronEntryException))]
		public void Parser_ThrowsException_IfSingleNumberIsLessThanExpected()
		{
			new DaysCronEntry("0");
		}

		[TestMethod]
		[ExpectedException(typeof(CronEntryException))]
		public void Parser_ThrowsException_IfSingleNumberIsNotANumber()
		{
			new DaysOfWeekCronEntry("a");
		}

		[TestMethod]
		[ExpectedException(typeof(CronEntryException))]
		public void Parser_ThrowsException_IfCommaSepIsGreaterThanExpected()
		{
			new DaysOfWeekCronEntry("1,10");
		}

		[TestMethod]
		[ExpectedException(typeof(CronEntryException))]
		public void Parser_ThrowsException_IfCommaSepIsNotANumber()
		{
			new DaysOfWeekCronEntry("1,a");
		}

		[TestMethod]
		[ExpectedException(typeof(CronEntryException))]
		public void Parser_ThrowsException_IfCommaSepDoesNotHaveEnoughParts1()
		{
			new DaysOfWeekCronEntry("1,");
		}

		[TestMethod]
		[ExpectedException(typeof(CronEntryException))]
		public void Parser_ThrowsException_IfCommaSepDoesNotHaveEnoughParts2()
		{
			new DaysOfWeekCronEntry(",1");
		}

		[TestMethod]
		[ExpectedException(typeof(CronEntryException))]
		public void Parser_ThrowsException_IfCommaSepDoesNotHaveEnoughParts3()
		{
			new DaysOfWeekCronEntry("1,,2");
		}

		[TestMethod]
		[ExpectedException(typeof(CronEntryException))]
		public void Parser_ThrowsException_IfCommaSepDoesNotHaveEnoughParts4()
		{
			new DaysOfWeekCronEntry(",");
		}

		[TestMethod]
		[ExpectedException(typeof(CronEntryException))]
		public void Parser_ThrowsException_IfCommaSepContainsInvalidChars1()
		{
			new DaysOfWeekCronEntry("1,a");
		}

		[TestMethod]
		[ExpectedException(typeof(CronEntryException))]
		public void Parser_ThrowsException_IfCommaSepContainsInvalidChars2()
		{
			new DaysOfWeekCronEntry("a,5");
		}

		[TestMethod]
		[ExpectedException(typeof(CronEntryException))]
		public void Parser_ThrowsException_IfSlashDoesNotHaveEnoughParts1()
		{
			new DaysOfWeekCronEntry("*/");
		}

		[TestMethod]
		[ExpectedException(typeof(CronEntryException))]
		public void Parser_ThrowsException_IfSlashDoesNotHaveEnoughParts2()
		{
			new DaysOfWeekCronEntry("/2");
		}

		[TestMethod]
		[ExpectedException(typeof(CronEntryException))]
		public void Parser_ThrowsException_IfSlashDoesNotHaveEnoughParts3()
		{
			new DaysOfWeekCronEntry("/");
		}

		[TestMethod]
		[ExpectedException(typeof(CronEntryException))]
		public void Parser_ThrowsException_IfIntervalIsNegative()
		{
			new DaysOfWeekCronEntry("*/-3");
		}

		[TestMethod]
		[ExpectedException(typeof(CronEntryException))]
		public void Parser_ThrowsException_IfSlashDoesContainsInvalidChar()
		{
			new DaysOfWeekCronEntry("/a");
		}

		[TestMethod]
		[ExpectedException(typeof(CronEntryException))]
		public void Parser_ThrowsException_IfDashDoesNotHaveEnoughParts1()
		{
			new DaysOfWeekCronEntry("1-");
		}

		[TestMethod]
		[ExpectedException(typeof(CronEntryException))]
		public void Parser_ThrowsException_IfDashDoesNotHaveEnoughParts2()
		{
			new DaysOfWeekCronEntry("-6");
		}

		[TestMethod]
		[ExpectedException(typeof(CronEntryException))]
		public void Parser_ThrowsException_IfDashDoesNotHaveEnoughParts3()
		{
			new DaysOfWeekCronEntry("-");
		}

		[TestMethod]
		[ExpectedException(typeof(CronEntryException))]
		public void Parser_ThrowsException_IfDashContainsInvalidChars1()
		{
			new DaysOfWeekCronEntry("a-5");
		}

		[TestMethod]
		[ExpectedException(typeof(CronEntryException))]
		public void Parser_ThrowsException_IfDashContainsInvalidChars2()
		{
			new DaysOfWeekCronEntry("1-x");
		}

		[TestMethod]
		[ExpectedException(typeof(CronEntryException))]
		public void Parser_ThrowsException_IfDashHasMinLessThanExpected()
		{
			new DaysCronEntry("0-10");
		}

		[TestMethod]
		[ExpectedException(typeof(CronEntryException))]
		public void Parser_ThrowsException_IfDashHasMaxGreaterThanExpected()
		{
			new DaysOfWeekCronEntry("1-100");
		}

		[TestMethod]
		[ExpectedException(typeof(CronEntryException))]
		public void Parser_ThrowsException_IfDashHasMaxLessThanMin()
		{
			new DaysOfWeekCronEntry("3-1");
		}

		[TestMethod]
		public void First_Returns_FirstValueInList()
		{
			MinutesCronEntry cronInfo = new MinutesCronEntry("*");
			Assert.IsTrue(cronInfo.First == cronInfo.Values[0]);
		}

		[TestMethod]
		public void Next_Returns_SameValue_WhenStartIsInMiddleAndIsInList()
		{
			MinutesCronEntry cronInfo =new MinutesCronEntry("*");
			Assert.IsTrue(cronInfo.Next(5) == 5);
		}

		[TestMethod]
		public void Next_Returns_NextGreaterValue_WhenStartIsInMiddleAndIsNotInList()
		{
			MinutesCronEntry cronInfo = new MinutesCronEntry("*/2");
			Assert.IsTrue(cronInfo.Next(5) == 6);
		}

		[TestMethod]
		public void Next_Returns_NextValueInList_WhenStartIsBeforeFirst()
		{
			MinutesCronEntry cronInfo = new MinutesCronEntry("*");
			Assert.IsTrue(cronInfo.Next(-10) == 0);
		}

		[TestMethod]
		public void Next_Returns_RolledOver_WhenGoingBeyondEndOfList()
		{
			MinutesCronEntry cronInfo = new MinutesCronEntry("*");
			Assert.IsTrue(cronInfo.Next(100) == CronEntryBase.RolledOver);
		}

		[TestMethod]
		public void Parser_CorrectlyHandles_SimpleCommaCase()
		{
			MinutesCronEntry cronInfo = new MinutesCronEntry("1,6,9");

			Assert.IsTrue(cronInfo.Values.Count == 3);
			Assert.IsTrue(cronInfo.Values.Contains(1));
			Assert.IsTrue(cronInfo.Values.Contains(6));
			Assert.IsTrue(cronInfo.Values.Contains(9));
		}

		[TestMethod]
		public void Parser_CorrectlyHandles_SimpleDashCase()
		{
			MinutesCronEntry cronInfo = new MinutesCronEntry("5-9");

			Assert.IsTrue(cronInfo.Values.Count == 5);
			Assert.IsTrue(cronInfo.Values.Contains(5));
			Assert.IsTrue(cronInfo.Values.Contains(6));
			Assert.IsTrue(cronInfo.Values.Contains(7));
			Assert.IsTrue(cronInfo.Values.Contains(8));
			Assert.IsTrue(cronInfo.Values.Contains(9));
		}

		[TestMethod]
		public void Parser_CorrectlyHandles_SimpleIntervalCase()
		{
			MinutesCronEntry cronInfo = new MinutesCronEntry("*/10");

			Assert.IsTrue(cronInfo.Values.Count == 6);
			Assert.IsTrue(cronInfo.Values.Contains(0));
			Assert.IsTrue(cronInfo.Values.Contains(10));
			Assert.IsTrue(cronInfo.Values.Contains(20));
			Assert.IsTrue(cronInfo.Values.Contains(30));
			Assert.IsTrue(cronInfo.Values.Contains(40));
			Assert.IsTrue(cronInfo.Values.Contains(50));
		}

		[TestMethod]
		public void Parser_CorrectlyHandles_SingleNumberIntervalCase()
		{
			MinutesCronEntry cronInfo = new MinutesCronEntry("5/10");

			Assert.IsTrue(cronInfo.Values.Count == 6);
			Assert.IsTrue(cronInfo.Values.Contains(5));
			Assert.IsTrue(cronInfo.Values.Contains(15));
			Assert.IsTrue(cronInfo.Values.Contains(25));
			Assert.IsTrue(cronInfo.Values.Contains(35));
			Assert.IsTrue(cronInfo.Values.Contains(45));
			Assert.IsTrue(cronInfo.Values.Contains(55));
		}

		[TestMethod]
		public void Parser_CorrectlyHandles_ComplexCase1()
		{
			MinutesCronEntry cronInfo = new MinutesCronEntry("2,4,6,15-18,30-40/2,40-50/3");

			int[] expected = new[] {2, 4, 6, 15, 16, 17, 18, 30, 32, 34, 36, 38, 40, 43, 46, 49};
			Assert.IsTrue(cronInfo.Values.Count == expected.Length);
			foreach (int value in expected)
			{
				Assert.IsTrue(cronInfo.Values.Contains(value));
			}
		}
	}
}