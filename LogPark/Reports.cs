using BusinessLayer;
using DataAccesLayer;
using Org.BouncyCastle.Asn1.X509;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace LogPark
{
    public partial class Reports : Form
    {
        public ParkingService parkingService;
        public Reports()
        {
            InitializeComponent();

        
            

            parkingService = new ParkingService(new ParkingRepository());
        }


        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            DashboardAdmin dashboardAdmin = new DashboardAdmin();
            dashboardAdmin.Show();
        }

        private void Reports_Load(object sender, EventArgs e)
        {
              label2.Visible = false;
             this.AcceptButton = button1;
            
           
        }

        private void tableLayoutPanel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {


           

            DateTime startDate = dateTimePicker1.Value;
            DateTime endDate = dateTimePicker2.Value;

           
            

            ReportData reportData = parkingService.GenerateReport(startDate, endDate);


            //   label6.Text = label1.Text.Replace("Total Cars", "") + $"{reportData.TotalCarsInParking}";
            label1.Text = reportData.TotalCarsInParking.ToString();

            //   label1.Text = $"Total Cars:{reportData.TotalCarsInParking}";
            label8.Text = label8.Text.Replace("Minimum Stay", " ") + $" {reportData.MinimumStayTime}";


            label5.Text = label5.Text.Replace("Maximum Stay", " ") + $" {reportData.MaximumStayTime}";


            label12.Text = label12.Text.Replace("Avarage Stay", " ") + $" {reportData.AverageStay}";
        }

        private void button2_Click_1(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);

           if (e.CloseReason == CloseReason.WindowsShutDown) return;

            // Confirm user wants to close
            switch (MessageBox.Show(this, "Are you sure you want to close?", "Closing", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
            {
                case DialogResult.No:
                    e.Cancel = true;
                    break;
                default:
                  DashboardAdmin dashboardAdmin = new DashboardAdmin();
                    dashboardAdmin.Show();
                    break;
                   

                    
            }
        }
    }
}