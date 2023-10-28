using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ECarSharing.Models
{
    public class EUserType
    {
        public byte Id { get; set; }

        [Required]
        [StringLength(30)]
        public string Name { get; set; }
    }
}