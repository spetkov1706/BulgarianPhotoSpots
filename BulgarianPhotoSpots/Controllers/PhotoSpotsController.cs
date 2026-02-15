using BulgarianPhotoSpots.Data;
using BulgarianPhotoSpots.Models;
using BulgarianPhotoSpots.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace BulgarianPhotoSpots.Controllers
{
    public class PhotoSpotsController : Controller
    {
        private readonly IPhotoSpotService _photoSpotService;
        public PhotoSpotsController(IPhotoSpotService photoSpotService)
        {
            _photoSpotService = photoSpotService;
        }

        public async Task<IActionResult> Index()
        {
            var photoSpots = await _photoSpotService.GetAllAsync();

            ViewData["PageTitle"] = "All Bulgarian Photo Spots";
            ViewBag.Count = photoSpots.Count();

            return View(photoSpots);
        }

        public async Task<IActionResult> Details(int id, string? tab)
        {
            var photoSpot = await _photoSpotService.GetByIdAsync(id);

            if (photoSpot == null)
                return NotFound();

            ViewBag.Tab = tab;

            return View(photoSpot);
        }

        public async Task<IActionResult> Create()
        {
            var categories = await _photoSpotService.GetAllCategoriesAsync();

            ViewData["CategoryId"] = new SelectList(
                categories,
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
                var categories = await _photoSpotService.GetAllCategoriesAsync();

                ViewData["CategoryId"] = new SelectList(
                    categories,
                    "Id",
                    "Name",
                    model.CategoryId
                );

                return View(model);
            }

            await _photoSpotService.CreateAsync(model);

            TempData["SuccessMessage"] = "Photo spot created successfully!";

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var photoSpot = await _photoSpotService.GetByIdAsync(id);

            if (photoSpot == null)
                return NotFound();

            var categories = await _photoSpotService.GetAllCategoriesAsync();

            ViewData["CategoryId"] = new SelectList(
                categories,
                "Id",
                "Name",
                photoSpot.CategoryId
            );

            return View(photoSpot);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, PhotoSpot model)
        {
            if (id != model.Id)
                return NotFound();

            if (!ModelState.IsValid)
            {
                var categories = await _photoSpotService.GetAllCategoriesAsync();

                ViewData["CategoryId"] = new SelectList(
                    categories,
                    "Id",
                    "Name",
                    model.CategoryId
                );

                return View(model);
            }

            await _photoSpotService.UpdateAsync(model);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var photoSpot = await _photoSpotService.GetByIdAsync(id);

            if (photoSpot == null)
                return NotFound();

            return View(photoSpot);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _photoSpotService.DeleteAsync(id);

            return RedirectToAction(nameof(Index));
        }

        public IActionResult About()
        {
            return View();
        }
    }
}
