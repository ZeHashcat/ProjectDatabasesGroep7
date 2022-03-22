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
        public List<Supervisor> GetActivitySupervisors(int activityId)
        {
            List<Supervisor> supervisor = Supervisordb.GetActivitySupervisors(activityId);
            return supervisor;
        }
        public List<Supervisor> GetAllActivitySupervisors()
        {
            List<Supervisor> supervisor = Supervisordb.GetAllActivitySupervisors();
            return supervisor;
        }        
        public void AddActivitySupervisor(int lecturerId, int activityId)
        {
            Supervisordb.AddActivitySupervisor(lecturerId, activityId);
        }
        public void DeleteActivitySupervisor(int lecturerId, int activityId)
        {
            Supervisordb.DeleteActivitySupervisor(lecturerId, activityId);
        }
    }
}
