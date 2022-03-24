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
    public class StudentService
    {
        StudentDao studentdb;

        public StudentService()
        {
            studentdb = new StudentDao();
        }

        public List<Student> GetStudents()
        {
            List<Student> students = studentdb.GetAllStudents();
            return students;
        }

        public List<Student> GetStudentsFromActivity(int ActivityID)
        {
            List<Student> students = studentdb.GetStudentsFromActivity(ActivityID);
            return students;
        }

        public void AddStudentToActivity(int ActivityID, int StudentID)
        {
            studentdb.AddStudentToActivity(ActivityID, StudentID);
        }
    }
}
