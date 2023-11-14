﻿
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;
using System.Xml.Linq;
using Org.BouncyCastle.Tsp;
using System.Globalization;
using System.Threading;
using Microsoft.IdentityModel.Tokens;
using BusinessLayer;
using DataAccesLayer;

namespace LogPark
{
    public partial class Login : Form
    {


        public Login()
        {
            InitializeComponent();
            string SaveLanguage = Properties.Settings.Default.Language;
            ChangeLanguage(SaveLanguage);
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(SaveLanguage);
           // Rectangle resolution = Screen.PrimaryScreen.Bounds;
        }
        

        private void Login_Load(object sender, EventArgs e)
        {

            //  Rectangle resolution = Screen.PrimaryScreen.Bounds; 
            string screenWidth = Screen.PrimaryScreen.Bounds.Width.ToString();
            string screenHeight = Screen.PrimaryScreen.Bounds.Height.ToString();

        }
        public void ChangeLanguage(string lang)
        {
            foreach (Control c in this.Controls)
            {
                ComponentResourceManager resources = new ComponentResourceManager(typeof(SettingsAdmin));
                CultureInfo cultureInfo = new CultureInfo(lang);


            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

    

        


        private void button1_Click_2(object sender, EventArgs e)
        {
            string username = textBox1.Text;
            string password = textBox2.Text;


            UserService userService = new UserService();


            Users user = userService.AuthenticateUser(username, password);


            if (user != null)
            {


                if (user.Profile == "Supervizor")
                {
                    this.Hide();
                    DashboardSupervizor dashboard = new DashboardSupervizor();


                    dashboard.ShowDialog();


                }
                else if (user.Profile == "Perdorues")
                {

                    this.Hide();
                    Dashboard dashboard = new Dashboard();
                    dashboard.ShowDialog();
                    this.Close();

                }
                else if (user.Profile == "Admin")
                {

                    this.Hide();
                    DashboardAdmin dashboard = new DashboardAdmin();
                    dashboard.ShowDialog();

                }


             }
                 else if (string.IsNullOrEmpty(username) || string.IsNullOrWhiteSpace(password))
                 {

                   MessageBox.Show("Please enter value in all field.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                  }

                 else
                  {

                      MessageBox.Show("Invalid username or password.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                  }


        }
         

    }
}
