using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ECarSharing.Models
{
    public class Vehicle
    {
        public int Id { get; set; }

        [Display(Name = "Localization")]
        public int ZoneId { get; set; }

        public Zone zone { get; set; }

        public int EUserId { get; set; }

        public EUser eUser { get; set; }

        [Display(Name = "Dollar/Hour")]
        public double Price_H { get; set; }

        public double EarnedValue { get; set; } = 0;

        [StringLength(30)]
        public string Name { get; set; }

        [Display(Name = "Nº Seats")]
        public int Seats { get; set; }

        [Display(Name = "Power CV")]
        public int Power { get; set; }

        [Display(Name = "Battery kWh")]
        public int Battery { get; set; }

        public int TotalRented { get; set; } = 0;

        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
    }
}