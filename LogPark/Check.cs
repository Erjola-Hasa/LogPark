using BusinessLayer;
using DataAccesLayer;
using System;
using System.Windows.Forms;

namespace LogPark
{
    public partial class Check : Form
    {
       private   ParkingService parkingService;



        /// <summary>
        /// Initializes a new instance of the ParkingService class with a reference to a ParkingRepository object.
        /// </summary>
        public Check()
        {
            InitializeComponent();
            parkingService = new ParkingService(new ParkingRepository());


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
        /// Checkin button to generate the barcode and the datetime
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click_2(object sender, EventArgs e)
        {
            Checkin checkin = new Checkin();
            checkin.Show();
            this.Hide();

        }
    }
}
