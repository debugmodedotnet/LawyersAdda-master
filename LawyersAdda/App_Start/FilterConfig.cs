using LawyersAdda.Filters;
using System.Web;
using System.Web.Mvc;

namespace LawyersAdda
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new CityCustomFilter());
            filters.Add(new HandleErrorAttribute());
        }
    }
}
