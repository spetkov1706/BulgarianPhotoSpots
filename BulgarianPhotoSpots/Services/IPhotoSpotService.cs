using BulgarianPhotoSpots.Models;

namespace BulgarianPhotoSpots.Services
{
    public interface IPhotoSpotService
    {
        Task<List<PhotoSpot>> GetAllAsync();
        Task<PhotoSpot?> GetByIdAsync(int id);
        Task<List<Category>> GetAllCategoriesAsync();
        Task CreateAsync(PhotoSpot model);
        Task UpdateAsync(PhotoSpot model);
        Task DeleteAsync(int id);
    }
}
