using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LawyersAdda.Entities
{
    public class LawyerImage
    {
        [Required]
        [StringLength(100)]
        public string Id { get; set; }

        [Required]
        [ForeignKey("Lawyer")]
        public string LawyerId { get; set; }

        public Lawyer Lawyer { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        [Required]
        public bool isDisplayPic { get; set; }
    }
}
