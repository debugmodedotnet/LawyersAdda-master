using LawyersAdda.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LawyersAdda.Entities
{
    public class Lawyer
    {
        public Lawyer()
        {
            Courts = new HashSet<Court>();
            ServiceTypes = new HashSet<ServiceType>();
        }
        [MaxLength(128)]
        [ForeignKey("User")]
        public string  Id { get; set; }
        [Required]
        public DateTime CreatedDate { get; set; }
        [Required]
        public DateTime ModifiedDate { get; set; }

        [Required]
        [StringLength(100)]
        public string CreatedBy { get; set; }

        [Required]
        [StringLength(100)]
        public string ModifiedBy { get; set; }

        public string ImageUrl { get; set; }

        public string Bio { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string PhoneNumber { get; set; }

        public string AlternatePhoneNumber { get; set; }

        public string Sex { get; set; }

        public string WebSiteUrl { get; set; }

      
        public virtual ApplicationUser User { get; set; }

        public ICollection<Court> Courts { get; set; }

        public ICollection<ServiceType> ServiceTypes { get; set; }
    }
}