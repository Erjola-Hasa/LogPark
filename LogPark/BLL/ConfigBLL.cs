using LogPark.DAL;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Org.BouncyCastle.Math.EC.ECCurve;

namespace LogPark.BLL
{
    public class ConfigBLL
    {
       

        public ConfigBLL()
        {
        }


        public void ConnectToDatabase(string UserId, string Password, string ServerName, String DatabaseName)
        {

            DAL.Config config = new DAL.Config();
            config.ConnectToDatabase(UserId, Password, ServerName, DatabaseName);

            if (config != null)
            {
                MessageBox.Show("Your Connection is successful ", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
                

            }
            else
            {
                MessageBox.Show("Please verify you credintial.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
          
           


        }

        public void TestDatabase(string UserId, string Password, string ServerName, String DatabaseName)
        {
            DAL.Config config = new DAL.Config();
          config.ConnectToDatabase(UserId, Password, ServerName, DatabaseName);

            if (string.IsNullOrWhiteSpace(ServerName) || string.IsNullOrWhiteSpace(DatabaseName) || string.IsNullOrWhiteSpace(UserId) || string.IsNullOrWhiteSpace(Password))
            {
                MessageBox.Show("Please enter value in all field.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
           else if (config !=null )
            {
                MessageBox.Show("Your Connection is successful ", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
          
            else
            {
                MessageBox.Show("Please verify you credintial.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        

        }
    }
}
