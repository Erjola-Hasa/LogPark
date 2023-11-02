using LogPark.BLL;
using LogPark.DAL;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;
using System.Xml.Linq;
using Org.BouncyCastle.Tsp;
using System.Globalization;
using System.Threading;
using Microsoft.IdentityModel.Tokens;

namespace LogPark
{
    public partial class Login : Form
    {


        public Login()
        {
            InitializeComponent();
            string SaveLanguage = Properties.Settings.Default.Language;
            ChangeLanguage(SaveLanguage);
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(SaveLanguage);

            this.WindowState = FormWindowState.Normal;
            int screenWidth = Screen.PrimaryScreen.Bounds.Width;
            int screenHeight = Screen.PrimaryScreen.Bounds.Height;
            Resolution objFormResizer = new Resolution();
            objFormResizer.ResizeForm(this, screenHeight, screenWidth);


        }
        private void button1_Click(object sender, EventArgs e)
        {



            string username = textBox1.Text;
            string password = textBox2.Text;


            UserService userService = new UserService();


            Users user = userService.AuthenticateUser(username, password);


            if (user != null)
            {


                if (user.Profile == "Supervizor")
                {
                    this.Hide();
                    DashboardSupervizor dashboard = new DashboardSupervizor();
                   
               
                    dashboard.ShowDialog();


                }
                else if (user.Profile == "Perdorues")
                {

                    this.Hide();
                    Dashboard dashboard = new Dashboard();
                    dashboard.ShowDialog();
                    this.Close();

                }
                else if (user.Profile == "Admin")
                {

                    this.Hide();
                    DashboardAdmin dashboard = new DashboardAdmin();
                    dashboard.ShowDialog();

                }


            }
            else
            {
                MessageBox.Show("Invalid username or password.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                DialogResult result1 = MessageBox.Show("Are you new in this app?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                {
                    if (result1 == DialogResult.Yes)
                    {
                        DatabaseSettings setting = new DatabaseSettings();
                        setting.ShowDialog();

                    }
                    else
                    {
                        Login login = new Login();
                        login.ShowDialog();
                    }
                }

            }


        }

        private void Login_Load(object sender, EventArgs e)
        {

        }
        public void ChangeLanguage(string lang)
        {
            foreach (Control c in this.Controls)
            {
                ComponentResourceManager resources = new ComponentResourceManager(typeof(SettingsAdmin));
                CultureInfo cultureInfo = new CultureInfo(lang);


            }
        }



    }
}
