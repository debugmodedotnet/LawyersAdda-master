using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LawyersAdda.Entities
{
    public class Answer
    {
        [Required]
        public string AnswerID { get; set; }

        [Required]
        [ForeignKey("Questions")]
        public string QuestionID { get; set; }

        [Required]
        [AllowHtml]
        [UIHint("tinymce_full_compressed")]
        [Display(Name = "HTML Text")]
        public string HTMLText { get; set; }

        [Required]
        public string PlainText { get; set; }

        [Required]
        public DateTime AnswerGivenDate { get; set; }

        [Required]
        public DateTime AnswerModifiedDate { get; set; }

        [Required]
        [ForeignKey("Lawyers")]
        public string LawyerID { get; set; }
        public virtual Lawyer Lawyers { get; set; }
        public virtual Question Questions { get; set; }
    }
}