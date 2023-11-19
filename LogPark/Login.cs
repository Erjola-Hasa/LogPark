using BusinessLayer;
using DataAccesLayer;
using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace LogPark
{
    public partial class Login : Form
    {
      

        public Login()
        {
            InitializeComponent();
            this.FormClosing += Form1_FormClosing;
            

        }
       

        /// <summary>
        /// Activate Enter Button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Login_Load(object sender, EventArgs e)
        {

            this.AcceptButton = button1;
           

        }




        /// <summary>
        /// Enter in the application with UserName and Password and validate them
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void button1_Click_2(object sender, EventArgs e)
        {
            this.AcceptButton = button1;

            string username = textBox1.Text;
            string password = textBox2.Text;
            password = Cryptography.Encrypt(password);
          


            UserService userService = new UserService();


            Users user = userService.AuthenticateUser(username, password);

            if (string.IsNullOrEmpty(username) || string.IsNullOrWhiteSpace(password))
            {


                MessageBox.Show("Please enter value in all field.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);



            }
            else   if (user != null)
            {

                AuthHelper.SetLoggedInUserRole(user.Profile);

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

            }
        }


        



        /// <summary>
        /// Override the Close Form event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_FormClosing(Object sender, FormClosingEventArgs e)
        {
            //In case windows is trying to shut down, don't hold the process up
            if (e.CloseReason == CloseReason.WindowsShutDown) return;

            if (this.DialogResult == DialogResult.Cancel)
            {
                // Assume that X has been clicked and act accordingly.
                // Confirm user wants to close
                switch (MessageBox.Show(this, "Are you sure?", "Do you still want ... ?", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                {
                    //Stay on this form
                    case DialogResult.No:
                        e.Cancel = true;
                        break;
                    default:
                        Environment.Exit(10);
                        break;
                }
            }
        }
    }
}
