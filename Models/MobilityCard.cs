using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ECarSharing.Models
{
    public class MobilityCard
    {
        public int Id { get; set; }

        [Required]
        [Display(Name ="Card Number")]
        [StringLength(16, MinimumLength = 16, ErrorMessage = "Card Number must have 16 digits")]
        public string NCard { get; set; }

        [Required]
        [Range(100, 999, ErrorMessage ="CVC needs to have 3 digits.")]
        public int Cvc { get; set; }

        [Required]
        public DateTime Validity { get; set; }

        public float Balance { get; set; } = 0;
    }
}