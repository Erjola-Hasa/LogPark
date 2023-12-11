using BusinessLayer;
using System;
using System.Windows.Forms;

namespace LogPark
{
    public partial class Login : Form
    {
        /// <summary>
        ///  Define a private  userService variable
        /// </summary>

        private UserService userService;

        /// <summary>
        ///  InitializeComponent is a method which is used to initialize  form
        ///  Initializes a new instance of the UserService 
        /// </summary>
        public Login()
        {
            InitializeComponent();
           userService = new UserService();
            

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
     /// Enter in the application with UserName and Password 
     /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>

         private void button1_Click_2(object sender, EventArgs e)
         {


            string username = textBox1.Text;
            string password = textBox2.Text;
            password = Cryptography.Encrypt(password);
          
            var user = userService.AuthenticateUser(username, password);

            if (string.IsNullOrEmpty(username) || string.IsNullOrWhiteSpace(password))
            {


                MessageBox.Show("Please enter value in all field.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);



            }
            else   if (user != null)
            {

                AuthHelper.SetLoggedInUserRole(user.Profile);

                if (user.Profile == "Supervizor" || user.Profile=="Supervisor")
                {
                    this.Hide();
                    DashboardSupervizor dashboard = new DashboardSupervizor();


                    dashboard.ShowDialog();


                }
                else if (user.Profile == "Users" || user.Profile=="Perdorues")
                {

                    this.Hide();
                    Dashboard dashboard = new Dashboard();
                    dashboard.ShowDialog();

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
           
           Environment.Exit(0);
        

        }
      

    }
}
