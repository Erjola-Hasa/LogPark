using BusinessLayer;
using DataAccesLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LogPark
{
    public partial class Check : Form
    {
        ParkingService parkingService;
        public Check()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Normal;
            int screenWidth = Screen.PrimaryScreen.Bounds.Width;
            int screenHeight = Screen.PrimaryScreen.Bounds.Height;
            Resolution objFormResizer = new Resolution();
            objFormResizer.ResizeForm(this, screenHeight, screenWidth);


           
        }

        private void Check_Load(object sender, EventArgs e)
        {
            int totalParkingSpaces = parkingService.GetTotalParkingSpaces();

            string status = "Aktive";
            int reservedSpaces = parkingService.GetNumberOfReservedSpaces(status);
            int freeSpaces = totalParkingSpaces - reservedSpaces;


            if (freeSpaces == totalParkingSpaces)
            {
                MessageBox.Show("Full", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        
            label2.Text =label2.Text.Replace(" Free Space", "") + $":{freeSpaces} /  {totalParkingSpaces}"; 
        }

        private void button1_Click(object sender, EventArgs e)
        {
            

            Checkin checkin = new Checkin();
            checkin.Show(); 
            this.Hide();
        }
       
    }
}
