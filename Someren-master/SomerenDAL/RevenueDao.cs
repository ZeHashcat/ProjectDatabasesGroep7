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
        // Get the revenue of a specified time frame
        public Revenue GetRevenue(DateTime startDate, DateTime endDate)
        {
            try
            {
                // Query joins 2 tables into 1 and and get the amount of sales the turnover and the amount of customers
                string query = ($"SELECT COUNT(TransactionID) AS Sales, SUM(SalePrice) AS Turnover, COUNT(DISTINCT PersonID) AS Customers FROM Drinks JOIN DrinkSupply ON Drinks.DrinkID = DrinkSupply.DrinkID WHERE TransactionTime >= '{startDate}' AND TransactionTime < '{endDate}'; ");
                SqlParameter[] sqlParameters = new SqlParameter[0];
                return ReadTables(ExecuteSelectQuery(query, sqlParameters));
            }
            catch(Exception e)
            {
                PrintDao Print = new PrintDao();
                Print.ErrorLog(e);
                throw new Exception("No sales found in this time");
            }
        }

        // Get the revenue of all time
        public Revenue GetRevenue()
        {
            // Query joins 2 tables into 1 and and get the amount of sales the turnover and the amount of customers
            string query = ($"SELECT COUNT(TransactionID) AS Sales, SUM(SalePrice) AS Turnover, COUNT(DISTINCT PersonID) AS Customers FROM Drinks JOIN DrinkSupply ON Drinks.DrinkID = DrinkSupply.DrinkID; ");
            SqlParameter[] sqlParameters = new SqlParameter[0];
            return ReadTables(ExecuteSelectQuery(query, sqlParameters));
        }
        private Revenue ReadTables(DataTable dataTable)
        {
            // Create revenue
            Revenue revenue = new Revenue();

            // Get attributes for revenue
            foreach (DataRow dr in dataTable.Rows)
            {
                revenue.Sales = (int)dr["Sales"];
                revenue.Turnover = (double)dr["Turnover"];
                revenue.AmountOfCustomers = (int)dr["Customers"];
            }
            return revenue;
        }
    }
}
