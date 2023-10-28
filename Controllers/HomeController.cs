using ECarSharing.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ECarSharing.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
             
            return View(new HomeViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(HomeViewModel model)
        {
            return RedirectToAction("Rent", "Rental", new { init = model.Init, end = model.End });
        }
    }
}