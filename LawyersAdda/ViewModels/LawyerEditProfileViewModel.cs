using LawyersAdda.Entities;
using LawyersAdda.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LawyersAdda.ViewModels
{
    public class LawyerEditProfileViewModel
    {
        [Required(ErrorMessage = "Please Enter Full Name")]
        [Display(Name = "FullName")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please Enter User Name")]
        [Display(Name = "UserName")]
        [RegularExpression("^[a-zA-Z0-9]*$", ErrorMessage = "User Name Should be Alpha-Numeric")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Please Enter Email")]
        [EmailAddress(ErrorMessage = "Email Address is Not Valid")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please Enter Phone Number")]
        [Display(Name = "PhoneNumber")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Please Enter Password")]
        //[StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [MinLength(6, ErrorMessage = "The Password must be at least 6 characters long.")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Blog or Website ")]
        public string WebSiteUrl { get; set; }

        [Required(ErrorMessage = "Please Enter City")]
        [Display(Name = "City")]
        public string CityId { get; set; }

        [Required(ErrorMessage = "Please Enter Experience")]
        [Display(Name = "Experience")]
        public int NumberOfExpereince { get; set; }

        [Required(ErrorMessage = "Please Enter Hourly Fee")]
        [Display(Name = "Hourly Fee")]
        public double HourlyRate { get; set; }

        [Required(ErrorMessage = "Please Enter Birthday")]
        [Display(Name = "Birthday")]
        public DateTime Dob { get; set; }
        [Display(Name = "Gender")]
        public string Sex { get; set; }

        [Display(Name = "Phone Number")]
        public string AlternatePhoneNumber { get; set; }

        [AllowHtml]
        [UIHint("tinymce_full_compressed")]
        [Display(Name = "Describe Yourself")]
        public string Bio { get; set; }

        public ICollection<Court> Courts { get; set; }

        public ICollection<ServiceType> ServiceTypes { get; set; }

        public ICollection<LawyerImage> LawyerImages { get; set; }
    }
}