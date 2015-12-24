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
            ViewBag.Cities = (from r in db.Cities select r);
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Search(string City, string Service)
        {
            //var lawyers = db.Lawyers as DbSet<Lawyer>;
            ViewBag.ListOfLawyers = (from r in db.Lawyers.Include(i => i.Courts) where r.CityId == City select r);
            return View();
        }
    }
}