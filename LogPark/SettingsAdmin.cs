
using BusinessLayer;
using System;
using System.ComponentModel;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;


namespace LogPark
{
    public partial class SettingsAdmin : Form
    {

        public SettingsAdmin()
        {
            InitializeComponent();
   

        }
        public void ChangeLanguage(string lang)
        {
            foreach (Control c in this.Controls)
            {
                ComponentResourceManager resources = new ComponentResourceManager(typeof(SettingsAdmin));
                CultureInfo cultureInfo = new CultureInfo(lang);

                doRecursiveLoading(this, cultureInfo, resources);
            }
        }

        private void doRecursiveLoading(Control parent, CultureInfo cultureInfo, ComponentResourceManager resources)
        {
            foreach (Control c in parent.Controls)
            {
                resources.ApplyResources(c, c.Name, cultureInfo);
                if (c.Controls.Count > 0)
                    doRecursiveLoading(c, cultureInfo, resources);
            }
        }


        private void SettingsAdmin_Load(object sender, EventArgs e)
        {

            comboBox2.Items.Add("English");
            comboBox2.Items.Add("Albanian");
            //comboBox2.SelectedIndex = 1;


            PriceService languageService = new PriceService();
            int Price = languageService.GetPrice();
            textBox5.Text = Price.ToString();

        }

        private void textBox5_TextChanged_1(object sender, EventArgs e)
        {
            textBox5.Enabled = true;
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            string SaveLanguage = Properties.Settings.Default.Language;
            ChangeLanguage(SaveLanguage);
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(SaveLanguage);

            PriceService languageService = new PriceService();
            // int Price = Convert.ToInt32(textBox5.Text);
            int Price = Convert.ToInt32(textBox5.Text);
            languageService.UpdatePrice(Price);
            textBox5.Text = Price.ToString();
            this.Hide();
            DashboardAdmin dsha = new DashboardAdmin();
            dsha.ShowDialog();

        }

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
              //  MessageBox.Show("Please verify you credintial.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Hide();
                SettingsAdmin settingsAdmin = new SettingsAdmin();
                settingsAdmin.Show();
            }
        }

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
                MessageBox.Show("Please verify you credintial.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                SettingsAdmin settingsAdmin = new SettingsAdmin();
                settingsAdmin.Show();
            }
        }

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

                //DashboardAdmin dashboardAdmin = new DashboardAdmin();
                //dashboardAdmin.Show();
            }
            catch (Exception ex)
            {

                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }

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

        private void tabPage1_Click_1(object sender, EventArgs e)
        {

           
        }
    }

}


