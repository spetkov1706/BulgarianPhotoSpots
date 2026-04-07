using BulgarianPhotoSpots.Infrastructure.Data;
using BulgarianPhotoSpots.Infrastructure.Services;
using BulgarianPhotoSpots.Models;
using Microsoft.EntityFrameworkCore;
using Xunit;

public class PhotoSpotServiceTests
{
    private ApplicationDbContext GetDbContext()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString()) 
            .Options;

        return new ApplicationDbContext(options);
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnCorrectPhotoSpot()
    {
        var context = GetDbContext();
        var service = new PhotoSpotService(context);

        var spot = new PhotoSpot
        {
            Id = 1,
            Title = "Test Spot",
            Description = "Test Description",
            Location = "Test Location"
        };

        context.PhotoSpots.Add(spot);
        await context.SaveChangesAsync();

        var result = await service.GetByIdAsync(1);

        Assert.NotNull(result);
        Assert.Equal("Test Spot", result.Title);
    }

    [Fact]
    public async Task CreateAsync_ShouldAddPhotoSpot()
    {
        var context = GetDbContext();
        var service = new PhotoSpotService(context);

        var spot = new PhotoSpot
        {
            Title = "New Spot",
            Description = "Desc",
            Location = "Loc"
        };

        await service.CreateAsync(spot);

        Assert.Equal(1, context.PhotoSpots.Count());
    }
    [Fact]
    public async Task DeleteAsync_ShouldRemovePhotoSpot()
    {
        var context = GetDbContext();
        var service = new PhotoSpotService(context);

        var spot = new PhotoSpot { Id = 1, Title = "Test" };
        context.PhotoSpots.Add(spot);
        await context.SaveChangesAsync();

        await service.DeleteAsync(1);

        Assert.Empty(context.PhotoSpots);
    }
    [Fact]
    public async Task UpdateAsync_ShouldUpdatePhotoSpot()
    {
        var context = GetDbContext();
        var service = new PhotoSpotService(context);

        var spot = new PhotoSpot
        {
            Id = 1,
            Title = "Old Title"
        };

        context.PhotoSpots.Add(spot);
        await context.SaveChangesAsync();

        spot.Title = "New Title";
        await service.UpdateAsync(spot);

        var updated = context.PhotoSpots.First();

        Assert.Equal("New Title", updated.Title);
    }
}