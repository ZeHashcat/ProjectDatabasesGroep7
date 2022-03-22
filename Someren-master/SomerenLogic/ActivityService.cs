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
    //Literally copy pasted from StudentService.cs and slightly altered.
    public class ActivityService
    {
        ActivityDao activitydb;

        public ActivityService()
        {
            activitydb = new ActivityDao();
        }

        public List<Activity> GetAllActivities()
        {
            List<Activity> activities = activitydb.GetAllActivities();
            return activities;
        }

        public void AddActivity(string activityName, DateTime startDate, DateTime endDate)
        {
            activitydb.AddActivity(activityName, startDate, endDate);
        }

        public void DeleteActivity(int activityId)
        {
            activitydb.DeleteActivity(activityId);
        }
    }
}
