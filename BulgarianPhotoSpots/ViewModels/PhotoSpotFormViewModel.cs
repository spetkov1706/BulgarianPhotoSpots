using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace BulgarianPhotoSpots.ViewModels
{
    public class PhotoSpotFormViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string Title { get; set; } = null!;

        [Required]
        [StringLength(500)]
        public string Description { get; set; } = null!;

        [Required]
        public string Location { get; set; } = null!;

        [Range(1, 10)]
        public int Rating { get; set; }

        [Required]
        public int CategoryId { get; set; }

        public IEnumerable<SelectListItem>? Categories { get; set; }
        public IFormFile? ImageFile { get; set; }
        public string? ImageUrl { get; set; }
    }
}
