using BusinessLayer;
using System;
using System.Windows.Forms;


namespace LogPark
{
    #region Menu Users


    public partial class Dashboard : Form
    {
        /// <summary>
        ///  Initializes a new instance of the ParkingService 
        /// </summary>
        private ParkingService parkingService = new ParkingService();



        /// <summary>
        /// InitializeComponent is a method which is used to initialize  form
        /// </summary>
        public Dashboard()
        {
          
            InitializeComponent();
            
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
                button1.Visible = false;
                
            }

            label1.Text = label1.Text.Replace(" Free Space", "") + $":{freeSpaces} /  {totalParkingSpaces}";


        }




        /// <summary>
        /// Checkin button who directs us in the checkin menu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            Check check = new Check();
            check.ShowDialog();
        }



        /// <summary>
        /// LogOut  button who directs us in the Login menu 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            Login login = new Login();
            login.ShowDialog();
        }


        /// <summary>
        /// CheckOut button who directs us in the CheckOut menu
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
        /// Settings button who directs us in the settings menu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
                this.Hide();
                Setting settings = new Setting();
                settings.ShowDialog();
            }




        /// <summary>
        /// Override the Close Form event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_FormClosing(Object sender, FormClosingEventArgs e)
        {

            this.Hide();
            Login login = new Login();
            login.ShowDialog();


        }
    }
}
#endregion