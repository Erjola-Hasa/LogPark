using LogPark.BLL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace LogPark
{
    public partial class DatabaseSettings : Form
    {
        public DatabaseSettings()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string ServerName = textBox1.Text;
            string DatabaseName = textBox2.Text;
            string UserId = textBox3.Text;
            string Password = textBox4.Text;

            ConfigBLL configBLL = new ConfigBLL();
            configBLL.TestDatabase(UserId, Password, ServerName, DatabaseName);

        }

        private void button2_Click(object sender, EventArgs e)
        {
            string ServerName = textBox1.Text;
            string DatabaseName = textBox2.Text;
            string UserId = textBox3.Text;
            string Password = textBox4.Text;

            ConfigBLL configBLL = new ConfigBLL();
            configBLL.ConnectToDatabase(UserId, Password, ServerName, DatabaseName);


        }
    }
}
