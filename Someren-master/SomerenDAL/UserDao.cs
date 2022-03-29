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

        // Query gets secret question for username
        public string GetUserQuestion(string username)
        {
            PrintDao printDao = new PrintDao();
            string query = "SELECT [SecretQuestion] FROM [User] WHERE [Username] = @Username;";
            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("Username", username);
            DataTable dataTable = ExecuteSelectQuery(query, sqlParameters);
            try
            {
                return dataTable.Rows[0].Field<string>("SecretQuestion").ToString();
            } 
            catch (Exception ex)
            {
                printDao.ErrorLog(ex);
                throw new Exception("No user found by that name!");
            }
        }

        // Query gets hashed secret answer and salt.
        public HashWithSaltResult GetUserAnswer(string username)
        {
            PrintDao printDao = new PrintDao();
            HashWithSaltResult hashWithSalt = new HashWithSaltResult("", "");
            string query = "SELECT [SecretAnswer], [SALT] FROM [User] WHERE [Username] = @Username;";
            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("Username", username);
            DataTable dataTable = ExecuteSelectQuery(query, sqlParameters);
            try
            {
                hashWithSalt.Hash = dataTable.Rows[0].Field<string>("SecretAnswer").ToString();
                hashWithSalt.Salt = dataTable.Rows[0].Field<string>("SALT").ToString();
                return hashWithSalt;
            }
            catch (Exception ex)
            {
                printDao.ErrorLog(ex);
                throw new Exception("No user found by that name!");
            }
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
