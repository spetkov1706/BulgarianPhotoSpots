using BulgarianPhotoSpots.Data;
using BulgarianPhotoSpots.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

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

            ViewData["PageTitle"] = "All Bulgarian Photo Spots";
            ViewBag.Count = photoSpots.Count;

            return View(photoSpots);
        }

        public async Task<IActionResult> Details(int id, string? tab)
        {
            var photoSpot = await _context.PhotoSpots
                .FirstOrDefaultAsync(p => p.Id == id);

            if (photoSpot == null)
            {
                return NotFound();
            }

            ViewBag.Tab = tab;

            return View(photoSpot);
        }

        public async Task<IActionResult> Create()
        {
            ViewData["CategoryId"] = new SelectList(
                await _context.Categories.ToListAsync(),
                "Id",
                "Name"
            );

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

            TempData["SuccessMessage"] = "Photo spot created successfully!";

            return RedirectToAction(nameof(Index));
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

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var spot = await _context.PhotoSpots
                .FirstOrDefaultAsync(m => m.Id == id);

            if (spot == null)
                return NotFound();

            return View(spot);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var photoSpot = await _context.PhotoSpots.FindAsync(id);
            if (photoSpot != null)
            {
                _context.PhotoSpots.Remove(photoSpot);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }


        public IActionResult About()
        {
            return View();
        }

    }
}
