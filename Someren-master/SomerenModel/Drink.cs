using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SomerenModel
{
    public class Drink
    {
        public int DrinkId { get; set; }
        public string DrinkName { get; set; }
        public double SalesPrice { get; set; }
        public int VoucherAmount { get; set; }
        public decimal VAT { get; set; }
        public int Quantity { get; set; }    
        public int Sold { get; set; }
    }
}
