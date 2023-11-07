using Dapper;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;


namespace DataAccesLayer
{
    public class ParkingRepository
    {
        // MyConnection db = new MyConnection();
        string ConnectionString = DataAccesLayer.Properties.Settings.Default.Connection;

        public int GetTotalParkingSpaces()
        {
            using (var db = new SqlConnection(ConnectionString))
            {
                db.Open();
                db.Close();

                return db.ExecuteScalar<int>("TotalParkingSpaces", commandType: CommandType.StoredProcedure);

            }
        }


        public int GetReservationsFromDatabase(string status)

        {
            using (var db = new SqlConnection(ConnectionString))
            {

                db.Open();
                db.Close();

                var parameters = new
                {
                    Status = status,
                };

                return db.ExecuteScalar<int>("SelectFreeSpace", parameters, commandType: CommandType.StoredProcedure);

            }
        }
         
        public void InsertCheckIn(string barcode, DateTime checkInDateTime)
        {
            using (var db = new SqlConnection(ConnectionString))
            {


                db.Open();


                var parameters = new
                {
                    RezervationCode = barcode,
                    StartDateTime = checkInDateTime,


                };

                db.Execute("InsertReservation", parameters, commandType: CommandType.StoredProcedure);

                db.Close();
            }
            
        }
        public void UpdateReservation(int reservationID, DateTime exitTime, decimal price)
        {
            using (var db = new SqlConnection(ConnectionString))
            {

                db.Open();

                var parameters = new
                {
                    ReservationID = reservationID,
                    EndDateTime = exitTime,
                    Price = price
                };

                db.Execute("UpdateReservation", parameters, commandType: CommandType.StoredProcedure);

                db.Close();
            }
        }
        public DateTime GetEntryTimeFromDatabase(string barcode)
        {
            using (var db = new SqlConnection(ConnectionString))
            {

                DateTime entryTime = DateTime.MinValue;


                db.Open();

                var parameters = new
                {
                    RezervationCode = barcode,
                };


                object result = db.ExecuteScalar("SelectEntryTime", parameters, commandType: CommandType.StoredProcedure);
                db.Close();
                if (result != null && result != DBNull.Value)
                {
                    entryTime = (DateTime)result;
                }
                return entryTime;
            }
           
        }

    



    
        public int GetReservationIDFromDatabase(string barcode)
        {
            using (var db = new SqlConnection(ConnectionString))
            {

                db.Open();
                int reservationID = -1;

                var parameters = new
                {
                    RezervationCode = barcode,
                };
                // db.con.Open();

                object result = db.ExecuteScalar<int>("SelectRezervationID", parameters, commandType: CommandType.StoredProcedure);
                db.Close();
                if (result != null && result != DBNull.Value)
                {
                    reservationID = (int)result;

                }
                return reservationID;
            }
        }
       







        public List<Reservations> GetReservationsInPeriod(DateTime StartDateTime, DateTime EndDateTime)
        {
            using (var db = new SqlConnection(ConnectionString))
            {

                List<Reservations> reservations = new List<Reservations>();

                try
                {
                    db.Open();
                    var parameters = new
                    {
                        StartDateTime = StartDateTime,
                        EndDateTime = EndDateTime
                    };
                    var reader = db.ExecuteReader("ReservationsInPeriod", parameters, commandType: CommandType.StoredProcedure);

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
                    db.Close();
                }

                return reservations;
            }
        }

    
            

     
        public int GetPricePerHours()
        {
            using (var db = new SqlConnection(ConnectionString))
            {

                // db.con.Open();

                return db.ExecuteScalar<int>("PricePerHour", commandType: CommandType.StoredProcedure);

            }

          //  string query = "SELECT PricePerHour FROM Configuration";

            //using (var command = new SqlCommand(query, db.con))
            //{
            //    return (int)command.ExecuteScalar();
            //}
        }

    }

}



           

