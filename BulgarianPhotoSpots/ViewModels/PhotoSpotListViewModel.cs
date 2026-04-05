using BulgarianPhotoSpots.Models;

namespace BulgarianPhotoSpots.ViewModels
{
    public class PhotoSpotListViewModel
    {
        public IEnumerable<PhotoSpot> PhotoSpots { get; set; } = new List<PhotoSpot>();

        public string PageTitle { get; set; } = string.Empty;

        public int TotalCount { get; set; }

        public string? SearchTerm { get; set; }

        public int? CategoryId { get; set; }

        public IEnumerable<Category>? Categories { get; set; }
    }
}
