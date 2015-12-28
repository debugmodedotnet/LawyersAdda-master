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
        public string QuestionID { get; set; }
        [Required]
        public DateTime CreatedDate { get; set; }
        [Required]
        public DateTime ModifiedDate { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string PlainText { get; set; }
        [Required]
        [AllowHtml]
        [UIHint("tinymce_full_compressed")]
        [Display(Name = "Page Content")]
        public string HTMLText { get; set; }
        public string ServiceID { get; set; }

        [MaxLength(128)]
        [ForeignKey("User")]
        public string UserID { get; set; }
        public bool isAnswered { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}