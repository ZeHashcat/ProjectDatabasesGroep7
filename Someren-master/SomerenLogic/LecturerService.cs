﻿using SomerenDAL;
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
    public class LecturerService
    {
        LecturerDao Lecturerdb;

        public LecturerService()
        {
            Lecturerdb = new LecturerDao();
        }

        public List<Teacher> GetLecturers()
        {
            List<Teacher> lectures = Lecturerdb.GetAllLecturers();
            return lectures;
        }
    }
}
