using IoC.Web.Models;
using IoC.Web.Services;
using System.Web.Mvc;

namespace IoC.Web.Controllers
{
    public class SalesController : Controller
    {
        private readonly ISalesService _salesService;
        private readonly IEmployeeService _employeeService;
        private readonly ILoggerService _logger;

        public SalesController(ILoggerService logger, ISalesService salesService, IEmployeeService employeeService)
        {
            _logger = logger;
            _salesService = salesService;
            _employeeService = employeeService;
        }

        public ActionResult Index(int id)
        {
            // get sales
            var sales = _salesService.GetSalesForEmployee(id);
            // get employee
            var salesPerson = _employeeService.GetEmployee(id);
            _logger.Log("Sales About view");

            return View(new SalesViewModel() { Sales = sales, SalesPerson = salesPerson });
        }
    }
}