using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IRepAndServices.Services;
using Quartz.Impl;
using IRepAndServices.IRepository;


namespace Infinite.GreenBill.WebFront.Jobs
{
    public class Test1Job : IJob
    {
        private readonly IService _Service;

        public Test1Job(IService Service)
        {
            _Service = Service;
        }

        public void Execute(IJobExecutionContext context)
        {
            _Service.testFlow();
            //_rep.Insert();

        }
    }

    public class Test2Job : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            //test
        }
    }

}