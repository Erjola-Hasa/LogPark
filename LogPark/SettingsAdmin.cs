using BusinessLayer;
using log4net;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Windows.Forms;


namespace LogPark
{
    #region Admin Menu
    public partial class SettingsAdmin : Form
    {
        /// <summary>
        /// Define a private  priceService,configBLL,userService variable
        /// </summary>
        private PriceService priceService;
        private ConfigService configBLL;
        private UserService userService;
        
        /// <summary>
        ///  Define a static logger variable
        /// </summary>

     
        private static readonly ILog log = LogManager.GetLogger(typeof(SettingsAdmin));


        

        /// <summary>
        /// InitializeComponent is a method which is used to initialize  form and activate tab enter 
        ///  Initializes a new instance of the PriceServie,ConfigService and UserService
        /// </summary>
        public SettingsAdmin()
        {
            InitializeComponent();
            AcceptButton = button2;
            priceService = new PriceService();
            configBLL = new ConfigService();
            userService = new UserService();
        }



        /// <summary>
        /// Disctionary names  languageDisplayNames mapping language codes(sq) to display names(Albania)
        /// </summary>

        private Dictionary<string, string> languageDisplayNames = new Dictionary<string, string>
     {
           {"sq", "Albania"},
           {"en", "English"},

     };

        /// <summary>
        /// An event handler triggered when the user    selected  language  ComboBox item(English or Albanian Language ) and save the current language in the settings
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// 

        private void comboBox2_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            {
                if (comboBox2.SelectedItem != null)
                {
                    string selectedLanguageCode = languageDisplayNames.FirstOrDefault(x => x.Value == comboBox2.SelectedItem.ToString()).Key;

                    ChangeLanguage(selectedLanguageCode);
                    Thread.CurrentThread.CurrentUICulture = new CultureInfo(selectedLanguageCode);
                    Properties.Settings.Default.Language = selectedLanguageCode;
                    Properties.Settings.Default.Save();
                }
            }
        }



        /// <summary>
        /// Display the Price/Hour
        /// Method to populate the combox with language display names during the forms load and display the database information
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SettingsAdmin_Load(object sender, EventArgs e)
        {

            int Price = priceService.GetPrice();
            textBox5.Text = Price.ToString();


            System.Data.SqlClient.SqlConnectionStringBuilder connBuilder = new System.Data.SqlClient.SqlConnectionStringBuilder();
            string ConnectionString = DataAccesLayer.Properties.Settings.Default.Connection;
            connBuilder.ConnectionString = ConnectionString;
            string server = connBuilder.DataSource;
            textBox6.Text = server;
            string database = connBuilder.InitialCatalog;
            string userid = connBuilder.UserID;
            textBox1.Text = database;
            textBox4.Text = userid;


            comboBox2.Items.AddRange(languageDisplayNames.Values.ToArray());
            string savedLanguage = Properties.Settings.Default.Language;
            string selectedLanguage = languageDisplayNames.ContainsKey(savedLanguage) ? languageDisplayNames[savedLanguage] : null;

            if (!string.IsNullOrEmpty(selectedLanguage))
            {
                comboBox2.SelectedItem = selectedLanguage;
            }
            else
            {

                comboBox2.SelectedIndex = 0;
            }


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
            this.Hide();
            DashboardAdmin dsha = new DashboardAdmin();
            dsha.ShowDialog();
           
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
                log.Error(ex.Message);
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

        private void tabPage1_Click(object sender, EventArgs e)
        {
            
        }
    }

}
#endregion