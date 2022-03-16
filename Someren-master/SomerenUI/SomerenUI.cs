using SomerenLogic;
using SomerenModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace SomerenUI
{
    public partial class SomerenUI : Form
    {
        public SomerenUI()
        {
            InitializeComponent();
        }

        private void SomerenUI_Load(object sender, EventArgs e)
        {
            showPanel("Dashboard");
            //Outputs Session start to ErrorLog.txt
            //string path = Path.Combine(Environment.CurrentDirectory, @"ErrorLog\", "ErrorLog.txt");
            //using (StreamWriter writer = new StreamWriter(path, true))
            //{
            //    writer.WriteLine($"╔═══╗───────────╔═══╗\n║╔═╗║───────────║╔═╗║\n║║─╚╬═╦══╦╗╔╦══╗╚╝╔╝║\n║║╔═╣╔╣╔╗║║║║╔╗║──║╔╝\n║╚╩═║║║╚╝║╚╝║╚╝║──║║\n╚═══╩╝╚══╩══╣╔═╝──╚╝\n────────────║║\n────────────╚╝\nSession Start: {DateTime.Now}");
            //    writer.Close();
            //}
        }

        private void showPanel(string panelName)
        {
            if (panelName == "Dashboard")
            {
                // Hide all other panels
                pnlStudents.Hide();
                pnlLecturers.Hide();
                pnlRooms.Hide();

                // Show dashboard
                pnlDashboard.Show();
                imgDashboard.Show();
            }
            else if (panelName == "Students")
            {
                // Hide all other panels
                pnlDashboard.Hide();
                imgDashboard.Hide();
                pnlLecturers.Hide();
                pnlRooms.Hide();

                // Show students
                pnlStudents.Show();             

                try
                {
                    // Fill the students listview within the students panel with a list of students
                    StudentService studService = new StudentService(); ;
                    List<Student> studentList = studService.GetStudents(); ;

                    // Clear the listview before filling it again
                    listViewStudents.Clear();

                    // Adds columns to the listview, took us a while to figure out that we needed this for it to work our way
                    listViewStudents.Columns.Add("Student ID", 100, HorizontalAlignment.Center);
                    listViewStudents.Columns.Add("First Name", 100, HorizontalAlignment.Center);
                    listViewStudents.Columns.Add("Last Name", 100, HorizontalAlignment.Center);

                    // Adds data to listview columns
                    foreach (Student s in studentList)
                    {
                        ListViewItem li = new ListViewItem(s.Number.ToString()); ;
                        ListViewItem.ListViewSubItem fName = new ListViewItem.ListViewSubItem(li, s.FirstName);
                        ListViewItem.ListViewSubItem lName = new ListViewItem.ListViewSubItem(li, s.LastName);
                        li.SubItems.Add(fName);
                        li.SubItems.Add(lName);
                        listViewStudents.Items.Add(li);
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show("Something went wrong while loading the students: " + e.Message);
                }
            }
            else if (panelName == "Lecturers")
            {
                // Hide all other panels
                pnlDashboard.Hide();
                imgDashboard.Hide();
                pnlStudents.Hide();
                pnlRooms.Hide();

                // Show lecturers
                pnlLecturers.Show();

                try
                {
                    // Fill the lecturers listview within the lecturers panel with a list of lecturers
                    LecturerService lecturerService = new LecturerService(); ;
                    List<Teacher> lecturerList = lecturerService.GetLecturers(); ;

                    // Clear the listview before filling it again
                    listViewLecturers.Clear();

                    // Adds columns to the listview, took us a while to figure out that we needed this for it to work our way
                    listViewLecturers.Columns.Add("Name", 100, HorizontalAlignment.Center);
                    listViewLecturers.Columns.Add("ID", 100, HorizontalAlignment.Center);

                    // Adds data to listview columns
                    foreach (Teacher t in lecturerList)
                    {
                        ListViewItem li = new ListViewItem(t.Name); ;
                        ListViewItem.ListViewSubItem id = new ListViewItem.ListViewSubItem(li, t.Number.ToString());
                        li.SubItems.Add(id);
                        listViewLecturers.Items.Add(li);
                    }
                }
                catch (Exception e)
                {
                MessageBox.Show("Something went wrong while loading the teachers: " + e.Message);
                }
            }
            else if (panelName == "Rooms")
            {
                // Hide all other panels
                pnlDashboard.Hide();
                imgDashboard.Hide();
                pnlStudents.Hide();
                pnlLecturers.Hide();

                // Show Rooms
                pnlRooms.Show();

                try
                {
                    // Fill the rooms listview within the rooms panel with a list of rooms
                    RoomService romService = new RoomService();
                    List<Room> roomList = romService.GetRooms();

                    // Clear the listview before filling it again
                    listViewRooms.Clear();

                    // Adds columns to the listview, took us a while to figure out that we needed this for it to work our way
                    listViewRooms.Columns.Add("Room Nr.", 100, HorizontalAlignment.Center);
                    listViewRooms.Columns.Add("Room Type", 100, HorizontalAlignment.Center);
                    listViewRooms.Columns.Add("Amount of Beds", 100, HorizontalAlignment.Center);

                    // Adds data to listview columns
                    foreach (Room room in roomList)
                    {
                        string roomType;

                        if (room.Type)
                        {
                            roomType = "Teacher";
                        }
                        else
                        {
                            roomType = "Student";
                        }

                        ListViewItem li = new ListViewItem(room.Number.ToString());

                        li.SubItems.Add(roomType);
                        li.SubItems.Add(room.Capacity.ToString());

                        listViewRooms.Items.Add(li);
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show("Something went wrong while loading the rooms: " + e.Message);
                }
            }
        }

        private void dashboardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void dashboardToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            showPanel("Dashboard");
        }

        private void label1_Click(object sender, EventArgs e)
        {
            // What is this? I mean I know what this is but why is this here?
        }

        private void imgDashboard_Click(object sender, EventArgs e)
        {
            MessageBox.Show("What happens in Someren, stays in Someren!");
        }

        private void studentsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            showPanel("Students");
        }

        private void lecturersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            showPanel("Lecturers");
        }
        private void roomsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            showPanel("Rooms");
        }

        private void drinkSupplyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            showPanel("DrinkSupply");
        }
    }
}
