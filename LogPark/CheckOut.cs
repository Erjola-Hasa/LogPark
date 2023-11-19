
using System;
using System.Windows.Forms;
using BusinessLayer;
using DataAccesLayer;

namespace LogPark
{
    public partial class CheckOut : Form
    {
        public ParkingService parkingService;
        /// <summary>
        ///  Initializes a new instance of the ParkingService class with a reference to a ParkingRepository object.
        /// </summary>
        public CheckOut()
        {
            InitializeComponent();
            parkingService = new ParkingService(new ParkingRepository());

        }


        /// <summary>
        /// Confirm Button if we want to checkOut by the parking
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void button1_Click(object sender, EventArgs e)
        {
             string barcode = textBox2.Text;

            DateTime entryTime = parkingService.GetEntryTimeFromDatabase(barcode);
            DateTime exitTime = DateTime.Now;
         

             decimal price = parkingService.CalculatePrice(entryTime, exitTime);
         
             int reservationID = parkingService.GetReservationIDFromDatabase(barcode);
             

            DialogResult result1 = MessageBox.Show("Are you sure you want to procced with the action?", "Confirm Action", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

            if (result1 == DialogResult.OK)
            {
                parkingService.UpdateReservation(reservationID, exitTime, price);
                this.Hide();
                MessageBox.Show("Your action was completed successfully", "Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information);

                if (AuthHelper.GetLoggedInUserRole() == "Admin")
                {
                    DashboardAdmin dashboard2 = new DashboardAdmin();
                    dashboard2.ShowDialog();

                }
                else if (AuthHelper.GetLoggedInUserRole() == "Supervizor")
                {
                    DashboardSupervizor dashboard1 = new DashboardSupervizor();
                    dashboard1.ShowDialog();

                }

                else
                {
                    this.Hide();
                    Dashboard dashboard = new Dashboard();
                    dashboard.ShowDialog();
                }
            }


            else
            {
                this.Hide();
                CheckOut checkOut = new CheckOut();
                checkOut.Show();
            }



        }
    
       

       /// <summary>
       /// Search Barcode Button and display the Entry time,ExitTime and the Price of parking 
       /// </summary>
       /// <param name="sender"></param>
       /// <param name="e"></param>

        private void button2_Click_1(object sender, EventArgs e)
        {
            string barcode = textBox2.Text;

            string Status = parkingService.GetStatus(barcode);
            if (Status == "Pasive")
            {
                MessageBox.Show("Invalid barcode.Please enter the correct Barcode ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Hide();
                CheckOut checkOut = new CheckOut();
                checkOut.Show();

            }


            int reservationID = parkingService.GetReservationIDFromDatabase(barcode);
            DateTime entryTime = parkingService.GetEntryTimeFromDatabase(barcode);

            if (entryTime != DateTime.MinValue)
            {
                label4.Text = label4.Text.Replace("Entry Time", "") + $" {entryTime}";
                DateTime exitTime = DateTime.Now;

                label5.Text = label5.Text.Replace("Exit Time", "") + $" {exitTime}";

                decimal price = parkingService.CalculatePrice(entryTime, exitTime);

                label6.Text = label6.Text.Replace("Price", "") + $" {price}";


            }
            else
            {
                MessageBox.Show("Please Enter the Barcode.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
    }
}
