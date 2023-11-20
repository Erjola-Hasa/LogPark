using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Windows.Forms;

namespace DataAccesLayer
{
    public class ParkingRepository
    {
        /// <summary>
        /// Take ConnectionString
        /// </summary>
        string ConnectionString = Properties.Settings.Default.Connection;


        /// <summary>
        /// Select all the parking Space 
        /// </summary>
        /// <returns></returns>
        public int GetTotalParkingSpaces()
        {
            using (var db = new SqlConnection(ConnectionString))
            {
                db.Open();
                db.Close();
                return db.ExecuteScalar<int>("TotalParkingSpaces", commandType: CommandType.StoredProcedure);

            }
        }






        /// <summary>
        /// Select all Parking Space who has the Status ="Aktive"
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
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





        /// <summary>
        /// Method to checkin in parking and generate barcode and datetime 
        /// </summary>
        /// <param name="barcode"></param>
        /// <param name="checkInDateTime"></param>
        /// 
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





        /// <summary>
        /// Method to update rezervation when do check-out and give datime and price
        /// </summary>
        /// <param name="reservationID"></param>
        /// <param name="exitTime"></param>
        /// <param name="price"></param>
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



        /// <summary>
        /// Select entry time about the specifik barcode
        /// </summary>
        /// <param name="barcode"></param>
        /// <returns></returns>
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




        /// <summary>
        ///  Method to give us the RezervationiD  of specifik Barcode 
        /// </summary>
        /// <param name="barcode"></param>
        /// <returns></returns>

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

    







        /// <summary>
        /// Select the start time and end time and give us the minimal time,totalCars ,maximalTime and AvarageTime
        /// </summary>
        /// <param name="StartDateTime"></param>
        /// <param name="EndDateTime"></param>
        /// <returns></returns>

        public List<Reservations> GetReservationsInPeriod(DateTime StartDateTime, DateTime EndDateTime)
        {
            try
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
            }catch(Exception ex)
            {
                File.AppendAllText("error.log", ex.ToString());
                MessageBox.Show("An error has occurred. Please verify you credintial.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }

        }



        /// <summary>
        /// Method to give us the status of specifik Barcode
        /// </summary>
        /// <param name="barcode"></param>
        /// <returns></returns>


        public string GetStatusDFromDatabase(string barcode)
        {
            try
            {


                using (var db = new SqlConnection(ConnectionString))
                {

                    db.Open();
                    var parameters = new
                    {
                        RezervationCode = barcode,

                    };

                    object result = db.ExecuteScalar("SelectRezervationstatus", parameters, commandType: CommandType.StoredProcedure);
                    {


                        Reservations reservations = new Reservations();
                        {
                            reservations.Status = (string)result;
                        }

                        db.Close();

                        return (string)result;
                    }

                }
            }
            catch (Exception ex)
            {
                File.AppendAllText("error.log", ex.ToString());
                MessageBox.Show("An error has occurred. Please verify you credintial.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;

            }
           // return null;

        }
    }
}



           

