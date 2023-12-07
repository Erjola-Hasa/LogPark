using BusinessLayer;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace LogPark
{
    public partial class Check : Form
    {
        /// <summary>
        ///  Define a private  parkingService variable
        /// </summary>
        private ParkingService parkingService;



        /// <summary>
        /// InitializeComponent is a method which is used to initialize  form
        /// Initializes a new instance of the ParkingService 
        /// </summary>
        public Check()
        {
            InitializeComponent();
            parkingService = new ParkingService();



        }



        /// <summary>
        /// Give the Real Time the number of free Space in the parking
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Check_Load(object sender, EventArgs e)
        {

          
            int totalParkingSpaces = parkingService.GetTotalParkingSpaces();

            string status = "Aktive";
            int reservedSpaces = parkingService.GetNumberOfReservedSpaces(status);
            int freeSpaces = totalParkingSpaces - reservedSpaces;
        
            label2.Text =label2.Text.Replace(" Free Space", "") + $":{freeSpaces} /  {totalParkingSpaces}"; 
        }

      
  



        /// <summary>
        /// Override the Close Form event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_FormClosing(Object sender, FormClosingEventArgs e)
        {
            this.Hide();
            if (AuthHelper.GetLoggedInUserRole() == "Admin")
            {
                
                DashboardAdmin dashboard2 = new DashboardAdmin();
                dashboard2.ShowDialog();

            }
            else if (AuthHelper.GetLoggedInUserRole() == "Supervizor" || AuthHelper.GetLoggedInUserRole() == "Supervisor")
            {
                
                DashboardSupervizor dashboard1 = new DashboardSupervizor();
                dashboard1.ShowDialog();

            }

            else
            {
                
                Dashboard dashboard = new Dashboard();
                dashboard.ShowDialog();
            }






        }




        /// <summary>
        ///  Checkin picturebox  to generate the barcode and the datetime
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBox1_Click(object sender, EventArgs e)
        {

            
            this.Hide();
            Checkin checkin = new Checkin();
            checkin.ShowDialog();
        }
    }
}
