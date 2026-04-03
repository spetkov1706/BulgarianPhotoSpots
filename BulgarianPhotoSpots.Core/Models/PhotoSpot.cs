using BulgarianPhotoSpots.Core.Models;
using System.ComponentModel.DataAnnotations;

namespace BulgarianPhotoSpots.Models
{
    public class PhotoSpot
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Title is required!")]
        [StringLength(100, MinimumLength = 3,
            ErrorMessage = "Title must be between 3 and 100 characters.")]
        public string Title { get; set; } = null!; 

        [Required(ErrorMessage = "Description is required.")]
        [StringLength(500, MinimumLength = 10,
            ErrorMessage = "Description must be between 10 and 500 characters.")]
        public string Description { get; set; } = null!;

        [Required(ErrorMessage = "Location is required.")]
        public string Location { get; set; } = null!;

        [Required]
        [Range(1, 10, ErrorMessage = "Rating must be between 1 and 10.")]
        public int Rating { get; set; }

        public int CategoryId { get; set; }

        public Category? Category { get; set; }

        public string UserId { get; set; } = null!;

        public ICollection<Review> Reviews { get; set; } = new List<Review>();
        public ICollection<Favorite> Favorites { get; set; } = new List<Favorite>();
    }
}
