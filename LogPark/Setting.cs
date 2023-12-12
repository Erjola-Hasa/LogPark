using BusinessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace LogPark
{
    public partial class Setting : Form
    {
        /// <summary>
        ///  Define a private  ConfigBLL variable
        /// </summary>
        private ConfigService configBLL;



        /// <summary>
        /// InitializeComponent is a method which is used to initialize  form and activate tab enter 
        /// Initializes a new instance of the ConfigService 
        /// </summary>
        public Setting()
        {
            InitializeComponent();
            this.AcceptButton = button2;
            configBLL = new ConfigService();
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

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
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
        /// Method to populate the combox with language display names during the forms load and display the database information
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
            private void Setting_Load(object sender, EventArgs e)
            {
            
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
        



            System.Data.SqlClient.SqlConnectionStringBuilder connBuilder = new System.Data.SqlClient.SqlConnectionStringBuilder();
            string ConnectionString = DataAccesLayer.Properties.Settings.Default.Connection;
            connBuilder.ConnectionString = ConnectionString;
            string server = connBuilder.DataSource;
            textBox6.Text = server;
            string database = connBuilder.InitialCatalog;
            string userid = connBuilder.UserID;
            textBox1.Text = database;
            textBox4.Text = userid;

             }


        /// <summary>
        /// Method to update the language settings 
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

            configBLL.TestDatabase(UserId, Password, ServerName, DatabaseName);
         

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

          
            configBLL.ConnectToDatabase(UserId, Password, ServerName, DatabaseName);
            this.Hide();
            Dashboard dashboard = new Dashboard();
            dashboard.ShowDialog();
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
