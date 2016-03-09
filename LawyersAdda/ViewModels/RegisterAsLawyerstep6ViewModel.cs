using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LawyersAdda.ViewModels
{
    public class RegisterAsLawyerstep6ViewModel
    {
        [AllowHtml]
        [UIHint("tinymce_full_compressed")]
        [Display(Name = "Describe Yourself")]
        public string Bio { get; set; }
    }
}