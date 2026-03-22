using BulgarianPhotoSpots.Models;

namespace BulgarianPhotoSpots.Core.Interfaces
{
    public interface IPhotoSpotService
    {
        Task<List<PhotoSpot>> GetAllAsync();
        Task<PhotoSpot?> GetByIdAsync(int id);
        Task CreateAsync(PhotoSpot model);
        Task UpdateAsync(PhotoSpot model);
        Task DeleteAsync(int id);
    }
}
