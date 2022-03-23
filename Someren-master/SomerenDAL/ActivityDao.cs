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
    }
}
