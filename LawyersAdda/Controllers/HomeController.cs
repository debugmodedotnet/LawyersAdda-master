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


        //DJ sir work on this, change the new object
        public ActionResult GetServices(string term)
        {
            var category = "Services";
            if (String.IsNullOrWhiteSpace(term))
            {
                return Json((from r in db.ServiceTypes select new { r.Name, r.Id, category }).ToList(), JsonRequestBehavior.AllowGet);
            }
            else
            {
                var services = (from r in db.ServiceTypes where r.Name.ToLower().Contains(term.ToLower()) select new { r.Name, r.Id, category }).ToList();
                category = "Lawyers";
                var lawyers = (from r in db.Lawyers where r.Name.ToLower().Contains(term.ToLower()) select new { r.Name, r.User.UserName, category }).ToList();
                // var finalResult = services.Concat(lawyers).ToList();

                List<Object> finalResult = (from x in services select (Object)x).ToList();
                finalResult.AddRange((from x in lawyers select (Object)x).ToList());
                return Json(finalResult, JsonRequestBehavior.AllowGet);
            }
           
            

               

          
           

            

            //if (term == " ")
            //    return Json(from r in db.ServiceTypes select r, JsonRequestBehavior.AllowGet);
            //else
            //    return Json((from r in db.ServiceTypes where r.Name.ToLower().Contains(term.ToLower()) select r), JsonRequestBehavior.AllowGet);
        }

        public ActionResult Search(string City, string Service)
        {
            var lawyers = db.Lawyers as DbSet<Lawyer>;
            ViewBag.ListOfLawyers = (from r in db.Lawyers.Include(i => i.Courts) where r.CityId == City select r);
            return View();
        }

        //public ActionResult SaveSelectedCityInSession(string sessionValue)
        //{
        //    //string strCurrentView = currentViewEnum == AllViewsNames.RazorIndex ? "Index" : "TestPage";


        //}
    }
}