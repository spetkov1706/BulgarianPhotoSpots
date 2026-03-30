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
    }
}
