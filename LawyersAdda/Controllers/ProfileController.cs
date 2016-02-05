using LawyersAdda.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using LawyersAdda.Entities;
using System.Data.Entity;

namespace LawyersAdda.Controllers
{
    public class ProfileController : Controller
    {
        //
        // GET: /Profile/
        public ActionResult Index()
        {
            ApplicationDbContext Context = new ApplicationDbContext();
            ApplicationUser user = new ApplicationUser();
            string UID = User.Identity.GetUserId();
            user = Context.Users.Where(t => t.Id == UID).Single();
            if (!(string.IsNullOrEmpty(user.CityId)))
            {
                City city = new City();
                city = Context.Cities.Where(t => t.Id == user.CityId).Single();
                user.City = city;
            }
            ViewBag.User = user;
            Lawyer l = new Lawyer();
            l = user.Lawyer;
            if (user.isLawyer)
            {
                List<ServiceType> lstServices = new List<ServiceType>();
                Context.Entry(l).Collection(t => t.ServiceTypes).Load();
                foreach (ServiceType s in l.ServiceTypes)
                {
                    lstServices.Add(s);
                }
                user.Lawyer.ServiceTypes = lstServices;
                List<Answer> lstAnswers = new List<Answer>();
                lstAnswers = Context.Answers.Where(t => t.LawyerID == UID).ToList();
                foreach (var Answer in lstAnswers)
                {
                    Question q = new Question();
                    q = Context.Questions.Where(t => t.QuestionID == Answer.QuestionID).Single();
                    Answer.Questions = q;
                }
                lstAnswers = lstAnswers.OrderByDescending(t => t.AnswerModifiedDate).Take(10).ToList();
                user.Lawyer.City = Context.Cities.Where(t => t.Id == user.Lawyer.CityId).Single();
                return View("LawyerProfile", lstAnswers);
            }
            else
            {
                List<Question> lstQuestions = new List<Question>();
                List<ServiceType> lstServiceType = new List<ServiceType>();
                List<Lawyer> lstLawyers=new List<Lawyer>();
                lstServiceType = Context.ServiceTypes.ToList();
                lstLawyers = Context.Lawyers.Where(t => t.CityId == user.CityId).Take(10).ToList();
                foreach (Lawyer lawyer in lstLawyers)
                {
                    Context.Entry(lawyer).Collection(t => t.ServiceTypes).Load();
                    foreach (ServiceType s in lawyer.ServiceTypes)
                    {
                        lstServiceType.Add(s);
                    }
                }
                ViewBag.LawyersInCities = lstLawyers; 
                lstQuestions = Context.Questions.Where(t => t.UserID == UID).ToList();
                lstServiceType = Context.ServiceTypes.ToList();
                foreach (var q in lstQuestions)
                {
                    ServiceType service = lstServiceType.Where(t => t.Id == q.ServiceID).Single();
                    q.Services = service;
                }
                lstQuestions = lstQuestions.OrderByDescending(t => t.ModifiedDate).ToList();
                List<City> lstCities = new List<City>();
                lstCities = Context.Cities.ToList();
                ViewBag.Cities = lstCities;
                Session["UserID"] = User.Identity.GetUserId();
                return View("UserProfile", lstQuestions);
            }
        }
        [HttpPost]
        public ActionResult GetQuestions(int? Order, int? SelectQuestion)
        {
            ApplicationDbContext Context = new ApplicationDbContext();
            List<Question> lstQuestions = new List<Question>();
            List<ServiceType> lstServiceType = new List<ServiceType>();
            lstQuestions = Context.Questions.ToList();
            string UID = User.Identity.GetUserId();
            lstQuestions = Context.Questions.Where(t => t.UserID == UID).ToList();
            lstServiceType = Context.ServiceTypes.ToList();
            foreach (var q in lstQuestions)
            {
                ServiceType service = lstServiceType.Where(t => t.Id == q.ServiceID).Single();
                q.Services = service;
            }
            //ViewBag.SelectQuestion = 1;
            switch (Order)
            {
                case 1:
                    lstQuestions = lstQuestions.OrderByDescending(t => t.ModifiedDate).ToList();
                    //ViewBag.Order = 1;
                    break;
                case 2:
                    lstQuestions = lstQuestions.OrderBy(t => t.ModifiedDate).ToList();
                    //ViewBag.Order = 2;
                    break;
                default:
                    lstQuestions = lstQuestions.OrderByDescending(t => t.ModifiedDate).ToList();
                    //ViewBag.Order = 1;
                    break;
            }
            switch (SelectQuestion)
            {
                case 2:
                    lstQuestions = lstQuestions.Where(t => t.isAnswered == true).ToList();
                    //ViewBag.SelectQuestion = 2;
                    break;
                case 3:
                    lstQuestions = lstQuestions.Where(t => t.isAnswered == false).ToList();
                    //ViewBag.SelectQuestion = 3;
                    break;
            }
            return Json(lstQuestions,JsonRequestBehavior.AllowGet);
        }
        public ActionResult Edit()
        {
            ApplicationDbContext Context = new ApplicationDbContext();
            ApplicationUser user = new ApplicationUser();
            string UID = User.Identity.GetUserId();
            user = Context.Users.Where(t => t.Id == UID).Single();
            var cityList = new CitiesController().GetCities();
            ViewBag.city = cityList;
            Lawyer l = new Lawyer();
            l = user.Lawyer;
            if (user.isLawyer)
            {
                List<ServiceType> lstServices = new List<ServiceType>();
                Context.Entry(l).Collection(t => t.ServiceTypes).Load();
                foreach (ServiceType s in l.ServiceTypes)
                {
                    lstServices.Add(s);
                }
                user.Lawyer.ServiceTypes = lstServices;
                ViewBag.Services = Context.ServiceTypes.ToList();
                return View("EditLawyerProfile", user.Lawyer);
            }
            else
            {
                return View("EditUserProfile", user);
            }
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult EditLawyer([Bind(Include = "Id,CreatedDate,ModifiedDate,CreatedBy,ModifiedBy,ImageUrl,Bio,Name,Email,PhoneNumber,AlternatePhoneNumber,Sex,WebSiteUrl,CityId")] Lawyer lawyer)
        {
            ApplicationDbContext Context = new ApplicationDbContext();
            lawyer.ModifiedDate = DateTime.Now;
            ApplicationUser usr = new ApplicationUser();
            usr = Context.Users.Where(t => t.Id == lawyer.Id).Single();
            usr.Email = lawyer.Email;
            usr.FullName = lawyer.Name;
            usr.PhoneNumber = lawyer.PhoneNumber;
            usr.UserName = lawyer.Email;
            if (ModelState.IsValid)
            {
                Context.Entry(lawyer).State = EntityState.Modified;
                //Context.SaveChanges();
                Context.Entry(usr).State = EntityState.Modified;
                Context.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult EditUser(ApplicationUser user)
        {
            ApplicationDbContext Context = new ApplicationDbContext();
            ApplicationUser usr = new ApplicationUser();
            usr = Context.Users.Where(t => t.Id == user.Id).Single();
            usr.CityId = user.CityId;
            //usr.Email = user.Email;
            usr.FullName = user.FullName;
            usr.ModifiedDate = DateTime.Now;
            usr.PhoneNumber = user.PhoneNumber;
            if (ModelState.IsValid)
            {
                Context.Entry(usr).State = EntityState.Modified;
                Context.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        public ActionResult AddUserProfilePic()
        {
            Session["UserID"] = User.Identity.GetUserId();
            return View();
        }
        public ActionResult MemberProfile(string ID)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            //var name = (from r in db.Users where r.UserName == ID select r).FirstOrDefault();
            //ViewBag.Name = name;
            var user = db.Users.Where(t => t.UserName == ID).FirstOrDefault();
            var Lawyer = db.Lawyers.Where(t => t.Id == user.Id).FirstOrDefault();
            
            List<ServiceType> lstServices = new List<ServiceType>();
            db.Entry(Lawyer).Collection(t => t.ServiceTypes).Load();
            foreach (ServiceType s in Lawyer.ServiceTypes)
            {
                lstServices.Add(s);
            }

            List<Court> lstCourts = new List<Court>();
            db.Entry(Lawyer).Collection(t => t.Courts).Load();
            foreach (Court c in Lawyer.Courts)
            {
                lstCourts.Add(c);
            }
            user.Lawyer.Courts = lstCourts;
            List<LawyerImage> lstImages=new List<LawyerImage>();
            try
            {
                lstImages = db.LawyerImages.Where(t => t.LawyerId == Lawyer.Id).ToList();
            }
            catch(Exception)
            {

            }
            City lcity = new City();
            lcity = db.Cities.Where(t => t.Id == Lawyer.CityId).SingleOrDefault();
            Lawyer.City = lcity;
            ViewBag.Lawyer=Lawyer;
            ViewBag.Images = lstImages;
            ViewBag.Users = user;
            return View();
        }
    }
}