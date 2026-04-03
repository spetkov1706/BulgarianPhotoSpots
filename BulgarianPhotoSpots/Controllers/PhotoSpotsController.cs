using BulgarianPhotoSpots.Core.Interfaces;
using BulgarianPhotoSpots.Infrastructure.Data;
using BulgarianPhotoSpots.Models;
using BulgarianPhotoSpots.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;

namespace BulgarianPhotoSpots.Controllers
{
    public class PhotoSpotsController : Controller
    {
        private readonly IPhotoSpotService _photoSpotService;
        private readonly ICategoryService _categoryService;
        private readonly ApplicationDbContext _context;

        public PhotoSpotsController(IPhotoSpotService photoSpotService, ICategoryService categoryService, ApplicationDbContext context)
        {
            _photoSpotService = photoSpotService;
            _categoryService = categoryService;
            _context = context;
        }

        public async Task<IActionResult> Index(string searchTerm, string location)
        {
            var photoSpots = await _photoSpotService.GetAllAsync();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                photoSpots = photoSpots
                    .Where(p => p.Title.Contains(searchTerm) ||
                                p.Description.Contains(searchTerm))
                    .ToList();
            }

            if (!string.IsNullOrEmpty(location))
            {
                photoSpots = photoSpots
                    .Where(p => p.Location.Contains(location))
                    .ToList();
            }

            var viewModel = new PhotoSpotListViewModel
            {
                PhotoSpots = photoSpots,
                PageTitle = "All Bulgarian Photo Spots",
                TotalCount = photoSpots.Count()
            };

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var favoriteIds = _context.Favorites
                .Where(f => f.UserId == userId)
                .Select(f => f.PhotoSpotId)
                .ToList();

            ViewBag.FavoriteIds = favoriteIds;

            return View(viewModel);
        }

        public async Task<IActionResult> Details(int id, string? tab)
        {
            var photoSpot = await _photoSpotService.GetByIdAsync(id);

            if (photoSpot == null)
                return NotFound();

            ViewBag.Tab = tab;

            return View(photoSpot);
        }

        [Authorize]
        public async Task<IActionResult> Create()
        {
            var categories = await _categoryService.GetAllAsync();

            var viewModel = new PhotoSpotFormViewModel
            {
                Categories = categories.Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Name
                })
            };

            return View(viewModel);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PhotoSpotFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var categories = await _categoryService.GetAllAsync();

                model.Categories = categories.Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Name
                });

                return View(model);
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var photoSpot = new PhotoSpot
            {
                Title = model.Title,
                Description = model.Description,
                Location = model.Location,
                Rating = model.Rating,
                CategoryId = model.CategoryId,
                UserId = userId 
            };

            await _photoSpotService.CreateAsync(photoSpot);

            TempData["SuccessMessage"] = "Photo spot created successfully!";

            return RedirectToAction(nameof(Index));
        }

        [Authorize]
        public async Task<IActionResult> Edit(int id)
        {
            var photoSpot = await _photoSpotService.GetByIdAsync(id);

            if (photoSpot == null)
                return NotFound();

            var categories = await _categoryService.GetAllAsync();

            ViewData["CategoryId"] = new SelectList(
                categories,
                "Id",
                "Name",
                photoSpot.CategoryId
            );

            return View(photoSpot);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, PhotoSpot model)
        {
            if (id != model.Id)
                return NotFound();

            if (!ModelState.IsValid)
            {
                var categories = await _categoryService.GetAllAsync();

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

        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            var photoSpot = await _photoSpotService.GetByIdAsync(id);

            if (photoSpot == null)
                return NotFound();

            return View(photoSpot);
        }

        [Authorize]
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
