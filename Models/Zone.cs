using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ECarSharing.Models
{
    public class Zone
    {
        public int Id { get; set; }
        public int ParentId { get; set; }

        [StringLength(50)]
        public string Name { get; set; }
    }
}