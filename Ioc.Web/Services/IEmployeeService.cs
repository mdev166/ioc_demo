using IoC.Web.Models;
using System.Collections.Generic;

namespace IoC.Web.Services
{
    public interface IEmployeeService
    {
        IList<Employee> GetEmployees();
        Employee GetEmployee(int employeeId);
    }
}
