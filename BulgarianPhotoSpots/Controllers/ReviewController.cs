using BulgarianPhotoSpots.Infrastructure.Data;
using BulgarianPhotoSpots.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using BulgarianPhotoSpots.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace BulgarianPhotoSpots.Controllers
{
    public class ReviewController : Controller
    {
        public IActionResult Index()
        {
            var reviews = _context.Reviews.ToList();
            return View(reviews);
        }

        private readonly ApplicationDbContext _context;

        public ReviewController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Review/Create
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

            var review = new Review
            {
                Comment = model.Comment,
                Rating = model.Rating,
                PhotoSpotId = model.PhotoSpotId,
                AuthorName = User.Identity?.Name ?? "Anonymous"
            };

            review.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            _context.Reviews.Add(review);
            _context.SaveChanges();

            return RedirectToAction("Details", "PhotoSpots", new { id = model.PhotoSpotId });
        }

        // GET: Review/Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            var review = _context.Reviews.FirstOrDefault(r => r.Id == id);

            if (review == null)
                return NotFound();

            var photoSpotId = review.PhotoSpotId;

            _context.Reviews.Remove(review);
            _context.SaveChanges();

            return RedirectToAction("Details", "PhotoSpots", new { id = photoSpotId });
        }
        // POST: Review/Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var review = _context.Reviews.FirstOrDefault(r => r.Id == id);

            if (review == null)
            {
                return NotFound();
            }

            var photoSpotId = review.PhotoSpotId; 

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (review.UserId != userId)
            {
                TempData["ErrorMessage"] = "You cannot delete someone else's review!";
                return RedirectToAction("Details", "PhotoSpots", new { id = photoSpotId });
            }

            _context.Reviews.Remove(review);
            _context.SaveChanges();

            return RedirectToAction("Details", "PhotoSpots", new { id = photoSpotId });
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

            _context.SaveChanges();

            return RedirectToAction("Details", "PhotoSpots", new { id = review.PhotoSpotId });
        }
    }
}
