using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Quartz.Spi;

namespace Infinite.GreenBill.WebFront.Infrastructure
{
    internal interface IContainer
    {
        void Register();

        Object Resolve(Type controllerType);

        void Release(IController controller);
    }
}
