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
using System.Security.Cryptography;

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
            else if (panelName == "ForgotPassword1")
            {
                // Hide all other panels
                HideAllPanels();

                // Show students
                pnlForgotPassword1.Show();

                try
                {

                }
                catch (Exception e)
                {
                    MessageBox.Show("Something went wrong while trying to reset your password: " + e.Message);
                }
            }
            else if (panelName == "ForgotPassword2")
            {
                // Hide all other panels
                HideAllPanels();

                // Show students
                pnlForgotPassword2.Show();

                try
                {

                }
                catch (Exception e)
                {
                    MessageBox.Show("Something went wrong while trying to reset your password: " + e.Message);
                }
            }
            else if (panelName == "ForgotPassword3")
            {
                // Hide all other panels
                HideAllPanels();

                // Show students
                pnlForgotPassword3.Show();

                try
                {

                }
                catch (Exception e)
                {
                    MessageBox.Show("Something went wrong while trying to reset your password: " + e.Message);
                }
            }
        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonForgotPassword_Click(object sender, EventArgs e)
        {
            showPanel("ForgotPassword1");
        }

        private void buttonCancel2_Click(object sender, EventArgs e)
        {
            showPanel("Login");
        }

        private void buttonCancel3_Click(object sender, EventArgs e)
        {
            showPanel("Login");
        }

        private void buttonCancel4_Click(object sender, EventArgs e)
        {
            showPanel("Login");
        }

        private void buttonBack2_Click(object sender, EventArgs e)
        {
            showPanel("ForgotPassword2");
        }

        private void buttonBack1_Click(object sender, EventArgs e)
        {
            showPanel("ForgotPassword1");
        }

        private void buttonNext1_Click(object sender, EventArgs e)
        {
            showPanel("ForgotPassword2");
        }

        private void buttonNext2_Click(object sender, EventArgs e)
        {
            showPanel("ForgotPassword3");
        }

        //Debug function.
        private void button4_Click(object sender, EventArgs e)
        {
            PasswordWithSaltHasher pwHasher = new PasswordWithSaltHasher();
            byte[] saltBytes = Encoding.ASCII.GetBytes(textBoxTestSalt.Text);
            int timesToHash = 99999;
            HashWithSaltResult hRSha512 = pwHasher.HashWithSalt(textBoxTestHash.Text, saltBytes, SHA512.Create());
            for (int i = 0; i < timesToHash; i++)
            {
                hRSha512 = pwHasher.HashWithSalt(hRSha512.Hash, saltBytes, SHA512.Create());
            }

            lblTestSalt.Text = hRSha512.Salt.ToString();
            textBoxTestHashOutput.Text = hRSha512.Hash.ToString();
        }
    }
}
