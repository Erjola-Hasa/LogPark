using BusinessLayer;
using DataAccesLayer;
using System;
using System.Drawing;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;


namespace LogPark
{
    public partial class Dashboard : Form
    {

        private ParkingService parkingService;


        public Dashboard()
        {
            InitializeComponent();
            string SaveLanguage = Properties.Settings.Default.Language;
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(SaveLanguage);
            parkingService = new ParkingService(new ParkingRepository());


        }

        private void Dashboard_Load(object sender, EventArgs e)
        {


            int totalParkingSpaces = parkingService.GetTotalParkingSpaces();
            string status = "Aktive";
            int reservedSpaces = parkingService.GetNumberOfReservedSpaces(status);
            int freeSpaces = totalParkingSpaces - reservedSpaces;

           
            label5.Text = label5.Text.Replace(" Free Space", "") + $":{freeSpaces} /  {totalParkingSpaces}";
            // label5.Text = $"Free Space : {freeSpaces} /  {totalParkingSpaces}";

        }




        private void label1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Checkin check = new Checkin();
            check.ShowDialog();

        }

        private void label3_Click(object sender, EventArgs e)
        {
            this.Hide();
            CheckOut checkOut = new CheckOut();
            checkOut.ShowDialog();


        }



        private void label4_Click(object sender, EventArgs e)
        {
            this.Hide();
            Setting settings = new Setting();
            settings.ShowDialog();


        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Login login = new Login();
            login.ShowDialog();
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }
    }
}
