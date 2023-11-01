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
            StartPosition = FormStartPosition.Manual;
            Rectangle screen = Screen.FromPoint(Cursor.Position).WorkingArea;
            int w = Width >= screen.Width ? screen.Width : (screen.Width + Width) / 2;
            int h = Height >= screen.Height ? screen.Height : (screen.Height + Height) / 2;
            Location = new Point(screen.Left + (screen.Width - w) / 2, screen.Top + (screen.Height - h) / 2);
            Size = new Size(w, h);
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

