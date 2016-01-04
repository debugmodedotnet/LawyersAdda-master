using LawyersAdda.Entities;
using LawyersAdda.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LawyersAdda.Controllers
{
    public class HomeController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            var cities = from r in db.Cities select r;
            ViewBag.Cities = cities;
            var lawservices = from r in db.ServiceTypes select r;
            ViewBag.LawServices = lawservices;
            return View();
        }

        public ActionResult GetCities()
        {
            var cities = from r in db.Cities select r;
            return Json(cities, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetServices()
        {
            var ListofServices = (from r in db.ServiceTypes select r.Name);       
            return Json(ListofServices, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Search(string City, string Service)
        {
            //var lawyers = db.Lawyers as DbSet<Lawyer>;
            ViewBag.ListOfLawyers = (from r in db.Lawyers.Include(i => i.Courts) where r.CityId == City select r);
            return View();
        }

        //public ActionResult SaveSelectedCityInSession(string sessionValue)
        //{
        //    //string strCurrentView = currentViewEnum == AllViewsNames.RazorIndex ? "Index" : "TestPage";


        //}
    }
}