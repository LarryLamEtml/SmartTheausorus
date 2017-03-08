using CronClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create the cron schedule.
            //
            CronExpression cronExpression = CronBuilder.CreateHourlyTrigger(new int[] { 0, 30 });
            CronSchedule cronSchedule = CronSchedule.Parse(cronExpression.ToString());
            List<CronSchedule> cronSchedules = new List<CronSchedule> { cronSchedule };

            // Create the data context for the cron object.
            //
            CronObjectDataContext dc = new CronObjectDataContext
            {
                Object = myObject,
                CronSchedules = cronSchedules,
                LastTrigger = DateTime.MinValue
            };

            // Create the cron object.
            //
            CronObject cron = new CronObject(dc);

            // Register for events.
            //
            cron.OnCronTrigger += Cron_OnCronTrigger;

            // Start the cron job.
            //
            cron.Start();

        }

    }
}
