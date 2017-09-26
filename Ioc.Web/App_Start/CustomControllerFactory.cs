using System;
using System.Web.Mvc;
using System.Web.Routing;

namespace IoC.Web
{
    public class CustomControllerFactory : DefaultControllerFactory
    {
        private readonly IContainer _container;

        public CustomControllerFactory(IContainer container)
        {
            _container = container;
        }

        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            // Resolve requested type from container
            return _container.Resolve(controllerType) as Controller;
        }
    }
}