using BusinessLayer;
using System;
using System.Windows.Forms;

namespace LogPark
{
    public partial class Check : Form
    {
        /// <summary>
        /// Initializes a new instance of the ParkingService 
        /// </summary>
        private ParkingService parkingService= new ParkingService();

        public Check()
        {
            InitializeComponent();


        }



        /// <summary>
        /// Give the Real Time the number of free Space in the parking
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Check_Load(object sender, EventArgs e)
        {

            this.AcceptButton = button1;

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
            this.Hide();
            Checkin checkin = new Checkin();
            checkin.ShowDialog();
           

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
    }
}
