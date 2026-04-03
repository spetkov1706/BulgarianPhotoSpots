using System.ComponentModel.DataAnnotations;

namespace BulgarianPhotoSpots.ViewModels
{
    public class ReviewViewModel
    {
        public int PhotoSpotId { get; set; }

        [Required]
        [StringLength(500)]
        public string Comment { get; set; } = null!;

        [Range(1, 10)]
        public int Rating { get; set; }
    }
}