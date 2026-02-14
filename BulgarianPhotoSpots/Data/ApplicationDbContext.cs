using Microsoft.EntityFrameworkCore;
using BulgarianPhotoSpots.Models;

namespace BulgarianPhotoSpots.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<PhotoSpot> PhotoSpots { get; set; }
        public DbSet<Category> Categories { get; set; }

    }
}
