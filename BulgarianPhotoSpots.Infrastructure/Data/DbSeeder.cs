using BulgarianPhotoSpots.Infrastructure.Data;
using BulgarianPhotoSpots.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

public static class DbSeeder
{
    public static void Seed(ApplicationDbContext context)
    {
        // Categories
        if (!context.Categories.Any())
        {
            context.Categories.AddRange(
                new Category { Name = "Mountains" },
                new Category { Name = "Sea" },
                new Category { Name = "City" }
            );

            context.SaveChanges();
        }

        // PhotoSpots
        if (!context.PhotoSpots.Any())
        {
            var categories = context.Categories.ToList();

            context.PhotoSpots.AddRange(
                new PhotoSpot
                {
                    Title = "Seven Rila Lakes",
                    Description = "7 lakes in Rila mountain",
                    Location = "Rila",
                    Rating = 10,
                    CategoryId = categories.First(c => c.Name == "Mountains").Id
                },
                new PhotoSpot
                {
                    Title = "Old Town Nesebar",
                    Description = "Ancient coastal town UNESCO site",
                    Location = "Nesebar",
                    Rating = 9,
                    CategoryId = categories.First(c => c.Name == "Sea").Id
                },
                new PhotoSpot
                {
                    Title = "Sofia Center",
                    Description = "Capital city center",
                    Location = "Sofia",
                    Rating = 8,
                    CategoryId = categories.First(c => c.Name == "City").Id
                }
            );

            context.SaveChanges();
        }
    }
}