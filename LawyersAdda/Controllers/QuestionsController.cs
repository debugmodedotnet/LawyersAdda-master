using LawyersAdda.Entities;
using LawyersAdda.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace LawyersAdda.Controllers
{
    public class QuestionsController : Controller
    {
        //
        // GET: /Questions/
        ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult AskQuestion()
        {
            var lawservices = from r in db.ServiceTypes select r;
            ViewBag.LawServices = lawservices;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AskQuestion(Question model)
        {
            var lawservices = from r in db.ServiceTypes select r;
            ViewBag.LawServices = lawservices;
            Random random = new Random();
            model.QuestionID=random.Next().ToString();
            model.CreatedDate = DateTime.Now;
            model.ModifiedDate = DateTime.Now;
            model.PlainText = model.HTMLText;
            model.UserID = User.Identity.GetUserId();
            ApplicationDbContext db1 = new ApplicationDbContext();
            db.Questions.Add(model);
            db.SaveChanges();
            return View();
        }
	}
}