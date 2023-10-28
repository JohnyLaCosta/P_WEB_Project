using ECarSharing.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ECarSharing.Controllers.API
{
    [RoutePrefix("api/images")]
    public class ImagesController : ApiController
    {
        private ApplicationDbContext _context;

        public ImagesController()
        {
            _context = new ApplicationDbContext();
        }

        //GET: /api/images
        [HttpGet]
        public IHttpActionResult GetImages()
        {
            return Ok(_context.Images.ToList());
        }

        //GET: /api/images/id
        [HttpGet]
        public IHttpActionResult GetImages(int vehicleId)
        {
            var temp = _context.VehicleImages.Where(p => p.VehicleId == vehicleId).ToList();

            var list = temp.Select(p => p.ImageId).Distinct().ToList();

            //var imagesList = _context.Images.Where(p => list.All(x => p.Id == x)).ToList();

            var images = _context.Images.ToList();

            List<Image> imagesList = new List<Image>();

            foreach(var item in list)
            {
                foreach(var image in images)
                {
                    if(image.Id == item)
                    {
                        imagesList.Add(image);
                    }
                }
            }

            return Ok(imagesList);
        }
    }

}
