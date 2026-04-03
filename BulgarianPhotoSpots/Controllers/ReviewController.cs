using BulgarianPhotoSpots.Infrastructure.Data;
using BulgarianPhotoSpots.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using BulgarianPhotoSpots.ViewModels;

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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ReviewViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var review = new Review
            {
                Comment = model.Comment,
                Rating = model.Rating,
                PhotoSpotId = model.PhotoSpotId,
                AuthorName = User.Identity.Name
            };

            _context.Reviews.Add(review);
            await _context.SaveChangesAsync();

            return RedirectToAction("Details", "PhotoSpots", new { id = model.PhotoSpotId });
        }

        // GET: Review/Delete/5
        public IActionResult Delete(int id)
        {
            var review = _context.Reviews.FirstOrDefault(r => r.Id == id);

            if (review == null)
            {
                return NotFound();
            }

            return View(review);
        }
        // POST: Review/Delete/5
        [HttpPost]
        public IActionResult Delete(Review model)
        {
            var review = _context.Reviews.FirstOrDefault(r => r.Id == model.Id);

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
