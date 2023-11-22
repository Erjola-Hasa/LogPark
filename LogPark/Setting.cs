using BusinessLayer;
using System;
using System.ComponentModel;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;

namespace LogPark
{
    public partial class Setting : Form
    {
        /// <summary>
        /// Initializes a new instance of the ConfigService 
        /// </summary>
        ConfigService configBLL = new ConfigService();



        /// <summary>
        /// InitializeComponent is a method which is used to initialize  form and activate tab enter 
        /// </summary>
        public Setting()
        {
            InitializeComponent();
            this.AcceptButton = button2;
        }


        /// <summary>
        /// Gets or sets the index of the selected ComboBox item(English or Albanian Language ) and save the current language 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (comboBox2.SelectedItem.ToString() == "Albanian"|| comboBox2.SelectedItem.ToString() == "Shqip")
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
        /// Method to  current input language
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
