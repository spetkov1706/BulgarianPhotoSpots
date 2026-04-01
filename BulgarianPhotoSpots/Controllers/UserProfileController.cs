using BulgarianPhotoSpots.Core.Models;
using BulgarianPhotoSpots.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BulgarianPhotoSpots.Controllers
{
    public class UserProfileController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UserProfileController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult MyProfile()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var profile = _context.UserProfiles
                .FirstOrDefault(p => p.UserId == userId);

            ViewBag.Username = User.Identity.Name;

            return View(profile);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(UserProfile model)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            model.UserId = userId;
            model.CreatedOn = DateTime.Now;

            _context.UserProfiles.Add(model);
            _context.SaveChanges();

            return RedirectToAction("MyProfile");
        }
    }
}
