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

        //Baton passing method between UI and DAL.
        public void AddActivity(string activityName, DateTime startDate, DateTime endDate)
        {
            activitydb.AddActivity(activityName, startDate, endDate);
        }

        //Baton passing method between UI and DAL.
        public void DeleteActivity(int activityId)
        {
            activitydb.DeleteActivity(activityId);
        }

        //Baton passing method between UI and DAL.
        public void ChangeActivity(int activityId, string activityName, DateTime startDate, DateTime endDate)
        {
            activitydb.ChangeActivity(activityId, activityName, startDate, endDate);
        }
    }
}
