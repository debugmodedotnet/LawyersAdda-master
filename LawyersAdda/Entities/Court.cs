using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LawyersAdda.Entities
{
    public class Court
    {
        public Court()
        {
            Lawyers = new HashSet<Lawyer>();
        }
        [MaxLength(128)]
        public string  Id { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime ModifiedDate { get; set; }

        [Required]
        [StringLength(100)]
        public string CreatedBy { get; set; }

        [Required]
        [StringLength(100)]
        public string ModifiedBy { get; set; }

        [Required]
       
        public string Name { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        public string CourtImageUrl { get; set; }

        [Required]
        public string City { get; set; }

        public string Address { get; set; }

        public ICollection<Lawyer> Lawyers { get; set; }
    }
}