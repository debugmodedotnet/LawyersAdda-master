using LawyersAdda.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using LawyersAdda.Entities;
using System.Data.Entity;
using System.Web.Routing;
using System.Threading.Tasks;
using LawyersAdda.ViewModels;

namespace LawyersAdda.Controllers
{
    public class ProfileController : Controller
    {
        private ApplicationUserManager _userManager;
        //
        // GET: /Profile/
        [Authorize]
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
                List<Lawyer> lstLawyers = new List<Lawyer>();
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
        [Authorize]
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
            return Json(lstQuestions, JsonRequestBehavior.AllowGet);
        }
        [Authorize]
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
                List<Court> lstCourts = new List<Court>();
                Context.Entry(l).Collection(t => t.Courts).Load();
                foreach (Court c in l.Courts)
                {
                    lstCourts.Add(c);
                }
                var ListofServices = (from r in Context.ServiceTypes select r);
                ViewBag.ListofServices = ListofServices;
                user.Lawyer.Courts = lstCourts;
                LawyerEditProfileViewModel ReturnViewModel = new LawyerEditProfileViewModel()
                {
                    Name=l.Name,
                    Email=l.Email,
                    PhoneNumber=l.PhoneNumber,
                    WebSiteUrl=l.WebSiteUrl,
                    CityId=l.CityId,
                    NumberOfExpereince = l.NumberOfExpereince,
                    HourlyRate = l.HourlyRate,
                    Dob = l.Dob,
                    Sex = l.Sex,
                    AlternatePhoneNumber = l.AlternatePhoneNumber,
                    Bio = l.Bio,
                    Courts = l.Courts,
                    ServiceTypes = l.ServiceTypes,
                    LawyerImages = l.LawyerImages

                };
                //return View("EditLawyerProfile", user.Lawyer);
                return View("EditLawyerProfile", ReturnViewModel);
            }
            else
            {
                return View("EditUserProfile", user);
            }
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        [Authorize]
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
        [Authorize]
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
        [Authorize]
        public ActionResult AddUserProfilePic()
        {
            Session["UserID"] = User.Identity.GetUserId();
            return View();
        }
        public ActionResult MemberProfileByID(string ID)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            string UserName = db.Users.Where(t => t.Id == ID).ToList()[0].UserName;
            return RedirectToAction("MemberProfile", new RouteValueDictionary(new { controller = "Profile", action = "MemberProfile", ID = UserName }));
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
            List<LawyerImage> lstImages = new List<LawyerImage>();
            try
            {
                lstImages = db.LawyerImages.Where(t => t.LawyerId == Lawyer.Id).ToList();
            }
            catch (Exception)
            {

            }
            City lcity = new City();
            lcity = db.Cities.Where(t => t.Id == Lawyer.CityId).SingleOrDefault();
            Lawyer.City = lcity;
            ViewBag.Lawyer = Lawyer;
            ViewBag.Images = lstImages;
            ViewBag.Users = user;
            return View();
        }
        [HttpPost]
        public ActionResult UpdateProfile(string Name, string PhoneNumber, string Email, string WebSiteUrl, string CityId)
        {
            ApplicationDbContext Context = new ApplicationDbContext();
            string UID = User.Identity.GetUserId();
            Lawyer l=Context.Lawyers.Where(t => t.Id == UID).Single();
            ApplicationUser u = Context.Users.Where(t => t.Id == UID).Single();

            l.Name = Name;
            l.PhoneNumber = PhoneNumber;
            l.Email = Email;
            l.WebSiteUrl = WebSiteUrl;
            l.CityId = CityId;
            l.ModifiedDate = DateTime.Now;

            u.FullName = Name;
            u.PhoneNumber = PhoneNumber;
            u.Email = Email;
            u.CityId = CityId;
            u.ModifiedDate = DateTime.Now;

            Context.Entry(l).State = EntityState.Modified;
            Context.Entry(u).State = EntityState.Modified;
            Context.SaveChanges();

            return RedirectToAction("Edit");
        }
        public JsonResult EditCourtsToLawyers(List<string> courts)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            string Uid=User.Identity.GetUserId().ToString();
            Lawyer l=db.Lawyers.Where(t=>t.Id==Uid).Single();
            Lawyer LawyerCourtsToDelete=db.Lawyers.Where(t=>t.Id==Uid).Single();
            db.Entry(LawyerCourtsToDelete).Collection(t => t.Courts).Load();
            
            db.Entry(LawyerCourtsToDelete.Courts).State = EntityState.Deleted;
            db.SaveChanges();
            try
            {
                foreach (var court in courts)
                {
                    var courtToAdd = db.Courts.Find(court);
                    l.Courts.Add(courtToAdd);
                }
                db.SaveChanges();
            }
            catch (Exception e)
            {
                return Json(false);
            }
            return Json(true);
        }

        public async Task<JsonResult> ChangePassword(string CurrentPassword, string NewPassword)
        {
            var result = await UserManager.ChangePasswordAsync(User.Identity.GetUserId(), CurrentPassword, NewPassword);
            if(result.Succeeded)
            {
                return Json(true);
            }
            else
            {
                return Json(false);
            }
        }
        [HttpPost]
        public ActionResult UpdateDescription(Lawyer model)
        {
            try
            {
                ApplicationDbContext db = new ApplicationDbContext();
                string Uid = User.Identity.GetUserId().ToString();
                Lawyer l = db.Lawyers.Where(t => t.Id == Uid).Single();
                l.Bio = model.Bio;
                db.Entry(l).State = EntityState.Modified;
                db.SaveChanges();
            }
            catch (Exception e)
            {

            }
            return RedirectToAction("Edit");
        }
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
    }
}