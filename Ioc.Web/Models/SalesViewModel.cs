using System.Collections.Generic;

namespace IoC.Web.Models
{
    public class SalesViewModel
    {
        public Employee SalesPerson { get; set; }
        public IList<Sale> Sales { get; set; }
    }
}