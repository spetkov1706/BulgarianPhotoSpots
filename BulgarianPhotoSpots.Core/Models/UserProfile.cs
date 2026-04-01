using System;
using System.Collections.Generic;
using System.Text;

namespace BulgarianPhotoSpots.Core.Models
{
    public class UserProfile
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        public string Bio { get; set; }

        public string ProfilePictureUrl { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
