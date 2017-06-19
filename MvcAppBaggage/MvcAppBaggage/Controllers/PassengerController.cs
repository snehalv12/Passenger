using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcAppBaggage.Models;

namespace MvcAppBaggage.Controllers
{
    public class PassengerController : Controller
    {
        //
        // GET: /Passenger/

        public ActionResult Index()
        {
            PassengerClass passenger = new PassengerClass();

            passenger.passengerFirstName = "John";
            passenger.passengerLastName = "Sander";
            passenger.shipName = "ShipA";
            passenger.cabin = 1234;
            passenger.returnDate = new DateTime(2017, 07, 04);

            passenger.bagTagNumber = 0019588765;
            passenger.lastScanDate = new DateTime(2017, 07, 04);
            passenger.bagStatus = "Delivered to Airport";

            passenger.airline = "AA";
            passenger.flightNumber = 601;
            passenger.departureTime = "2:15 PM";
            passenger.departureAirport = "Orlando International (MCO)";

            return View(passenger);
        }

    }
}
