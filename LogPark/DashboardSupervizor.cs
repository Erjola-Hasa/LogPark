using BusinessLayer;
using DataAccesLayer;
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
    public partial class DashboardSupervizor : Form
    {
        private ParkingService parkingService;
        public DashboardSupervizor()
        {
            InitializeComponent();


            string SaveLanguage = Properties.Settings.Default.Language;

            Thread.CurrentThread.CurrentUICulture = new CultureInfo(SaveLanguage);

            parkingService = new ParkingService(new ParkingRepository());

        }

        private void DashboardSupervizor_Load(object sender, EventArgs e)
        {
            string status = "Aktive";
            int totalParkingSpaces = parkingService.GetTotalParkingSpaces();

            int reservedSpaces = parkingService.GetNumberOfReservedSpaces(status);
            int freeSpaces = totalParkingSpaces - reservedSpaces;

            if (freeSpaces == totalParkingSpaces)
            {
                MessageBox.Show("Full", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


            label5.Text = label5.Text.Replace(" Free Space", "") + $":{freeSpaces} /  {totalParkingSpaces}";





        }

        private void label1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Check check = new Check();
            check.Show();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            this.Hide();
            CheckOut check = new CheckOut();
            check.Show();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Reports reports = new Reports();
            reports.Show();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            this.Hide();
            SettingSupervizor setting = new SettingSupervizor();
            setting.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Login login = new Login();
            login.Show();
        }
    }
}
