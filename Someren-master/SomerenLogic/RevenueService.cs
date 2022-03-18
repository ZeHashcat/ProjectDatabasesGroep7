using SomerenDAL;
using SomerenModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace SomerenLogic
{
    public class RevenueService
    {
        RevenueDao Revenuedb;

        public RevenueService()
        {
            Revenuedb = new RevenueDao();
        }

        // Get the revenue of a specified time frame
        public Revenue GetRevenue(DateTime startDate, DateTime endDate)
        {
            Revenue revenue = Revenuedb.GetRevenue(startDate, endDate);
            return revenue;
        }

        // Get the revenue of all time
        public Revenue GetRevenue()
        {
            Revenue revenue = Revenuedb.GetRevenue();
            return revenue;
        }
    }
}
