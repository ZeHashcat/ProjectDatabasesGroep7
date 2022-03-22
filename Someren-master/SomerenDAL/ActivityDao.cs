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
    // Literally copy pasted from StudentDao.cs and slightly altered
    public class ActivityDao : BaseDao
    {
        public List<Activity> GetAllActivities()
        {
            // Query retrieves all needed data.
            string query = "SELECT ActivityID, [Description], StartDateTime, EndDateTime FROM Activity;";
            SqlParameter[] sqlParameters = new SqlParameter[0];
            return ReadTables(ExecuteSelectQuery(query, sqlParameters));
        }
        private List<Activity> ReadTables(DataTable dataTable)
        {
            // Create rooms list
            List<Activity> activities = new List<Activity>();

            // Loop through each row in table
            foreach (DataRow dr in dataTable.Rows)
            {
                // Create room and add to list
                Activity activity = new Activity()
                {
                    ActivityId = (int)dr["ActivityID"],
                    ActivityName = (string)(dr["Description"]),
                    StartDate = ((DateTime)dr["StartDateTime"]),
                    EndDate = ((DateTime)dr["EndDateTime"])
                };
                activities.Add(activity);
            }
            return activities;
        }

        // Query adds activity to database.
        public void AddActivity(string activityName, DateTime startDate, DateTime endDate)
        {  
            string query = "INSERT INTO Activity (Description, StartDateTime, EndDateTime) VALUES(@ActivityName, @StartTime, @EndTime);";
            SqlParameter[] sqlParameters = new SqlParameter[3];
            sqlParameters[0] = new SqlParameter("@ActivityName", activityName);
            sqlParameters[1] = new SqlParameter("@StartTime", startDate.ToString("yyyy-M-dd HH:mm:ss"));
            sqlParameters[2] = new SqlParameter("@EndTime", endDate.ToString("yyyy-M-dd HH:mm:ss"));
            ExecuteEditQuery(query, sqlParameters);
        }

        //Query deletes activity from the database, related tables got their foreign keys set to cascade.
        public void DeleteActivity(int activityId)
        {
            string query = "DELETE FROM Activity WHERE ActivityId = @activityId; ";
            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@ActivityId", activityId);
            ExecuteEditQuery(query, sqlParameters);
        }

        // Query updates all values of a row with with new or the same ones.
        public void ChangeActivity(int activityId, string activityName, DateTime startDate, DateTime endDate)
        {
            
            string query = "UPDATE Activity SET Description = @Description, StartDateTime = @StartTime, EndDateTime = @EndTime WHERE ActivityID = @ActivityID;";
            SqlParameter[] sqlParameters = new SqlParameter[4];
            sqlParameters[0] = new SqlParameter("@ActivityID", activityId);
            sqlParameters[1] = new SqlParameter("@Description", activityName);
            sqlParameters[2] = new SqlParameter("@StartTime", startDate.ToString("yyyy-M-dd HH:mm:ss"));
            sqlParameters[3] = new SqlParameter("@EndTime", endDate.ToString("yyyy-M-dd HH:mm:ss"));
            ExecuteEditQuery(query, sqlParameters);
        }
    }
}
