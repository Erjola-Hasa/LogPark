﻿using BusinessLayer;
using System;
using System.Windows.Forms;

namespace LogPark
{
    public partial class CheckOut : Form
    {
        /// <summary>
        /// Define a private  parkingService variable
        /// </summary>
        public ParkingService parkingService;




        /// <summary>
        /// InitializeComponent is a method which is used to initialize  form
        ///  Initializes a new instance of the ParkingService 
        /// </summary>
        public CheckOut()
        {
            InitializeComponent();
            parkingService = new ParkingService();

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
            if (string.IsNullOrEmpty(barcode))
            {
                MessageBox.Show("Please Enter the Barcode ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            else
            {

                DialogResult result1 = MessageBox.Show("Are you sure you want to procced with the action?", "Confirm Action", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

                if (result1 == DialogResult.OK)
                {

                    parkingService.UpdateReservation(reservationID, exitTime, price);

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
                        this.Hide();
                        Dashboard dashboard = new Dashboard();
                        dashboard.ShowDialog();
                    }

                }




                else
                {
                    this.Hide();
                    CheckOut checkOut = new CheckOut();
                    checkOut.ShowDialog();
                }

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
            if (Status == "Pasive" || string.IsNullOrEmpty(barcode))
            {
                MessageBox.Show("Invalid barcode.Please enter the correct Barcode ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Hide();
                CheckOut checkOut = new CheckOut();
                checkOut.ShowDialog();

            }

                int reservationID = parkingService.GetReservationIDFromDatabase(barcode);
                DateTime entryTime = parkingService.GetEntryTimeFromDatabase(barcode);

            if (entryTime != DateTime.MinValue)
            {

                   label4.Text = entryTime.ToString();
                    DateTime exitTime = DateTime.Now;
                    label5.Text = exitTime.ToString();
                    decimal price = parkingService.CalculatePrice(entryTime, exitTime);
                    label6.Text = price.ToString();


            }
        
            else
            {
                MessageBox.Show("Please Enter the correct Barcode.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Hide();
                CheckOut checkOut = new CheckOut();
                checkOut.ShowDialog();

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



        /// <summary>
        /// Activate Tab Enter 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CheckOut_Load(object sender, EventArgs e)
        {

            this.AcceptButton = button2;
        }
    }
}
