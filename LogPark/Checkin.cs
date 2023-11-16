using BusinessLayer;
using DataAccesLayer;
using System;
using System.Windows.Forms;

namespace LogPark
{
    public partial class Checkin : Form
    {
        private ParkingService parkingService;

        public Checkin()
        {
            InitializeComponent();
            parkingService = new ParkingService(new ParkingRepository());

        }

        public void Checkin_Load(object sender, EventArgs e)
        {
         
            DateTime checkInDateTime = DateTime.Now;
            label3.Text = GenerateRandomBarcode();
            label4.Text = checkInDateTime.ToString();

            

        }

        public void button1_Click(object sender, EventArgs e)
        {

            DialogResult result1 = MessageBox.Show("Are you sure you want to proceed with the action?", "Confirmation", MessageBoxButtons.OKCancel,MessageBoxIcon.Question);
          
            if (result1 == DialogResult.OK)
            {
                string barcode = label3.Text;
                DateTime checkInDateTime = DateTime.Now;
                parkingService.CheckIn(barcode,checkInDateTime);
                MessageBox.Show("Your action was completed successfully", "Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Hide();
                DashboardAdmin dashboardAdmin = new DashboardAdmin();
                dashboardAdmin.Show();
            }
            else  
            {
                this.Hide();
                Dashboard dashboard = new Dashboard();
                dashboard.ShowDialog();

            
            }



        }
        private   string GenerateRandomBarcode()
        {
            
            Random random = new Random();
            int randomNumber = random.Next(10000, 99999);
            return randomNumber.ToString();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}

