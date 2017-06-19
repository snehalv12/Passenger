using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PassengerWebAPI.Models
{
    public class RegGuest
    {
        public int EventGuestID { get; set; }
        public string Client { get; set; }
        public string ProcessType { get; set; }
        public int EventId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string EmailAddress { get; set; }
        public int Phone { get; set; }
        public int NumberOfBags { get; set; }
        public string Status { get; set; }
        public string Gender { get; set; }
        public DateTime BirthDate { get; set; }
        public string PassportCitizenship { get; set; }
        public string PassportNumber { get; set; }
        public DateTime PassportExpiration { get; set; }
        public string PassportCountry { get; set; }
        public string Opt1 { get; set; }
        public string FirstNameAlias { get; set; }
        public string LastNameAlias { get; set; }
        public bool Eligible { get; set; }
        public string PartyGroupID { get; set; }
        public DateTime ArrivalDate { get; set; }
        public DateTime DepartDate { get; set; }
        public string DepartGroup { get; set; }
        public string GuestType { get; set; }
        public List<FlightSegment> Segments { get; set; }
    }
}