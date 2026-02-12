using BulgarianPhotoSpots.Models;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using BulgarianPhotoSpots.Data;
using Microsoft.EntityFrameworkCore;

namespace BulgarianPhotoSpots.Controllers
{
    public class PhotoSpotsController : Controller
    {
        private readonly ApplicationDbContext _context;
        public PhotoSpotsController(ApplicationDbContext context)
        {
            _context = context;
        }

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
