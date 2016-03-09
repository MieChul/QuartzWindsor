using System;
using System.Web;
using System.Web.Mvc;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using System.Collections.Generic;
using Quartz;
using System.Linq;
using System.Data;
using Quartz.Spi;
using System.Web.Compilation;
using QuartzWithDependancy.Controllers;
using Infinite.GreenBill.WebFront.App_Start;
using Infinite.GreenBill.WebFront.Jobs;
using IRepAndServices.Services;
using IRepAndServices.IRepository;
using Repository;


namespace Infinite.GreenBill.WebFront.Infrastructure
{
    internal class CastleWindsorContainer : IContainer
    {
        public static WindsorContainer Container { get; private set; }

        public CastleWindsorContainer()
        {
            Container = new WindsorContainer();
        }

        public void Register()
        {
            Container.Register( Component.For<HomeController>().LifestylePerWebRequest());
           Container.Register(Component.For<ITestRepository>().ImplementedBy<TestRepository>().LifestyleTransient());
        }

        public Object Resolve(Type controllerType)
        {
            return Container.Resolve(controllerType);
        }

        public void Release(IController controller)
        {
            Container.Release(controller);
        }


        public void RegisterJobs(IJobFactory jobFactory)
        {
            Container.Register(Component.For<IJobFactory>().Instance(jobFactory).LifestyleTransient());
            Container.Register(Component.For<IQuartzInitializer>().ImplementedBy<JobsConfig>().LifestyleTransient());
            Container.Register(Component.For<IService>().ImplementedBy<Service>());
            Container.Register(
                Component.For<Test1Job>().ImplementedBy<Test1Job>().LifestyleTransient(),
                Component.For<Test2Job>().ImplementedBy<Test2Job>().LifestyleTransient()
            );
            Container.Resolve<IQuartzInitializer>().RegisterJobs();
            
        }

    }

}