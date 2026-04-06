using BulgarianPhotoSpots.Core.Interfaces;
using BulgarianPhotoSpots.Infrastructure.Data;
using BulgarianPhotoSpots.Models;
using BulgarianPhotoSpots.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
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

        public async Task<IActionResult> Index(string? searchTerm, string? location, int? categoryId, int page = 1)
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

            if (categoryId.HasValue)
            {
                photoSpots = photoSpots
                    .Where(p => p.CategoryId == categoryId.Value)
                    .ToList();
            }

            int pageSize = 5;
            int totalCount = photoSpots.Count();

            var pagedSpots = photoSpots
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            var viewModel = new PhotoSpotListViewModel
            {
                PhotoSpots = pagedSpots,
                TotalCount = totalCount,
                PageTitle = "All Bulgarian Photo Spots",

                SearchTerm = searchTerm,
                CategoryId = categoryId,
                Categories = _context.Categories.ToList(),

                CurrentPage = page,
                TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize)
            };

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var favoriteIds = _context.Favorites
                .Where(f => f.UserId == userId)
                .Select(f => f.PhotoSpotId)
                .ToList();

            ViewBag.FavoriteIds = favoriteIds;

            return View(viewModel);
        }

        public async Task<IActionResult> Details(int id, string? tab, string? sort)
        {
            var photoSpot = _context.PhotoSpots
                .Include(p => p.Reviews)
                .FirstOrDefault(p => p.Id == id);

            if (photoSpot == null)
                return NotFound();

            ViewBag.Tab = tab;
            ViewBag.CurrentSort = sort;

            var reviews = photoSpot.Reviews.AsQueryable();

            reviews = sort switch
            {
                "rating_desc" => reviews.OrderByDescending(r => r.Rating),
                "rating_asc" => reviews.OrderBy(r => r.Rating),
                _ => reviews.OrderByDescending(r => r.Id)
            };

            photoSpot.Reviews = reviews.ToList();

            ViewBag.AverageRating = photoSpot.Reviews.Any()
                ? photoSpot.Reviews.Average(r => r.Rating)
                : 0;

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
                UserId = userId! 
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

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (photoSpot.UserId != userId)
            {
                TempData["ErrorMessage"] = "You cannot edit other users' photo spots!";
                return RedirectToAction(nameof(Index)); 
            }

            return View(photoSpot);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, PhotoSpotFormViewModel model)
        {
            var photoSpot = await _photoSpotService.GetByIdAsync(id);

            if (photoSpot == null)
            {
                return NotFound();
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (photoSpot.UserId != userId)
            {
                TempData["ErrorMessage"] = "You cannot edit other users' photo spots.";
                return RedirectToAction(nameof(Index));
            }

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

            photoSpot.Title = model.Title;
            photoSpot.Description = model.Description;
            photoSpot.Location = model.Location;
            photoSpot.Rating = model.Rating;
            photoSpot.CategoryId = model.CategoryId;

            await _photoSpotService.UpdateAsync(photoSpot);

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
