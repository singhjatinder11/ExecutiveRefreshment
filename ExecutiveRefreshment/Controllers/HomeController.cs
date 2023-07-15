using ExecutiveRefreshment.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ExecutiveRefreshment.Controllers
{
    public class HomeController : Controller
    {
        ecommerce2Entities db = new ecommerce2Entities();
        public ActionResult Index()
        {
            return View();
        }

        [ChildActionOnly]
        public ActionResult Menu()
        {
            var query = db.service.ToList();
            //var model = new DashboardVM
         
             return PartialView("_Menu", query);
        }
        public ActionResult AboutUs()
        {
             

            return View();
        }



        public ActionResult ContactUs()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}