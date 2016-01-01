using LawyersAdda.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using LawyersAdda.Entities;

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
            string UID=User.Identity.GetUserId();
            user = Context.Users.Where(t => t.Id == UID).Single();
            ViewBag.User = user;
            Lawyer l=new Lawyer();
            l=user.Lawyer;
            if(user.isLawyer)
            {
                
                List<ServiceType> lstServices=new List<ServiceType>();
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
                return View("LawyerProfile",lstAnswers);
            }
            else
            {
                List<Question> lstQuestions = new List<Question>();
                List<ServiceType> lstServiceType = new List<ServiceType>();
                lstQuestions = Context.Questions.Where(t => t.UserID == UID).ToList();
                foreach (var q in lstQuestions)
                {
                    ServiceType service = lstServiceType.Where(t => t.Id == q.ServiceID).Single();
                    q.Services = service;
                }
                lstQuestions = lstQuestions.OrderByDescending(t=>t.ModifiedDate).Take(10).ToList(); ;
                return View("UserProfile", lstQuestions);
            }
        }
	}
}