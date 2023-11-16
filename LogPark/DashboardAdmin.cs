using BusinessLayer;
using DataAccesLayer;
using System;
using System.Windows.Forms;

namespace LogPark
{
    public partial class DashboardAdmin : Form
    {
        private ParkingService parkingService;

        public DashboardAdmin()
        {
            InitializeComponent();


           
            parkingService = new ParkingService(new ParkingRepository());

        }


        private void DashboardAdmin_Load(object sender, EventArgs e)
        {
         

            string status = "Aktive";
            int totalParkingSpaces = parkingService.GetTotalParkingSpaces();

            int reservedSpaces = parkingService.GetNumberOfReservedSpaces(status);
            int freeSpaces = totalParkingSpaces - reservedSpaces;
            if (freeSpaces == 0)
            {
                MessageBox.Show("Full Parking","Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                

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
