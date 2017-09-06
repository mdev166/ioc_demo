using IoC.Web.Models;
using System.Collections.Generic;

namespace IoC.Web.Services
{
    public interface ISalesService
    {
        IList<Sale> GetSalesForEmployee(int employeeId);
    }
}
