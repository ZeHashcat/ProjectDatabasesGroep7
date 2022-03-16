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
    class RevenueService
    {
        RevenueDao Revenuedb;

        public RevenueService()
        {
            Revenuedb = new RevenueDao();
        }

        public Revenue GetLecturers(DateTime startDate, DateTime endDate)
        {
            Revenue revenue = Revenuedb.GetRevenue(startDate, endDate);
            return revenue;
        }
    }
}
