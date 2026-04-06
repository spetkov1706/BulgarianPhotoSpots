using System.ComponentModel.DataAnnotations;

namespace BulgarianPhotoSpots.ViewModels
{
    public class ReviewViewModel
    {
        public int Id { get; set; }
        public int PhotoSpotId { get; set; }

        [Required(ErrorMessage = "Comment is required")]
        [StringLength(500, MinimumLength = 3, ErrorMessage = "Comment must be between 3 and 500 characters")]
        public string Comment { get; set; } = null!;

        [Range(1, 10, ErrorMessage = "Rating must be between 1 and 10")]
        public int Rating { get; set; }
    }
}