using ECarSharing.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ECarSharing.ViewModels
{
    public class VehicleViewModel
    {
        public Vehicle vehicle { get; set; }
        public IEnumerable<Zone> zones { get; set; }
        public IEnumerable<Image> vehicleImages { get; set; }
    }
}