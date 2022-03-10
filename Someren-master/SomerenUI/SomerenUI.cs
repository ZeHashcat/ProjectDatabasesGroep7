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
        }

        private void showPanel(string panelName)
        {

            if (panelName == "Dashboard")
            {
                // hide all other panels
                pnlStudents.Hide();
                pnlLecturers.Hide();

                // show dashboard
                pnlDashboard.Show();
                imgDashboard.Show();
            }
            else if (panelName == "Students")
            {
                // hide all other panels
                pnlDashboard.Hide();
                imgDashboard.Hide();
                pnlLecturers.Hide();

                // show students
                pnlStudents.Show();

                try
                {
                    // fill the students listview within the students panel with a list of students
                    StudentService studService = new StudentService(); ;
                    List<Student> studentList = studService.GetStudents(); ;

                    // clear the listview before filling it again
                    listViewStudents.Clear();

                    listViewStudents.Columns.Add("Name", 100, HorizontalAlignment.Center);
                    listViewStudents.Columns.Add("ID", 100, HorizontalAlignment.Center);
                    listViewStudents.Columns.Add("Birth Date", 100, HorizontalAlignment.Center);

                    foreach (Student s in studentList)
                    {
                        ListViewItem li = new ListViewItem(s.Name); ;
                        ListViewItem.ListViewSubItem id = new ListViewItem.ListViewSubItem(li, s.Number.ToString());
                        ListViewItem.ListViewSubItem dob = new ListViewItem.ListViewSubItem(li, s.BirthDate.ToString());
                        li.SubItems.Add(id);
                        li.SubItems.Add(dob);
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
                // hide all other panels
                pnlDashboard.Hide();
                imgDashboard.Hide();
                pnlStudents.Hide();

                // show students
                pnlLecturers.Show();

                try
                {
                    // fill the students listview within the students panel with a list of students
                    LecturerService lecturerService = new LecturerService(); ;
                    List<Teacher> lecturerList = lecturerService.GetLecturers(); ;

                    // clear the listview before filling it again
                    listViewLecturers.Clear();

                    listViewLecturers.Columns.Add("Name", 100, HorizontalAlignment.Center);
                    listViewLecturers.Columns.Add("ID", 100, HorizontalAlignment.Center);

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
                    MessageBox.Show("Something went wrong while loading the students: " + e.Message);
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
    }
}
