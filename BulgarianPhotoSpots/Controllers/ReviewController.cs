using Microsoft.AspNetCore.Mvc;
using BulgarianPhotoSpots.Infrastructure.Data;

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
    }
}
