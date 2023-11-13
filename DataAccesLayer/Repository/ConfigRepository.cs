using System;
using System.Data.SqlClient;
using System.Windows.Forms;


namespace DataAccesLayer
{
    public class ConfigRepository
    {
        public ConfigRepository()
        {
            // string ConnectionString = Properties.Settings.Default.ConnectionString;
        }

        public bool ConnectDatabase(string UserId, string Password, string ServerName, String DatabaseName)
        {
            string ConnectionString = $"Data Source={ServerName};Initial Catalog={DatabaseName};User Id ={UserId};Password={Password} Trusted_Connection=true; MultipleActiveResultSets=true;Integrated Security=True;TrustServerCertificate=Yes";
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                try
                {
                    con.Open();
                    if (string.IsNullOrWhiteSpace(ServerName) || string.IsNullOrWhiteSpace(DatabaseName) || string.IsNullOrWhiteSpace(UserId) || string.IsNullOrWhiteSpace(Password))
                    {
                        MessageBox.Show("Please enter value in all field.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    }
                    else
                    {
                       Properties.Settings.Default.Connection = ConnectionString;
                       Properties.Settings.Default.Save();

                        con.Close();
                        return true;

                    }

                }


                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                return false;
            }



        }
        public bool TestDatabase(string UserId, string Password, string ServerName, String DatabaseName)
        {
            string ConnectionString = $"Data Source={ServerName};Initial Catalog={DatabaseName};User Id ={UserId};Password={Password} Trusted_Connection=true; MultipleActiveResultSets=true;Integrated Security=True;TrustServerCertificate=Yes";
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                try
                {
                    con.Open();
                    if (string.IsNullOrWhiteSpace(ServerName) || string.IsNullOrWhiteSpace(DatabaseName) || string.IsNullOrWhiteSpace(UserId) || string.IsNullOrWhiteSpace(Password))
                    {
                        MessageBox.Show("Please enter value in all field.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        Properties.Settings.Default.Connection = ConnectionString;

                        con.Close();
                        return true;

                    }

                }


                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                return false;
            }



        }


    }
    
}
