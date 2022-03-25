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
    public class StudentDao : BaseDao
    {      
        public List<Student> GetAllStudents()
        {
            // Query joins 2 tables into 1 and shows id and full name
            string query = "SELECT studentid, firstname, lastname FROM student JOIN Person ON Student.personid=person.personid;";
            SqlParameter[] sqlParameters = new SqlParameter[0];
            return ReadTables(ExecuteSelectQuery(query, sqlParameters));
        }

        public List<Student> GetStudentsFromActivity(int ActivityID)
        {
            // Query joins 2 tables into 1 and shows id and full name
            string query = "SELECT Student.StudentID, firstname, lastname FROM student JOIN Person ON Student.personid=person.personid JOIN ActivityStudent ON Student.StudentID = ActivityStudent.StudentID WHERE ActivityID = @ActivityID;";
            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@ActivityID", ActivityID);
            return ReadTables(ExecuteSelectQuery(query, sqlParameters));
        }

        public void AddStudentToActivity(int ActivityID, int StudentID)
        {
            string queryCheckEntry = "SELECT COUNT(StudentID) FROM ActivityStudent WHERE StudentID = @StudentID AND ActivityID = @ActivityID;";
            SqlParameter[] sqlParametersCheckEntry = new SqlParameter[2];
            sqlParametersCheckEntry[0] = new SqlParameter("@StudentID", StudentID);
            sqlParametersCheckEntry[1] = new SqlParameter("@ActivityID", ActivityID);
            DataTable dataTable = ExecuteSelectQuery(queryCheckEntry, sqlParametersCheckEntry);
            int countEntries = dataTable.Rows[0].Field<int>(0);
            
            if(countEntries == 0)
            {
                // Query adds drink to database
                string query = "INSERT INTO ActivityStudent (StudentID, ActivityID) VALUES(@StudentID, @ActivityID);";
                SqlParameter[] sqlParameters = new SqlParameter[2];
                sqlParameters[0] = new SqlParameter("@StudentID", StudentID);
                sqlParameters[1] = new SqlParameter("@ActivityID", ActivityID);
                ExecuteEditQuery(query, sqlParameters);
            }
            else
            {
                throw new Exception("This student already participates in this activity");
            }
            
        }

        public void DeleteStudentFrom(int ActivityID, int StudentID)
        {
            // Query deletes drink and its past transactions
            string query = "DELETE FROM ActivityStudent WHERE ActivityID = @ActivityID AND StudentID = @StudentID; ";
            SqlParameter[] sqlParameters = new SqlParameter[2];
            sqlParameters[0] = new SqlParameter("@ActivityID", ActivityID);
            sqlParameters[1] = new SqlParameter("@StudentID", StudentID);
            ExecuteEditQuery(query, sqlParameters);
        }
        private List<Student> ReadTables(DataTable dataTable)
        {
            // Create students list
            List<Student> students = new List<Student>();

            // Loop through each row in table
            foreach (DataRow dr in dataTable.Rows)
            {
                // Create student and add to list
                Student student = new Student()
                {
                    Number = (int)dr["studentid"],
                    FirstName = (string)(dr["firstname"].ToString()),
                    LastName = (string)(dr["lastname"].ToString())
                };
                students.Add(student);
            }
            return students;
        }
    }
}
