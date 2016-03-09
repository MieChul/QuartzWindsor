using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Infinite.GreenBill.WebFront.Jobs;
using Quartz;
using Quartz.Impl;
using Quartz.Impl.Matchers;
using Quartz.Impl.Triggers;
using QuartzWithDependancy.Models;

namespace QuartzWithDependancy.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View(new ReScheduleTime());
        }

        public ActionResult ReScheduleJoB(ReScheduleTime model)
        {

            ISchedulerFactory schedulerFactory = new StdSchedulerFactory();
            var scheduler = schedulerFactory.GetScheduler();
            // Get an instance of the Quartz.Net scheduler

            var tnew = new CronTriggerImpl("Job1")
        {
            CronExpressionString = "0 0/" + model.time + " * * * ?",
            TimeZone = TimeZoneInfo.Utc
        };
            //var jobNew = new JobDetailImpl("Job1", "Tests", typeof(IJob));
            scheduler.RescheduleJob(new TriggerKey("Job1", "Tests"), tnew);

            // IList<string> triggerGroups = scheduler.GetTriggerGroupNames();

            return RedirectToAction("Index");
        }

        public ActionResult ViewCurrentlyRunningJobs()
        {
            var lstmodel = new List<JobsDetailViewModel>();
            ISchedulerFactory schedulerFactory = new StdSchedulerFactory();
            var scheduler = schedulerFactory.GetScheduler();
            IList<string> jobGroups = scheduler.GetJobGroupNames();
            foreach (string group in jobGroups)
            {
                var groupMatcher = GroupMatcher<JobKey>.GroupContains(group);
                var jobKeys = scheduler.GetJobKeys(groupMatcher);
                foreach (var jobKey in jobKeys)
                {
                    var detail = scheduler.GetJobDetail(jobKey);
                    var triggers = scheduler.GetTriggersOfJob(jobKey);
                    foreach (ITrigger trigger in triggers)
                    {
                        var model= new JobsDetailViewModel();
                        model.JobGroup = group;
                        model.JobName = jobKey.Name;
                        model.JobDescription = detail.Description;
                        model.TriggerName = trigger.Key.Name;
                        model.TriggerGroup = trigger.Key.Group;
                        model.TriggerTypeName = trigger.GetType().Name;
                        model.TriggerState = scheduler.GetTriggerState(trigger.Key).ToString();
                        DateTimeOffset? nextFireTime = trigger.GetNextFireTimeUtc();
                        if (nextFireTime.HasValue)
                        {
                            model.NextFireTime = nextFireTime.Value.LocalDateTime.ToString();
                        }

                        DateTimeOffset? previousFireTime = trigger.GetPreviousFireTimeUtc();
                        if (previousFireTime.HasValue)
                        {
                            model.PreviousFireTime = previousFireTime.Value.LocalDateTime.ToString();
                        }
                        lstmodel.Add(model);
                    }
                }
            }

            return View(lstmodel);
        }
    }
}

