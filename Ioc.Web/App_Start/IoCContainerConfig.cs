using IoC.Web.Controllers;
using IoC.Web.Services;

namespace IoC.Web.App_Start
{
    public static class IoCContainerConfig
    {
        public static void Configure(IContainer container)
        {
            // Configure custom IoC container

            //  register types
            container.Register<HomeController, HomeController>();
            container.Register<SalesController, SalesController>();
            container.Register<ILoggerService, LoggerService>(LifeCycleType.Singleton);
            container.Register<IEmployeeService, EmployeeService>(LifeCycleType.Singleton);
            container.Register<ISalesService, SalesService>(LifeCycleType.Singleton);
        }
    }
}