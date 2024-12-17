using Ambev.DeveloperEvaluation.WebApi;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Testcontainers.MongoDb;
using Testcontainers.PostgreSql;
using Xunit;
using Ambev.DeveloperEvaluation.ORM;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.Integration.Infrastructure;

public class IntegrationTestFixture : WebApplicationFactory<Program>, IAsyncLifetime
{
    private readonly MongoDbContainer _mongoContainer;
    private readonly PostgreSqlContainer _postgresContainer;

    public IntegrationTestFixture()
    {
        _mongoContainer = new MongoDbBuilder()
            .WithImage("mongo:latest")
            .WithPortBinding(27017, true)
            .Build();

        _postgresContainer = new PostgreSqlBuilder()
            .WithImage("postgres:latest")
            .WithPortBinding(5432, true)
            .WithDatabase("ambev-dev-evaluation")
            .WithUsername("postgres")
            .WithPassword("postgres")
            .Build();
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureAppConfiguration((context, config) =>
        {
            config.AddInMemoryCollection(new Dictionary<string, string>
            {
                ["ConnectionStrings:MongoDB"] = _mongoContainer.GetConnectionString(),
                ["ConnectionStrings:PostgreSQL"] = _postgresContainer.GetConnectionString()
            });
        });

        builder.UseEnvironment("Testing");
    }

    public async Task InitializeAsync()
    {
        await _mongoContainer.StartAsync();
        await _postgresContainer.StartAsync();

        using var scope = Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        await dbContext.Database.MigrateAsync();
        await DatabaseSeeder.SeedTestData(dbContext, scope.ServiceProvider);
    }

    public new async Task DisposeAsync()
    {
        await _mongoContainer.DisposeAsync();
        await _postgresContainer.DisposeAsync();
    }
} 