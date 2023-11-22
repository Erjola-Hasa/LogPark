﻿using log4net;
using System;
using System.Data.SqlClient;
using System.Windows.Forms;


namespace DataAccesLayer
{
    public class ConfigRepository
    {
        /// <summary>
        ///  Define a static logger variable
        /// </summary>
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);




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

                      
                        return true;
                       

                    }
                    con.Close();

                }


                catch (Exception ex)
                {
                    log.Error(ex);
                    MessageBox.Show("An error has occurred.Please verify you credintial", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
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
            string ConnectionString = $"Data Source={ServerName};Initial Catalog={DatabaseName};User Id ={UserId};Password={Password} Trusted_Connection=true; MultipleActiveResultSets=true;Integrated Security=True;TrustServerCertificate=Yes";
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                try
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

                }


                catch (Exception ex)
                {
                   log.Error (ex);
                   MessageBox.Show("An error has occurred. Please try again later.","Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                 
                }
                return false;
            }



        }


    }

}
