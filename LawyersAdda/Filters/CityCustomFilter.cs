using LawyersAdda.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
//using System.Web.Http.Filters;
using System.Web.Mvc;


namespace LawyersAdda.Filters
{
   public class CityCustomFilter : ActionFilterAttribute
    {
        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            var cities = from r in db.Cities select r;
            filterContext.Controller.ViewBag.Cities = cities;
            base.OnResultExecuting(filterContext);
        }
        
       
    }
}
