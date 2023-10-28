using ECarSharing.Models;
using ECarSharing.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ECarSharing.Controllers
{
    [Authorize(Roles = "Renter")]
    public class RentalController : Controller
    {
        private ApplicationDbContext _context;

        public RentalController()
        {
            _context = new ApplicationDbContext();
        }

        // GET: Rental/Rent
        [HttpGet]
        public ActionResult Rent(bool? success, DateTime? init, DateTime? end)
        {
            var viewData = new RentalViewModel();

            if (success == true)
            {
                viewData.success = true;
                return View(viewData);
            }

            if (init.HasValue && end.HasValue)
            {
                viewData.rental = new Rental
                {
                    PlannedInit = init.Value,
                    PlannedEnd = end.Value
                };
            }

            string email = User.Identity.Name;
            var eUser = _context.EUsers.Where(p => p.Email == email).First();

            viewData.eUser = eUser;
            viewData.eUser.MobilityCard = _context.MobilityCards.Where(p => p.Id == eUser.MobilityCardId).First();

            return View(viewData);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Rent(RentalViewModel viewData)
        {
            //validar o saldo
            viewData.rental.vehicle = null;

            var gained = (float)viewData.rental.TotalCost * Constants.Profit;

            var tenantGain = (float)viewData.rental.TotalCost - gained;

            Vehicle vehicle = _context.Vehicles.Single(x => x.Id == viewData.rental.VehicleId);
            vehicle.EarnedValue = vehicle.EarnedValue + viewData.rental.TotalCost;
            vehicle.TotalRented = vehicle.TotalRented + 1;
            _context.SaveChanges();

            AppAccount account = _context.AppAccounts.Single(p => p.Id == Constants.AccountId);
            account.Value = account.Value + gained;
            _context.SaveChanges();

            var eUser = _context.EUsers.Where(p => p.Id == vehicle.EUserId).First();
            MobilityCard eUserAccount = _context.MobilityCards.Single(p => p.Id == eUser.MobilityCardId);
            eUserAccount.Balance = eUserAccount.Balance + tenantGain;
            _context.SaveChanges();

            _context.Rentals.Add(viewData.rental);
            _context.SaveChanges();

            ModelState.Clear();

            return RedirectToAction("Rent","Rental", new { success = true });

        }

        //GET: Rental/MyRentals
        [HttpGet]
        public ActionResult MyRentals()
        {
            string email = User.Identity.Name;
            var eUser = _context.EUsers.Where(p => p.Email == email).First();
            var rents = _context.Rentals.Where(p => p.EUserId == eUser.Id).ToList();
            List<RentalViewModel> temp = new List<RentalViewModel>();


            foreach (var item in rents)
            {
                RentalViewModel rVM = new RentalViewModel
                {
                    rental = item
                };

                rVM.rental.vehicle = _context.Vehicles.Where(p => p.Id == item.VehicleId).First();

                temp.Add(rVM);
            }


            MyRentalsViewModel rentals = new MyRentalsViewModel
            {
                rentals = temp.AsEnumerable()
            };


            return View(rentals);
        }
    }
}