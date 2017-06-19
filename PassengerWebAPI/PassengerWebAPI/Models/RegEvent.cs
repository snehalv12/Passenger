using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PassengerWebAPI.Models
{
    public class RegEvent
    {
        public int EventId { get; set; }
        public string Client { get; set; }
        public string ProcessType { get; set; }
        public string Location { get; set; }
        public DateTime DepartDate { get; set; }
        public DateTime ArrivalDate { get; set; }
        public string City { get; set; }
        public string LocationID { get; set; }
        public string Source { get; set; }
    }
}