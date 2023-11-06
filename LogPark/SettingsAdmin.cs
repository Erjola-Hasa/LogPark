
using LogPark.BLL;
using LogPark.DAL;
using Microsoft.Data.SqlClient;
using System;
using System.ComponentModel;
using System.Drawing;
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
            this.WindowState = (FormWindowState)FormStartPosition.CenterScreen;
            this.WindowState = FormWindowState.Normal;
            int screenWidth = Screen.PrimaryScreen.Bounds.Width;
            int screenHeight = Screen.PrimaryScreen.Bounds.Height;
            Resolution objFormResizer = new Resolution();
            objFormResizer.ResizeForm(this, screenHeight, screenWidth);

            string SaveLanguage = Properties.Settings.Default.Language;
            ChangeLanguage(SaveLanguage);
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(SaveLanguage);

        }

        private void button1_Click(object sender, EventArgs e)
        {
          
                try
                {

                string firstName = textBox1.Text;
                string lastName = textBox2.Text;
                string userName = textBox3.Text;
                string password = textBox4.Text;
                string profile = comboBox1.Text;




                UserService userService = new UserService();
               
                userService.InsertUser(firstName, lastName, userName, password, profile);
                MessageBox.Show("Your Account is created ", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);

                DashboardAdmin dashboardAdmin = new DashboardAdmin();
                dashboardAdmin.Show();
                }
                 catch (Exception ex)
                {

                MessageBox.Show("An error occurred: " + ex.Message);
                }
         }

          private void button2_Click(object sender, EventArgs e)
          {
            string SaveLanguage = Properties.Settings.Default.Language;
            ChangeLanguage(SaveLanguage);
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(SaveLanguage);

            LanguageService languageService = new LanguageService();
           // int Price = Convert.ToInt32(textBox5.Text);
           int Price = Convert.ToInt32(textBox5.Text);
            languageService.UpdatePrice(Price);
            textBox5.Text = Price.ToString();
            this.Hide();
            DashboardAdmin dsha = new DashboardAdmin();
            dsha.ShowDialog();


        }



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

        public  void ChangeLanguage(string lang)
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
            

            LanguageService languageService = new LanguageService();
            int Price = languageService.GetPrice();
            textBox5.Text = Price.ToString();


            string SaveLanguage = Properties.Settings.Default.Language;
            ChangeLanguage(SaveLanguage);
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(SaveLanguage);




        }

        private void button5_Click(object sender, EventArgs e)
        {

            string ServerName = textBox6.Text;
            string DatabaseName = textBox7.Text;
            string UserId = textBox9.Text;
            string Password = textBox8.Text;
      


            ConfigService configBLL = new ConfigService();
            configBLL.ConnectToDatabase(UserId, Password, ServerName, DatabaseName);

        }

        private void button4_Click(object sender, EventArgs e)
        {
            string ServerName = textBox6.Text;
            string DatabaseName = textBox7.Text;
            string UserId = textBox9.Text;
            string Password = textBox8.Text;

            ConfigService configBLL = new ConfigService();
            configBLL.TestDatabase(UserId, Password, ServerName, DatabaseName);

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {
           


            string SaveLanguage = Properties.Settings.Default.Language;
            ChangeLanguage(SaveLanguage);
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(SaveLanguage);


        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            textBox5.Enabled = true;
          


        }
    }

}

    
