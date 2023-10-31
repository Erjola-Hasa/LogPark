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
        MyConnection db = new MyConnection();


     

        public void UpdatePrice(int PricePerHour)
        {
            db.con.Open();

            var parameters = new
            {
                PricePerHour = PricePerHour
              
            
            };

            db.con.Execute("UpdatePrice", parameters, commandType: CommandType.StoredProcedure);

            db.con.Close();
        }

    }
}
