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
    public class LecturerDao : BaseDao
    {
        // Literally copy pasted from StudentDao.cs and slightly altered
        public List<Teacher> GetAllLecturers()
        {
            // Query joins 2 tables into 1 and shows id and first name
            string query = "SELECT teacherid, firstname FROM teacher JOIN Person ON Teacher.personid=person.personid;";
            SqlParameter[] sqlParameters = new SqlParameter[0];
            return ReadTables(ExecuteSelectQuery(query, sqlParameters));
        }
        
        
        private List<Teacher> ReadTables(DataTable dataTable)
        {
            // Create lecturers list
            List<Teacher> Lecturers = new List<Teacher>();

            // Loop through  each row in table
            foreach (DataRow dr in dataTable.Rows)
            {
                // Create teacher and add to list
                Teacher teacher = new Teacher()
                {
                    Number = (int)dr["teacherid"],
                    Name = (string)(dr["firstname"].ToString())
                };
                Lecturers.Add(teacher);
            }
            return Lecturers;
        }
    }
}
