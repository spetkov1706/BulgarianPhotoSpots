using BulgarianPhotoSpots.Infrastructure.Data;
using BulgarianPhotoSpots.Models;
using BulgarianPhotoSpots.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace BulgarianPhotoSpots.Controllers
{
    public class ReviewController : Controller
    {
        public async Task<IActionResult> Index(string sortOrder)
        {
            IQueryable<Review> reviewsQuery = _context.Reviews;

            reviewsQuery = sortOrder switch
            {
                "highest" => reviewsQuery.OrderByDescending(r => r.Rating),
                "lowest" => reviewsQuery.OrderBy(r => r.Rating),
                "newest" => reviewsQuery.OrderByDescending(r => r.Id),
                _ => reviewsQuery.OrderByDescending(r => r.Id) 
            };

            var reviews = await reviewsQuery.ToListAsync();
            return View(reviews);
        }

        private readonly ApplicationDbContext _context;

        public ReviewController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Review/Create
        [Authorize]
        public IActionResult Create(int photoSpotId)
        {
            var model = new ReviewViewModel
            {
                PhotoSpotId = photoSpotId
            };

            return View(model);
        }

        // POST: Review/Create
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ReviewViewModel model)
        {
            Console.WriteLine(model.PhotoSpotId);

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var userName = User.FindFirst("Name")?.Value;

            var review = new Review
            {
                Comment = model.Comment,
                Rating = model.Rating,
                PhotoSpotId = model.PhotoSpotId,
                CreatedOn = DateTime.Now,
                UserName = User.Identity?.Name ?? "Anonymous"
            };

            review.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            _context.Reviews.Add(review);
            _context.SaveChanges();

            return RedirectToAction("Details", "PhotoSpots", new { id = model.PhotoSpotId });
        }

        // GET: Review/Delete
        [HttpGet]
        [Authorize]
        public IActionResult Delete(int id)
        {
            var review = _context.Reviews.FirstOrDefault(r => r.Id == id);
            if (review == null) return NotFound();

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (review.UserId != userId && !User.IsInRole("Admin"))
            {
                return Forbid();
            }

            return View(review);
        }

        // POST: Review/Delete
        [HttpPost, ActionName("Delete")]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var review = await _context.Reviews.FindAsync(id);

            if (review == null)
            {
                return RedirectToAction("Index", "PhotoSpots");
            }

            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (!User.IsInRole("Admin") && review.UserId != currentUserId)
            {
                return Forbid();
            }

            var spotId = review.PhotoSpotId;

            _context.Reviews.Remove(review);
            await _context.SaveChangesAsync();

            return RedirectToAction("Details", "PhotoSpots", new { id = spotId });
        }

        // GET: Review/Edit
        public IActionResult Edit(int id)
        {
            var review = _context.Reviews.Find(id);

            if (review == null)
            {
                return NotFound();
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (review.UserId != userId)
            {
                TempData["ErrorMessage"] = "You cannot edit someone else's review!";
                return RedirectToAction("Details", "PhotoSpots", new { id = review.PhotoSpotId });
            }

            var model = new ReviewViewModel
            {
                Comment = review.Comment,
                Rating = review.Rating,
                PhotoSpotId = review.PhotoSpotId
            };

            return View(model);
        }

        // POST: Review/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, ReviewViewModel model)
        {
            var review = _context.Reviews.Find(id);

            if (review == null)
            {
                return NotFound();
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (review.UserId != userId)
            {
                TempData["ErrorMessage"] = "You cannot edit someone else's review!";
                return RedirectToAction("Details", "PhotoSpots", new { id = review.PhotoSpotId });
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            review.Comment = model.Comment;
            review.Rating = model.Rating;
            review.EditedOn = DateTime.Now;

            _context.SaveChanges();

            return RedirectToAction("Details", "PhotoSpots", new { id = review.PhotoSpotId });
        }
    }
}
