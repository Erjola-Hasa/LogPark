using BusinessLayer;
using DataAccesLayer;
using System;
using System.Windows.Forms;

namespace LogPark
{
    public partial class Login : Form
    {
       

        public Login()
        {
            InitializeComponent();

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
        /// Login form with UserName and Password
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

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



      



        /// <summary>
        /// Close the program 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);

            if (e.CloseReason == CloseReason.WindowsShutDown) return;

            // Confirm user wants to close
            switch (MessageBox.Show(this, "Are you sure you want to close?", "Closing", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
            {
                case DialogResult.No:
                    e.Cancel = true;
                    break;
                default:
                    e.Cancel = false;
                    break;



            }
        }
    }
}
