using BulgarianPhotoSpots.Infrastructure.Data;
using BulgarianPhotoSpots.Models;
using Microsoft.EntityFrameworkCore;
using BulgarianPhotoSpots.Core.Interfaces;

namespace BulgarianPhotoSpots.Infrastructure.Services
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
                .ToListAsync();
        }

        public async Task<PhotoSpot?> GetByIdAsync(int id)
        {
            return await _context.PhotoSpots
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
            var entity = await _context.PhotoSpots.FirstOrDefaultAsync(p => p.Id == id);

            if (entity == null)
            {
                return;
            }

            _context.PhotoSpots.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}
