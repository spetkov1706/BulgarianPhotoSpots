using BulgarianPhotoSpots.Core.Models;
using BulgarianPhotoSpots.Infrastructure.Data;
using BulgarianPhotoSpots.Infrastructure.Services;
using BulgarianPhotoSpots.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

public class PhotoSpotServiceTests
{
    private ApplicationDbContext GetDbContext(string dbName)
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: dbName)
            .Options;

        return new ApplicationDbContext(options);
    }

    [Fact]
    public async Task CreateAsync_ShouldAddPhotoSpot()
    {
        var context = GetDbContext("CreateDb");
        var service = new PhotoSpotService(context);

        var photo = new PhotoSpot
        {
            Title = "Test",
            Description = "Desc",
            Location = "Sofia",
            Rating = 5,
            CategoryId = 1
        };

        await service.CreateAsync(photo);

        Assert.Equal(1, context.PhotoSpots.Count());
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnAll()
    {
        var context = GetDbContext("GetAllDb");
        var service = new PhotoSpotService(context);

        await service.CreateAsync(new PhotoSpot
        {
            Title = "A",
            Description = "Some description here",
            Location = "Sofia",
            Rating = 5,
            CategoryId = 1
        });

        await service.CreateAsync(new PhotoSpot
        {
            Title = "B",
            Description = "Another description here",
            Location = "Plovdiv",
            Rating = 6,
            CategoryId = 1
        });

        var result = await service.GetAllAsync();

        Assert.Equal(2, result.Count);
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnCorrectItem()
    {
        var context = GetDbContext("GetByIdDb");
        var service = new PhotoSpotService(context);

        var photo = new PhotoSpot
        {
            Title = "Test",
            Description = "Some description here",
            Location = "Sofia",
            Rating = 5,
            CategoryId = 1
        };

        await service.CreateAsync(photo);

        var result = await service.GetByIdAsync(photo.Id);

        Assert.NotNull(result);
        Assert.Equal("Test", result.Title);
    }

    [Fact]
    public async Task DeleteAsync_ShouldRemoveItem()
    {
        var context = GetDbContext("DeleteDb");
        var service = new PhotoSpotService(context);

        var photo = new PhotoSpot
        {
            Title = "ToDelete",
            Description = "Some description here",
            Location = "Sofia",
            Rating = 5,
            CategoryId = 1
        };

        await service.CreateAsync(photo);

        await service.DeleteAsync(photo.Id);

        Assert.Equal(0, context.PhotoSpots.Count());
    }
}