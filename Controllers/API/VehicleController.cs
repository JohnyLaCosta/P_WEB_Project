using ECarSharing.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ECarSharing.Controllers.API
{
    [RoutePrefix("api/vehicle")]
    public class VehicleController : ApiController
    {
        private ApplicationDbContext _context;

        public VehicleController()
        {
            _context = new ApplicationDbContext();
        }

        //GET: /api/vehicle
        [HttpGet]
        public IHttpActionResult GetVehicle()
        {
            return Ok(_context.Vehicles.ToList());
        }

        //GET: /api/vehicle/id
        [HttpGet]
        public IHttpActionResult GetVehicle(int id)
        {
            return Ok(_context.Vehicles.Where(p => p.Id == id));
        }

        //GET: /api/vehicle/data
        [Route("data")]
        [HttpGet]
        public IHttpActionResult GetVehicle(DateTime init, DateTime end)
        {
            //var rentals = _context.Rentals.Where(p =>p.End == null).ToList();

            var rentalsUnavailable = _context.Rentals.Where(p => p.PlannedInit <= init && p.PlannedEnd > init).ToList();

            var unavailableIds = rentalsUnavailable.Select(p => p.VehicleId).Distinct().ToList();

            var availableVehicles = _context.Vehicles.Where(p => unavailableIds.All(x => p.Id != x)).ToList();

            foreach(var item in availableVehicles)
            {
                item.zone = _context.Zones.Where(p => p.Id == item.ZoneId).First();
            }

            return Ok(availableVehicles);
        }

        //POST: /api/vehicle
        [HttpPost]
        public IHttpActionResult CreateVehicle(Vehicle vehicle)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            _context.Vehicles.Add(vehicle);
            _context.SaveChanges();

            return Created(new Uri(Request.RequestUri + "/" + vehicle.Id), vehicle);
        }
    }
}
