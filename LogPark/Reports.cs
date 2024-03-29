﻿using BusinessLayer;
using System;
using System.Windows.Forms;

namespace LogPark
{
    public partial class Reports : Form
    {

        /// <summary>
        ///  Define a private  parkingService variable
        /// </summary>
        public ParkingService parkingService;


        /// <summary>
        /// InitializeComponent is a method which is used to initialize  form
        /// Initializes a new instance of the ParkingService class 
        /// </summary>
        public Reports()
        {
            InitializeComponent();
            parkingService = new ParkingService();
           
        }

       
        /// <summary>
        /// Activate the generate button with enter Tab
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Reports_Load(object sender, EventArgs e)
        {
              label2.Visible = false;
             this.AcceptButton = button1;
            
           
        }

      
        /// <summary>
        /// Fill the Start Date and End Date and generate the information about Total Cars ,Minimun stay,Maximum stay and Avarage stay
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click_1(object sender, EventArgs e)
        {

            DateTime startDate = dateTimePicker1.Value;
            DateTime endDate = dateTimePicker2.Value;

            if (startDate >= endDate)
            {
                MessageBox.Show("Start Date must be less than or equal to today's date.Please fill in the correct way!", "Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                
            }
            else
            {


               var reportData = parkingService.GenerateReport(startDate, endDate);
              
               
                label1.Text = reportData.TotalCarsInParking.ToString();
                label8.Text=reportData.MinimumStayTime.ToString();
                label5.Text=reportData.MaximumStayTime.ToString() ;
                label12.Text=reportData.AverageStay.ToString();

            }
        }




        /// <summary>
        /// Exit button 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            if (AuthHelper.GetLoggedInUserRole() == "Admin")
            {
                DashboardAdmin dashboard2 = new DashboardAdmin();
                dashboard2.ShowDialog();

            }
            else 
            {
                DashboardSupervizor dashboard1 = new DashboardSupervizor();
                dashboard1.ShowDialog();

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
            else
            {
                DashboardSupervizor dashboard1 = new DashboardSupervizor();
                dashboard1.ShowDialog();

            }


        }
    }
}