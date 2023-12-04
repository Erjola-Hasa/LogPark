using DataAccesLayer;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace BusinessLayer
{
    public class ParkingService
    {
        private ParkingRepository parkingDAL;
        private PriceRepository _priceRepository;


        /// <summary>
        /// Initializes a new instance of the ParkingRepository,PriceRepository
        /// </summary>


        public ParkingService()
        {
            parkingDAL = new ParkingRepository();
            _priceRepository = new PriceRepository();
            
        }

        /// <summary>
        ///  Define a static logger variable
        /// </summary>
        /// 
        private static readonly ILog log = LogManager.GetLogger(typeof(ParkingService));


        /// <summary>
        /// Initializes a new instance of the ParkingService class with a reference to a ParkingRepository object.
        /// </summary>
        /// <param name="parkingDAL"></param>


        



        /// <summary>
        /// Retrieves the total number of parking spaces 
        /// </summary>
        /// <returns></returns>
        public int GetTotalParkingSpaces()
        {
            return parkingDAL.GetTotalParkingSpaces();
        }




        /// <summary>
        /// Retrieves the number of reserved parking spaces based on the provided reservation status.
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        public int GetNumberOfReservedSpaces(string status)
        {
           
            int reservations = parkingDAL.GetReservationsFromDatabase(status);
            return reservations;
        }



        /// <summary>
        /// Inserts a check-in record into the parking system with the given barcode and check-in date/time.
        /// </summary>
        /// <param name="barcode"></param>
        /// <param name="checkInDateTime"></param>
        public string CheckIn(string barcode, DateTime checkInDateTime)
        {
            try
            {

                if (IsValidBarcode(barcode) && IsValidCheckInDateTime(checkInDateTime))

                {
                    parkingDAL.InsertCheckIn(barcode, checkInDateTime);
                    
                    return parkingDAL.ToString();
                }
            }
            catch (Exception ex)
            {
               log.Error(ex.Message);
                MessageBox.Show("An error has occurred. Please verify your Data!");
              
            }
            return null;
        }


        /// <summary>
        /// Validates if the provided check-in date/time is equal to the current date/time.
        /// </summary>
        /// <param name="checkInDateTime"></param>
        /// <returns></returns>
        private bool IsValidCheckInDateTime(DateTime checkInDateTime)
        {
          
            return checkInDateTime == DateTime.Now;
        }


        /// <summary>
        /// Validates if the provided barcode is not null or empty.
        /// </summary>
        /// <param name="barcode"></param>
        /// <returns></returns>
        private bool IsValidBarcode(string barcode)
        {
            
            return !string.IsNullOrEmpty(barcode);
        }




        /// <summary>
        /// Calculates the current exit time for a vehicle that entered the parking lot at the specified entry time.
        /// </summary>
        /// <param name="entryTime"></param>
        /// <returns></returns>
        public DateTime CalculateExitTime(DateTime entryTime)
        {
            return DateTime.Now; 
        }




        /// <summary>
        /// Calculates the parking price based on the provided entry and exit times.
        /// </summary>
        /// <param name="entryTime"></param>
        /// <param name="exitTime"></param>
        /// <returns></returns>
        public int CalculatePrice(DateTime entryTime, DateTime exitTime)
        {
               
                int pricePerHour = _priceRepository.GetPrice();
                TimeSpan duration = exitTime - entryTime;
                int hours = (int)duration.TotalHours;

                int totalPrice = (int)hours * pricePerHour;
                return totalPrice;
           
        }




        /// <summary>
        /// Retrieves the entry time for a vehicle with the given barcode from the database.
        /// </summary>
        /// <param name="barcode"></param>
        /// <returns></returns>
        public DateTime GetEntryTimeFromDatabase(string barcode)
        {
            return parkingDAL.GetEntryTimeFromDatabase(barcode);
        }



        /// <summary>
        /// Retrieves the reservation ID for a vehicle with the given barcode from the database.
        /// </summary>
        /// <param name="barcode"></param>
        /// <returns></returns>
        public int GetReservationIDFromDatabase(string barcode)
        {
            return parkingDAL.GetReservationIDFromDatabase(barcode);


        }



        /// <summary>
        /// Retrieves the status (e.g., reserved, checked-in) for a vehicle with the given barcode from the database.
        /// </summary>
        /// <param name="barcode"></param>
        /// <returns></returns>
        public string GetStatus(string barcode)
        {
            return parkingDAL.GetStatusDFromDatabase(barcode);
          
        }


        /// <summary>
        /// Updates the reservation record in the database with the provided reservation ID, exit time, and price.
        /// </summary>
        /// <param name="reservationID"></param>
        /// <param name="exitTime"></param>
        /// <param name="price"></param>
        public void UpdateReservation(int reservationID, DateTime exitTime, decimal price)
        {
            parkingDAL.UpdateReservation(reservationID, exitTime, price);
        }



        /// <summary>
        /// Generates a parking report for the specified period, including statistics such as the total number of cars,
        /// minimum and maximum stay times, and average stay time. The report is returned as a ReportData object.
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public ReportData GenerateReport(DateTime startDate, DateTime endDate)
        {
            try
            {

                List<Reservations> reservations = parkingDAL.GetReservationsInPeriod(startDate, endDate);

                DateTime startTime;
                DateTime endTime;

                if (reservations.Any())
                {
                    startTime = reservations.Min(reservation => reservation.StartDateTime);
                    endTime = reservations.Max(reservation => reservation.EndDateTime);
                }
                else
                {

                    startTime = DateTime.MinValue;
                    endTime = DateTime.MinValue;
                }

                int minimumStayTime = 0;
                int maximumStayTime = 0;
                int averageStay = 0;

                bool hasValidStayTimes = false;

                int totalStayTime = 0;

                foreach (var reservation in reservations)
                {
                    TimeSpan stayTime = reservation.EndDateTime - reservation.StartDateTime;
                    int stayTimeHours = (int)stayTime.TotalHours;

                    if (!hasValidStayTimes || stayTimeHours < minimumStayTime)
                    {
                        minimumStayTime = stayTimeHours;
                        hasValidStayTimes = true;
                    }

                    if (!hasValidStayTimes || stayTimeHours > maximumStayTime)
                    {
                        maximumStayTime = stayTimeHours;
                        hasValidStayTimes = true;
                    }


                    totalStayTime += stayTimeHours;
                }

                int totalCarsInParking = reservations.Count;


                if (totalCarsInParking > 0)
                {
                    averageStay = totalStayTime / totalCarsInParking;
                }

                ReportData reportData = new ReportData
                {
                    TotalCarsInParking = totalCarsInParking,
                    MinimumStayTime = hasValidStayTimes ? minimumStayTime : 0,
                    MaximumStayTime = hasValidStayTimes ? maximumStayTime : 0,
                    AverageStay = (int)averageStay
                };

                return reportData;
            }
             catch(Exception ex)
            {
                log.Error(ex.Message);
                MessageBox.Show("An error has occurred. Please try again.");
            }
            return null;
            }


    }
}

