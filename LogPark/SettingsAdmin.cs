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
        /// <summary>
        /// Initializes a new instance of the PriceServie,ConfigService and UserService
        /// </summary>
        PriceService priceService = new PriceService();
        ConfigService configBLL = new ConfigService();
        UserService userService = new UserService();

        /// <summary>
        ///  Define a static logger variable
        /// </summary>

        static readonly log4net.ILog log =
            log4net.LogManager.GetLogger
     (System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);






        /// <summary>
        /// InitializeComponent is a method which is used to initialize  form and activate tab enter 
        /// </summary>
        public SettingsAdmin()
        {
            InitializeComponent();
            this.AcceptButton = button2;
        }




        /// <summary>
        /// Gets or sets the index of the selected ComboBox item(English or Albanian Language ) and save the current language 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBox2_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (comboBox2.SelectedItem.ToString() == "Albanian" || comboBox2.SelectedItem.ToString() == "Shqip")
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
        ///  Method to  current input language
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
        /// Display the Price/Hour
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SettingsAdmin_Load(object sender, EventArgs e)
        {

           
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
            try
            {


                int Price = Convert.ToInt32(textBox5.Text);
                priceService.UpdatePrice(Price);
                textBox5.Text = Price.ToString();
                this.Hide();
                DashboardAdmin dsha = new DashboardAdmin();
                dsha.ShowDialog();
            }
            catch (Exception ex)
            {
               

                log.Error(ex.Message);
                MessageBox.Show("An error has occurred. Please Enter a number.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

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

             configBLL.TestDatabase(UserId, Password, ServerName, DatabaseName);
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
           
           configBLL.ConnectToDatabase(UserId, Password, ServerName, DatabaseName);
           
        }




        /// <summary>
        /// Create New User 
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

              

                userService.InsertUser(firstName, lastName, userName, password, profile);
               

            }
            catch (Exception ex)
            {

                MessageBox.Show("An error occurred: " + ex.Message);
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
            DashboardAdmin dashboard = new DashboardAdmin();
            dashboard.ShowDialog();




        }
    }

}
#endregion