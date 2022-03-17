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
            string query = "SELECT DrinkID, DrinkName, SalePrice, VoucherAmount, VAT, Quantity FROM Drinks";
            SqlParameter[] sqlParameters = new SqlParameter[0];
            return ReadTables(ExecuteSelectQuery(query, sqlParameters));
        }
        public List<Drink> GetDrinkSupply()
        {
            // Query joins 2 tables into 1 and shows id and full name
            string query = "SELECT DrinkID, DrinkName, SalePrice, Quantity, VoucherAmount, VAT, Sold FROM Drinks WHERE Quantity > 1 AND VoucherAmount > 1 AND VAT > 9.00 ORDER BY Quantity DESC, SalePrice DESC, Sold DESC; ";
            SqlParameter[] sqlParameters = new SqlParameter[0];
            return ReadTables(ExecuteSelectQuery(query, sqlParameters));
        }
        public void AddDrink(Drink drink)
        {
            // Query joins 2 tables into 1 and shows id and full name
            string query = "INSERT INTO Drinks (DrinkID, DrinkName, SalePrice, Quantity, VAT, VoucherAmount, Sold) VALUES(@DrinkID, @DrinkName, @SalesPrice, @Quantity, @VAT, @VoucherAmount, @Sold);";
            SqlParameter[] sqlParameters = new SqlParameter[7];
            sqlParameters[0] = new SqlParameter("@DrinkID", drink.DrinkId);
            sqlParameters[1] = new SqlParameter("@DrinkName", drink.DrinkName);
            sqlParameters[2] = new SqlParameter("@SalesPrice", drink.SalesPrice);
            sqlParameters[3] = new SqlParameter("@Quantity", drink.Quantity);
            sqlParameters[4] = new SqlParameter("@VAT", drink.VAT);
            sqlParameters[5] = new SqlParameter("@VoucherAmount", drink.VoucherAmount);
            sqlParameters[6] = new SqlParameter("@Sold", drink.Sold);
            ExecuteEditQuery(query, sqlParameters);
        }
        public void UpdateDrink(string originalDrinkName, string newDrinkName, double salePrice, int quantity)
        {
            string query = "UPDATE Drinks SET DrinkName = @newDrinkName, salePrice = @salePrice, quantity = @quantity WHERE DrinkName = @originalDrinkName; ";
            SqlParameter[] sqlParameters = new SqlParameter[4];
            sqlParameters[0] = new SqlParameter("@newDrinkName", newDrinkName);
            sqlParameters[1] = new SqlParameter("@salePrice", salePrice);
            sqlParameters[2] = new SqlParameter("@quantity", quantity);
            sqlParameters[3] = new SqlParameter("@originalDrinkName", originalDrinkName);
            ExecuteEditQuery(query, sqlParameters);
        }
        public void DeleteDrink(int DrinkId)
        {
            string query = "DELETE FROM [Order] WHERE DrinkId = @DrinkId; DELETE FROM Drinks WHERE DrinkId = @DrinkId; ";
            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@DrinkId", DrinkId);
            ExecuteEditQuery(query, sqlParameters);
        }
        public int GetHighestDrinkID()
        {
            // Query joins 2 tables into 1 and shows id and full name
            string query = "SELECT MAX(DrinkID) AS HighestDrinkID FROM Drinks; ";
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
