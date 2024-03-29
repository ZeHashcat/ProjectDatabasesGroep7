﻿using SomerenLogic;
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
using System.Security.Cryptography;

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

            //Displays login screen.
            LoginUI loginUI = new LoginUI();
            loginUI.ShowDialog();
        }

        private void HideAllPanels()
        {
            pnlDashboard.Hide();
            imgDashboard.Hide();
            pnlStudents.Hide();
            pnlRooms.Hide();
            pnlLecturers.Hide();
            pnlRevenue.Hide();
            pnlCashRegister.Hide();
            pnlDrinkSupply.Hide();
            pnlActivities.Hide();
            pnlParticants.Hide();
            pnlSupervisors.Hide();
        }

        private void showPanel(string panelName)
        {
            if (panelName == "Dashboard")
            {
                // Hide all other panels
                HideAllPanels();

                // Show dashboard
                pnlDashboard.Show();
                imgDashboard.Show();
            }
            else if (panelName == "Students")
            {
                // Hide all other panels
                HideAllPanels();

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
                HideAllPanels();

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
                HideAllPanels();

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
                HideAllPanels();

                // Show Cash Register
                pnlCashRegister.Show();
                try
                {
                    // Get drinks data from SQL server
                    DrinkSupplyService drinkService = new DrinkSupplyService();
                    List<Drink> drinkSupplyList = drinkService.GetAllDrinkSupply();

                    // Clear the listview before filling it again
                    listViewDrinkSupply.Clear();

                    // Adds columns to the listview, took us a while to figure out that we needed this for it to work our way
                    listViewDrinkSupply.Columns.Add("Drink ID", 59, HorizontalAlignment.Center);
                    listViewDrinkSupply.Columns.Add("Drink Name", 100, HorizontalAlignment.Center);
                    listViewDrinkSupply.Columns.Add("Sales Price", 100, HorizontalAlignment.Center);
                    listViewDrinkSupply.Columns.Add("Voucher Amount", 0, HorizontalAlignment.Center);
                    listViewDrinkSupply.Columns.Add("Quantity", 100, HorizontalAlignment.Center);

                    // Adds data to listview columns
                    foreach (Drink drink in drinkSupplyList)
                    {
                        ListViewItem li = new ListViewItem(drink.DrinkId.ToString());
                        li.SubItems.Add(drink.DrinkName);
                        li.SubItems.Add($"€{drink.SalesPrice:0.00}");
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
                    listViewStudents2.Clear();

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
                HideAllPanels();

                // Show Rooms
                pnlRevenue.Show();
                try
                {
                    // Get the revenue of all time
                    RevenueService revenuService = new RevenueService();
                    Revenue revenue = revenuService.GetRevenue();

                    // Clear the listview before filling it again
                    listViewRevenueReport.Clear();

                    // Adds columns to the listview
                    listViewRevenueReport.Columns.Add("Sales", 100, HorizontalAlignment.Center);
                    listViewRevenueReport.Columns.Add("Turnover", 100, HorizontalAlignment.Center);
                    listViewRevenueReport.Columns.Add("Amount of customers", 120, HorizontalAlignment.Center);

                    ListViewItem li = new ListViewItem(revenue.Sales.ToString());

                    li.SubItems.Add($"€{revenue.Turnover:0.00}");
                    li.SubItems.Add(revenue.AmountOfCustomers.ToString());

                    listViewRevenueReport.Items.Add(li);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Something went wrong with getting the revenue report!" + ex.Message.ToString());
                }
            }
            else if (panelName == "DrinkSupply")
            {
                // Hide all other panels
                HideAllPanels();

                // Show Drink supply
                pnlDrinkSupply.Show();

                try
                {

                    // Get drinks data from SQL server
                    DrinkSupplyService drinkService = new DrinkSupplyService();
                    List<Drink> drinkSupplyList = drinkService.GetDrinkSupply();

                    // Clear the listview before filling it again
                    listViewDrinkSupply2.Clear();

                    // Adds columns to the listview
                    listViewDrinkSupply2.Columns.Add("Drink Name", 100, HorizontalAlignment.Center);
                    listViewDrinkSupply2.Columns.Add("Sales Price", 100, HorizontalAlignment.Center);
                    listViewDrinkSupply2.Columns.Add("Quantity", 100, HorizontalAlignment.Center);

                    // Store DrinkId in listview with invisible column
                    listViewDrinkSupply2.Columns.Add("ID", 0, HorizontalAlignment.Center);

                    // Create image list and add images
                    ImageList imageList = new ImageList();
                    imageList.ImageSize = new Size(16, 16);
                    imageList.Images.Add("ThumbsUp", Properties.Resources.thumbsup);
                    imageList.Images.Add("ThumbsDown", Properties.Resources.thumbsdown);

                    // Add imagelist to liestview
                    listViewDrinkSupply2.SmallImageList = imageList;

                    // Adds data to listview columns
                    foreach (Drink drink in drinkSupplyList)
                    {
                        ListViewItem li = new ListViewItem();
                        li.Text = drink.DrinkName;
                        li.SubItems.Add(drink.SalesPrice.ToString());
                        li.SubItems.Add(drink.Quantity.ToString());
                        li.SubItems.Add(drink.DrinkId.ToString());

                        // Check stock to show wich image from the image list has to be used
                        if (drink.Quantity < 10)
                            li.ImageKey = "ThumbsDown";
                        else
                            li.ImageKey = "ThumbsUp";
                        listViewDrinkSupply2.Items.Add(li);
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show("Something went wrong while loading the drink supply: " + e.Message);
                }

            }
            else if (panelName == "Activities")
            {
                // Hide all other panels
                HideAllPanels();

                // Show Activities panel
                pnlActivities.Show();

                //Reset input/output boxes.
                lblActId.Text = "Activity ID number:           ...";
                lblDescriptionFull.Text = "Full description:";
                textBoxDescription.Text = string.Empty;
                textBoxTimeStart.Text = string.Empty;
                textBoxTimeEnd.Text = string.Empty;
                dateTimePickerActStart.Text = DateTime.Now.ToString();
                dateTimePickerActEnd.Text = DateTime.Now.ToString();


                try
                {
                    // Get activities data from SQL server
                    ActivityService activityService = new ActivityService();
                    List<Activity> activitiesList = activityService.GetAllActivities();

                    // Clear the listview before filling it again
                    listViewActivities.Clear();

                    // Adds data to listview columns
                    foreach (Activity activity in activitiesList)
                    {
                        ListViewItem li = new ListViewItem(activity.ActivityName);
                        li.SubItems.Add(activity.ActivityId.ToString());
                        li.SubItems.Add(activity.StartDate.ToString());
                        li.SubItems.Add(activity.EndDate.ToString());
                        listViewActivities.Items.Add(li);
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show("Something went wrong while loading the Activities: " + e.Message);
                }
            }
            else if (panelName == "Participants")
            {
                // Hide all other panels
                HideAllPanels();
                

                // Show Participants panel
                pnlParticants.Show();
                listViewActivity.Items.Add("sadf");
                try
                {
                    // Get activities data from SQL server
                    ActivityService activityService = new ActivityService();
                    List<Activity> activitiesList = activityService.GetAllActivities();

                    // Clear the listview before filling it again
                    listViewParticipant.Clear();
                    listViewActivity.Clear();

                    // Make columns
                    listViewActivity.Columns.Add("Activity Name", 150, HorizontalAlignment.Center);
                    listViewActivity.Columns.Add("Activity ID", 100, HorizontalAlignment.Center);

                    // Adds data to listview columns
                    foreach (Activity activity in activitiesList)
                    {
                        ListViewItem li = new ListViewItem(activity.ActivityName);
                        li.SubItems.Add(activity.ActivityId.ToString());
                        //li.SubItems.Add(activity.StartDate.ToString());
                        //li.SubItems.Add(activity.EndDate.ToString());
                        listViewActivity.Items.Add(li);
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show("Something went wrong while loading the Participants: " + e.Message);
                }
            }
            else if (panelName == "Supervisors")
            {
                // Hide all other panels
                HideAllPanels();

                // Show Supervisors
                pnlSupervisors.Show();

                PrintService printService = new PrintService();

                try
                {
                    // Get Activities data from SQL server
                    
                 ActivityService activityService = new ActivityService();
                    List<Activity> activitiesList = activityService.GetAllActivities();

                    // Clear the listview before filling it again
                 listViewActivities2.Clear();
                 
                    //clear supervisors when no activity has yet been selected
                    listViewActivitySupervisors.Clear();

                    // Adds columns to the listview
                    listViewActivities2.Columns.Add("Activity ID", 100, HorizontalAlignment.Center);
                    listViewActivities2.Columns.Add("Description", 100, HorizontalAlignment.Center);
                    listViewActivities2.Columns.Add("Start Time", 150, HorizontalAlignment.Center);
                    listViewActivities2.Columns.Add("End Time", 150, HorizontalAlignment.Center);

                    // Adds data to listview columns
                    foreach (Activity activity in activitiesList)
                    {
                        ListViewItem li = new ListViewItem(activity.ActivityId.ToString());
                        li.SubItems.Add(activity.ActivityName);
                        li.SubItems.Add(activity.StartDate.ToString());
                        li.SubItems.Add(activity.EndDate.ToString());
                        listViewActivities2.Items.Add(li);
                    }
                }
                catch (Exception e)
                {
                    printService.Print(e);
                    MessageBox.Show("Something went wrong while loading the Activities: " + e.Message);
                } 
                try
                {
                    // Fill the lecturers listview within the lecturers panel with a list of lecturers
                    LecturerService lecturerService = new LecturerService();
                    List<Teacher> lecturerList = lecturerService.GetLecturers();

                    comboBoxLecturers.ResetText();
                    comboBoxLecturers.Items.Clear();

                    // Adds data to listview columns
                    foreach (Teacher t in lecturerList)
                    {
                        comboBoxLecturers.Items.Add(t.Name + " " + t.Number.ToString());
                    }
                }
                catch (Exception e)
                {
                    printService.Print(e);
                    MessageBox.Show("Something went wrong while loading the teachers: " + e.Message);
                }                
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

                    li.SubItems.Add($"€{revenue.Turnover:0.00}");
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
            MessageBox.Show("What happens in Someren, stays in Someren! ;)");
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
            lbl_totalMoney.Text = $"Total Price:";
            lbl_TotalVoucher.Text = $"Amount of Vouchers:";
            showPanel("CashRegister");
        }

        private void listViewDrinkSupply_SelectedIndexChanged(object sender, EventArgs e)
        {
            double totalMoney = 0;
            int totalVouchers = 0;

            //Sums up all the prices in both munnies and vouchers, then outputs those to the text labels.
            foreach (ListViewItem Drink in listViewDrinkSupply.SelectedItems)
            {
                totalMoney += double.Parse(Drink.SubItems[2].Text.Split('€')[1]);
                totalVouchers += int.Parse(Drink.SubItems[3].Text);
            }
            lbl_totalMoney.Text = $"Total Price: €{totalMoney:0.00}";
            lbl_TotalVoucher.Text = $"Amount of Vouchers: {totalVouchers}";
        }

        // If clicked, add a alcoholic drink to database and update listview
        private void buttonAdd_Click(object sender, EventArgs e)
        {
            DrinkSupplyService drinkService = new DrinkSupplyService();
            Drink drink = new Drink();
            PrintService printService = new PrintService();
            try
            {
                try
                {
                    drink.DrinkId = drinkService.GetHighestDrinkID() + 1;
                    drink.DrinkName = textBoxDrinkName.Text;
                    drink.SalesPrice = double.Parse(textBoxSalePrice.Text);
                    drink.Quantity = int.Parse(textBoxQuantity.Text);
                    drink.VAT = 21m;
                    if (drink.SalesPrice > 0 && drink.SalesPrice <= 5)
                        drink.VoucherAmount = 1;
                    else if (drink.SalesPrice > 5 && drink.SalesPrice <= 10)
                        drink.VoucherAmount = 2;
                    else if (drink.SalesPrice > 10 && drink.SalesPrice <= 15)
                        drink.VoucherAmount = 3;
                    else if (drink.SalesPrice > 15 && drink.SalesPrice <= 20)
                        drink.VoucherAmount = 4;
                    drink.Sold = 0;
                }   
                catch (Exception ex)
                {
                    printService.Print(ex);
                    throw new Exception("Enter valid value");
                }
                drinkService.AddDrink(drink);
            }
            catch(Exception ex)
            {
                MessageBox.Show("Could not add drink: " + ex.Message);
            }            

            showPanel("DrinkSupply");
        }

        // If clicked, selected drink will be deleted from the the database including its past transactions and update listview
        private void buttonDelete_Click(object sender, EventArgs e)
        {
            PrintService printService = new PrintService();
            try
            {
                if (!(listViewDrinkSupply2.SelectedItems.Count > 0))
                    throw new Exception("Select a row again");
                int drinkId = int.Parse(listViewDrinkSupply2.SelectedItems[0].SubItems[3].Text);
                DrinkSupplyService drinkService = new DrinkSupplyService();
                drinkService.DeleteDrink(drinkId);
            }
            catch (Exception ex)
            {
                printService.Print(ex);
                MessageBox.Show("Something went wrong trying to delete the drink: " + ex.Message);
            }
            textBoxDrinkName.Text = "";
            textBoxSalePrice.Text = "";
            textBoxQuantity.Text = "";

            showPanel("DrinkSupply");
        }

        // If clicked, alter a drink (name, quantity, sale price) in the database with textbox values
        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            PrintService printService = new PrintService();
            string newDrinkName;
            double salePrice;
            int quantity;
            try
            {
                if (!(listViewDrinkSupply2.SelectedItems.Count > 0))
                    throw new Exception("Select a row again");
                string originalDrinkName = listViewDrinkSupply2.SelectedItems[0].SubItems[0].Text;
                try
                {
                    salePrice = double.Parse(textBoxSalePrice.Text);
                    newDrinkName = textBoxDrinkName.Text;
                    quantity = int.Parse(textBoxQuantity.Text);
                }
                catch(Exception ex)
                {
                    printService.Print(ex);
                    throw new Exception("Enter valid value");
                }                
                DrinkSupplyService drinkService = new DrinkSupplyService();
                drinkService.UpdateDrink(originalDrinkName, newDrinkName, salePrice, quantity);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Something went wrong updating: " + ex.Message);
            }
            
            showPanel("DrinkSupply");
        }

        // Show selected listview item in textboxes for ease of use
        private void listViewDrinkSupply2_MouseClick(object sender, MouseEventArgs e)
        {
            textBoxDrinkName.Text = listViewDrinkSupply2.SelectedItems[0].SubItems[0].Text;
            textBoxSalePrice.Text = listViewDrinkSupply2.SelectedItems[0].SubItems[1].Text;
            textBoxQuantity.Text = listViewDrinkSupply2.SelectedItems[0].SubItems[2].Text;
        }

        private void activitiesToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            showPanel("Activities");
        }

        private void supervisorsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            showPanel("Supervisors");
        }

        private void listViewActivities2_MouseClick(object sender, MouseEventArgs e)
        {
            fillListviewSupervisor();
        }

        // If clicked supervisor will be add to the database and listview will be updated
        private void buttonAddSupervisor_Click(object sender, EventArgs e)
        {
            PrintService printService = new PrintService();

            try
            {
                ActivitySupervisorService activitySupervisorService = new ActivitySupervisorService();
                List<Supervisor> supervisors = activitySupervisorService.GetAllActivitySupervisors();

                if (listViewActivities2.SelectedItems.Count <= 0)
                    throw new Exception("select the activity where you want to add a supervisor");
                int activityId = int.Parse(listViewActivities2.SelectedItems[0].SubItems[0].Text);
                if (comboBoxLecturers.SelectedIndex == -1)
                    throw new Exception("Select a lecturer to add.");

                string lecturer = comboBoxLecturers.SelectedItem.ToString();

                string[] lecid = lecturer.Split(' ');
                int lecturerid = int.Parse(lecid[1]);

                foreach (Supervisor supervisor in supervisors)
                {
                    if (lecturerid == supervisor.TeacherId && activityId == supervisor.ActivityId)
                        throw new Exception("You cannot add a lecturer that is already a supervisor for this activity.");
                }

                activitySupervisorService.AddActivitySupervisor(lecturerid, activityId);
                fillListviewSupervisor();
            }
            catch(Exception ex)
            {
                printService.Print(ex);
                MessageBox.Show("Could not add supervisor: \n" + ex.Message);
            }
        }

        // If clicked supervisor will be deleted from the database and listview will be updated
        private void buttonDeleteSupervisor_Click(object sender, EventArgs e)
        {
            PrintService printService = new PrintService();

            try
            {
                if (listViewActivities2.SelectedItems.Count <= 0)
                    throw new Exception("select the activity where you want to delete a supervisor");
                int activityId = int.Parse(listViewActivities2.SelectedItems[0].SubItems[0].Text);
                if (listViewActivitySupervisors.SelectedItems.Count == 0)
                    throw new Exception("Select a supervisor to delete.");
                int lecturerId = int.Parse(listViewActivitySupervisors.SelectedItems[0].SubItems[0].Text);
                ActivitySupervisorService activitySupervisorService = new ActivitySupervisorService();
                DialogResult dialogAnswer = MessageBox.Show("Are you sure you want to delete a supervisor", "Delete Supervisor", MessageBoxButtons.YesNo);
                if (dialogAnswer == DialogResult.Yes)
                {
                    activitySupervisorService.DeleteActivitySupervisor(lecturerId, activityId);
                }
                fillListviewSupervisor();

                
            }
            catch (Exception ex)
            {
                printService.Print(ex);
                MessageBox.Show("Could not delete supervisor: \n" + ex.Message);
            }
        }

        public void fillListviewSupervisor()
        {
            PrintService printService = new PrintService();

            try
            {
                int activityId = int.Parse(listViewActivities2.SelectedItems[0].SubItems[0].Text);

                // Get Activities data from SQL server
                ActivitySupervisorService activitySupervisorService = new ActivitySupervisorService();
                List<Supervisor> activitySupervisorList = activitySupervisorService.GetActivitySupervisors(activityId);

                // Clear the listview before filling it again
                listViewActivitySupervisors.Clear();

                // Adds columns to the listview
                listViewActivitySupervisors.Columns.Add("Teacher ID", 100, HorizontalAlignment.Center);
                listViewActivitySupervisors.Columns.Add("Activity ID", 100, HorizontalAlignment.Center);

                // Adds data to listview columns
                foreach (Supervisor supervisor in activitySupervisorList)
                {
                    ListViewItem li = new ListViewItem(supervisor.TeacherId.ToString());
                    li.SubItems.Add(supervisor.ActivityId.ToString());
                    listViewActivitySupervisors.Items.Add(li);
                }
            }
            catch (Exception ex)
            {
                printService.Print(ex);
                MessageBox.Show("Could not load supervisors for selected activity.\n" + ex.Message);
            }
        }    
        //Adds relevant data to textboxes and calander when selecting a row, foreach was used to avoid headaches with index not valid exceptions.
        private void listViewActivities_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (ListViewItem activity in listViewActivities.SelectedItems)
            {
                lblDescriptionFull.Text = $"Full description:       {activity.SubItems[0].Text}";
                textBoxDescription.Text = activity.SubItems[0].Text;
                lblActId.Text = $"Activity ID number:           {activity.SubItems[1].Text}";
                dateTimePickerActStart.Text = activity.SubItems[2].Text;
                textBoxTimeStart.Text = $"{Convert.ToDateTime(activity.SubItems[2].Text):HH:mm}";
                dateTimePickerActEnd.Text = activity.SubItems[3].Text;
                textBoxTimeEnd.Text = $"{Convert.ToDateTime(activity.SubItems[3].Text):HH:mm}";
            }
        }

        //Adds activity to the database.
        private void buttonActAdd_Click(object sender, EventArgs e)
        {
            PrintService errorControl = new PrintService();
            ActivityService activityService = new ActivityService();

            try
            {
                //Runs validator to check for exceptions.
                ActivityValidator(false, true, true);

                //writes row to database.
                activityService.AddActivity(textBoxDescription.Text, DateTime.Parse($"{dateTimePickerActStart.Value.ToString("d-M-yyyy")} {textBoxTimeStart.Text}"), DateTime.Parse($"{dateTimePickerActEnd.Value.ToString("d-M-yyyy")} {textBoxTimeEnd.Text}"));               
            }
            catch (Exception ex)
            {
                errorControl.Print(ex);
                MessageBox.Show("Oh noes, something went wrong:\n\n" + ex.Message);
            }

            //refreshes panel.
            showPanel("Activities");
        }

        //Deletes row from the database.
        private void buttonActDelete_Click(object sender, EventArgs e)
        {
            PrintService errorControl = new PrintService();
            ActivityService activityService = new ActivityService();
            List<Activity> activities = activityService.GetAllActivities();

            try
            {
                //Validator checks if there are no rows selected.
                ActivityValidator(true, false, false);

                //Pops a dialogue box in your face because you don't know what you're doing.
                DialogResult dialogResult = MessageBox.Show("Are you sure that you wish to remove this activity?’", "Delete Activity", MessageBoxButtons.YesNo);

                //If you click Yes in the dialogue box then the selected row will be deleted.
                if (dialogResult == DialogResult.Yes)
                {
                    activityService.DeleteActivity(int.Parse(listViewActivities.SelectedItems[0].SubItems[1].Text));
                }
            }
            catch(Exception ex)
            {
                errorControl.Print(ex);
                MessageBox.Show("Oh noes, something went wrong:\n\n" + ex.Message);
            }

            showPanel("Activities");
        }

        private void buttonActChange_Click(object sender, EventArgs e)
        {
            PrintService errorControl = new PrintService();
            ActivityService activityService = new ActivityService();
            List<Activity> activities = activityService.GetAllActivities();

            try
            {
                //Runs validator which checks everything.
                ActivityValidator(true, true, false);

                activityService.ChangeActivity(int.Parse(listViewActivities.SelectedItems[0].SubItems[1].Text), textBoxDescription.Text, DateTime.Parse($"{dateTimePickerActStart.Value.ToString("d-M-yyyy")} {textBoxTimeStart.Text}"), DateTime.Parse($"{dateTimePickerActEnd.Value.ToString("d-M-yyyy")} {textBoxTimeEnd.Text}"));
            }
            catch (Exception ex)
            {
                errorControl.Print(ex);
                MessageBox.Show("Oh noes, something went wrong:\n\n" + ex.Message);
            }

            showPanel("Activities");
        }

        private void ActivityValidator(bool selectCheck, bool inOutputCheck, bool nameCheck)
        {
            PrintService errorControl = new PrintService();
            ActivityService activityService = new ActivityService();
            List<Activity> activities = activityService.GetAllActivities();
            DateTime startDate;
            DateTime endDate;

            if(selectCheck)
            {
                //Checks if there are no rows selected.
                if (listViewActivities.SelectedItems.Count == 0)
                {
                    throw new Exception("Select a row to change/delete!");
                }
            }

            if (inOutputCheck)
            {
                //Checks if description textbox is empty.
                if (textBoxDescription.Text == string.Empty)
                {
                    throw new Exception("Description cannot be empty!");
                }

                //Checks if time textboxes are empty.
                if(textBoxTimeStart.Text == string.Empty || textBoxTimeEnd.Text == string.Empty)
                {
                    throw new Exception("Start and end time cannot be empty!");
                }

                //tries to convert calender date and textbox time to DateTime in order to see if it's valid.
                try
                {
                    startDate = DateTime.Parse($"{dateTimePickerActStart.Value.ToString("d-M-yyyy")} {textBoxTimeStart.Text}");
                    endDate = DateTime.Parse($"{dateTimePickerActEnd.Value.ToString("d-M-yyyy")} {textBoxTimeEnd.Text}");
                }
                catch (Exception ex)
                {
                    errorControl.Print(ex);
                    throw new Exception($"Enter a valid time!");
                }                

                //Checks if start date is later then end date.
                if (startDate > endDate)
                {
                    throw new Exception("Starting date and time can't be later then the end date and time!");
                }
            }

            if(nameCheck)
            {
                //Checks if there is already an activity with the same name in the database because every activity may only occur once. (Apparently...)
                foreach (Activity activity in activities)
                {
                    if (activity.ActivityName == textBoxDescription.Text)
                    {
                        throw new Exception("Activity is already in the list!\n\nYou wanted the same activity multiple times?\n\nWell... \n\n\n\nSucks for you!\n\n(But seriously, just change the description slightly.)");
                    }
                }
            }
        }

        private void studentsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            showPanel("Participants");
        }

        private void btnShowAllStudents_Click(object sender, EventArgs e)
        {
            try
            {
                // Fill the students listview within the students panel with a list of students
                StudentService studService = new StudentService(); ;
                List<Student> studentList = studService.GetStudents(); ;

                // Clear the listview before filling it again
                listViewParticipant.Clear();

                // Adds columns to the listview, took us a while to figure out that we needed this for it to work our way
                listViewParticipant.Columns.Add("Student ID", 100, HorizontalAlignment.Center);
                listViewParticipant.Columns.Add("First Name", 100, HorizontalAlignment.Center);
                listViewParticipant.Columns.Add("Last Name", 100, HorizontalAlignment.Center);

                // Adds data to listview columns
                foreach (Student s in studentList)
                {
                    ListViewItem li = new ListViewItem(s.Number.ToString()); ;
                    ListViewItem.ListViewSubItem fName = new ListViewItem.ListViewSubItem(li, s.FirstName);
                    ListViewItem.ListViewSubItem lName = new ListViewItem.ListViewSubItem(li, s.LastName);
                    li.SubItems.Add(fName);
                    li.SubItems.Add(lName);
                    listViewParticipant.Items.Add(li);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Something went wrong while loading the students: " + ex.Message);
            }
        }

        private void listViewActivity_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void btnAddStudentActivity_Click(object sender, EventArgs e)
        {
            try
            {
                StudentService studService = new StudentService();
                studService.AddStudentToActivity(int.Parse(listViewActivity.SelectedItems[0].SubItems[1].Text), int.Parse(listViewParticipant.SelectedItems[0].SubItems[0].Text));
            }
            catch(Exception ex)
            {
                MessageBox.Show("Something went wrong while loading the students: " + ex.Message);
            }
            
        }

        private void buttonShowParticipant_Click(object sender, EventArgs e)
        {
            showParticipants();
        }

        private void btnDeleteStudentFromActivity_Click(object sender, EventArgs e)
        {
            var confirmResult = MessageBox.Show("Are you sure you want to delete this student from the activity?", "Delete Participant", MessageBoxButtons.YesNo);
            if(confirmResult == DialogResult.Yes)
            {
                StudentService studService = new StudentService();
                studService.DeleteStudentFromActivity(int.Parse(listViewActivity.SelectedItems[0].SubItems[1].Text), int.Parse(listViewParticipant.SelectedItems[0].SubItems[0].Text));
            }
            showParticipants();
        }
        private void showParticipants()
        {
            try
            {
                // Clear the listview before filling it again
                listViewParticipant.Clear();

                // Fill the students listview within the students panel with a list of students
                StudentService studService = new StudentService();
                List<Student> studentList = studService.GetStudentsFromActivity(int.Parse(listViewActivity.SelectedItems[0].SubItems[1].Text));



                // Adds columns to the listview, took us a while to figure out that we needed this for it to work our way
                listViewParticipant.Columns.Add("Student ID", 100, HorizontalAlignment.Center);
                listViewParticipant.Columns.Add("First Name", 100, HorizontalAlignment.Center);
                listViewParticipant.Columns.Add("Last Name", 100, HorizontalAlignment.Center);

                // Adds data to listview columns
                foreach (Student s in studentList)
                {
                    ListViewItem li = new ListViewItem(s.Number.ToString()); ;
                    ListViewItem.ListViewSubItem fName = new ListViewItem.ListViewSubItem(li, s.FirstName);
                    ListViewItem.ListViewSubItem lName = new ListViewItem.ListViewSubItem(li, s.LastName);
                    li.SubItems.Add(fName);
                    li.SubItems.Add(lName);
                    listViewParticipant.Items.Add(li);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Something went wrong while loading the students: " + ex.Message);
            }
        }

        private void TestPasswordHasher(byte[] saltBytes)
        { 
            
            PasswordWithSaltHasher pwHasher = new PasswordWithSaltHasher();
            HashWithSaltResult hRSha256 = pwHasher.HashWithSalt("Ultra_Safe_p455w0rD", saltBytes, SHA256.Create());
            HashWithSaltResult hRSha512 = pwHasher.HashWithSalt("Ultra_Safe_p455w0rD", saltBytes, SHA512.Create());

            //lbl256.Text = $"{hRSha256.Hash}\n {hRSha256.Salt}";
            //lbl512.Text = $"{hRSha512.Hash}\n {hRSha512.Salt}";
        }

        private void loginToolStripMenuItem_Click(object sender, EventArgs e)
        {
            showPanel("login");
            
        }

    }
}
