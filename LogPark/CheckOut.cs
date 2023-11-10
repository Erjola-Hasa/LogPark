
using System;
using System.Windows.Forms;
using BusinessLayer;
using DataAccesLayer;

namespace LogPark
{
    public partial class CheckOut : Form
    {
        public ParkingService parkingService;
        public CheckOut()
        {
            InitializeComponent();
            parkingService = new ParkingService(new ParkingRepository());

        }

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
                Dashboard dashboard = new Dashboard();
                dashboard.Show();

               
            }
         
            else
            {
                this.Hide();
                CheckOut checkOut = new CheckOut();
                checkOut.Show();
            }



        }
    
        private void CheckOut_Load(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            string barcode = textBox2.Text;

            int reservationID = parkingService.GetReservationIDFromDatabase(barcode);
            DateTime entryTime = parkingService.GetEntryTimeFromDatabase(barcode);

            if (entryTime != DateTime.MinValue)
            {
                label4.Text = label4.Text.Replace("Entry Time", "") + $" {entryTime}";
                //  label1.Text = $"Entry Time: {entryTime}";


                DateTime exitTime = DateTime.Now;

                label5.Text = label5.Text.Replace("Exit Time", "") + $" {exitTime}";

                decimal price = parkingService.CalculatePrice(entryTime, exitTime);

                label6.Text = label6.Text.Replace("Price", "") + $" {price}";


            }
            else
            {
                MessageBox.Show("Invalid barcode or code not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
    }
}
