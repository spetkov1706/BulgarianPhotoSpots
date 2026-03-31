using BulgarianPhotoSpots.Models;

namespace BulgarianPhotoSpots.Core.Models
{
    public class Favorite
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        public int PhotoSpotId { get; set; }

        public PhotoSpot PhotoSpot { get; set; }
    }
}
