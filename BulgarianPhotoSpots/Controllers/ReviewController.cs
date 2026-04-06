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

            _context.Reviews.Add(review);
            _context.SaveChanges();

            return RedirectToAction("Details", "PhotoSpots", new { id = model.PhotoSpotId });
        }

        // GET: Review/Delete
        public IActionResult Delete(int id)
        {
            var review = _context.Reviews.FirstOrDefault(r => r.Id == id);

            if (review == null)
            {
                return NotFound();
            }

            return View(review);
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

            _context.Reviews.Remove(review);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
