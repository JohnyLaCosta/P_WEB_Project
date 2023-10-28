using ECarSharing.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ECarSharing.ViewModels
{
    public class RentalViewModel
    {
        public Rental rental { get; set; }
        public EUser eUser { get; set; }
        public bool? success { get; set; } = null;
    }
}