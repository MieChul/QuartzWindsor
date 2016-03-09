using System;
using System.Web.Mvc;
using System.Web.Routing;

namespace Infinite.GreenBill.WebFront.Infrastructure
{
    internal class ControllerFactory : DefaultControllerFactory
    {
        private readonly IContainer _container;

        public ControllerFactory(IContainer container)
        {
            if(container == null)
                throw new ArgumentNullException("Container object is null");

            _container = container;
        }

        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            if (controllerType == null)
            {
                return base.GetControllerInstance(requestContext, controllerType);
            }

            var controller = _container.Resolve(controllerType) as Controller;

            return controller;
        }

        public override void ReleaseController(IController controller)
        {
            _container.Release(controller);
        }
    }
}