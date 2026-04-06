using System.ComponentModel.DataAnnotations;

namespace BulgarianPhotoSpots.Models
{
    public class Review
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string AuthorName { get; set; } = null!;

        [Required]
        [StringLength(500)]
        public string Comment { get; set; } = null!;

        [Range(1, 10)]
        public int Rating { get; set; }

        public int PhotoSpotId { get; set; }

        public PhotoSpot? PhotoSpot { get; set; }

        public DateTime CreatedOn { get; set; }

        public string UserId { get; set; } = null!;
    }
}
