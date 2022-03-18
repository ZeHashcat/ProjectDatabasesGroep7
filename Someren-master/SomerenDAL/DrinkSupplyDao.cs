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
        public List<Drink> GetDrinksWithStockGreaterThenOneAndCostMoreThenOneVoucher()
        {
            // Query joins 2 tables into 1 and shows id and full name
            string query = "SELECT DrinkName, firstname, lastname FROM student JOIN Person ON Student.personid=person.personid;";
            SqlParameter[] sqlParameters = new SqlParameter[0];
            return ReadTables(ExecuteSelectQuery(query, sqlParameters));
        }

        //Retrieves PersonID from Student Table.
        public int GetPersonId(string argument)
        {
            int personID = 0;
            string query = "SELECT StudentID, PersonID FROM Student";
            SqlParameter[] sqlParameters = new SqlParameter[0];
            DataTable students = ExecuteSelectQuery(query, sqlParameters);            

            foreach (DataRow row in students.Rows)
            {
                //Finds correct PersonID associated with given StudentID. 
                if ((int)row["StudentID"] == int.Parse(argument))
                {
                    personID = (int)row["PersonID"];                    
                }
            }
            return personID;
        }
        
        //Writes Transaction to a new row in the Database.
        public void WriteTransaction(string argument, string argumentTwo)
        {
            string query = "INSERT INTO [Order] (DrinkID, PersonID, TransactionTime) VALUES (@DrinkID, @PersonID, CURRENT_TIMESTAMP)";
            SqlParameter[] sqlParameters = new SqlParameter[2];            
            sqlParameters[0] = new SqlParameter("@DrinkID", argument);
            sqlParameters[1] = new SqlParameter("@PersonID", argumentTwo);            
            ExecuteEditQuery(query, sqlParameters);
            //Had to add another SqlParameter[] because otherwise it gives an error along the lines of parameter is already contained withing other SqlParameter collection.
            SqlParameter[] sqlParametersTwo = new SqlParameter[1];
            //Had to change @DrinkID to @DrinkIDTwo, otherwise same error.
            sqlParametersTwo[0] = new SqlParameter("@DrinkIDTwo", argument);
            query = "UPDATE [Drinks] SET Quantity = Quantity - 1, Sold = Sold + 1 WHERE DrinkID = @DrinkIDTwo";
            ExecuteEditQuery(query, sqlParametersTwo);
        }

        public List<int> GetTransactionIds(int argument)
        {
            List<int> transactionIds = new List<int>();
            string query = "SELECT TransactionID FROM (SELECT TOP (@Count) TransactionID FROM [Order] ORDER BY TransactionID desc)[Order] ORDER BY TransactionID ASC";
            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@Count", argument);
            DataTable transactions = ExecuteSelectQuery(query, sqlParameters);

            foreach (DataRow row in transactions.Rows)
            {
                transactionIds.Add((int)row["TransactionID"]);
            }
            return transactionIds;
        }

        public void WriteVoucher(string argument, string argumentTwo)
        {
            string query = "INSERT INTO [Voucher] (TransactionID, PersonID, TransactionTime) VALUES (@TransactionID, @PersonID, CURRENT_TIMESTAMP)";
            SqlParameter[] sqlParameters = new SqlParameter[2];
            sqlParameters[0] = new SqlParameter("@TransactionID", argument);
            sqlParameters[1] = new SqlParameter("@PersonID", argumentTwo);
            ExecuteEditQuery(query, sqlParameters);
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
                    Quantity = (int)(dr["Quantity"])
                };
                drinkSupply.Add(drink);
            }
            return drinkSupply;
        }
    }
}
