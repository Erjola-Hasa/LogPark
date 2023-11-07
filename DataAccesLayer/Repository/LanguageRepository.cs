using Dapper;

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccesLayer
{
    public class LanguageRepository
    {
        //  MyConnection db = new MyConnection();
        string ConnectionString = DataAccesLayer.Properties.Settings.Default.Connection;



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
