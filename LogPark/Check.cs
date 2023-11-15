﻿using BusinessLayer;
using DataAccesLayer;
using System;
using System.Windows.Forms;

namespace LogPark
{
    public partial class Check : Form
    {
       private   ParkingService parkingService;
        public Check()
        {
            InitializeComponent();
            parkingService = new ParkingService(new ParkingRepository());


        }

        private void Check_Load(object sender, EventArgs e)
        {
            int totalParkingSpaces = parkingService.GetTotalParkingSpaces();

            string status = "Aktive";
            int reservedSpaces = parkingService.GetNumberOfReservedSpaces(status);
            int freeSpaces = totalParkingSpaces - reservedSpaces;


            if (freeSpaces == totalParkingSpaces)
            {
                MessageBox.Show("Full", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        
            label2.Text =label2.Text.Replace(" Free Space", "") + $":{freeSpaces} /  {totalParkingSpaces}"; 
        }

      
        private void button1_Click_1(object sender, EventArgs e)
        {
          

        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click_2(object sender, EventArgs e)
        {
            Checkin checkin = new Checkin();
            checkin.Show();
            this.Hide();

        }
    }
}
