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
        //Might Not need this anymore.
        public List<User> GetAllUsers()
        {
            // Query gets the full user table
            string query = "SELECT [Username], [Password], [SecretQuestion], [SecretAnswer], [SALT] FROM [User];";
            SqlParameter[] sqlParameters = new SqlParameter[0];
            return ReadTables(ExecuteSelectQuery(query, sqlParameters));
        }

        // Adds a new user to the database
        public void AddUser(string username, HashWithSaltResult password, string secretQuestion, HashWithSaltResult secretAnswer)
        {
            string query = "INSERT INTO [User] VALUES (@username, @password, @secretQuestion, @secretAnswer, @SALT);";
            SqlParameter[] sqlParameters = new SqlParameter[5];
            sqlParameters[0] = new SqlParameter("@username", username);
            sqlParameters[1] = new SqlParameter("@password", password.Hash);
            sqlParameters[2] = new SqlParameter("@secretQuestion", secretQuestion);
            sqlParameters[3] = new SqlParameter("@secretAnswer", secretAnswer.Hash);
            sqlParameters[4] = new SqlParameter("@SALT", password.Salt);
            ExecuteEditQuery(query, sqlParameters);
        }

        // Query gets secret question for username
        public string GetUserQuestion(string username)
        {
            PrintDao printDao = new PrintDao();
            string query = "SELECT [SecretQuestion] FROM [User] WHERE [Username] = @Username;";
            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@Username", username);
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
            sqlParameters[0] = new SqlParameter("@Username", username);
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

        // Query gets salt.
        public string GetSalt(string username)
        {
            PrintDao printDao = new PrintDao();
            string query = "SELECT [SALT] FROM [User] WHERE [Username] = @Username;";
            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@Username", username);
            DataTable dataTable = ExecuteSelectQuery(query, sqlParameters);
            try
            {
                return dataTable.Rows[0].Field<string>("SALT").ToString();
            }
            catch (Exception ex)
            {
                printDao.ErrorLog(ex);
                throw new Exception("No user found by that name!");
            }
        }

        // Query updates hashed password.
        public void UpdatePassword(string username, string password)
        {
            string query = "UPDATE [User] SET [Password] = @Password WHERE [username] = @username;";
            SqlParameter[] sqlParameters = new SqlParameter[2];
            sqlParameters[0] = new SqlParameter("@Password", password);
            sqlParameters[1] = new SqlParameter("@Username", username);
            ExecuteEditQuery(query, sqlParameters);
        }

        public string GetHashedPassword(string username)
        {
            PrintDao printDao = new PrintDao();
            string query = "SELECT [Password] FROM [User] WHERE [Username] = @Username;";
            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@Username", username);
            DataTable dataTable = ExecuteSelectQuery(query, sqlParameters);
            try
            {
                return dataTable.Rows[0].Field<string>("Password").ToString();
            }
            catch (Exception ex)
            {
                printDao.ErrorLog(ex);
                throw new Exception("No user found by that name!");
            }
        }

        //Might not need this anymore.
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
