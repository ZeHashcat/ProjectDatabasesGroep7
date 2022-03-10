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
        public List<Teacher> GetAllLecturers()
        {
            string query = "SELECT teacherid, firstname FROM teacher JOIN Person ON Teacher.personid=person.personid;";
            SqlParameter[] sqlParameters = new SqlParameter[0];
            return ReadTables(ExecuteSelectQuery(query, sqlParameters));
        }

        private List<Teacher> ReadTables(DataTable dataTable)
        {
            List<Teacher> Lecturers = new List<Teacher>();

            foreach (DataRow dr in dataTable.Rows)
            {
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
