using BulgarianPhotoSpots.Core.Models;
using BulgarianPhotoSpots.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BulgarianPhotoSpots.Infrastructure.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>

    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<PhotoSpot> PhotoSpots { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Favorite> Favorites { get; set; }
    }
}
