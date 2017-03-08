using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading;

namespace Rlc.Cron.Tests
{
	/// <summary>
	/// Summary description for CronEventManagerTests
	/// </summary>
	[TestClass]
	public class CronEventManagerTests
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
		public void Ctor_ThrowsException_IfParameterIsNull()
		{
			new CronObject(null);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentException))]
		public void Ctor_ThrowsException_IfCronPackageIsNull()
		{
			CronSchedule cronSchedule = CronSchedule.Parse("* * * * *");
			List<CronSchedule> cronSchedules = new List<CronSchedule> { cronSchedule };
			CronObjectDataContext dc = new CronObjectDataContext
			                           	{
			                           		Object = null,
			                           		CronSchedules = cronSchedules,
			                           		LastTrigger = DateTime.MinValue
			                           	};
			new CronObject(dc);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentException))]
		public void Ctor_ThrowsException_IfCronSchedulesIsNull()
		{
			CronObjectDataContext dc = new CronObjectDataContext
			                           	{
			                           		Object = DateTime.Now,
			                           		CronSchedules = null,
			                           		LastTrigger = DateTime.MinValue
			                           	};
			new CronObject(dc);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentException))]
		public void Ctor_ThrowsException_IfCronSchedulesIsEmpty()
		{
			List<CronSchedule> cronSchedules = new List<CronSchedule>();
			CronObjectDataContext dc = new CronObjectDataContext
			                           	{
			                           		Object = DateTime.Now,
			                           		CronSchedules = cronSchedules,
			                           		LastTrigger = DateTime.MinValue
			                           	};
			new CronObject(dc);
		}

		[TestMethod]
		public void CronPackage_Returns_ObjectEmbeddedIntoContext()
		{
			DateTime now = DateTime.Now;
			CronObject cron = CreateCronObject(now);
			DateTime package = (DateTime)cron.Object;
			Assert.IsTrue(package == now);
		}

		[TestMethod]
		public void LastTrigger_Returns_CorrectValue()
		{
			DateTime now = DateTime.Now;
			CronSchedule cronSchedule = CronSchedule.Parse("* * * * *");
			List<CronSchedule> cronSchedules = new List<CronSchedule> { cronSchedule };
			CronObjectDataContext dc = new CronObjectDataContext
			                           	{
			                           		Object = DateTime.Now,
			                           		CronSchedules = cronSchedules,
			                           		LastTrigger = now
			                           	};
			CronObject cron = new CronObject(dc);
			Assert.IsTrue(cron.LastTigger == now);
		}

		[TestMethod]
		public void Id_Returns_Value()
		{
			DateTime now = DateTime.Now;
			CronSchedule cronSchedule = CronSchedule.Parse("* * * * *");
			List<CronSchedule> cronSchedules = new List<CronSchedule> { cronSchedule };
			CronObjectDataContext dc = new CronObjectDataContext
			{
				Object = DateTime.Now,
				CronSchedules = cronSchedules,
				LastTrigger = now
			};
			CronObject cron = new CronObject(dc);
			Assert.IsNotNull(cron.Id);
			Assert.IsTrue(cron.Id == Guid.Empty);
		}

		[TestMethod]
		public void Stop_ReturnsFalse_IfNotStarted()
		{
			CronObject cron = CreateCronObject();
			Assert.IsFalse(cron.Stop());
		}

		[TestMethod]
		public void Start_Raises_OnStartedEvent()
		{
			CronObject cron = CreateCronObject();
			_isOnStarted = false;
			cron.Start();
			cron.Stop();
			Assert.IsTrue(_isOnStarted);
		}

		[TestMethod]
		public void Stop_Raises_OnStoppedEvent()
		{
			CronObject cron = CreateCronObject();
			cron.Start();
			_isOnStopped = false;
			cron.Stop();
			Assert.IsTrue(_isOnStopped);
		}

		[TestMethod]
		public void Stop_Raises_OnThreadAbort_IfNotAbleToStopOnOwn()
		{
			CronObject cron = CreateCronObject();
			cron.OnCronTrigger += Cron_Trigger_NeverEnds;
			_isOnThreadAbort = false;
			_beenTriggered = false;
			cron.Start();
			while(!_beenTriggered)
			{
				Thread.Sleep(50);
			}
			cron.Stop();
			Assert.IsTrue(_isOnThreadAbort);
		}

		[TestMethod]
		public void Start_ReturnsFalse_IfAlreadyStarted()
		{
			CronObject cron = CreateCronObject();

			cron.Start();

			Assert.IsFalse(cron.Start());

			cron.Stop();
		}

		[TestMethod]
		public void Start_Causes_Trigger_Once_Per_Minute_Test_Takes_At_Minimum_60Seconds_At_Maximum_120Seconds()
		{
			CronExpression expression = CronBuilder.CreateMinutelyTrigger();
			CronSchedule schedule = CronSchedule.Parse(expression.ToString());
			CronObject cron = CreateCronObject(DateTime.Now, schedule);

			// Add the trigger event.
			//	Notice: MsTest runs test methods in parallel. If another test
			//	is also using this event, this method will fail.
			//
			cron.OnCronTrigger += Cron_OnCronTrigger;

			_count = 0;
			cron.Start();

			DateTime start = DateTime.Now;
			const int maxAllowedError = 50;
			int currentCount = _count;
			DateTime lastTrigger = DateTime.MinValue;
			TimeSpan timeBetweenTriggers = DateTime.Now - lastTrigger;
			while (_count < 3 && (DateTime.Now - start).TotalSeconds < 125)
			{
				Thread.Sleep(maxAllowedError);
				if (_count != currentCount)
				{
					DateTime now = DateTime.Now;
					timeBetweenTriggers = now - lastTrigger;
					lastTrigger = now;
					currentCount = _count;
				}
			}
			cron.Stop();

			double error = Math.Abs(60.0 - timeBetweenTriggers.TotalSeconds)*1000.0;
			Assert.IsTrue(error < maxAllowedError);
		}

		#region Helpers

		private static CronObjectDataContext CreateDataContext(object cronObject)
		{
			CronSchedule cronSchedule = CronSchedule.Parse("* * * * *");
			return CreateDataContext(cronObject, cronSchedule);
		}

		private static CronObjectDataContext CreateDataContext(object cronObject, CronSchedule cronSchedule)
		{
			List<CronSchedule> cronSchedules = new List<CronSchedule> { cronSchedule };
			CronObjectDataContext dc = new CronObjectDataContext
			{
				Object = cronObject,
				CronSchedules = cronSchedules,
				LastTrigger = DateTime.MinValue
			};
			return dc;
		}

		private CronObject CreateCronObject()
		{
			return CreateCronObject(DateTime.Now);
		}

		private CronObject CreateCronObject(object cronObject)
		{
			CronObjectDataContext dc = CreateDataContext(cronObject);
			CronObject cron = new CronObject(dc);
			cron.OnStarted += Cron_OnStarted;
			cron.OnStopped += Cron_OnStopped;
			cron.OnThreadAbort += Cron_OnThreadAbort;
			return cron;
		}

		private CronObject CreateCronObject(object cronObject, CronSchedule cronSchedule)
		{
			CronObjectDataContext dc = CreateDataContext(cronObject, cronSchedule);
			CronObject cron = new CronObject(dc);
			cron.OnStarted += Cron_OnStarted;
			cron.OnStopped += Cron_OnStopped;
			cron.OnThreadAbort += Cron_OnThreadAbort;
			return cron;
		}

		private bool _isOnThreadAbort;
		private void Cron_OnThreadAbort(CronObject cronObject)
		{
			_isOnThreadAbort = true;
		}

		private bool _isOnStopped;
		private void Cron_OnStopped(CronObject cronObject)
		{
			_isOnStopped = true;
		}

		private volatile int _count = 0;
		private void Cron_OnCronTrigger(CronObject cronObject)
		{
			_count++;
		}

		private bool _isOnStarted;
		private void Cron_OnStarted(CronObject cronObject)
		{
			_isOnStarted = true;
		}

		private static bool _beenTriggered = false;
		private static void Cron_Trigger_NeverEnds(CronObject cronObject)
		{
			while(true)
			{
				_beenTriggered = true;
				Thread.Sleep(1000);
			}
		}

		#endregion
	}
}