using LawyersAdda.Entities;
using LawyersAdda.Models;
using LawyersAdda.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
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
                var user = new ApplicationUser { UserName = model.Email, FullName = model.FullName, Email = model.Email,PhoneNumber=model.PhoneNumber };
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
                //TempData["LUserId"] = user.Id;

                return RedirectToAction("AddLawyerProfile");
            }
           return View(model);
            //  AddErrors(result);
        }



        // GET: Lawyers/Create
        public ActionResult AddLawyerProfile()
        {
            return View();
        }

        [HttpPost]
        //[AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult AddLawyerProfile(LawyerProfileViewModel model)
        {
            if (ModelState.IsValid)
            {
                Lawyer lawyerToAdd = new Lawyer();
                lawyerToAdd.Email = TempData["LEmail"].ToString();
                lawyerToAdd.Name = TempData["LName"].ToString();
                lawyerToAdd.PhoneNumber = TempData["LPhoneNumber"].ToString();
                lawyerToAdd.Id = TempData["LUserId"].ToString();
                lawyerToAdd.Bio = model.Bio;
                lawyerToAdd.AlternatePhoneNumber = model.AlternatePhoneNumber;
                lawyerToAdd.Sex = "Male";
                lawyerToAdd.CreatedBy = "DJ";
                lawyerToAdd.ModifiedBy = "DJ";
                lawyerToAdd.CreatedDate = DateTime.Now;
                lawyerToAdd.ModifiedDate = DateTime.Now;
                lawyerToAdd.WebSiteUrl = model.BlogUrl;
                 lawyerToAdd.User = TempData["LUser"] as ApplicationUser;
                try
                {
                    ApplicationDbContext c = new ApplicationDbContext();
                    c.Lawyers.Add(lawyerToAdd);
                    c.SaveChanges();
                }
                catch(Exception ex)
                {
                    var r = ex.InnerException.Message;

                }
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View();
            }
            
        }





        // GET: Lawyers/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Lawyers/Edit/5
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
    }
}
