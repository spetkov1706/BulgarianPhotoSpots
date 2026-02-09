using Microsoft.AspNetCore.Mvc;

namespace BulgarianPhotoSpots.Controllers
{
    public class PhotoSpotsController : Controller
    {
        public IActionResult Index()
        {
            return Content("PhotoSpots Index page");
        }
        public IActionResult Details(int id)
        {
            return Content($"Photo spot details for id = {id}");
        }
    }
}
