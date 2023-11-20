using BusinessLayer;
using DataAccesLayer;
using System;
using System.Windows.Forms;

namespace LogPark
{
    public partial class Checkin : Form
    {

       
        private ParkingService parkingService;

        /// <summary>
        /// Initializes a new instance of the ParkingService class with a reference to a ParkingRepository object.
        /// </summary>
        public Checkin()
        {
            InitializeComponent();
            parkingService = new ParkingService(new ParkingRepository());

        }


        /// <summary>
        /// Generate the Randon number for Barcode and dateTime now
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Checkin_Load(object sender, EventArgs e)
        {
         
            DateTime checkInDateTime = DateTime.Now;
            label3.Text = GenerateRandomBarcode();
            label4.Text = checkInDateTime.ToString();

            

        }



        /// <summary>
        /// Confirm Button and proceed with checkin in parking
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void button1_Click(object sender, EventArgs e)
        {




            DialogResult result1 = MessageBox.Show("Are you sure you want to proceed with the action?", "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

            if (result1 == DialogResult.OK)
            {
                string barcode = label3.Text;
                DateTime checkInDateTime = DateTime.Now;
                parkingService.CheckIn(barcode, checkInDateTime);
                MessageBox.Show("Your action was completed successfully", "Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.Hide();
                if (AuthHelper.GetLoggedInUserRole() == "Admin")
                {
                    DashboardAdmin dashboard2 = new DashboardAdmin();
                    dashboard2.ShowDialog();

                }
                else if (AuthHelper.GetLoggedInUserRole() == "Supervizor" || AuthHelper.GetLoggedInUserRole() == "Supervisor")
                {
                   
                    DashboardSupervizor dashboard1 = new DashboardSupervizor();
                    dashboard1.ShowDialog();

                }

                else
                {
                    Dashboard dashboard = new Dashboard();
                    dashboard.ShowDialog();
                }
            }
        }






        /// <summary>
        /// Override the Close Form event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>


        private void Form1_FormClosing(Object sender, FormClosingEventArgs e)
            {
                this.Hide();
               Check check = new Check();
               check.ShowDialog();




            }






            /// <summary>
            /// Method to generate the random number
            /// </summary>
            /// <returns></returns>
            private   string GenerateRandomBarcode()
        {
            
            Random random = new Random();
            int randomNumber = random.Next(10000, 99999);
            return randomNumber.ToString();
        }

        
    }
}

