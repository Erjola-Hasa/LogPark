using System;
using System.ComponentModel;
using System.Windows.Forms;
using System.Globalization;

using System.Threading;

using Microsoft.VisualBasic;
using System.Drawing;
using BusinessLayer;

namespace LogPark
{
    public partial class Setting : Form
    {


        public Setting()
        {
            InitializeComponent();

        }


        /// <summary>
        /// Dropdown list to change the language english or albanian
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
        ///  Recursive Method to change language in all  controls and sub controls.
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
        /// Save button if we change language of the application
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void button2_Click(object sender, EventArgs e)
        {

            string SaveLanguage = Properties.Settings.Default.Language;
            ChangeLanguage(SaveLanguage);
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(SaveLanguage);
            Properties.Settings.Default.Save();

            this.Hide();
            Dashboard dashboard = new Dashboard();
            dashboard.ShowDialog();
        }


        /// <summary>
        /// Test Button to test the configuration of the Database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click_1(object sender, EventArgs e)
        {
            string ServerName = textBox6.Text;
            string UserId = textBox4.Text;
            string Password = textBox7.Text;
            string DatabaseName = textBox1.Text;


            ConfigService configBLL = new ConfigService();
            bool IsConnected = configBLL.TestDatabase(UserId, Password, ServerName, DatabaseName);
            if (IsConnected == true)
            {
                MessageBox.Show("Your Credintial are correct ", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }

            else
            {
                //  MessageBox.Show("Please verify you credintial.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Hide();
                Setting settingsAdmin = new Setting();
                settingsAdmin.Show();
            }

        }



        /// <summary>
        /// Save button to save the connection of database 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click_1(object sender, EventArgs e)
        {
            string ServerName = textBox6.Text;
            string UserId = textBox4.Text;
            string Password = textBox7.Text;
            string DatabaseName = textBox1.Text;

            ConfigService configBLL = new ConfigService();
            bool IsConnected = configBLL.ConnectToDatabase(UserId, Password, ServerName, DatabaseName);
            if (IsConnected == true)
            {
                MessageBox.Show("Your Connection is Successful ", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Hide();
                Dashboard login = new Dashboard();
                login.ShowDialog();
            }
            else
            {
                MessageBox.Show("Please verify you credintial.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Setting databaseSetting = new Setting();
                databaseSetting.Show();
            }
        }




        /// <summary>
        /// Override the Close Form event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_FormClosing(Object sender, FormClosingEventArgs e)
        {
            this.Hide();
            Dashboard dashboard = new Dashboard();
            dashboard.ShowDialog();




        }

    }
}
