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

        /// <summary>
        /// Resolve requested type from container
        /// </summary>
        /// <param name="requestContext"></param>
        /// <param name="controllerType"></param>
        /// <returns></returns>
        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            return _container.Resolve(controllerType) as Controller;
        }
    }
}