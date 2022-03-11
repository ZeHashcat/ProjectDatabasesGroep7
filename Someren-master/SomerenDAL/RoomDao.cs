using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Collections.ObjectModel;
using SomerenModel;

namespace SomerenDAL
{
    // Literally copy pasted from StudentDao.cs and slightly altered
    public class RoomDao : BaseDao
    {
        public List<Room> GetAllRooms()
        {
            // Query retrieves all needed data.
            string query = "SELECT roomID, Beds, Type FROM GuestRoom;";
            SqlParameter[] sqlParameters = new SqlParameter[0];
            return ReadTables(ExecuteSelectQuery(query, sqlParameters));
        }

        private List<Room> ReadTables(DataTable dataTable)
        {
            // Create rooms list
            List<Room> rooms = new List<Room>();

            // Loop through each row in table
            foreach (DataRow dr in dataTable.Rows)
            {
                string typeString = (string)(dr["Type"].ToString());

                // Create room and add to list
                Room room = new Room()
                {
                    Number = (int)dr["RoomID"],
                    Capacity = (int)(dr["Beds"]),
                    // If string == Teacher then Type = true
                    Type = (typeString == "Teacher")
                };
                rooms.Add(room);
            }
            return rooms;
        }
    }
}
