using Dapper;
using Microsoft.SqlServer.Management.Smo;
using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.IdentityModel.Protocols.WSTrust;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogPark.DAL
{
    public class ParkingDAL
    {
        MyConnection db = new MyConnection();
        public int GetTotalParkingSpaces()
        {

            db.con.Open();
            db.con.Close();

            return db.con.ExecuteScalar<int>("TotalParkingSpaces", commandType: CommandType.StoredProcedure);

          
        }


        public int GetReservationsFromDatabase(string status)

        {    db.con.Open(); 
            db.con.Close(); 

            var parameters = new
            {
                Status = status,
            };

            return db.con.ExecuteScalar<int>("SelectFreeSpace", parameters, commandType: CommandType.StoredProcedure);
            

        }
         
        public void InsertCheckIn(string barcode, DateTime checkInDateTime)
        {
           
                db.con.Open();
               

            var parameters = new
                {
                    RezervationCode = barcode,
                    StartDateTime = checkInDateTime,
                   
                   
                };
 
                db.con.Execute("InsertReservation", parameters, commandType: CommandType.StoredProcedure);

            db.con.Close();
            
        }
        public void UpdateReservation(int reservationID, DateTime exitTime, decimal price)
        {
            db.con.Open();
         
            var parameters = new
            {
                ReservationID = reservationID,
                EndDateTime = exitTime,
                Price = price
            };

            db.con.Execute("UpdateReservation", parameters, commandType: CommandType.StoredProcedure);

            db.con.Close();
        }
        public DateTime GetEntryTimeFromDatabase(string barcode)
        {
            DateTime entryTime = DateTime.MinValue; 

            
           db.con.Open();

            var parameters = new
            {
                RezervationCode = barcode,
            };
      

            object result = db.con.ExecuteScalar("SelectEntryTime", parameters, commandType: CommandType.StoredProcedure);
            db.con.Close();
            if (result != null && result != DBNull.Value)
            {
                entryTime = (DateTime)result;
            }
            return entryTime;
           
        }

    



    
        public int GetReservationIDFromDatabase(string barcode)
        {
            db.con.Open();
            int reservationID = -1;
         
            var parameters = new
            {
                RezervationCode = barcode,
            };
           // db.con.Open();

            object result = db.con.ExecuteScalar<int>("SelectRezervationID",parameters, commandType: CommandType.StoredProcedure);
            db.con.Close();
            if (result != null && result != DBNull.Value)
            {
                reservationID = (int)result;
              
            }
            return reservationID;
        }
       







        public List<Reservations> GetReservationsInPeriod(DateTime StartDateTime, DateTime EndDateTime)
        {
            List<Reservations> reservations = new List<Reservations>();

            try
            {
                db.con.Open();
                var parameters = new
                {
                    StartDateTime = StartDateTime,
                    EndDateTime = EndDateTime
                };
                  var reader= db.con.ExecuteReader("ReservationsInPeriod", parameters, commandType: CommandType.StoredProcedure);
               
                  {
                     while (reader.Read())
                     {
                        Reservations reservation = new Reservations
                        {
                            ReservationID = (int)reader["ReservationID"],
                            StartDateTime = reader.GetDateTime(reader.GetOrdinal("StartDateTime")),
                            EndDateTime = reader.GetDateTime(reader.GetOrdinal("EndDateTime")),


                        };
                        reservations.Add(reservation);
                     }
                  }
            }
            finally
            {
                db.con.Close();
            }

            return reservations;
        }

    
            

     
        public int GetPricePerHours()
        {

           // db.con.Open();

            return db.con.ExecuteScalar<int>("PricePerHour", commandType: CommandType.StoredProcedure);
      


          //  string query = "SELECT PricePerHour FROM Configuration";

            //using (var command = new SqlCommand(query, db.con))
            //{
            //    return (int)command.ExecuteScalar();
            //}
        }

    }

}



           

