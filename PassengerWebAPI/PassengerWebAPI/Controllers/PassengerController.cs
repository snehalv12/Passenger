using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using PassengerWebAPI.DbClass;
using PassengerWebAPI.Models;


namespace PassengerWebAPI.Controllers
{
    public class PassengerController : ApiController
    {
        // GET: Passenger
        [System.Web.Http.HttpPost]
        public IHttpActionResult RegisterEvent(RegEvent EventRegistration)
        {
            Dbclass passenger = new Dbclass();
            int EventId = passenger.RegisterEvent(EventRegistration);
            return Ok("Event Id:" + EventId);
        }


        [System.Web.Http.HttpPost]
        public IHttpActionResult RegisterGuest(RegGuest GuestRegistration)
        {
            Dbclass passenger = new Dbclass();
            bool registeredguest = passenger.RegisterGuest(GuestRegistration);
            return Ok(registeredguest);
        }


    }
}

