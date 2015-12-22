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
        [StringLength(100)]
        public string Id { get; set; }      

        [Required]
        public string ImageUrl { get; set; }

        [Required]
        public bool isDisplayPic { get; set; }

        public string LawyerId { get; set; }

        public virtual Lawyer Lawyer { get; set; }
    }
}
