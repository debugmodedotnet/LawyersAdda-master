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
    [Authorize]
    public class QuestionsController : Controller
    {
        //
        // GET: /Questions/
        ApplicationDbContext db = new ApplicationDbContext();
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
            model.QuestionID=Guid.NewGuid().ToString();
            model.CreatedDate = DateTime.Now;
            model.ModifiedDate = DateTime.Now;
            Regex regex = new Regex("\\<[^\\>]*\\>");
            model.PlainText = regex.Replace(model.HTMLText, string.Empty);
            model.UserID = User.Identity.GetUserId();
            ApplicationDbContext db1 = new ApplicationDbContext();
            db1.Questions.Add(model);
            db1.SaveChanges();
            ViewBag.SubmittedSuccessfully = true;
            return View();
        }
        public ActionResult ViewQuestion(string id)
        {
            ApplicationDbContext context = new ApplicationDbContext();
            Question question = new Question();
            List<ServiceType> Services = new List<ServiceType>();
            question = context.Questions.Where(t => t.QuestionID == id).Single();
            Services=context.ServiceTypes.ToList();
            ServiceType service;
            service = Services.Where(t => t.Id == question.ServiceID).Single();
            question.Services = service;

            Answer answer = new Answer();
            List<Answer> lstanswers = new List<Answer>();
            lstanswers = context.Answers.Where(t => t.QuestionID == id).ToList();
            if(lstanswers.Count>0)
            {
                try 
                { 
                    answer = lstanswers.Where(t => t.IsAccepted == true || t.IsAccepted == null).OrderByDescending(t => t.AnswerModifiedDate).Take(1).Single();
                }
                catch(Exception ex)
                {

                }
            }
            ViewBag.Answer = answer;
            return View(question);
        }
        public ActionResult MarkQuestion(string QuestionID, string AnswerID, string Status)
        {
            ApplicationDbContext context = new ApplicationDbContext();
            Answer answer = new Answer();
            Question question = new Question();
            answer = context.Answers.Where(t => t.AnswerID == AnswerID).Single();
            question = context.Questions.Where(t => t.QuestionID == QuestionID).Single();
            //status 1 for accept
            if (Status == "1")
            {
                answer.IsAccepted = true;
                question.isAnswered = true;
                context.SaveChanges();
            }
            //regejct
            else if(Status=="0")
            {
                answer.IsAccepted = false;
                context.SaveChanges();
            }
            return RedirectToAction("ViewQuestion", new {id=QuestionID });
        }

        public ActionResult DownloadPDF(string id)
        {
            ApplicationDbContext context = new ApplicationDbContext();
            Question question = new Question();
            List<ServiceType> Services = new List<ServiceType>();
            question = context.Questions.Where(t => t.QuestionID == id).Single();
            Services = context.ServiceTypes.ToList();
            ServiceType service;
            service = Services.Where(t => t.Id == question.ServiceID).Single();
            question.Services = service;
            Answer answer = new Answer();
            List<Answer> lstanswers = new List<Answer>();
            lstanswers = context.Answers.Where(t => t.QuestionID == id).ToList();
            if (lstanswers.Count > 0)
            {
                try
                {
                    answer = lstanswers.Where(t => t.IsAccepted == true || t.IsAccepted == null).OrderByDescending(t => t.AnswerModifiedDate).Take(1).Single();
                }
                catch (Exception ex)
                {

                }
            }
            answer.Questions = question;
            return new Rotativa.ViewAsPdf("DownloadPDF", answer){FileName = "Answer.pdf"};
            //return View(answer);
        }
	}
}