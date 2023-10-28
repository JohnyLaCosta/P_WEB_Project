using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ECarSharing.Models;
using ECarSharing.ViewModels;
using System.IO;
using System.Collections.Generic;

namespace ECarSharing.Controllers
{
    [Authorize(Roles = "Tenant")]
    public class VehicleController : Controller
    {

        private ApplicationDbContext _context;

        public VehicleController()
        {
            _context = new ApplicationDbContext();
        }
        // GET: Vehicle
        public ActionResult Create()
        {
            var viewModel = new VehicleViewModel
            {
                zones = _context.Zones.Where(p => p.ParentId != 0).ToList()
            };
            return View(viewModel);
        }


        //POST: Vehicle
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(VehicleViewModel viewModel, IEnumerable<HttpPostedFileBase> imageFiles)
        {
            string email = User.Identity.Name;

            var eUser = _context.EUsers.Where(p => p.Email == email).First();

            viewModel.vehicle.EUserId = eUser.Id;

            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            _context.Vehicles.Add(viewModel.vehicle);
            _context.SaveChanges();

            var path = Server.MapPath(Constants.Path + viewModel.vehicle.Id + "/");
            Directory.CreateDirectory(path);

            foreach (var item in imageFiles)
            {
                if (item != null && item.ContentLength > 0)
                {

                    Image image = new Image
                    {
                        Name = Constants.CreateFile(item, path, Constants.Path + viewModel.vehicle.Id + "/")
                    };

                    _context.Images.Add(image);
                    _context.SaveChanges();

                    VehicleImage vehicleImage = new VehicleImage
                    {
                        VehicleId = viewModel.vehicle.Id,
                        ImageId = image.Id
                    };

                    _context.VehicleImages.Add(vehicleImage);
                    _context.SaveChanges();
                }
            }

            return RedirectToAction("MyVehicles", "Vehicle");
        }

        //GET: get user vehicles
        public ActionResult MyVehicles(string? message)
        {
            ViewBag.StatusMessage = message;

            string email = User.Identity.Name;

            var eUser = _context.EUsers.Where(p => p.Email == email).First();

            var myVehicles = _context.Vehicles.Where(p => p.EUserId == eUser.Id).ToList();

            foreach(var vehicle in myVehicles)
            {
                vehicle.zone = _context.Zones.Single(p => p.Id == vehicle.ZoneId);
            }

            MyVehiclesViewModel viewData = new MyVehiclesViewModel
            {
                myVehicles = myVehicles
            };

            return View(viewData);
        }

        [HttpGet]
        public ActionResult Edit(int Id)
        {
            VehicleViewModel viewData = new VehicleViewModel();

            viewData.vehicle = _context.Vehicles.Single(p => p.Id == Id);

            var temp = _context.VehicleImages.Where(p => p.VehicleId == Id).Select(p => p.VehicleId).ToList();

            viewData.vehicleImages = _context.Images.Where(p => temp.All(x => p.Id == x)).ToList();

            return View(viewData);
        }

        [HttpPost]
        public ActionResult Edit(VehicleViewModel model, IEnumerable<HttpPostedFileBase> imageFiles)
        {
            Vehicle vehicle = _context.Vehicles.Single(p => p.Id == model.vehicle.Id);
            vehicle.Name = model.vehicle.Name;
            vehicle.Description = model.vehicle.Description;
            vehicle.Price_H = model.vehicle.Price_H;
            _context.SaveChanges();

            var path = Server.MapPath(Constants.Path + model.vehicle.Id + "/");

            if (imageFiles.Any())
            {
                if (Directory.Exists(path))
                {
                    Directory.Delete(path, true);
                }

                Directory.CreateDirectory(path);
            }
            

            foreach (var item in imageFiles)
            {
                if (item != null && item.ContentLength > 0)
                {

                    Image image = new Image
                    {
                        Name = Constants.CreateFile(item, path, Constants.Path + model.vehicle.Id + "/")
                    };

                    _context.Images.Add(image);
                    _context.SaveChanges();

                    VehicleImage vehicleImage = new VehicleImage
                    {
                        VehicleId = model.vehicle.Id,
                        ImageId = image.Id
                    };

                    _context.VehicleImages.Add(vehicleImage);
                    _context.SaveChanges();
                }
            }

            return RedirectToAction("MyVehicles");
        }

        
        public ActionResult Delete(int Id)
        {
            var vehicle = _context.Vehicles.Single(p => p.Id == Id);

            var vehicleRentals = _context.Rentals.Where(p => p.VehicleId == Id).ToList();

            DateTime now = DateTime.Now;

            var rentals = vehicleRentals.Where(p => p.PlannedEnd > now).ToList();

            if (rentals.Any())
            {
                return RedirectToAction("MyVehicles", new { message = "Vehicle " + vehicle.Name + "  has pendent rentals" });
            }

            _context.Vehicles.Remove(vehicle);

            return RedirectToAction("MyVehicles");
        }
    }
}