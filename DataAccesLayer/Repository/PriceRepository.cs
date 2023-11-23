﻿using Dapper;
using log4net;
using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Windows.Forms;

namespace DataAccesLayer
{
    public class PriceRepository
    {
       /// <summary>
       /// Take ConnectionString
       /// </summary>
        string ConnectionString = Properties.Settings.Default.Connection;

       
        /// <summary>
        /// Method to Update Price/Hour In parking
        /// </summary>
        /// <param name="PricePerHour"></param>
        public void UpdatePrice(int PricePerHour)
        {
           
                using (SqlConnection db = new SqlConnection(ConnectionString))
                {
                    db.Open();


                    var parameters = new
                    {
                        PricePerHour = PricePerHour


                    };

                    db.Execute("UpdatePrice", parameters, commandType: CommandType.StoredProcedure);

                    db.Close();
                }
           
        }




        /// <summary>
        /// Select the price of the parking
        /// </summary>
        /// <returns></returns>
        public  int GetPrice()
        {
           
                using (SqlConnection db = new SqlConnection(ConnectionString))
                {

                    return db.ExecuteScalar<int>("PricePerHour", commandType: CommandType.StoredProcedure);

                }
            
            

        }

    }
}
