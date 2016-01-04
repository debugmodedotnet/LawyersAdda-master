using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LawyersAdda.ViewModels
{
    public class LawyerSearchViewModel
    {
        [Display(Name = "Lawyer Name")]
        public string LawyerName { get; set; }


        public IQueryable<Entities.City> ListOfCities { get; set; }
        public IQueryable<Entities.ServiceType> ListOfServiceTypes { get; set; }
    }
}
