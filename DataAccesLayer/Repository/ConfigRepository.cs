using System;
using System.Data.SqlClient;
using System.Windows.Forms;


namespace DataAccesLayer
{
    public class ConfigRepository
    {
      


        /// <summary>
        /// Method to Save the ConnectionString 
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="Password"></param>
        /// <param name="ServerName"></param>
        /// <param name="DatabaseName"></param>
        /// <returns></returns>

        public bool ConnectDatabase(string UserId, string Password, string ServerName, String DatabaseName)
        {
            string ConnectionString = $"Data Source={ServerName};Initial Catalog={DatabaseName};User Id ={UserId};Password={Password} Trusted_Connection=true; MultipleActiveResultSets=true;Integrated Security=True;TrustServerCertificate=Yes";
            using (SqlConnection con = new SqlConnection(ConnectionString))
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

                      
                        return true;
                       

                    }
                    con.Close();

                


               
                return false;
            }



        }








        /// <summary>
        /// Method to Test the ConnestionString if  UserId,Password,ServerName,DatabaseName are correct
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="Password"></param>
        /// <param name="ServerName"></param>
        /// <param name="DatabaseName"></param>
        /// <returns></returns>


        public bool TestDatabase(string UserId, string Password, string ServerName, string DatabaseName)
        {
            string ConnectionString = $"Data Source={ServerName};Initial Catalog={DatabaseName};User Id ={UserId};Password={Password}; Trusted_Connection=true; Integrated Security=false; TrustServerCertificate=Yes";
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
               
                    if (string.IsNullOrWhiteSpace(ServerName) || string.IsNullOrWhiteSpace(DatabaseName) || string.IsNullOrWhiteSpace(UserId) || string.IsNullOrWhiteSpace(Password))
                    {
                        MessageBox.Show("Please enter value in all field.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {

                        con.Open();
                        Properties.Settings.Default.Connection = ConnectionString;
                        con.Close();
                        return true;

                    }

                


               
                return false;
            }



        }


    }

}
