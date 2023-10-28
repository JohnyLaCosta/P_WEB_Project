using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ECarSharing.Models
{
    public class VehicleImage
    {
        public int Id { get; set; }

        public int VehicleId { get; set; }
        public Vehicle Vehicle { get; set; }

        public int ImageId { get; set; }
        public Image Image { get; set; }
    }
}