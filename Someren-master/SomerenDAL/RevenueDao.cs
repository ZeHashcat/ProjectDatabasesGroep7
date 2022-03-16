using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Collections.ObjectModel;
using SomerenModel;

namespace SomerenDAL
{
    public class RevenueDao : BaseDao
    {
        public Revenue GetRevenue(DateTime startDate, DateTime endDate)
        {
            // Query joins 2 tables into 1 and and get the amount of sales the turnover and the amount of customers
            string query = "SELECT COUNT(TransactionID), SUM(SalePrice), COUNT(DISTINCT PersonID) FROM Drinks JOIN DrinkSupply ON Drinks.DrinkID = DrinkSupply.DrinkID; ";
            SqlParameter[] sqlParameters = new SqlParameter[0];
            return ReadTables(ExecuteSelectQuery(query, sqlParameters));
        }
        private Revenue ReadTables(DataTable dataTable)
        {
            // Create lecturers list
            Revenue revenue = new Revenue();
            revenue.Sales = (int)dataTable.Rows[0][0];

            // Loop through  each row in table
            foreach (DataRow dr in dataTable.Rows)
            {

            }
            return revenue;
        }
    }
}
