using BulgarianPhotoSpots.Models;

namespace BulgarianPhotoSpots.Services
{
    public interface ICategoryService
    {
        Task<List<Category>> GetAllAsync();
        Task<Category?> GetByIdAsync(int id);
    }
}
