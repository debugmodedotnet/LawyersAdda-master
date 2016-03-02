using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LawyersAdda.Entities
{
    public class Documentation
    {
        [Key]
        public string ID { get; set; }
        [Required]
        public string DocumentType { get; set; }

        [AllowHtml]
        [UIHint("tinymce_full_compressed")]
        [Display(Name = "HTML Text")]
        [Required]
        public string DocumentDescription { get; set; }
    }
}