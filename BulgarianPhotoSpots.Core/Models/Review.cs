using System.ComponentModel.DataAnnotations;
using BulgarianPhotoSpots.Core.Models;

namespace BulgarianPhotoSpots.Models
{
    public class Review
    {
        public int Id { get; set; }

        [Required]
        [StringLength(500)]
        public string Comment { get; set; } = null!;

        [Range(1, 10)]
        public int Rating { get; set; }

        public int PhotoSpotId { get; set; }
        public PhotoSpot PhotoSpot { get; set; } = null!;

        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;

        public string UserId { get; set; } = null!;
        
        public DateTime? EditedOn { get; set; }
        public string UserName { get; set; } = null!;
    }
}
