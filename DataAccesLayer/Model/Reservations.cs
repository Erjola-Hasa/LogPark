using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogPark.DAL
{
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
