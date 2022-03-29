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
    public partial class LoginUI : Form
    {
        public LoginUI()
        {
            InitializeComponent();
            
        }

        private void HideAllPanels()
        {
            pnlLogin.Hide();
            pnlAddNewUser.Hide();
            pnlForgotPassword1.Hide();
            pnlForgotPassword2.Hide();
            pnlForgotPassword3.Hide();
        }

        private void showPanel(string panelName)
        {
            if (panelName == "Login")
            {
                // Hide all other panels
                HideAllPanels();

                // Show Login panel
                pnlLogin.Show();

                try
                {

                }
                catch (Exception e)
                {
                    MessageBox.Show("Something went wrong while logging in: " + e.Message);
                }
            }
            else if (panelName == "AddNewUser")
            {
                // Hide all other panels
                HideAllPanels();

                // Show students
                pnlAddNewUser.Show();

                try
                {

                }
                catch (Exception e)
                {
                    MessageBox.Show("Something went wrong while trying to add new user: " + e.Message);
                }
            }
            else if (panelName == "ForgotPassword")
            {
                // Hide all other panels
                HideAllPanels();

                // Show students
                pnlAddNewUser.Show();

                try
                {

                }
                catch (Exception e)
                {
                    MessageBox.Show("Something went wrong while trying to reset password: " + e.Message);
                }
            }
        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
