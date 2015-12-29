using LawyersAdda.Entities;
using LawyersAdda.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace LawyersAdda.Controllers
{
    public class AnswersController : Controller
    {
        //
        // GET: /Answers/
        public ActionResult Index()
        {
            ApplicationDbContext context = new ApplicationDbContext();
            List<Question> lstQuestion = new List<Question>();
            List<ApplicationUser> lstUsers = new List<ApplicationUser>();
            List<ServiceType> lstServiceType = new List<ServiceType>();

            lstQuestion = context.Questions.Where(t => t.isAnswered == false).ToList();
            lstUsers = context.Users.Where(t => t.isLawyer == false).ToList();
            lstServiceType = context.ServiceTypes.ToList();

            foreach (Question q in lstQuestion)
            {
                ApplicationUser user = lstUsers.Where(t => t.Id == q.UserID).Single();
                q.User = user;
                ServiceType service = lstServiceType.Where(t => t.Id == q.ServiceID).Single();
                q.Services = service;
            }
            ViewBag.Questions = lstQuestion.OrderByDescending(t=>t.ModifiedDate);
            return View();
        }
        public ActionResult QuestionInDetails(string id)
        {
            ApplicationDbContext context = new ApplicationDbContext();
            Question question = new Question();
            question = context.Questions.Where(t => t.QuestionID == id).Single();
            ViewBag.Questions = question;
            return View();
        }
        public ActionResult GiveAnswer(string id)
        {
            ApplicationDbContext context = new ApplicationDbContext();
            Question question = new Question();
            question = context.Questions.Where(t => t.QuestionID == id).Single();
            ViewBag.Questions = question;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GiveAnswer(Answer model)
        {
            model.AnswerGivenDate = DateTime.Now;
            model.AnswerModifiedDate = DateTime.Now;
            Regex regex = new Regex("\\<[^\\>]*\\>");
            model.PlainText = regex.Replace(model.HTMLText, string.Empty);
            model.LawyerID = User.Identity.GetUserId();
            ApplicationDbContext Context = new ApplicationDbContext();
            Context.Answers.Add(model);
            Context.SaveChanges();
            return View();
        }
	}
}