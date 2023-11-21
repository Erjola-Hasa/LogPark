using BusinessLayer;
using System;
using System.Windows.Forms;


namespace LogPark
{
    public partial class DatabaseSettings : Form
    {
        public DatabaseSettings()
        {
            InitializeComponent();

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// Enter the ServerName,DatabaseName, UserId and Password and call the testDatabase method
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            string ServerName = textBox1.Text;
            string DatabaseName = textBox2.Text;
            string UserId = textBox3.Text;
            string Password = textBox4.Text;

            ConfigService configBLL = new ConfigService();
            bool IsConnected = configBLL.TestDatabase(UserId, Password, ServerName, DatabaseName);
            if (IsConnected == true)
            {
                MessageBox.Show("Your Credintial are correct ", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }

            else
            {
                
                this.Hide();
                DatabaseSettings databaseSetting = new DatabaseSettings();
                databaseSetting.ShowDialog();
            }
        }




        /// <summary>
        ///  Enter the ServerName,DatabaseName, UserId and Password  and call the connectDatabase Method
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            string ServerName = textBox1.Text;
            string DatabaseName = textBox2.Text;
            string UserId = textBox3.Text;
            string Password = textBox4.Text;

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
               this.Hide();
                DatabaseSettings databaseSetting = new DatabaseSettings();
                databaseSetting.ShowDialog();
            }

        }
    }
}
