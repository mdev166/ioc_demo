using IoC.Web.Services;
using System.Web.Mvc;

namespace IoC.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IEmployeeService _employeeService;
        private readonly ILoggerService _logger;

        public HomeController(ILoggerService logger, IEmployeeService employeeService)
        {
            _employeeService = employeeService;
            _logger = logger;
        }

        public ActionResult Index()
        {
            _logger.Log("Home Index view");

            // get employees
            var employees = _employeeService.GetEmployees();
            return View(employees);
        }

        public ActionResult About()
        {
            _logger.Log("Home About view");

            ViewBag.Message = "IoC demo app.";
            return View();
        }
    }
}