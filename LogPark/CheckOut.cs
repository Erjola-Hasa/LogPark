using LogPark.BLL;
using LogPark.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace LogPark
{
    public partial class CheckOut : Form
    {
        public CheckOut()
        {
            InitializeComponent();
           


        }

        public void button1_Click(object sender, EventArgs e)
        {
             string barcode = textBox1.Text;
            
            ParkingService parkingService = new ParkingService(new ParkingDAL());


            DateTime entryTime = parkingService.GetEntryTimeFromDatabase(barcode);
            DateTime exitTime = DateTime.Now;
         

             decimal price = parkingService.CalculatePrice(entryTime, exitTime);
         
             int reservationID = parkingService.GetReservationIDFromDatabase(barcode);

            DialogResult result1 = MessageBox.Show("Are you sure you want to procced with the action?",
                "Confirm Action",
                MessageBoxButtons.OKCancel,
                MessageBoxIcon.Question);

            if (result1 == DialogResult.OK)
            {
                parkingService.UpdateReservation(reservationID, exitTime, price);
                this.Hide();

                Dashboard dashboard = new Dashboard();

                dashboard.Show();

                MessageBox.Show("Your Check Out ", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
         
            else
            {
                this.Hide();
                CheckOut checkOut = new CheckOut();
                checkOut.Show();
            }



        }


        public  void button2_Click(object sender, EventArgs e)
        {
            string barcode = textBox1.Text;

           

            ParkingService parkingService = new ParkingService(new ParkingDAL());

            int reservationID = parkingService.GetReservationIDFromDatabase(barcode);
            DateTime entryTime = parkingService.GetEntryTimeFromDatabase(barcode);
       

            if (entryTime != DateTime.MinValue)
            {
                label4.Text=label4.Text.Replace("Entry Time","")+$" {entryTime}";
              //  label1.Text = $"Entry Time: {entryTime}";

               
                DateTime exitTime = DateTime.Now; 
              
                label5.Text = label5.Text.Replace("Exit Time","") + $" {exitTime}";

                decimal price = parkingService.CalculatePrice(entryTime, exitTime);
              
                label6.Text = label6.Text.Replace("Price", "") + $" {price}";

            
            }
            else
            {
                MessageBox.Show("Invalid barcode or code not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        
     
        
       
        }

    }
}
