using BulgarianPhotoSpots.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Area("Admin")]
[Authorize(Roles = "Admin")]
public class AdminPhotoSpotsController : Controller
{
    private readonly IPhotoSpotService _photoSpotService;

    public AdminPhotoSpotsController(IPhotoSpotService photoSpotService)
    {
        _photoSpotService = photoSpotService;
    }

    public async Task<IActionResult> Index()
    {
        var photoSpots = await _photoSpotService.GetAllAsync();
        return View(photoSpots);
    }
}