using BusinessLayer;
using DataAccesLayer;
using System;
using System.ComponentModel;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;

namespace LogPark
{
    public partial class Login : Form
    {
       

        public Login()
        {
            InitializeComponent();
        }


        private void Login_Load(object sender, EventArgs e)
        {
         
            this.AcceptButton = button1;
        }
        public void ChangeLanguage(string lang)
        {
            foreach (Control c in this.Controls)
            {
                ComponentResourceManager resources = new ComponentResourceManager(typeof(SettingsAdmin));
                CultureInfo cultureInfo = new CultureInfo(lang);


            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }


        private void button1_Click_2(object sender, EventArgs e)
        {
            this.AcceptButton = button1;
            string username = textBox1.Text;
            string password = textBox2.Text;

            
            UserService userService = new UserService();


            Users user = userService.AuthenticateUser(username, password);

            if (string.IsNullOrEmpty(username) || string.IsNullOrWhiteSpace(password))
            {

                MessageBox.Show("Invalid username or password.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
           


               else if (user.Profile == "Supervizor")
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


            
           

            else
            {

               
                MessageBox.Show("Please enter value in all field.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }

        private void tableLayoutPanel2_Paint(object sender, PaintEventArgs e)
        {
          
        }
    }
}
