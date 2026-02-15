using BulgarianPhotoSpots.Data;
using BulgarianPhotoSpots.Models;
using Microsoft.EntityFrameworkCore;

namespace BulgarianPhotoSpots.Services
{
    public class PhotoSpotService : IPhotoSpotService
    {
        private readonly ApplicationDbContext _context;

        public PhotoSpotService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<PhotoSpot>> GetAllAsync()
        {
            return await _context.PhotoSpots
                .Include(p => p.Category)
                .ToListAsync();
        }

        public async Task<PhotoSpot?> GetByIdAsync(int id)
        {
            return await _context.PhotoSpots
                .Include(p => p.Category)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task CreateAsync(PhotoSpot model)
        {
            await _context.PhotoSpots.AddAsync(model);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(PhotoSpot model)
        {
            _context.PhotoSpots.Update(model);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.PhotoSpots.FindAsync(id);

            if (entity != null)
            {
                _context.PhotoSpots.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}
