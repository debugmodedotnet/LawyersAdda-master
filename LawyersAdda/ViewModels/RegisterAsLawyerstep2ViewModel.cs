﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LawyersAdda.ViewModels
{
   public class RegisterAsLawyerstep2ViewModel
    {
       
 
        [Display(Name = "Blog or Website ")]
        public string BlogUrl { get; set; }
        [Required]

        [Display(Name = "City")]
        public string City { get; set; }
        [Required]

        [Display(Name = "Experience")]
        public int Experience { get; set; }
        [Required]

        [Display(Name = "Hourly Fee")]
        public double HourlyRate { get; set; }

        [Display(Name = "Birthday")]
        public DateTime Dob { get; set; }
        [Display(Name = "Gender")]
        public string Gender { get; set; }
        [Display(Name = "Phone Number")]
        public string AlternatePhoneNumber { get; set; }
    }
}
