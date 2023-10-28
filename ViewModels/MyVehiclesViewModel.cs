using ECarSharing.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ECarSharing.ViewModels
{
    public class MyVehiclesViewModel
    {
        public IEnumerable<Vehicle> myVehicles { get; set; }
    }
}