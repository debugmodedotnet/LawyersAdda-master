using LawyersAdda.Entities;
using LawyersAdda.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using System.Text.RegularExpressions;

namespace LawyersAdda.Controllers
{
    public class QuestionsController : Controller
    {
        //
        // GET: /Questions/
        ApplicationDbContext db = new ApplicationDbContext();
        //public ActionResult Index()
        //{
        //    ApplicationDbContext Context = new ApplicationDbContext();
        //    List<Question> lstQuestion=new List<Question>();
        //    List<ServiceType> lstServiceType = new List<ServiceType>();
        //    string UID = User.Identity.GetUserId();
        //    lstQuestion = Context.Questions.Where(t=>t.UserID==UID).ToList();
        //    lstServiceType = Context.ServiceTypes.ToList();
        //    foreach (var q in lstQuestion)
        //    {
        //        ServiceType service = lstServiceType.Where(t => t.Id == q.ServiceID).Single();
        //        q.Services = service;
        //    }
        //    lstQuestion.OrderByDescending(t => t.ModifiedDate);
        //    ViewBag.Order = 1;
        //    ViewBag.SelectQuestion = 1;
        //    return View(lstQuestion);
        //}

        public ActionResult Index(int? Order, int? SelectQuestion)
        {
            ApplicationDbContext Context = new ApplicationDbContext();
            List<Question> lstQuestion = new List<Question>();
            List<ServiceType> lstServiceType = new List<ServiceType>();
            string UID = User.Identity.GetUserId();
            lstQuestion = Context.Questions.Where(t => t.UserID == UID).ToList();
            lstServiceType = Context.ServiceTypes.ToList();
            foreach (var q in lstQuestion)
            {
                ServiceType service = lstServiceType.Where(t => t.Id == q.ServiceID).Single();
                q.Services = service;
            }
            ViewBag.SelectQuestion = 1;
            switch(Order)
            {
                case 1:
                    lstQuestion = lstQuestion.OrderByDescending(t => t.ModifiedDate).ToList();
                    ViewBag.Order = 1;
                    break;
                case 2:
                    lstQuestion = lstQuestion.OrderBy(t => t.ModifiedDate).ToList();
                    ViewBag.Order = 2;
                    break;
                default:
                    lstQuestion = lstQuestion.OrderByDescending(t => t.ModifiedDate).ToList();
                    ViewBag.Order = 1;
                    break;
            }
            switch(SelectQuestion)
            {
                case 2:
                    lstQuestion=lstQuestion.Where(t => t.isAnswered == true).ToList();
                    ViewBag.SelectQuestion = 2;
                    break;
                case 3:
                    lstQuestion=lstQuestion.Where(t => t.isAnswered == false).ToList();
                    ViewBag.SelectQuestion = 3;
                    break;
            }
            return View(lstQuestion);
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
            Regex regex = new Regex("\\<[^\\>]*\\>");
            model.PlainText = regex.Replace(model.HTMLText, string.Empty);
            model.UserID = User.Identity.GetUserId();
            ApplicationDbContext db1 = new ApplicationDbContext();
            db1.Questions.Add(model);
            db1.SaveChanges();
            return View();
        }
	}
}