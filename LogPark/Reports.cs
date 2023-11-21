using BusinessLayer;
using DataAccesLayer;
using System;
using System.Windows.Forms;

namespace LogPark
{
    public partial class Reports : Form
    {

        /// <summary>
        /// Initializes a new instance of the ParkingService class 
        /// </summary>
        public ParkingService parkingService= new ParkingService();
        
        public Reports()
        {
            InitializeComponent();
           
        }

       
        /// <summary>
        /// Activate the generate button with enter Tab
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Reports_Load(object sender, EventArgs e)
        {
              label2.Visible = false;
             this.AcceptButton = button1;
            
           
        }

      
        /// <summary>
        /// Fill the Start Date and End Date and generate the information about Total Cars ,Minimun stay,Maximum stay and Avarage stay
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click_1(object sender, EventArgs e)
        {

            DateTime startDate = dateTimePicker1.Value;
            DateTime endDate = dateTimePicker2.Value;

            if (startDate >= endDate)
            {
                MessageBox.Show("Start Date must be less than or equal to today's date.Please fill in the correct way!", "Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                
                

               
            }
            else
            {


                ReportData reportData = parkingService.GenerateReport(startDate, endDate);
               
                label1.Text = reportData.TotalCarsInParking.ToString();

                label8.Text = label8.Text.Replace("Minimum Stay", " ") + $" {reportData.MinimumStayTime}";

                label5.Text = label5.Text.Replace("Maximum Stay", " ") + $" {reportData.MaximumStayTime}";

                label12.Text = label12.Text.Replace("Avarage Stay", " ") + $" {reportData.AverageStay}";
            }
        }




        /// <summary>
        /// Exit button 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click_1(object sender, EventArgs e)
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