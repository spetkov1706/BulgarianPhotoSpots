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

        public async Task<IActionResult> Index()
        {
            var photoSpots = await _context.PhotoSpots.ToListAsync();
            return View(photoSpots);
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
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PhotoSpot model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            _context.PhotoSpots.Add(model);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }


        public IActionResult About()
        {
            return View();
        }

    }
}
