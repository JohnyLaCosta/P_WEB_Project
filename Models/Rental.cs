using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ECarSharing.Models
{
    public class Rental
    {
        public int Id { get; set; }

        public int EUserId { get; set; }

        public int VehicleId { get; set; }

        public Vehicle vehicle { get; set; }

        public double TotalCost { get; set; } = 0;

        [Required]
        [Display(Name ="Init Date")]
        [DataType(DataType.DateTime)]
        public DateTime PlannedInit { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? Init { get; set; }

        [Required]
        [Display(Name = "End Date")]
        [DataType(DataType.DateTime)]
        public DateTime PlannedEnd { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? End { get; set; }
    }
}