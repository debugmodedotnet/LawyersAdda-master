using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LawyersAdda.ViewModels
{
    public class RegisterAsLawyerstep1ViewModel
    {
        [Required(ErrorMessage = "Please Enter Full Name")]
        [Display(Name = "FullName")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Please Enter User Name")]
        [Display(Name = "UserName")]
        [RegularExpression("^[a-zA-Z0-9]*$",ErrorMessage="User Name Should be Alpha-Numeric")]
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
    }
}
