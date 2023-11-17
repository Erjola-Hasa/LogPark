using BusinessLayer;
using DataAccesLayer;
using System;
using System.Windows.Forms;

namespace LogPark
{
    #region Menu of Supervizor
    public partial class DashboardSupervizor : Form
    {
        private ParkingService parkingService;
        /// <summary>
        /// Initializes a new instance of the ParkingService class with a reference to a ParkingRepository object.
        /// </summary>
        public DashboardSupervizor()
        {
            InitializeComponent();
            parkingService = new ParkingService(new ParkingRepository());
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
            check.Show();
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
            check.Show();
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
            reports.Show();
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
            setting.Show();
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
            login.Show();
        }


        private void Form1_FormClosing(Object sender, FormClosingEventArgs e)
        {

            if (e.CloseReason == CloseReason.WindowsShutDown) return;

        }
    }
}
#endregion