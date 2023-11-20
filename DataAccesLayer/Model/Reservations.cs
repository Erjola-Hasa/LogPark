using System;

namespace DataAccesLayer
{

    /// <summary>
    /// Model about Reservations
    /// </summary>
    public  class Reservations
    {
        public int ReservationID { get; set; }
        public int ParkingSpaceID { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public string Status { get; set; }
        public int RezervationCode { get; set; }
        public int Price { get; set; }
        


    }
}
