using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PassengerWebAPI.Models
{
    public class FlightSegment
    {
        public int SegmentID { get; set; }
        public int EventGuestID { get; set; }
        public string Carrier { get; set; }
        public int FlightNumber { get; set; }
        public string DepartureCity { get; set; }
        public string ArrivalCity { get; set; }
        public DateTime DepartureDate { get; set; }
        public string DepartureTime { get; set; }
        public string PNR { get; set; }
        public string Operation { get; set; }
    }
}