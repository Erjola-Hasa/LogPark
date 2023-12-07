using Dapper;
using System.Data;
using System.Data.SqlClient;

namespace DataAccesLayer
{
    public class PriceRepository
    {
       /// <summary>
       /// Take ConnectionString
       /// </summary>
        string ConnectionString = Properties.Settings.Default.Connection;

       
        /// <summary>
        /// Method to Update Price/Hour In parking using SP
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
        /// Select the price of the parking using SP
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
