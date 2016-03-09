#region "Development Information"
//=================================================================================================================== 
// ParkerAccountPaymentJob.cs
//=================================================================================================================== 
// Added by   : Rohit Shetty
// Added Date : Jan 27, 2016
// Description: Quartz.net Job to charge pending payments.
//===================================================================================================================

#endregion

#region "References"

using Castle.Windsor;
using Infinite.GreenBill.WebFront.App_Start;
using Infinite.GreenBill.WebFront.Jobs;
using Infinite.GreenBill.WebFront.MVCInfrastructure;
using Quartz;
using Quartz.Impl;
using Quartz.Spi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

#endregion

namespace Infinite.GreenBill.WebFront
{
    public class JobsConfig : IQuartzInitializer
    {

        private readonly IJobFactory _jobFactory;

        public JobsConfig(IJobFactory jobFactory)
        {
            _jobFactory = jobFactory;
        }


        /// <summary>
        /// Registers the jobs.
        /// </summary>
        public void RegisterJobs()
        {
            ISchedulerFactory schedulerFactory = new StdSchedulerFactory();

            // Get an instance of the Quartz.Net scheduler
            var scheduler = schedulerFactory.GetScheduler();

            scheduler.JobFactory = _jobFactory;
            // start scheduler
            scheduler.Start();

            // associates a job with name 'AutoCardPayments' and group name 'Payments', Queues the job on second of every month
            ScheduleJobs<Test1Job>(scheduler, "Job1", "Tests","Testing Job1",50);
            ScheduleJobs<Test2Job>(scheduler, "Job2", "Tests","Testing Job2", 40);

        }

        public static void ScheduleJobs<TClass>(IScheduler scheduler, string jobName, string groupName,string description, int cronExpression) 
            where TClass : IJob
        {
            // Define the Job to be scheduled
            var job = JobBuilder.Create<TClass>()
                .WithIdentity(jobName, groupName)
                .WithDescription(description)
                .RequestRecovery()
                .Build();

            // Create and set a Cron trigger that fires on the specified date and time.
            var trigger = TriggerBuilder.Create()
                .WithIdentity(jobName, groupName)
                .WithDescription(description)
                .StartNow()
                  .WithSimpleSchedule(x => x
                      .WithIntervalInSeconds(cronExpression)
                      .RepeatForever())
                  .Build();

            // Schedule a trigger to fire the associated payment job.
            scheduler.ScheduleJob(job, trigger);
        }
    }
}