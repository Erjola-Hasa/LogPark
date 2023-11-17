﻿using BusinessLayer;
using System;
using System.ComponentModel;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;

namespace LogPark
{
    #region Supervizor Menu
    public partial class SettingSupervizor : Form
    {
        public ConfigService configBLL = new ConfigService();
        public PriceService languageService = new PriceService();

        public SettingSupervizor()
        {
            InitializeComponent();


        }


        /// <summary>
        /// Method to change the language 
        /// </summary>
        /// <param name="lang"></param>

        private void ChangeLanguage(string lang)
        {
            foreach (Control c in this.Controls)
            {
                ComponentResourceManager resources = new ComponentResourceManager(typeof(Setting));
                CultureInfo cultureInfo = new CultureInfo(lang);

                doRecursiveLoading(this, cultureInfo, resources);

            }
        }



        /// <summary>
        /// Recursive Method to change language in all  controls and sub controls.
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="cultureInfo"></param>
        /// <param name="resources"></param>
        private void doRecursiveLoading(Control parent, CultureInfo cultureInfo, ComponentResourceManager resources)
        {
            foreach (Control c in parent.Controls)
            {
                resources.ApplyResources(c, c.Name, cultureInfo);
                if (c.Controls.Count > 0)
                    doRecursiveLoading(c, cultureInfo, resources);
            }
        }





        /// <summary>
        /// Retrive the price of the parking in the menu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SettingSupervizor_Load(object sender, EventArgs e)
        {
         

            int Price = languageService.GetPrice();
            textBox2.Text = Price.ToString();
        }






        /// <summary>
        /// Dropdown list with language we want to change 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox2.SelectedItem.ToString() == "Albanian")
            {
                ChangeLanguage("sq-AL");
                Thread.CurrentThread.CurrentUICulture = new CultureInfo("sq");
                Properties.Settings.Default.Language = "sq-AL";
            }

            else
            {
                ChangeLanguage("en");
                Thread.CurrentThread.CurrentUICulture = new CultureInfo("en");
                Properties.Settings.Default.Language = "en";
            }
            Properties.Settings.Default.Save();

        }


        /// <summary>
        /// Test Button to test the configuration of the Database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            string ServerName = textBox6.Text;
            string UserId = textBox4.Text;
            string Password = textBox7.Text;
            string DatabaseName = textBox1.Text;


            // ConfigService configBLL = new ConfigService();
            bool IsConnected = configBLL.TestDatabase(UserId, Password, ServerName, DatabaseName);
            if (IsConnected == true)
            {
                MessageBox.Show("Your Credintial are correct ", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }

            else
            {
                MessageBox.Show("Please verify you credintial.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Hide();
                SettingSupervizor settingSupervizor = new SettingSupervizor();
                settingSupervizor.Show();
            }
        }



        /// <summary>
        /// Test Button to Coonect the configuration of the Database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void button3_Click(object sender, EventArgs e)
        {
            string ServerName = textBox6.Text;
            string UserId = textBox4.Text;
            string Password = textBox7.Text;
            string DatabaseName = textBox1.Text;


            //  ConfigService configBLL = new ConfigService();
            bool IsConnected = configBLL.ConnectToDatabase(UserId, Password, ServerName, DatabaseName);
            if (IsConnected == true)
            {
                MessageBox.Show("Your Connection is Successful ", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Hide();
                Login login = new Login();
                login.ShowDialog();
            }
            else
            {
                MessageBox.Show("Please verify you credintial.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                SettingSupervizor settingSupervizor = new SettingSupervizor();
                settingSupervizor.Show();
            }
        }

       


        /// <summary>
        /// Save button of the language we change and price if we change it 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            string SaveLanguage = Properties.Settings.Default.Language;
            ChangeLanguage(SaveLanguage);
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(SaveLanguage);

            PriceService languageService = new PriceService();
            // int Price = Convert.ToInt32(textBox5.Text);
            int Price = Convert.ToInt32(textBox2.Text);
            languageService.UpdatePrice(Price);
            textBox2.Text = Price.ToString();
            this.Hide();
            DashboardSupervizor dsha = new DashboardSupervizor();
            dsha.ShowDialog();
        }


       
    }
}

#endregion