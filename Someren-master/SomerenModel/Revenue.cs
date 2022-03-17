using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SomerenModel
{
    public class Revenue
    {
        public int Sales { get; set; } // Number of drinks sold.
        public double Turnover { get; set; } // The amount of sales times the cost of the drink
        public int AmountOfCustomers { get; set; } // The amount of customers that bought at least one drink.
    }
}
