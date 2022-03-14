using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SomerenModel
{
    public class Room
    {
        public int Number { get; set; } // RoomNumber, e.g. 206
        public int Capacity { get; set; } // Number of beds, either 4,6,8,12 or 16
        public bool Type { get; set; } // Student = false, teacher = true <- Would've been nice if we read this line before we made the DB and filled the tables
    }
}
