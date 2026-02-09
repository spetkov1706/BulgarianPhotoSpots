using Microsoft.AspNetCore.Mvc;

namespace BulgarianPhotoSpots.Controllers
{
    public class PhotoSpotsController : Controller
    {
        public IActionResult Index()
        {
            ViewData["Message"] = "Explore beautiful photo spots in Bulgaria";
            return View();
        }
        public IActionResult About()
        {
            return View();
        }
    }
}
