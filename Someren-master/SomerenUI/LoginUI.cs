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
using System.Windows;

namespace SomerenUI
{
    public partial class LoginUI : Form
    {
        public LoginUI()
        {
            InitializeComponent();
            showPanel("Login");
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
            UserService userService = new UserService();

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
            ForgetPasswordSweeper();
        }

        private void buttonCancel3_Click(object sender, EventArgs e)
        {
            showPanel("Login");
            ForgetPasswordSweeper();
        }

        private void buttonCancel4_Click(object sender, EventArgs e)
        {
            showPanel("Login");
            ForgetPasswordSweeper();
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

        //String hasher.
        private HashWithSaltResult StringHasher(string hash, string salt)
        {
            PasswordWithSaltHasher pwHasher = new PasswordWithSaltHasher();
            byte[] saltBytes = Encoding.ASCII.GetBytes(salt);
            int timesToHash = 99999;
            HashWithSaltResult hashWithSalt = pwHasher.HashWithSalt(hash, saltBytes, SHA512.Create());

            for (int i = 0; i < timesToHash; i++)
            {
                hashWithSalt = pwHasher.HashWithSalt(hashWithSalt.Hash, saltBytes, SHA512.Create());
            }
            return hashWithSalt;
        }

        //Empty all relevant fields for forget password
        private void ForgetPasswordSweeper()
        {
            buttonNext1.Enabled = false;
            buttonNext2.Enabled = false;
            lblSecretQuestion2.Text = ($"Secret Question: ");
            textBoxUsername3.Text = string.Empty;
            lblUsernameHidden.Text = string.Empty;
            textBoxSecretAnswer2.Text = string.Empty;
            textBoxNewPassword1.Text = string.Empty;
            textBoxNewPassword2.Text = string.Empty;
        }

        //Requests and displays secret question, then moves on to next screen.
        private void buttonRequestQuestion_Click(object sender, EventArgs e)
        {
            PrintService printService = new PrintService();
            UserService userService = new UserService();

            try
            {
                //Checks if textBox is empty.
                if (textBoxUsername3.Text == string.Empty)
                {
                    throw new Exception("Username may not be empty!");
                }

                //Returns secret question, updates text, moves to next panel and enables next button on current page.
                string SecretQuestion = userService.GetUserQuestion(textBoxUsername3.Text);
                lblUsernameHidden.Text = textBoxUsername3.Text;
                lblSecretQuestion2.Text = ($"Secret Question: {SecretQuestion}");
                showPanel("ForgotPassword2");
                buttonNext1.Enabled = true;
            }
            catch (Exception ex)
            {
                buttonNext1.Enabled = false;
                printService.Print(ex);
                MessageBox.Show("Something went wrong!\n\n" + ex.Message);
            }           
        }

        private void buttonRequestPassword_Click(object sender, EventArgs e)
        {
            PrintService printService = new PrintService();
            UserService userService = new UserService();

            try
            {
                //Checks if textBox is empty.
                if (textBoxSecretAnswer2.Text == string.Empty)
                {
                    throw new Exception("Answer may not be empty!");
                }

                //Gets hashed secret answer and plain text salt and stores them in a single object.
                HashWithSaltResult retrievedHashWithSalt = userService.GetUserAnswer(lblUsernameHidden.Text);

                //Gets secret answer from textbox input, hashes it and stores it along with the retrieved salt from the database in a single object.
                HashWithSaltResult inputHashWithSalt = StringHasher(textBoxSecretAnswer2.Text.ToLower(), retrievedHashWithSalt.Salt);

                //Compares the input and retrieved hashed secret answer.
                if (inputHashWithSalt.Hash != retrievedHashWithSalt.Hash)
                {
                    throw new Exception("Wrong answer!");
                }

                //Go to next panel and enable next button.
                showPanel("ForgotPassword3");
                buttonNext2.Enabled = true;
            }
            catch (Exception ex)
            {
                buttonNext2.Enabled = false;
                printService.Print(ex);
                MessageBox.Show("Something went wrong!\n\n" + ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            PrintService printService = new PrintService();
            UserService userService = new UserService();

            try
            {
                //Checks if textBoxes are empty.
                if (textBoxNewPassword1.Text == string.Empty || textBoxNewPassword2.Text == string.Empty)
                {
                    throw new Exception("Please enter your new password twice!");
                }

                //Checks if the two password inputs are different.
                if (textBoxNewPassword1.Text != textBoxNewPassword2.Text)
                {
                    throw new Exception("Entered passwords are not the same!");
                }

                //Creates a object that stores hashed version of the new password, salt is retrieved from database.
                HashWithSaltResult passwordHashWithSalt = StringHasher(textBoxNewPassword1.Text, userService.GetSalt(lblUsernameHidden.Text));
                userService.UpdatePassword(lblUsernameHidden.Text, passwordHashWithSalt.Hash);
                MessageBox.Show("Password has been updated!");
                ForgetPasswordSweeper();
                showPanel("Login");
            }
            catch (Exception ex)
            {
                printService.Print(ex);
                MessageBox.Show("Something went wrong!\n\n" + ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void buttonAddNewUser2_Click(object sender, EventArgs e)
        {
            PrintService printService = new PrintService();
            UserService usrService = new UserService();
            PasswordWithSaltHasher pwHasher = new PasswordWithSaltHasher();
            Random random = new Random();
            string registrationKey = "XsZAb - tgz3PsD - qYh69un - WQCEx";

            try
            {
                // Check for input
                string userInputRegistrationKey = textBoxRegistrationKey.Text;
                if (string.IsNullOrEmpty(userInputRegistrationKey) || string.IsNullOrEmpty(textBoxUsername2.Text) || string.IsNullOrEmpty(textBoxSecretQuestion1.Text) || string.IsNullOrEmpty(textBoxPassword2.Text) || string.IsNullOrEmpty(textBoxSecretAnswer1.Text))
                    throw new Exception("Please fill all fields.");  
                // Check if user registration key is correct
                if (userInputRegistrationKey != registrationKey)
                    throw new Exception("License key is wrong.");

                string Username = textBoxUsername2.Text;
                string question = textBoxSecretQuestion1.Text;

                int salt = random.Next(1, 1000);

                // Hash and Salt password and answer
                HashWithSaltResult password = StringHasher(textBoxPassword2.Text, salt.ToString());
                HashWithSaltResult secretAnswer = StringHasher(textBoxSecretAnswer1.Text.ToLower(), salt.ToString());
                usrService.AddUser(Username, password, question, secretAnswer, salt.ToString());
                MessageBox.Show("User added.");
                showPanel("Login");
            }
            catch (Exception ex)
            {
                printService.Print(ex);
                MessageBox.Show("Something went wrong while adding a new user.\n" + ex.Message);
            }
        }

        private void buttonAddNewUser1_Click(object sender, EventArgs e)
        {
            showPanel("AddNewUser");
        }

        private void buttonCancel1_Click(object sender, EventArgs e)
        {
            showPanel("Login");
            textBoxUsername2.Text = string.Empty;
            textBoxPassword2.Text = string.Empty;
            textBoxSecretQuestion1.Text = string.Empty;
            textBoxSecretAnswer1.Text = string.Empty;
            textBoxRegistrationKey.Text = string.Empty;
        }
    }
}
