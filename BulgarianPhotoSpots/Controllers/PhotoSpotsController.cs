using Microsoft.AspNetCore.Mvc;

namespace BulgarianPhotoSpots.Controllers
{
    public class PhotoSpotsController : Controller
    {
        public IActionResult Index()
        {
            var photoSpots = new List<string>
            {
                "Седемте рилски езера",
                "Белоградчишките скали",
                "Нос Калиакра",
                "Мелнишките пирамиди",
                "Рилски манастир"
            };

            ViewData["Title"] = "Photo Spots in Bulgaria";
            ViewData["Description"] = "Explore beautiful photo spots in Bulgaria";
            ViewData["PhotoSpots"] = photoSpots;
            return View();
        }
        public IActionResult About()
        {
            return View();
        }
    }
}
