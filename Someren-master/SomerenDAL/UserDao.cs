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
    public class UserDao : BaseDao
    {
        public List<User> GetAllUsers()
        {
            // Query gets the full user table
            string query = "SELECT [Username], [Password], [SecretQuestion], [SecretAnswer], [SALT] FROM [User];";
            SqlParameter[] sqlParameters = new SqlParameter[0];
            return ReadTables(ExecuteSelectQuery(query, sqlParameters));
        }

        private List<User> ReadTables(DataTable dataTable)
        {
            // Create users list
            List<User> users = new List<User>();

            // Loop through each row in table
            foreach (DataRow dr in dataTable.Rows)
            {
                // Create user and add to list
                User user = new User()
                {
                    Username = (string)dr["[Username]"],
                    Password = new HashWithSaltResult((string)dr["Password"], (string)dr["SALT"]),
                    SecretQuestion = (string)(dr["SecretQuestion"]),
                    SecretAnswer = new HashWithSaltResult((string)dr["SecretAnswer"], (string)dr["SALT"])
                };
                users.Add(user);
            }
            return users;
        }
    }
}
