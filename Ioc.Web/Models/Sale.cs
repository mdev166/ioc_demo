using System;
using System.Globalization;

namespace IoC.Web.Models
{
    public class Sale
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }

        public string FormattedAmount {
            get {
                return string.Format(CultureInfo.InvariantCulture, "{0:C2}", Amount);
            }
        }
    }
}