using BulgarianPhotoSpots.Infrastructure.Data;
using BulgarianPhotoSpots.Models;
using Microsoft.AspNetCore.Mvc;

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
        public IActionResult Create()
        {
            return View();
        }

        // POST: Review/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Review model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            model.CreatedOn = DateTime.Now;

            _context.Reviews.Add(model);
            _context.SaveChanges();

            return RedirectToAction("Index");

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
