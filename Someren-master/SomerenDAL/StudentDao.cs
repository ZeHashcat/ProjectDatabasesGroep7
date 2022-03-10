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
            //query joins 2 tables into 1 and shows id and full name
            string query = "SELECT studentid, firstname, lastname FROM student JOIN Person ON Student.personid=person.personid;";
            SqlParameter[] sqlParameters = new SqlParameter[0];
            return ReadTables(ExecuteSelectQuery(query, sqlParameters));
        }

        private List<Student> ReadTables(DataTable dataTable)
        {
            List<Student> students = new List<Student>();

            foreach (DataRow dr in dataTable.Rows)
            {
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
