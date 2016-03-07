using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LawyersAdda.Entities
{
    public class QuestionRelatedDocuments
    {
        [Key]
        public string ID { get; set; }
        public string DocumentURL { get; set; }

        [ForeignKey("Questions")]
        public string QuestionID { get; set; }

        public virtual Question Questions { get; set; }
    }
}