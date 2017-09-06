using IoC.Web.Controllers;
using IoC.Web.Models;
using IoC.Web.Services;
using Moq;
using System.Collections.Generic;
using System.Web.Mvc;
using Xunit;

namespace IoC.Web.Test
{
    public class SalesControllerTests
    {
        [Fact]
        public void IndexSucceedsTest()
        {
            var logger = new Mock<ILoggerService>();
            var service = new Mock<ISalesService>();
            var empService = new Mock<IEmployeeService>();
            service.Setup(svc => svc.GetSalesForEmployee(10))
                .Returns(new List<Sale>(new List<Sale> { new Sale() {  Amount = 1200.00M } }));
            empService.Setup(svc => svc.GetEmployee(10))
                .Returns(new Employee() { Id = 10, FirstName = "Sally", LastName = "Test" });
            var controller = new SalesController(logger.Object, service.Object, empService.Object);

            var result = controller.Index(10) as ViewResult;
            Assert.NotNull(result);
            var model = result.Model as SalesViewModel;
            Assert.NotNull(model);
            Assert.NotNull(model.SalesPerson);
            Assert.Equal("Sally", model.SalesPerson.FirstName);
            Assert.Equal(1, model.Sales.Count);
        }
    }
}
