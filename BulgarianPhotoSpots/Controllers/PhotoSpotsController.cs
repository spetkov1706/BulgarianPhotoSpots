using Microsoft.AspNetCore.Mvc;

namespace BulgarianPhotoSpots.Controllers
{
    public class PhotoSpotsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Details()
        {
            return View();
        }
        public IActionResult Create()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
        }

    }
}
