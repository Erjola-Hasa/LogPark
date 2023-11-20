using BusinessLayer;
using DataAccesLayer;
using System;
using System.Windows.Forms;

namespace LogPark
{
    #region Menu Admin
    public partial class DashboardAdmin : Form
    {
        private ParkingService parkingService;

        /// <summary>
        /// Initializes a new instance of the ParkingService class with a reference to a ParkingRepository object.
        /// </summary>
        public DashboardAdmin()
        {
            InitializeComponent();
            parkingService = new ParkingService(new ParkingRepository());

        }


        /// <summary>
        /// Give the Real Time about Free Space in the parking 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DashboardAdmin_Load(object sender, EventArgs e)
        {


            string status = "Aktive";
            int totalParkingSpaces = parkingService.GetTotalParkingSpaces();
            int reservedSpaces = parkingService.GetNumberOfReservedSpaces(status);
            int freeSpaces = totalParkingSpaces - reservedSpaces;
            if (freeSpaces == 0)
            {
                MessageBox.Show("Full Parking", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);


            }
            label1.Text = label1.Text.Replace(" Free Space", "") + $":{freeSpaces} /  {totalParkingSpaces}";


        }




        /// <summary>
        ///  Check button who directs us in the Check menu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Check checkin = new Check();
            checkin.ShowDialog();
        }




        /// <summary>
        /// CheckOut button who directs us in the Checkout menu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            CheckOut checkOut = new CheckOut();
            checkOut.ShowDialog();
        }




        /// <summary>
        /// Reports  button who directs us in the Reports menu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            Reports reports = new Reports();
            reports.ShowDialog();
        }





        /// <summary>
        ///  SettingsAdmin  button who directs us in the SettingsAdmin menu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            SettingsAdmin settingsAdmin = new SettingsAdmin();
            settingsAdmin.ShowDialog();
        }





        /// <summary>
        /// Exit button who directs us in the login menu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void label6_Click(object sender, EventArgs e)
        {
            this.Hide();
            Login Login = new Login();
            Login.ShowDialog();
        }




        /// <summary>
        /// Close the application
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
      


    }
}
#endregion