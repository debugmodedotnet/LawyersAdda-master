using LawyersAdda.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LawyersAdda.Entities
{
    public class Question
    {
        public Question()
        {
            
        }

        [Required]
        public string QuestionID { get; set; }
        [Required]
        public DateTime CreatedDate { get; set; }
        [Required]
        public DateTime ModifiedDate { get; set; }
        [Required(ErrorMessage="Please Provide Question Title")]
        public string Title { get; set; }
        [Required]
        public string PlainText { get; set; }
        [Required]
        [AllowHtml]
        [UIHint("tinymce_full_compressed")]
        [Display(Name = "HTML Text")]
        public string HTMLText { get; set; }

        [ForeignKey("Services")]
        public string ServiceID { get; set; }

        [MaxLength(128)]
        [ForeignKey("User")]
        public string UserID { get; set; }
        public bool isAnswered { get; set; }
        public virtual ApplicationUser User { get; set; }
        public virtual ServiceType Services { get; set; }
    }
}