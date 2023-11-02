using Dapper;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogPark.DAL
{
    public class LanguageRepository
    {
        //  MyConnection db = new MyConnection();
        string ConnectionString = Properties.Settings.Default.Connection;



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
        public  int GetPrice()
        {
            using (SqlConnection db = new SqlConnection(ConnectionString))
            {

                return db.ExecuteScalar<int>("PricePerHour", commandType: CommandType.StoredProcedure);

            }

        }

    }
}
