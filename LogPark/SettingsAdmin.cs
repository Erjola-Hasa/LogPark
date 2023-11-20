using BusinessLayer;
using System;
using System.ComponentModel;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;


namespace LogPark
{
    #region Admin Menu
    public partial class SettingsAdmin : Form
    {


        public SettingsAdmin()
        {
            InitializeComponent();


        }


        /// <summary>
        /// Method to change language 
        /// </summary>
        /// <param name="lang"></param>
        public void ChangeLanguage(string lang)
        {
            foreach (Control c in this.Controls)
            {
                ComponentResourceManager resources = new ComponentResourceManager(typeof(SettingsAdmin));
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
        /// Get the price/hour 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SettingsAdmin_Load(object sender, EventArgs e)
        {

            PriceService priceService = new PriceService();
            int Price = priceService.GetPrice();
            textBox5.Text = Price.ToString();

        }



        /// <summary>
        /// Price is enable and we can change
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBox5_TextChanged_1(object sender, EventArgs e)
        {
            textBox5.Enabled = true;
        }




        /// <summary>
        /// Save button to change the language and price of the parking
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click_1(object sender, EventArgs e)
        {
            string SaveLanguage = Properties.Settings.Default.Language;
            ChangeLanguage(SaveLanguage);
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(SaveLanguage);

            PriceService priceService = new PriceService();
            int Price = Convert.ToInt32(textBox5.Text);
            priceService.UpdatePrice(Price);
            textBox5.Text = Price.ToString();
            this.Hide();
            DashboardAdmin dsha = new DashboardAdmin();
            dsha.ShowDialog();

        }



        /// <summary>
        /// Test Button about the configuration of database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click_1(object sender, EventArgs e)
        {
            string ServerName = textBox6.Text;
            string DatabaseName = textBox1.Text;
            string UserId = textBox4.Text;
            string Password = textBox7.Text;

            ConfigService configBLL = new ConfigService();
            bool IsConnected = configBLL.TestDatabase(UserId, Password, ServerName, DatabaseName);
            if (IsConnected == true)
            {
                MessageBox.Show("Your Credintial are correct ", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }

            else
            {
                this.Hide();
                SettingsAdmin settingsAdmin = new SettingsAdmin();
                settingsAdmin.ShowDialog();
            }
        }



        /// <summary>
        /// Save button of Connection of Database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
            string ServerName = textBox6.Text;
            string DatabaseName = textBox1.Text;
            string UserId = textBox4.Text;
            string Password = textBox7.Text;



            ConfigService configBLL = new ConfigService();
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

                SettingsAdmin settingsAdmin = new SettingsAdmin();
                settingsAdmin.Show();
            }
        }




        /// <summary>
        /// Create button of the users in base of role 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button4_Click(object sender, EventArgs e)
        {
            try
            {

                string firstName = textBox2.Text;
                string lastName = textBox3.Text;
                string userName = textBox8.Text;
                string password = textBox9.Text;
                string profile = comboBox1.Text;

                UserService userService = new UserService();

                userService.InsertUser(firstName, lastName, userName, password, profile);
               

            }
            catch (Exception ex)
            {

                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }



        /// <summary>
        /// Dropdown list with language we want to change 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBox2_SelectedIndexChanged_1(object sender, EventArgs e)
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
        /// Override the Close Form event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_FormClosing(Object sender, FormClosingEventArgs e)
        {
            this.Hide();
            DashboardAdmin dashboard = new DashboardAdmin();
            dashboard.ShowDialog();




        }
    }

}
#endregion