using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LawyersAdda.Entities
{
    public class RelatedDocument
    {
        [Key]
        public string ID { get; set; }
        public string URL { get; set; }

        [ForeignKey("Document")]
        public string DocumentID { get; set; }
        public virtual Documentation Document { get; set; }
    }
}