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
    public class DrinkSupplyDao : BaseDao
    {
        public List<DrinkSupply> GetAllDrinkSupply()
        {
            // Query joins 2 tables into 1 and shows id and full name
            string query = "SELECT studentid, firstname, lastname FROM student JOIN Person ON Student.personid=person.personid;";
            SqlParameter[] sqlParameters = new SqlParameter[0];
            return ReadTables(ExecuteSelectQuery(query, sqlParameters));
        }
        public List<DrinkSupply> GetDrinksWithStockGreaterThenOneAndCostMoreThenOneVoucher()
        {
            // Query joins 2 tables into 1 and shows id and full name
            string query = "SELECT DrinkName, firstname, lastname FROM student JOIN Person ON Student.personid=person.personid;";
            SqlParameter[] sqlParameters = new SqlParameter[0];
            return ReadTables(ExecuteSelectQuery(query, sqlParameters));
        }

        private List<DrinkSupply> ReadTables(DataTable dataTable)
        {
            // Create students list
            List<DrinkSupply> drinkSupply = new List<DrinkSupply>();

            // Loop through each row in table
            foreach (DataRow dr in dataTable.Rows)
            {
                // Create student and add to list
                DrinkSupply drink = new DrinkSupply()
                {
                    DrinkName = (string)(dr["DrinkName"].ToString()),
                    SalesPrice = (double)dr["SalePrice"],
                    VoucherAmount = (int)(dr["VoucherAmount"]),
                    VAT = (decimal)(dr["VAT"]),
                    Quantity = (int)(dr["Quantity"])
                };
                drinkSupply.Add(drink);
            }
            return drinkSupply;
        }
    }
}
