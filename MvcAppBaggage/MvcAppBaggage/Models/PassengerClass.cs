using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcAppBaggage.Models
{
    public class PassengerClass
    {

        public string passengerFirstName { get; set; }
        public string passengerLastName { get; set; }
        public string shipName { get; set; }
        public string bagStatus { get; set; }
        public string airline { get; set; }
        public string departureAirport { get; set; }
        public int cabin { get; set; }
        public int bagTagNumber { get; set; }
        public int flightNumber { get; set; }
        public DateTime returnDate { get; set; }
        public DateTime lastScanDate { get; set; }
        public string departureTime { get; set; }
    }


}