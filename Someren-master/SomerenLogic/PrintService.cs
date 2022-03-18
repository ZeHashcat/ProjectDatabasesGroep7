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
    public class PrintService
    {
        //Baton passing method between UI and DAL.
        public void Print(Exception ex)
        {
            PrintDao printDao = new PrintDao();
            printDao.ErrorLog(ex);
        }
    }
}
