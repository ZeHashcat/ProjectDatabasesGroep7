using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SomerenDAL;
using SomerenModel;

namespace SomerenLogic
{
    public class ActivitySupervisorService
    {
        ActivitySupervisorDao Supervisordb;

        public ActivitySupervisorService()
        {
            Supervisordb = new ActivitySupervisorDao();
        }

        //Baton passing method between UI and DAL.
        public List<Supervisor> GetActivitySupervisors(int activityId)
        {
            List<Supervisor> supervisor = Supervisordb.GetActivitySupervisors(activityId);
            return supervisor;
        }

        //Baton passing method between UI and DAL.
        public List<Supervisor> GetAllActivitySupervisors()
        {
            List<Supervisor> supervisor = Supervisordb.GetAllActivitySupervisors();
            return supervisor;
        }

        //Baton passing method between UI and DAL.
        public void AddActivitySupervisor(int lecturerId, int activityId)
        {
            Supervisordb.AddActivitySupervisor(lecturerId, activityId);
        }

        //Baton passing method between UI and DAL.
        public void DeleteActivitySupervisor(int lecturerId, int activityId)
        {
            Supervisordb.DeleteActivitySupervisor(lecturerId, activityId);
        }
    }
}
