using BusinessLayer;
using DataAccesLayer;

using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LogPark
{
    public partial class DashboardAdmin : Form
    {
        private ParkingService parkingService;

        public DashboardAdmin()
        {
            InitializeComponent();
        

            string SaveLanguage = Properties.Settings.Default.Language;
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(SaveLanguage);
            parkingService = new ParkingService(new ParkingRepository());

        }


        private void DashboardAdmin_Load(object sender, EventArgs e)
        {
            string screenWidth = Screen.PrimaryScreen.Bounds.Width.ToString();
            string screenHeight = Screen.PrimaryScreen.Bounds.Height.ToString();

            string status = "Aktive";
            int totalParkingSpaces = parkingService.GetTotalParkingSpaces();

            int reservedSpaces = parkingService.GetNumberOfReservedSpaces(status);
            int freeSpaces = totalParkingSpaces - reservedSpaces;

            if (freeSpaces == totalParkingSpaces)
            {
                MessageBox.Show("Full Parking ", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


            label1.Text = label1.Text.Replace(" Free Space", "") + $":{freeSpaces} /  {totalParkingSpaces}";


        }
     
      
        private void label1_Click_1(object sender, EventArgs e)
        {

        }



        private void label2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Check checkin = new Check();
            checkin.ShowDialog();

        }

        private void label4_Click(object sender, EventArgs e)
        {
            this.Hide();
            CheckOut checkOut = new CheckOut();
            checkOut.ShowDialog();

        }

        private void label3_Click(object sender, EventArgs e)
        {
            this.Hide();
            Reports reports = new Reports();
            reports.ShowDialog();
        }

        private void label5_Click(object sender, EventArgs e)
        {

            this.Hide();
            SettingsAdmin settingsAdmin = new SettingsAdmin();
            settingsAdmin.ShowDialog();
        }

        private void label6_Click(object sender, EventArgs e)
        {
            this.Hide();
            Login Login = new Login();
            Login.ShowDialog();
        }
    }
}
