using IoC.Web.Models;
using System;
using System.Collections.Generic;

namespace IoC.Web.Services
{
    public class SalesService : ISalesService
    {
        private readonly ILoggerService _logger;

        public SalesService(ILoggerService logger)
        {
            // additionally, could extend this by injecting a data respository
            //  and unit testing this service

            // log- Info, Warning or Error
            _logger = logger;
        }

        #region ISalesService methods

        public IList<Sale> GetSalesForEmployee(int employeeId)
        {
            _logger.Log("GetSalesForEmployee");

            return GetDummyData(employeeId);
        }

        #endregion

        private static IList<Sale> GetDummyData(int employeeId)
        {
            // return demo data
            List<Sale> sales = new List<Sale>();
            switch (employeeId)
            {
                case 1002:
                    sales.Add(new Sale { Id = 454213, Amount = 11010.01m, Date = new DateTime(2017, 06, 21) });
                    sales.Add(new Sale { Id = 454221, Amount = 11200.64m, Date = new DateTime(2017, 06, 29) });
                    sales.Add(new Sale { Id = 454270, Amount = 23492.00m, Date = new DateTime(2017, 07, 15) });
                    sales.Add(new Sale { Id = 454301, Amount = 17541.25m, Date = new DateTime(2017, 08, 01) });
                    break;

                case 1008:
                    sales.Add(new Sale { Id = 465213, Amount = 12002.23m, Date = new DateTime(2017, 06, 21) });
                    sales.Add(new Sale { Id = 465232, Amount = 21759.51m, Date = new DateTime(2017, 06, 30) });
                    sales.Add(new Sale { Id = 465500, Amount = 36785.17m, Date = new DateTime(2017, 07, 15) });
                    sales.Add(new Sale { Id = 465675, Amount = 13547.87m, Date = new DateTime(2017, 07, 31) });
                    break;

                case 1010:
                    sales.Add(new Sale { Id = 471213, Amount = 17500.23m, Date = new DateTime(2017, 06, 11) });
                    sales.Add(new Sale { Id = 472221, Amount = 11200.51m, Date = new DateTime(2017, 06, 29) });
                    sales.Add(new Sale { Id = 472270, Amount = 13492.00m, Date = new DateTime(2017, 07, 01) });
                    sales.Add(new Sale { Id = 474301, Amount = 14652.07m, Date = new DateTime(2017, 08, 01) });
                    break;

                case 1012:
                    sales.Add(new Sale { Id = 481213, Amount = 410621.75m, Date = new DateTime(2017, 08, 24) });
                    break;

                case 1013:
                    sales.Add(new Sale { Id = 491213, Amount = 21000.23m, Date = new DateTime(2017, 06, 21) });
                    sales.Add(new Sale { Id = 492221, Amount = 17231.51m, Date = new DateTime(2017, 06, 29) });
                    sales.Add(new Sale { Id = 493270, Amount = 33450.00m, Date = new DateTime(2017, 07, 15) });
                    sales.Add(new Sale { Id = 494301, Amount = 11641.07m, Date = new DateTime(2017, 08, 01) });
                    break;

                case 1024:
                    sales.Add(new Sale { Id = 505213, Amount = 28674.00m, Date = new DateTime(2017, 08, 17) });
                    sales.Add(new Sale { Id = 505218, Amount = 2171.00m, Date = new DateTime(2017, 08, 21) });
                    sales.Add(new Sale { Id = 505320, Amount = 18475.00m, Date = new DateTime(2017, 09, 01) });
                    break;

                case 1029:
                    sales.Add(new Sale { Id = 517274, Amount = 42674.00m, Date = new DateTime(2017, 08, 17) });
                    break;

                case 1031:
                    // none yet
                    break;
            }
            return sales;
        }
    }
}