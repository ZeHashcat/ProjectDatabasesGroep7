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
    public class RoomService
    {
        RoomDao roomdb;

        public RoomService()
        {
            roomdb = new RoomDao();
        }

        public List<Room> GetRooms()
        {
            List<Room> rooms = roomdb.GetAllRooms();
            return rooms;
        }
    }
}
