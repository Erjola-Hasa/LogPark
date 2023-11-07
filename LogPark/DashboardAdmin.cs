using LogPark.BLL;
using LogPark.DAL;
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

            this.WindowState = FormWindowState.Normal;
            int screenWidth = Screen.PrimaryScreen.Bounds.Width;
            int screenHeight = Screen.PrimaryScreen.Bounds.Height;
            Resolution objFormResizer = new Resolution();
            objFormResizer.ResizeForm(this, screenHeight, screenWidth);


            parkingService = new ParkingService(new ParkingRepository());

        }


        private void label1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Check checkin = new Check();
            checkin.ShowDialog();

        }

        private void label3_Click(object sender, EventArgs e)
        {
            this.Hide();
            CheckOut checkOut = new CheckOut();
            checkOut.ShowDialog();
            this.Hide();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Reports reports = new Reports();
            reports.ShowDialog();
            this.Hide();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            this.Hide();
            SettingsAdmin settingsAdmin = new SettingsAdmin();
            settingsAdmin.ShowDialog();

         
        }
    




        private void DashboardAdmin_Load(object sender, EventArgs e)
        {

            string status = "Aktive";
            int totalParkingSpaces = parkingService.GetTotalParkingSpaces();

            int reservedSpaces = parkingService.GetNumberOfReservedSpaces(status);
            int freeSpaces = totalParkingSpaces - reservedSpaces;

            if (freeSpaces == totalParkingSpaces)
            {
                MessageBox.Show("Full Parking ", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


            label5.Text = label5.Text.Replace(" Free Space", "") + $":{freeSpaces} /  {totalParkingSpaces}";


        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Login login = new Login();
            login.ShowDialog();
        }
    }
}
