using LogPark.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace LogPark.BLL
{
    public class ParkingService
    {
        private ParkingRepository parkingDAL;

        public ParkingService(ParkingRepository parkingDAL)
        {
            this.parkingDAL = parkingDAL;
        }

        public int GetTotalParkingSpaces()
        {
            return parkingDAL.GetTotalParkingSpaces();
        }
        public int GetNumberOfReservedSpaces(string status)
        {
           
            int reservations = parkingDAL.GetReservationsFromDatabase(status);

            int reservedSpaces = reservations;

            return reservedSpaces;
        }
        public void CheckIn(string barcode, DateTime checkInDateTime)
        {
            

            if (IsValidBarcode(barcode) || IsValidCheckInDateTime(checkInDateTime))

            {
              parkingDAL.InsertCheckIn(barcode, checkInDateTime);

            }
        }

        private bool IsValidCheckInDateTime(DateTime checkInDateTime)
        {
          
            return checkInDateTime == DateTime.Now;
        }

        private bool IsValidBarcode(string barcode)
        {
            
            return !string.IsNullOrEmpty(barcode);
        }

        public DateTime CalculateExitTime(DateTime entryTime)
        {
            return DateTime.Now; 
        }

        public int CalculatePrice(DateTime entryTime, DateTime exitTime)
        {
             int  pricePerHour= parkingDAL.GetPricePerHours();
            TimeSpan duration = exitTime - entryTime;
            int  hours =(int)duration.TotalHours;

            int totalPrice = (int)hours * pricePerHour;

            return totalPrice;
        }
        public DateTime GetEntryTimeFromDatabase(string barcode)
        {
            return parkingDAL.GetEntryTimeFromDatabase(barcode);
        }
        public int GetReservationIDFromDatabase(string barcode)
        {
            return parkingDAL.GetReservationIDFromDatabase(barcode);


        }
        public void UpdateReservation(int reservationID, DateTime exitTime, decimal price)
        {
            parkingDAL.UpdateReservation(reservationID, exitTime, price);
        }
        public ReportData GenerateReport(DateTime startDate, DateTime endDate)
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
            int maximumStayTime= 0;
            int averageStay = 0;

            bool hasValidStayTimes = false;

            int totalStayTime = 0; 

            foreach (var reservation in reservations)
            {
                TimeSpan stayTime = reservation.EndDateTime - reservation.StartDateTime;
                int stayTimeHours =(int) stayTime.TotalMinutes; 

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


    }
}

