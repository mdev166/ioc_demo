using IoC.Web.App_Start;
using IoC.Web.Controllers;
using IoC.Web.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace IoC.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            // create IoC container
            var container = new Container();
            IoCContainerConfig.Configure(container);

            var factory = new CustomControllerFactory(container);
            ControllerBuilder.Current.SetControllerFactory(factory);
        }
    }
}
