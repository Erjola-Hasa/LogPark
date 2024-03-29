﻿using BusinessLayer;
using System;
using System.Windows.Forms;

namespace LogPark
{
    #region Menu of Supervizor
    public partial class DashboardSupervizor : Form
    {

        /// <summary>
        /// Define a private  parkingService variable
        /// </summary>
        private ParkingService parkingService;


        /// <summary>
        /// InitializeComponent is a method which is used to initialize  form
        /// Initializes a new instance of the ParkingService class
        /// </summary>
        public DashboardSupervizor()
        {
            InitializeComponent();
            parkingService = new ParkingService();
            
        }

        /// <summary>
        /// Retrive Real Time free Space in parking
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DashboardSupervizor_Load(object sender, EventArgs e)
        {
            string status = "Aktive";
            int totalParkingSpaces = parkingService.GetTotalParkingSpaces();

            int reservedSpaces = parkingService.GetNumberOfReservedSpaces(status);
            int freeSpaces = totalParkingSpaces - reservedSpaces;
            if (freeSpaces == 0)
            {
                MessageBox.Show("Full Parking", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                button2.Visible = false;
               
            }
            label5.Text = label5.Text.Replace(" Free Space", "") + $":{freeSpaces} /  {totalParkingSpaces}";





        }



        /// <summary>
        /// Checkin  button who directs us in the checkin menu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Check check = new Check();
            check.ShowDialog();
        }

        /// <summary>
        /// CheckOut button who directs us in the checkOut menu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            CheckOut check = new CheckOut();
            check.ShowDialog();
        }




        /// <summary>
        /// Reports button button who directs us in the reports menu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            Reports reports = new Reports();
            reports.ShowDialog();
        }




        /// <summary>
        /// Settings Supervizor button who directs us in the  SettingSupervizor menu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();
            SettingSupervizor setting = new SettingSupervizor();
            setting.ShowDialog();
        }



        /// <summary>
        /// LogOut  button who directs us in the Login menu 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Login login = new Login();
            login.ShowDialog();
        }




        /// <summary>
        /// Override the Close Form event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_FormClosing(Object sender, FormClosingEventArgs e)
        {

            this.Hide();
            Login login = new Login();
            login.ShowDialog();


        }
    }
}
#endregion