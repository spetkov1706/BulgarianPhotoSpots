using BulgarianPhotoSpots.Infrastructure.Data;
using BulgarianPhotoSpots.Infrastructure.Migrations;
using BulgarianPhotoSpots.Models;
using BulgarianPhotoSpots.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using System.Security.Claims;

namespace BulgarianPhotoSpots.Controllers
{
    public class ReviewController : Controller
    {
        public IActionResult Index(string? sort)
        {
            ViewBag.CurrentSort = sort;

            var reviews = _context.Reviews
                .Include(r => r.PhotoSpot)
                .AsQueryable();

            reviews = sort switch
            {
                "rating_desc" => reviews.OrderByDescending(r => r.Rating),
                "rating_asc" => reviews.OrderBy(r => r.Rating),
                _ => reviews.OrderByDescending(r => r.Id) 
            };

            return View(reviews.ToList());
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

            if (review == null)
            {
                return NotFound();
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (review.UserId != userId)
            {
                return Forbid();
            }

            return View(review);
        }

        // POST: Review/Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public IActionResult DeleteConfirmed(int id)
        {
            var review = _context.Reviews.FirstOrDefault(r => r.Id == id);

            if (review == null)
            {
                return NotFound();
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (review.UserId != userId)
            {
                return Forbid();
            }

            var photoSpotId = review.PhotoSpotId;

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
            review.EditedOn = DateTime.Now;

            _context.SaveChanges();

            return RedirectToAction("Details", "PhotoSpots", new { id = review.PhotoSpotId });
        }
    }
}
