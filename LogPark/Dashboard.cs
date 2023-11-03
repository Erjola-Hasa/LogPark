using LogPark.BLL;
using LogPark.DAL;
using System;
using System.Drawing;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;


namespace LogPark
{
    public partial class Dashboard : Form
    {
        
        private ParkingService parkingService;
       

        public Dashboard( )
        {
            InitializeComponent();

            this.WindowState = FormWindowState.Normal;
            int screenWidth = Screen.PrimaryScreen.Bounds.Width;
            int screenHeight = Screen.PrimaryScreen.Bounds.Height;
            Resolution objFormResizer = new Resolution();
            objFormResizer.ResizeForm(this, screenHeight, screenWidth);

            string SaveLanguage = Properties.Settings.Default.Language;
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(SaveLanguage);

            parkingService = new ParkingService(new ParkingRepository());
        }

        private void Dashboard_Load(object sender, EventArgs e)
        {
  

            int totalParkingSpaces = parkingService.GetTotalParkingSpaces();
            string status = "Aktive";
            int reservedSpaces = parkingService.GetNumberOfReservedSpaces(status);
            int freeSpaces = totalParkingSpaces - reservedSpaces;


            if (freeSpaces == totalParkingSpaces)
            {
                MessageBox.Show("Full", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            label5.Text = label5.Text.Replace(" Free Space", "") + $":{freeSpaces} /  {totalParkingSpaces}";
            // label5.Text = $"Free Space : {freeSpaces} /  {totalParkingSpaces}";

        }
        
        


            private void label1_Click(object sender, EventArgs e)
            {
            this.Hide();
            Check check = new Check();
            check.ShowDialog();
            this.Hide();
             }

        private void label3_Click(object sender, EventArgs e)
        {
            this.Hide();
            CheckOut checkOut = new CheckOut();
            checkOut.ShowDialog();
            this.Hide();

        }

       

        private void label4_Click(object sender, EventArgs e)
        {
            this.Hide();
            Setting settings = new Setting();
            settings.ShowDialog();


        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Login login = new Login();
            login.ShowDialog();
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }
    }
}
