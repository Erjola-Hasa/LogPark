using BusinessLayer;
using DataAccesLayer;
using System;
using System.Windows.Forms;


namespace LogPark
{
    #region Menu Users

    
    public partial class Dashboard : Form
    {

        private ParkingService parkingService;

        /// <summary>
        ///  Initializes a new instance of the ParkingService class with a reference to a ParkingRepository object.
        /// </summary>
        public Dashboard()
        {
            InitializeComponent();
            parkingService = new ParkingService(new ParkingRepository());


        }



        /// <summary>
        /// Give the Real Time about Free Space in the parking 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Dashboard_Load(object sender, EventArgs e)
        {


            int totalParkingSpaces = parkingService.GetTotalParkingSpaces();
            string status = "Aktive";
            int reservedSpaces = parkingService.GetNumberOfReservedSpaces(status);
            int freeSpaces = totalParkingSpaces - reservedSpaces;

            if (freeSpaces == 0)
            {
                MessageBox.Show("Full Parking", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            label5.Text = label5.Text.Replace(" Free Space", "") + $":{freeSpaces} /  {totalParkingSpaces}";


        }



        /// <summary>
        /// Checkin button who directs us in the checkin menu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Checkin check = new Checkin();
            check.ShowDialog();

        }




        /// <summary>
        /// CheckOut button who directs us in the CheckOut menu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            CheckOut checkOut = new CheckOut();
            checkOut.ShowDialog();
        }





        /// <summary>
        /// Settings button who directs us in the settings menu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            Setting settings = new Setting();
            settings.ShowDialog();
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

    }
}
#endregion