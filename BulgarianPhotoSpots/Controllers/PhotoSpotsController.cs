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

        public async Task<IActionResult> Details(int id)
        {
            var photoSpot = await _context.PhotoSpots
                .FirstOrDefaultAsync(p => p.Id == id);

            if (photoSpot == null)
            {
                return NotFound();
            }

            return View(photoSpot);
        }
        
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var photoSpot = await _context.PhotoSpots.FindAsync(id);

            if (photoSpot == null)
            {
                return NotFound();
            }

            return View(photoSpot);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, PhotoSpot model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            _context.Update(model);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
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
