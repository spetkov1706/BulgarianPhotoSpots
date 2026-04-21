using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace BulgarianPhotoSpots.ViewModels
{
    public class UserProfileViewModel
    {
        public string? DisplayName { get; set; } 

        public string? Bio { get; set; }

        public string? ProfilePictureUrl { get; set; }

        public bool HasProfile { get; set; }

        public IFormFile? ProfileImage { get; set; } 

        public List<FavoriteSpotViewModel> FavoriteSpots { get; set; } = new();
    }

    public class FavoriteSpotViewModel
    {
        public int Id { get; set; }
        public int FavoriteId { get; set; }
        public string Title { get; set; }
        public string ImageUrl { get; set; }
        public string Location { get; set; }
    }
}