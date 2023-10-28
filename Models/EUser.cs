using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ECarSharing.Models
{
    public class EUser
    {
        public int Id { get; set; }

        public int MobilityCardId { get; set; }

        public MobilityCard MobilityCard { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Name too long.")]
        public string Name { get; set; }

        [Display(Name = "User Image")]
        public string Image { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(254, ErrorMessage = "Email too long")]
        public string Email { get; set; }

        [Display(Name = "User Type")]
        public byte EUserTypeId { get; set; }

        public EUserType eUserType { get; set; }

    }
}