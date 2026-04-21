using BulgarianPhotoSpots.Core.Models;
using BulgarianPhotoSpots.Infrastructure.Data;
using BulgarianPhotoSpots.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
                .FirstOrDefault(u => u.UserId == userId);

            var favorites = _context.Favorites
                .Include(f => f.PhotoSpot)
                .Where(f => f.UserId == userId)
                .Select(f => new FavoriteSpotViewModel
                {
                    Id = f.PhotoSpot.Id,
                    FavoriteId = f.Id,
                    Title = f.PhotoSpot.Title,
                    ImageUrl = f.PhotoSpot.ImageUrl,
                    Location = f.PhotoSpot.Location
                })
                .ToList();

            var viewModel = new UserProfileViewModel
            {
                DisplayName = profile?.DisplayName ?? User.Identity?.Name,
                Bio = profile?.Bio,
                ProfilePictureUrl = profile?.ProfilePictureUrl,
                HasProfile = profile != null,
                FavoriteSpots = favorites
            };

            return View(viewModel);
        }

        public IActionResult Create()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (_context.UserProfiles.Any(u => u.UserId == userId))
            {
                return RedirectToAction("Edit");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(UserProfileViewModel model)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            string? uniqueFileName = null;
            if (model.ProfileImage != null)
            {
                string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "profiles");
                if (!Directory.Exists(uploadsFolder)) Directory.CreateDirectory(uploadsFolder);

                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.ProfileImage.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await model.ProfileImage.CopyToAsync(fileStream);
                }
            }

            var profile = new UserProfile
            {
                UserId = userId,
                DisplayName = model.DisplayName ?? User.Identity?.Name ?? "User",
                Bio = model.Bio,
                ProfilePictureUrl = uniqueFileName != null ? "/images/profiles/" + uniqueFileName : "/images/default-avatar.png",
                CreatedOn = DateTime.Now
            };

            _context.UserProfiles.Add(profile);
            await _context.SaveChangesAsync();

            return RedirectToAction("MyProfile");
        }

        public IActionResult Edit()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var profile = _context.UserProfiles.FirstOrDefault(p => p.UserId == userId);

            if (profile == null) return RedirectToAction("Create");

            return View(profile);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(UserProfile model)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var profile = _context.UserProfiles.FirstOrDefault(p => p.UserId == userId);

            if (profile == null) return NotFound();

            profile.Bio = model.Bio;
            profile.ProfilePictureUrl = model.ProfilePictureUrl;

            _context.SaveChanges();
            return RedirectToAction("MyProfile");
        }
    }
}