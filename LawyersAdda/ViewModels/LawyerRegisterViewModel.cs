using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LawyersAdda.ViewModels
{
  public  class LawyerRegisterViewModel
    {


       
            [Required]

            [Display(Name = "FullName")]
            public string FullName { get; set; }



            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

           [Required]

            [Display(Name = "PhoneNumber")]
            public string PhoneNumber { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }



        [Display(Name = "About Yourself")]
        public string Bio { get; set; }

        [Display(Name = "Alternate PhoneNumber")]
        public string AlternatePhoneNumber { get; set; }

        [Display(Name = "Blog or Website ")]
        public string BlogUrl { get; set; }


    }
    }

