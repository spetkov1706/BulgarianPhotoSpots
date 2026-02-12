using BulgarianPhotoSpots.Models;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace BulgarianPhotoSpots.Controllers
{
    public class PhotoSpotsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Details()
        {
            return View();
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(PhotoSpot model)
        {
            if (model.Title == "Test")
            {
                ModelState.AddModelError("Title", "The title 'Test' is not allowed.");
            }
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            return RedirectToAction("Index");
        }

        public IActionResult About()
        {
            return View();
        }

    }
}
