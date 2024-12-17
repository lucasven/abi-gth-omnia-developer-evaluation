using System.Net.Http.Headers;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Ambev.DeveloperEvaluation.Integration.Infrastructure;

public abstract class IntegrationTestBase : IClassFixture<IntegrationTestFixture>
{
    protected readonly HttpClient Client;
    protected readonly IntegrationTestFixture Fixture;

    protected IntegrationTestBase(IntegrationTestFixture fixture)
    {
        Fixture = fixture;
        Client = fixture.CreateClient();
    }

    protected void AuthenticateClient(string token)
    {
        Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
    }

    protected T GetService<T>() where T : class
    {
        return Fixture.Services.GetRequiredService<T>();
    }
} 