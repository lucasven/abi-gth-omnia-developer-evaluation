using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.ORM;
using Ambev.DeveloperEvaluation.Common.Security;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Ambev.DeveloperEvaluation.WebApi;

namespace Ambev.DeveloperEvaluation.Integration.Infrastructure;

public static class DatabaseSeeder
{
    public static async Task SeedTestData(ApplicationDbContext dbContext, IServiceProvider services)
    {
        await SeedUsers(dbContext, services);
        await dbContext.SaveChangesAsync();
    }

    private static async Task SeedUsers(ApplicationDbContext dbContext, IServiceProvider services)
    {
        if (await dbContext.Users.AnyAsync())
            return;

        var passwordHasher = services.GetRequiredService<IPasswordHasher>();
        var logger = services.GetRequiredService<ILogger<Program>>();

        const string password = "Admin@123";
        var hashedPassword = passwordHasher.HashPassword(password);
        logger.LogInformation("Seeding admin user with password: {Password}, hashed: {HashedPassword}", password, hashedPassword);

        var adminUser = new User
        {
            Id = Guid.NewGuid(),
            Email = "admin@ambev.com",
            Username = "AdminUser",
            Password = hashedPassword,
            Phone = "+5511999999999",
            Role = UserRole.Admin,
            Status = UserStatus.Active,
            CreatedAt = DateTime.UtcNow
        };

        await dbContext.Users.AddAsync(adminUser);
    }
}