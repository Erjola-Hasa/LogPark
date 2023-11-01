using LogPark.BLL;
using LogPark.DAL;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace LogPark
{
    public partial class Reports : Form
    {
        public Reports()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Normal;
            int screenWidth = Screen.PrimaryScreen.Bounds.Width;
            int screenHeight = Screen.PrimaryScreen.Bounds.Height;
            Resolution objFormResizer = new Resolution();
            objFormResizer.ResizeForm(this, screenHeight, screenWidth);

        }


        private void button1_Click(object sender, EventArgs e)
        {
            DateTime startDate = dateTimePicker1.Value;
            DateTime endDate = dateTimePicker2.Value;

             ParkingService parkingService = new ParkingService(new ParkingDAL());

             ReportData reportData = parkingService.GenerateReport(startDate, endDate);

          
             label5.Text = reportData.TotalCarsInParking.ToString();

            //   label1.Text = $"Total Cars:{reportData.TotalCarsInParking}";
             label6.Text=label6.Text.Replace("Minimum Stay","")+$" { reportData.MinimumStayTime}";

           
            label7.Text = label7.Text.Replace("Maximum Stay", "") + $" {reportData.MaximumStayTime}";

           
            label8.Text =label8.Text.Replace("Avarage Stay","")+$" { reportData.AverageStay}";

        }
    }
}