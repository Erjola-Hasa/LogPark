using LogPark.BLL;
using LogPark.DAL;
using MySqlX.XDevAPI.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LogPark
{
    public partial class Checkin : Form
    {
        private ParkingService parkingService;

      
        public Rectangle ClientArea { get; }

        public Checkin()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Normal;
            int screenWidth = Screen.PrimaryScreen.Bounds.Width;
            int screenHeight = Screen.PrimaryScreen.Bounds.Height;
            Resolution objFormResizer = new Resolution();
            objFormResizer.ResizeForm(this, screenHeight, screenWidth);

            parkingService = new ParkingService(new ParkingDAL());

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

    }
}

