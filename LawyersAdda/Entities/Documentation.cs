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

        [MaxLength(128)]
        [ForeignKey("User")]
        public string UserID { get; set; }
        public virtual ApplicationUser User { get; set; }
        public ICollection<RelatedDocument> RelatedDocuments { get; set; }
    }
}