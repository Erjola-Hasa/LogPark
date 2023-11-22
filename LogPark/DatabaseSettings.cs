using BusinessLayer;
using System;
using System.Windows.Forms;


namespace LogPark
{


    public partial class DatabaseSettings : Form
    {
        /// <summary>
        /// Initializes a new instance of the ConfigService class
        /// </summary>
        ConfigService configBLL = new ConfigService();




        /// <summary>
        /// InitializeComponent is a method which is used to initialize  form
        /// </summary>
        public DatabaseSettings()
        {
            InitializeComponent();

        }

       

        /// <summary>
        /// Enter the ServerName,DatabaseName, UserId and Password and test Database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            string ServerName = textBox1.Text;
            string DatabaseName = textBox2.Text;
            string UserId = textBox3.Text;
            string Password = textBox4.Text;

           
            configBLL.TestDatabase(UserId, Password, ServerName, DatabaseName);
           
        }




        /// <summary>
        ///  Enter the ServerName,DatabaseName, UserId and Password  and ConnectDatabase
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            string ServerName = textBox1.Text;
            string DatabaseName = textBox2.Text;
            string UserId = textBox3.Text;
            string Password = textBox4.Text;

            bool IsConnected = configBLL.ConnectToDatabase(UserId, Password, ServerName, DatabaseName);
            if (IsConnected == true)
            {
              
                this.Hide();
                Login login = new Login();
                login.ShowDialog();
            }
            else
            {
               this.Hide();
                DatabaseSettings databaseSetting = new DatabaseSettings();
                databaseSetting.ShowDialog();
            }

        }
    }
}
