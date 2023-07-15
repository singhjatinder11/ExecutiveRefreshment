using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ExecutiveRefreshment.Controllers
{
    public class ServicesController : Controller
    {
        // GET: Services
        public ActionResult BottledAndFilteredWater()
        {
            return View();
        }
        public ActionResult Vending()
        {
            return View();
        }
        public ActionResult OfficeCoffeeAndTea()
        {
            return View();
        }
        public ActionResult RefreshmentsAndSnackes()
        {
            return View();
        }
        public ActionResult BreakRoomAndJanitorialSupplies()
        {
            return View();
        }
        public ActionResult MicroConventionalStore()
        {
            return View();
        }
    }
}