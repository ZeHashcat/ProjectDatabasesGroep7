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
            string path = Path.Combine(Environment.CurrentDirectory, @"ErrorLog.txt");
            using (StreamWriter writer = new StreamWriter(path, true))
            {
                writer.WriteLine($"╔═══╗───────────╔═══╗\n║╔═╗║───────────║╔═╗║\n║║─╚╬═╦══╦╗╔╦══╗╚╝╔╝║\n║║╔═╣╔╣╔╗║║║║╔╗║──║╔╝\n║╚╩═║║║╚╝║╚╝║╚╝║──║║\n╚═══╩╝╚══╩══╣╔═╝──╚╝\n────────────║║\n────────────╚╝\nSession Start: {DateTime.Now}");
                writer.Close();
            }
        }

        private void hideAllPanels()
        {
            pnlDashboard.Hide();
            imgDashboard.Hide();
            pnlStudents.Hide();
            pnlRooms.Hide();
            pnlLecturers.Hide();
            pnlRevenue.Hide();
        }

        private void showPanel(string panelName)
        {
            if (panelName == "Dashboard")
            {
                // Hide all other panels
                hideAllPanels();

                // Show dashboard
                pnlDashboard.Show();
                imgDashboard.Show();
            }
            else if (panelName == "Students")
            {
                // Hide all other panels
                hideAllPanels();

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
                hideAllPanels();

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
                hideAllPanels();

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
            else if (panelName == "CashRegister")
            {
                // Hide all other panels
                pnlDashboard.Hide();
                imgDashboard.Hide();
                pnlStudents.Hide();
                pnlLecturers.Hide();
                pnlRooms.Hide();
                panel1.Hide();

                // Show Cash Register
                pnlCashRegister.Show();
                try
                {
                    // Get drinks data from SQL server
                    DrinkSupplyService drinkService = new DrinkSupplyService();
                    List<Drink> drinkSupplyList = drinkService.GetDrinkSupply();

                    // Clear the listview before filling it again
                    listViewDrinkSupply.Clear();

                    // Adds columns to the listview, took us a while to figure out that we needed this for it to work our way
                    listViewDrinkSupply.Columns.Add("Drink ID", 100, HorizontalAlignment.Center);
                    listViewDrinkSupply.Columns.Add("Drink Name", 100, HorizontalAlignment.Center);
                    listViewDrinkSupply.Columns.Add("Sales Price", 100, HorizontalAlignment.Center);
                    listViewDrinkSupply.Columns.Add("Voucher Amount", 0, HorizontalAlignment.Center);
                    listViewDrinkSupply.Columns.Add("Quantity", 100, HorizontalAlignment.Center);

                    // Adds data to listview columns
                    foreach (Drink drink in drinkSupplyList)
                    {
                        ListViewItem li = new ListViewItem(drink.DrinkId.ToString());
                        li.SubItems.Add(drink.DrinkName);
                        li.SubItems.Add(drink.SalesPrice.ToString());
                        li.SubItems.Add(drink.VoucherAmount.ToString());
                        li.SubItems.Add(drink.Quantity.ToString());
                        listViewDrinkSupply.Items.Add(li);
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show("Something went wrong while loading the drinks: " + e.Message);
                }
                try
                {
                    // Get student data from SQL server
                    StudentService studService = new StudentService(); ;
                    List<Student> studentList = studService.GetStudents(); ;

                    // Clear the listview before filling it again
                    listViewStudents.Clear();

                    // Adds columns to the listview, took us a while to figure out that we needed this for it to work our way
                    listViewStudents2.Columns.Add("Student ID", 100, HorizontalAlignment.Center);
                    listViewStudents2.Columns.Add("Name", 100, HorizontalAlignment.Center);


                    // Adds data to listview columns
                    foreach (Student s in studentList)
                    {
                        ListViewItem li = new ListViewItem(s.Number.ToString()); ;
                        li.SubItems.Add($"{s.FirstName} {s.LastName}");
                        listViewStudents2.Items.Add(li);
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show("Something went wrong while loading the students: " + e.Message);
                }
            }
            else if (panelName == "RevenueReport")
            {
                // Hide all other panels
                hideAllPanels();

                // Show Rooms
                pnlRevenue.Show();

                // Get the revenue of all time
                RevenueService revenuService = new RevenueService();
                Revenue revenue = revenuService.GetRevenue();
                // Clear the listview before filling it again
                listViewRevenueReport.Clear();

                // Adds columns to the listview
                listViewRevenueReport.Columns.Add("Sales", 100, HorizontalAlignment.Center);
                listViewRevenueReport.Columns.Add("Turnover", 100, HorizontalAlignment.Center);
                listViewRevenueReport.Columns.Add("Amount of customers", 100, HorizontalAlignment.Center);

                ListViewItem li = new ListViewItem(revenue.Sales.ToString());

                li.SubItems.Add($"€{ revenue.Turnover.ToString("0.00")}");
                li.SubItems.Add(revenue.AmountOfCustomers.ToString());

                listViewRevenueReport.Items.Add(li);
            }
        }

        // Displays the revenue report whenever a new date is selected
        private void displayRevenue()
        {
            DateTime startTime = dateTimePickerStartDate.Value;
            DateTime endTime = dateTimePickerEndDate.Value;
            listViewRevenueReport.Clear();
            if (startTime < endTime && endTime < DateTime.Now)
            {
                try
                {
                    // Get the revenue of the specified time frame
                    RevenueService revenuService = new RevenueService();
                    Revenue revenue = revenuService.GetRevenue(startTime, endTime);
                    // Clear the listview before filling it again
                    

                    // Adds columns to the listview
                    listViewRevenueReport.Columns.Add("Sales", 100, HorizontalAlignment.Center);
                    listViewRevenueReport.Columns.Add("Turnover", 100, HorizontalAlignment.Center);
                    listViewRevenueReport.Columns.Add("Amount of customers", 100, HorizontalAlignment.Center);

                    ListViewItem li = new ListViewItem(revenue.Sales.ToString());

                    li.SubItems.Add($"€{ revenue.Turnover.ToString("0.00")}");
                    li.SubItems.Add(revenue.AmountOfCustomers.ToString());

                    listViewRevenueReport.Items.Add(li);
                }
                catch (Exception e)
                {
                    MessageBox.Show("Something went wrong while loading the revenue: " + e.Message);
                }
            }
            else
            {
                MessageBox.Show($"{startTime.Date} till {endTime.Date} is an Invalid date!");
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
        private void revenueReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            showPanel("RevenueReport");
        }

        private void dateTimePickerStartDate_ValueChanged(object sender, EventArgs e)
        {
            displayRevenue();
        }

        private void dateTimePickerEndDate_ValueChanged(object sender, EventArgs e)
        {
            displayRevenue();
        }
        private void drinkSupplyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            showPanel("DrinkSupply");
		}
        private void cashRegisterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            showPanel("CashRegister");
        }
        private void button1_Click(object sender, EventArgs e)
        {
            DrinkSupplyService drinkSupplyService = new DrinkSupplyService();
            PrintService printService = new PrintService();
            int personId;
            string argument;
            List<string> drinksList = new List<string>();

            try
            {
                try
                {
                    //Adds all selected DrinkID's to drinksList in string format for use in query, also converts value from StudentID column to string.
                    foreach (ListViewItem drink in listViewDrinkSupply.SelectedItems)
                    {
                        drinksList.Add(drink.SubItems[0].Text);
                    }
                    argument = listViewStudents2.SelectedItems[0].Text;
                }
                catch (Exception ex)
                {
                    printService.Print(ex);
                    throw new Exception("Please select rows from both columns!");
                }

                //Retrieves PersonID by passing on the StudentID.
                personId = drinkSupplyService.GetPersonId(argument);

                //Writes a transaction to the database for every selected drink and student.
                foreach(string drink in drinksList)
                {
                    drinkSupplyService.WriteTransaction(drink, personId);
                    try
                    {
                        //If checkBoxVoucher is checked vouchers will be used to pay, thus vouchers will be needed to be added to database table Voucher.
                        if (checkBoxVoucher.Checked)
                        {
                            int totalVouchers = 0;

                            //Sums up the price in vouchers.
                            foreach (ListViewItem drinkItem in listViewDrinkSupply.SelectedItems)
                            {
                                if(drinkItem.SubItems[0].Text == drink)
                                {
                                    totalVouchers = int.Parse(drinkItem.SubItems[3].Text);
                                }
                            }

                            //For each voucher needed add one to database along with a PersonID and TransactionID
                            List<int> transactionIds = drinkSupplyService.GetTransactionIds(totalVouchers);

                            foreach (int transactionId in transactionIds)
                            {
                                drinkSupplyService.WriteVoucher(transactionId.ToString(), personId.ToString());
                            }
                            
                        }
                    }
                    catch (Exception ex)
                    {
                        printService.Print(ex);
                        throw new Exception("Failed to add vouchers to database!");
                    }
                }

                
            }
            catch (Exception ex)
            {
                printService.Print(ex);
                MessageBox.Show($"Something went wrong with checkout! {ex.Message}");
            }

            //Refreshes the panel.
            showPanel("DrinkSupply");
        }

        private void listViewDrinkSupply_SelectedIndexChanged(object sender, EventArgs e)
        {
            double totalMoney = 0;
            int totalVouchers = 0;

            //Sums up all the prices in both munnies and vouchers, then outputs those to the text labels.
            foreach (ListViewItem Drink in listViewDrinkSupply.SelectedItems)
            {
                totalMoney += double.Parse(Drink.SubItems[2].Text);
                totalVouchers += int.Parse(Drink.SubItems[3].Text);
            }
            lbl_totalMoney.Text = $"Total Price: €{totalMoney:0.00}";
            lbl_TotalVoucher.Text = $"Amount of Vouchers: {totalVouchers}";
        }
    }
}
