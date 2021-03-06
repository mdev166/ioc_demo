﻿using IoC.Web.Controllers;
using IoC.Web.Models;
using IoC.Web.Services;
using Moq;
using System.Collections.Generic;
using System.Web.Mvc;
using Xunit;

namespace IoC.Web.Test
{
    public class HomeControllerTests
    {
        [Fact]
        public void IndexRequestSucceedsTest()
        {
            var service = new Mock<IEmployeeService>();
            var logger = new Mock<ILoggerService>();
            service.Setup(svc => svc.GetEmployees())
                .Returns(new List<Employee>(new List<Employee> { new Employee() {  FirstName = "Terry", LastName = "Smith" } }));
            var controller = new HomeController(logger.Object, service.Object);

            var result = controller.Index() as ViewResult;

            Assert.NotNull(result);
            var model = result.Model as IList<Employee>;
            Assert.NotNull(model);
            Assert.Equal(1, model.Count);
            Assert.Equal("Terry", model[0].FirstName);
            Assert.Equal("Smith", model[0].LastName);
        }

        [Fact]
        public void AboutRequestSucceedsTest()
        {
            var service = new Mock<IEmployeeService>();
            var logger = new Mock<ILoggerService>();
            var controller = new HomeController(logger.Object, service.Object);

            var result = controller.About() as ViewResult;

            Assert.NotNull(result);
        }
    }
}
