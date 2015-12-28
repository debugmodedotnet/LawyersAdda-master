using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LawyersAdda.Entities
{
    public class City
    {
        [StringLength(100)]
        public string Id { get; set; }

        [Required]
        public string CityName { get; set; }

        [Required]
        public bool isVerified { get; set; }

        [Required]
        public bool isActive { get; set; }

        [Required]
        public bool isPriority { get; set; }
        //sldnlkdsnflds
    }
}
