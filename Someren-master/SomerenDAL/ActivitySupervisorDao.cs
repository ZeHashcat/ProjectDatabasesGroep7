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
    public class ActivitySupervisorDao : BaseDao
    {
        public List<Supervisor> GetActivitySupervisors(int activityId)
        {
            // 
            string query = "SELECT teacherid, ActivityId FROM ActivitySupervisor WHERE ActivityId=@ID";
            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@ID", activityId);
            return ReadTables(ExecuteSelectQuery(query, sqlParameters));
        }

        private List<Supervisor> ReadTables(DataTable dataTable)
        {
            // Create supervisors list
            List<Supervisor> supervisors = new List<Supervisor>();

            // Loop through  each row in table
            foreach (DataRow dr in dataTable.Rows)
            {
                // Create supervisor and add to list
                Supervisor supervisor = new Supervisor()
                {
                    TeacherId = (int)dr["TeacherId"],
                    ActivityId = (int)dr["ActivityId"]
                };
                supervisors.Add(supervisor);
            }
            return supervisors;
        }
    }
}
