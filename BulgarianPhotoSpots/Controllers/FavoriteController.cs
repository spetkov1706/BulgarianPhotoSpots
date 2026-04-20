using BulgarianPhotoSpots.Core.Models;
using BulgarianPhotoSpots.Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace BulgarianPhotoSpots.Controllers
{
    [Authorize]
    public class FavoriteController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FavoriteController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult Add(int photoSpotId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var favorite = new Favorite
            {
                UserId = userId,
                PhotoSpotId = photoSpotId
            };

            _context.Favorites.Add(favorite);
            _context.SaveChanges();

            return RedirectToAction("Index", "PhotoSpots");
        }

        public IActionResult Remove(int id)
        {
            var favorite = _context.Favorites.Find(id);

            if (favorite != null)
            {
                _context.Favorites.Remove(favorite);
                _context.SaveChanges();
            }

            return RedirectToAction("MyFavorites");
        }

        public IActionResult MyFavorites()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var favorites = _context.Favorites
                .Include(f => f.PhotoSpot)
                .Where(f => f.UserId == userId)
                .ToList();

            return View(favorites);
        }
        [HttpPost]
        public IActionResult ToggleFavorite(int photoSpotId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var favorite = _context.Favorites
                .FirstOrDefault(f => f.PhotoSpotId == photoSpotId && f.UserId == userId);

            if (favorite == null)
            {
                var newFavorite = new Favorite
                {
                    UserId = userId,
                    PhotoSpotId = photoSpotId
                };

                _context.Favorites.Add(newFavorite);
            }
            else
            {
                _context.Favorites.Remove(favorite);
            }

            _context.SaveChanges();

            return RedirectToAction("Index", "PhotoSpots");
        }
    }
}