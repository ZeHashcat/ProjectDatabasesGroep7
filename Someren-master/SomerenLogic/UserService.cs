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
    //Literally copy pasted from StudentService.cs and slightly altered, new methods were added though.
    public class UserService
    {
        UserDao userdb;

        public UserService()
        {
            userdb = new UserDao();
        }

        //Returns list with all users. We might not need this anymore.
        public List<User> GetAllUsers()
        {
            List<User> Users = userdb.GetAllUsers();
            return Users;
        }

        //Baton passing method between UI and DAL.
        public string GetUserQuestion(string username)
        {
            return userdb.GetUserQuestion(username);
        }

        //Baton passing method between UI and DAL.
        public HashWithSaltResult GetUserAnswer(string username)
        {
            return userdb.GetUserAnswer(username);
        }
        public string GetSalt(string username)
        {
            return userdb.GetSalt(username);
        }

        //Baton passing method between UI and DAL.
        public void UpdatePassword(string username, string password)
        {
            userdb.UpdatePassword(username, password);
        }

        //Baton passing method between UI and DAL.
        public void AddUser(string username, HashWithSaltResult password, string secretQuestion, HashWithSaltResult secretAnswer)
        {
            userdb.AddUser(username, password, secretQuestion, secretAnswer);
        }

        //Baton passing method between UI and DAL.
        public string GetHashedPassword(string username)
        {
            return userdb.GetHashedPassword(username);
        }

    }
}
