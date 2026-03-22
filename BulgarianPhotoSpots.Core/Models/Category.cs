using System.ComponentModel.DataAnnotations;

namespace BulgarianPhotoSpots.Models
{
    public class Category
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string Name { get; set; }

        public ICollection<PhotoSpot> PhotoSpots { get; set; } 
            = new List<PhotoSpot>();
    }
}
