using ECarSharing.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ECarSharing.ViewModels
{
    public class MyRentalsViewModel
    {
        public IEnumerable<RentalViewModel> rentals { get; set; }
    }
}