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
        public List<Supervisor> GetAllActivitySupervisors()
        {
            // Query to get a list of all activity supervisors
            string query = "SELECT teacherid, ActivityId FROM ActivitySupervisor";
            SqlParameter[] sqlParameters = new SqlParameter[0];
            return ReadTables(ExecuteSelectQuery(query, sqlParameters));
        }
        public List<Supervisor> GetActivitySupervisors(int activityId)
        {
            // Quary wity paratmeter to get a supervisors for the selected activity
            string query = "SELECT teacherid, ActivityId FROM ActivitySupervisor WHERE ActivityId=@ID";
            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@ID", activityId);
            return ReadTables(ExecuteSelectQuery(query, sqlParameters));
        }
        public void AddActivitySupervisor(int lecturerId, int activityId)
        {
            // Query to add an activity supervisors
            string query = "INSERT INTO ActivitySupervisor VALUES (@lecturerId, @activityId)";
            SqlParameter[] sqlParameters = new SqlParameter[2];
            sqlParameters[0] = new SqlParameter("@lecturerId", lecturerId);
            sqlParameters[1] = new SqlParameter("@activityId", activityId);
            ExecuteEditQuery(query, sqlParameters);
        }
        public void DeleteActivitySupervisor(int lecturerId, int activityId)
        {
            // Query to delete an activity supervisors
            string query = "DELETE FROM ActivitySupervisor WHERE TeacherId = @LecturerId AND ActivityId = @activityId";
            SqlParameter[] sqlParameters = new SqlParameter[2];
            sqlParameters[0] = new SqlParameter("@lecturerId", lecturerId);
            sqlParameters[1] = new SqlParameter("@activityId", activityId);
            ExecuteEditQuery(query, sqlParameters);
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
