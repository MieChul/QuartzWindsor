/*
 * Name: Nikhil Pinto
 * Date: 7-Aug-2015
 * Comments: Inherited from AsyncControllerActionInvoker instead of ControllerActionInvoker to support async functions present mainly in identity provided controllers
 * 
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Castle.Windsor;
using System.Web.Mvc.Async;

namespace Infinite.GreenBill.WebFront.Infrastructure
{
    public class WindsorActionInvoker : AsyncControllerActionInvoker
    {    
        //Inject Properties to Container Kernel
        protected override AuthorizationContext InvokeAuthorizationFilters(ControllerContext controllerContext, IList<IAuthorizationFilter> filters, ActionDescriptor actionDescriptor)
        {
            foreach (IAuthorizationFilter actionFilter in filters)
            {
                CastleWindsorContainer.Container.Kernel.InjectProperties(actionFilter);
            }
            return base.InvokeAuthorizationFilters(controllerContext, filters, actionDescriptor);
        }
    }
}