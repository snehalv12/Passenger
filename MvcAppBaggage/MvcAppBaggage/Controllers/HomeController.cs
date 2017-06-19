using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using MvcAppBaggage.Models;


namespace MvcAppBaggage.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/       
        public ActionResult Index()
        {

            return View();
            // return RedirectToAction("BaggageDetails","PassengerController");
        }
        [HttpPost]
        public ActionResult Subscribe(HomeClass model)
        {
            return RedirectToAction("BaggageDetails", "PassengerController", model);
        }

    }
}
