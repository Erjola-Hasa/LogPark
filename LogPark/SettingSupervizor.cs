using BusinessLayer;
using log4net;
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
        /// <summary>
        
        /// Define a private  ConfigBLL,languageService variable
        /// </summary>
        private ConfigService configBLL;
        private PriceService languageService;


        /// <summary>
        ///  Define a static logger variable
        /// </summary>
      
        private static readonly ILog log = LogManager.GetLogger(typeof(SettingSupervizor));



        /// <summary>
        /// InitializeComponent is a method which is used to initialize  form and activate tab enter 
        ///  Initializes a new instance of the ConfigService,PriceService 
        /// </summary>
        public SettingSupervizor()
        {
            InitializeComponent();
            this.AcceptButton = button2;
            configBLL = new ConfigService();
            languageService = new PriceService();


        }



        /// <summary>
        /// Gets or sets the index of the selected ComboBox item(English or Albanian Language ) and save the current language 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox2.Text == "SQ")
            {
                ChangeLanguage("sq");
                Thread.CurrentThread.CurrentUICulture = new CultureInfo("sq");
                Properties.Settings.Default.Language = "sq";
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

        private void ChangeLanguage(string lang)
        {
            foreach (Control c in this.Controls)
            {
                ComponentResourceManager resources = new ComponentResourceManager(typeof(SettingSupervizor));
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
        /// Display the price of the parking 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SettingSupervizor_Load(object sender, EventArgs e)
        {
            int Price = languageService.GetPrice();
            textBox2.Text = Price.ToString();


            System.Data.SqlClient.SqlConnectionStringBuilder connBuilder = new System.Data.SqlClient.SqlConnectionStringBuilder();
            string ConnectionString = DataAccesLayer.Properties.Settings.Default.Connection;
            connBuilder.ConnectionString = ConnectionString;
            string server = connBuilder.DataSource;
            textBox6.Text = server;
            string database = connBuilder.InitialCatalog;
            string userid = connBuilder.UserID;
            textBox1.Text = database;
            textBox4.Text = userid;

            comboBox2.Items.Add("SQ");
            comboBox2.Items.Add("EN");


            comboBox2.Text = Properties.Settings.Default.Language;

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
                configBLL.TestDatabase(UserId, Password, ServerName, DatabaseName);
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

            configBLL.ConnectToDatabase(UserId, Password, ServerName, DatabaseName);
            this.Hide();
            DashboardSupervizor dshs= new DashboardSupervizor();
            dshs.ShowDialog();
        }

       


        /// <summary>
        /// Save button for price and Language 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
               
                PriceService languageService = new PriceService();
                int Price = Convert.ToInt32(textBox2.Text);
                languageService.UpdatePrice(Price);
                textBox2.Text = Price.ToString();
                this.Hide();
                DashboardSupervizor dsha = new DashboardSupervizor();
                dsha.ShowDialog();
            }
            catch 
            (Exception ex)
            {
                log.Error( ex.Message);
                MessageBox.Show("An error has occurred. Please Enter a number.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            DashboardSupervizor dashboard = new DashboardSupervizor();
            dashboard.ShowDialog();




        }

       
    }
}

#endregion