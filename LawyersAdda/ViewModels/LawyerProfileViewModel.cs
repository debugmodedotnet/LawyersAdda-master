using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LawyersAdda.ViewModels
{
   public class LawyerProfileViewModel
    {
        [Display(Name = "About Yourself")]
        public string Bio { get; set; }

        [Display(Name ="Alternate PhoneNumber")]
        public string AlternatePhoneNumber { get; set; }

        [Display(Name = "Blog or Website ")]
        public string BlogUrl { get; set; }


    }
}
