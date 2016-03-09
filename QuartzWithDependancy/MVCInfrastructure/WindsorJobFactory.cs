using Castle.Windsor;
using Infinite.GreenBill.WebFront.Infrastructure;
using Quartz;
using Quartz.Spi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Quartz.Simpl;

namespace Infinite.GreenBill.WebFront.MVCInfrastructure
{
    internal class WindsorJobFactory : IJobFactory
    {
        private readonly IContainer _container;

        public WindsorJobFactory(IContainer container)
        {
            if (container == null)
                throw new ArgumentNullException("Container object is null");

            _container = container;
        }

        public IJob NewJob(TriggerFiredBundle bundle, IScheduler scheduler)
        {
            return (IJob)_container.Resolve(bundle.JobDetail.JobType);
        }

        public void ReturnJob(IJob job)
        {
            throw new NotImplementedException();
        }
    }
}