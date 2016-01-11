using LawyersAdda.Entities;
using LawyersAdda.Models;
using LawyersAdda.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using PagedList;
using PagedList.Mvc;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;


namespace LawyersAdda.Controllers
{
    public class LawyersController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        ApplicationDbContext db = new ApplicationDbContext();
        // GET: Lawyers
        public ActionResult Index()
        {
            return View();
        }

        // GET: Lawyers/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Lawyers/Create
        public ActionResult RegisterAsLawyer()
        {
            var cityList = new CitiesController().GetCities();
            ViewBag.city = cityList;
            return View();
        }

        // POST: Lawyers/Create
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RegisterAsLawyer(LawyerRegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = model.UserName, FullName = model.FullName, Email = model.Email, PhoneNumber = model.PhoneNumber };
                //normal user registeration. Hence islawyer is set to false
                user.isLawyer = true;
                var result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {

                    Lawyer lawyerToAdd = new Lawyer();
                    lawyerToAdd.Email = model.Email;
                    lawyerToAdd.Name = model.FullName;
                    lawyerToAdd.PhoneNumber = model.PhoneNumber;
                    lawyerToAdd.Bio = model.Bio;
                    lawyerToAdd.AlternatePhoneNumber = model.AlternatePhoneNumber;
                    lawyerToAdd.Sex = "Male";
                    lawyerToAdd.CreatedBy = "DJ";
                    lawyerToAdd.ModifiedBy = "DJ";
                    lawyerToAdd.CreatedDate = DateTime.Now;
                    lawyerToAdd.ModifiedDate = DateTime.Now;
                    lawyerToAdd.WebSiteUrl = model.BlogUrl;
                    lawyerToAdd.Id = user.Id;
                    lawyerToAdd.CityId = model.City;
                    lawyerToAdd.Dob = model.Dob;
                    lawyerToAdd.NumberOfExpereince = model.Experience;
                    lawyerToAdd.HourlyRate = model.HourlyRate;

                    // lawyerToAdd.User = user;
                    try
                    {
                        ApplicationDbContext c = new ApplicationDbContext();
                        c.Lawyers.Add(lawyerToAdd);
                        c.SaveChanges();
                    }
                    catch (DbEntityValidationException e)
                    {
                        foreach (var eve in e.EntityValidationErrors)
                        {
                            var str = string.Format("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:", eve.Entry.Entity.GetType().Name, eve.Entry.State);
                            foreach (var ve in eve.ValidationErrors)
                            {
                                var a = string.Format("- Property: \"{0}\", Error: \"{1}\"",
                                       ve.PropertyName, ve.ErrorMessage);
                            }
                        }

                    }

                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);

                }
                //TempData["LName"] = model.FullName;
                //TempData["LEmail"] = model.Email;
                //TempData["LPhoneNumber"] = model.PhoneNumber;
                //TempData["LUser"] = user;
                Session["LUserId"] = TempData["LUserId"] = user.Id;
                TempData["Lcity"] = model.City;
                TempData.Keep();
                return RedirectToAction("AddCourtToLawyer");
            }
            return View(model);
            //  AddErrors(result);
        }

        // GET: Lawyers/Create
        public ActionResult AddCourtToLawyer()
        {
            //   var cid = TempData["Lcity"].ToString();
            // TempData.Keep();
            //  ApplicationDbContext db = new ApplicationDbContext();
            // var ListofCities = (from r in db.Cities select r);
            //  var ListOfCourts = (from r in db.Courts where r.CityId == cid select r);
            //  ViewBag.cities = ListofCities;
            return View();
        }

        // GET: Lawyers/Create
        public ActionResult AddServiceToLawyer()
        {
            var ListofServices = (from r in db.ServiceTypes select r);
            ViewBag.ListofServices = ListofServices;
            return View();
        }

        // GET: Lawyers/Create
        public ActionResult AddImagesToLawyer()
        {
            return View();
        }

        //Fetching Cities as JSON
        public JsonResult GetCities()
        {
            ApplicationDbContext context = new ApplicationDbContext();
            var cities = from r in context.Cities select r;
            return Json(cities, JsonRequestBehavior.AllowGet);
        }

        // Fetching Courts of a city as JSON
        public JsonResult GetCourts(string cityId)
        {
            ApplicationDbContext context = new ApplicationDbContext();
            var courts = from r in context.Courts where r.CityId == cityId select r;
            return Json(courts, JsonRequestBehavior.AllowGet);

        }

        //get laywer object by id
        public Lawyer GetLawyerById(string id)
        {
            var lawyer = db.Lawyers.Find(id);
            return lawyer;
        }

        //add court to lawyer
        public JsonResult SaveCourtsToLawyers(List<string> courts)
        {
            var l = GetLawyerById(Session["LUserId"].ToString());
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

        //add services to lawyer
        public JsonResult SaveServicesToLawyers(List<string> services)
        {
            var l = GetLawyerById(Session["LUserId"].ToString());
            try
            {
                foreach (var service in services)
                {
                    l.ServiceTypes.Add(db.ServiceTypes.Find(service));
                }
                db.SaveChanges();
            }
            catch (Exception e)
            {
                return Json(false);
            }
            return Json(true);
        }


        // GET: Lawyers/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Lawyers/Edit/5
        //testing
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Lawyers/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Lawyers/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
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
        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }
        public ActionResult GetLawyers()
        {
            ApplicationDbContext context = new ApplicationDbContext();
            return Json(context.Lawyers.Select(t=>t.Name).ToList() ,JsonRequestBehavior.AllowGet);
        }

        //search params
        public JsonResult SetSearchLawyersParam(string CityList = null, string LawServiceList = null, string exp = null, List<string> courtList = null, string lowerFees = null, string upperFees = null)
        {
            if(CityList != null)
                Session["CityList"] = CityList;
            if(LawServiceList != null)
                Session["LawServiceList"] = LawServiceList;
            if (courtList != null)
                Session["courtList"] = courtList;
            if (exp != null)
                Session["Experience"] = exp;
            if(lowerFees != null)
                Session["lowerFees"] = lowerFees;
            if(upperFees != null)
                Session["upperFees"] = upperFees;
            
            return Json(true);
        }

        //DJ sir to modify the query
        public ActionResult SearchLawyers(int page = 1, int pageSize = 4)
        {
            string CityList = Session["CityList"].ToString();
            string LawServiceList = Session["LawServiceList"].ToString();
            List<string> Courts = Session["courtList"] as List<String>;
            double lowerFees = Convert.ToDouble(Session["lowerFees"]);
            double upperFees = Convert.ToDouble(Session["upperFees"]);
            ApplicationDbContext context = new ApplicationDbContext();
            List<ServiceType> lstServices;
            List<Court> lstCourts;
            List<Lawyer> lstLawyers;
            lstServices = context.ServiceTypes.Where(t=>t.Id == LawServiceList).ToList();
            lstCourts = context.Courts.ToList();
            lstLawyers = context.Lawyers.Where(t=>t.CityId== CityList
                            && (t.ServiceTypes.Any(u=>u.Id == LawServiceList.ToString())) 
                            && (t.HourlyRate > lowerFees)
                            && (upperFees == 0 || t.HourlyRate < upperFees)
                            ).ToList();
            foreach (Lawyer l in lstLawyers)
            {
                context.Entry(l).Collection(t => t.ServiceTypes).Load();
                context.Entry(l).Collection(t => t.Courts).Load();
            }
            PagedList<Lawyer> model = new PagedList<Lawyer>(lstLawyers, page, pageSize);
            return View("SearchLawyer", model);
        }
    }
}
