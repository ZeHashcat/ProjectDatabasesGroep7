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
        public List<Drink> GetAllDrinkSupply()
        {
            // Query joins 2 tables into 1 and shows id and full name
            string query = "SELECT DrinkID, DrinkName, SalePrice, VoucherAmount, VAT, Quantity FROM DrinkSupply";
            SqlParameter[] sqlParameters = new SqlParameter[0];
            return ReadTables(ExecuteSelectQuery(query, sqlParameters));
        }
        public List<Drink> GetDrinkSupply()
        {
            // Query joins 2 tables into 1 and shows id and full name
            string query = "SELECT DrinkID, DrinkName, SalePrice, Quantity, VoucherAmount, VAT, Sold FROM DrinkSupply WHERE Quantity > 1 AND VoucherAmount > 1 ORDER BY Quantity DESC, SalePrice DESC, Sold DESC; ";
            SqlParameter[] sqlParameters = new SqlParameter[0];
            return ReadTables(ExecuteSelectQuery(query, sqlParameters));
        }
        public void AddDrink(Drink drink)
        {
            // Query joins 2 tables into 1 and shows id and full name
            string query = $"INSERT INTO DrinkSupply (DrinkID, DrinkName, SalePrice, Quantity, VAT, VoucherAmount, Sold) VALUES({drink.DrinkId}, {drink.SalesPrice}, {drink.Quantity}, {drink.VAT}, {drink.VoucherAmount}, {drink.Sold}),";
            SqlParameter[] sqlParameters = new SqlParameter[0];
            //sqlParameters["@DrinkID"]. = (drink.DrinkId).ToString();
            ExecuteEditQuery(query, sqlParameters);
        }
        public int GetHighestDrinkID()
        {
            // Query joins 2 tables into 1 and shows id and full name
            string query = "SELECT MAX(DrinkID) AS HighestDrinkID FROM DrinkSupply; ";
            SqlParameter[] sqlParameters = new SqlParameter[0];
            return readNumber(ExecuteSelectQuery(query, sqlParameters));
        }
        private int readNumber(DataTable dataTable)
        {
            int number = 0;
            // Loop through each row in table
            foreach (DataRow dr in dataTable.Rows)
            {
                // Create student and add to list
                number = (int)dr["HighestDrinkID"];
            }
            return number;
        }
        private List<Drink> ReadTables(DataTable dataTable)
        {
            // Create students list
            List<Drink> drinkSupply = new List<Drink>();

            // Loop through each row in table
            foreach (DataRow dr in dataTable.Rows)
            {
                // Create student and add to list
                Drink drink = new Drink()
                {
                    DrinkId = (int)dr["DrinkID"],
                    DrinkName = (string)(dr["DrinkName"]),
                    SalesPrice = (double)dr["SalePrice"],
                    VoucherAmount = (int)(dr["VoucherAmount"]),
                    VAT = (decimal)(dr["VAT"]),
                    Quantity = (int)(dr["Quantity"]),
                    Sold = (int)(dr["Sold"])
                };
                drinkSupply.Add(drink);
            }
            return drinkSupply;
        }
    }
}
