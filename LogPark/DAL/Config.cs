using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace LogPark.DAL
{
    public class Config
    {

        public string connectionString;
        public Config()
        {
            string ConnectionString = Properties.Settings.Default.ConnectionString;
        }
 
        public void   ConnectToDatabase(string UserId, string Password, string ServerName, String DatabaseName)
        {
            string ConnectionString = $"Data Source={ServerName};Initial Catalog={DatabaseName};User Id ={UserId};Password={Password} Trusted_Connection=true; MultipleActiveResultSets=true;Integrated Security=True;TrustServerCertificate=Yes";
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                try
                {
                    con.Open();

                    Properties.Settings.Default.Connection = ConnectionString;
                    Properties.Settings.Default.Save();
                    con.Close();

                }

                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }

            }
         


        }
     


    }
}
