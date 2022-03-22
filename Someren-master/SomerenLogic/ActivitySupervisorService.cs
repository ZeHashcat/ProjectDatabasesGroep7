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
        public List<Supervisor> GetAllSupervisors(d)
        {
            List<Supervisor> supervisor = Supervisordb.GetALLSupervisors();
            return supervisor;
        }
        public List<Supervisor> GetActivitySupervisors(int activityId)
        {
            List<Supervisor> supervisor = Supervisordb.GetActivitySupervisors(activityId);
            return supervisor;
        }
    }
}
