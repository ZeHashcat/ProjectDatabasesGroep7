using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SomerenModel
{
    public class Student
    { 
        public string FirstName { get; set; } // Sudent first name, e.g. Chris
        public string LastName { get; set; } // Sudent last name, e.g. Koersen
        public int Number { get; set; } // Student number, e.g. 474791
        
        // We don't need this yet according to rubric.
        // Public DateTime BirthDate { get; set; }
    }
}
