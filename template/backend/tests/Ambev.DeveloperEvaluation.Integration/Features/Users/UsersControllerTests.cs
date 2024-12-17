using System.Net;
using System.Net.Http.Json;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Integration.Infrastructure;
using Ambev.DeveloperEvaluation.ORM;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Xunit;
using Bogus;

namespace Ambev.DeveloperEvaluation.Integration.Features.Users;

public class UsersControllerTests : IntegrationTestBase
{
    private readonly Faker _faker;

    public UsersControllerTests(IntegrationTestFixture fixture) : base(fixture)
    {
        _faker = new Faker();
    }

    [Fact]
    public async Task CreateUser_WithValidData_ShouldCreateUserAndReturnDetails()
    {
        // Arrange
        var createRequest = new
        {
            Username = _faker.Internet.UserName(),
            Password = _faker.Internet.Password(8, false, "\\w", "@1"),
            Email = _faker.Internet.Email(),
            Phone = _faker.Phone.PhoneNumber("+55119########"),
            Status = UserStatus.Active,
            Role = UserRole.Customer
        };

        // Act
        var response = await Client.PostAsJsonAsync("/api/Users", createRequest);

        // Assert
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        
        var content = await response.Content.ReadFromJsonAsync<ApiResponseWithData<CreateUserResponse>>();
        Assert.NotNull(content?.Data);
        Assert.Equal(createRequest.Email, content.Data.Email);
        Assert.Equal(createRequest.Username, content.Data.UserName);
        Assert.Equal(createRequest.Phone, content.Data.Phone);
        Assert.Equal(createRequest.Role, content.Data.Role);
        Assert.Equal(createRequest.Status, content.Data.Status);

        // Verify user exists in database
        using var scope = Fixture.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        var user = await dbContext.Users.FirstOrDefaultAsync(u => u.Email == createRequest.Email);
        Assert.NotNull(user);
        Assert.Equal(createRequest.Username, user.Username);
        Assert.Equal(createRequest.Phone, user.Phone);
        Assert.Equal(createRequest.Role, user.Role);
        Assert.Equal(createRequest.Status, user.Status);
    }

    [Fact]
    public async Task CreateUser_ThenAuthenticate_ShouldSucceed()
    {
        // Arrange - Create user request
        var createRequest = new
        {
            Username = _faker.Internet.UserName(),
            Password = _faker.Internet.Password(8, false, "\\w", "@1"),
            Email = _faker.Internet.Email(),
            Phone = _faker.Phone.PhoneNumber("+55119########"),
            Status = UserStatus.Active,
            Role = UserRole.Customer
        };

        // Act 1 - Create user
        var createResponse = await Client.PostAsJsonAsync("/api/Users", createRequest);
        Assert.Equal(HttpStatusCode.Created, createResponse.StatusCode);

        // Arrange - Auth request
        var authRequest = new
        {
            Email = createRequest.Email,
            Password = createRequest.Password
        };

        // Act 2 - Authenticate
        var authResponse = await Client.PostAsJsonAsync("/api/Auth", authRequest);

        // Assert
        Assert.Equal(HttpStatusCode.OK, authResponse.StatusCode);
        var authContent = await authResponse.Content.ReadFromJsonAsync<ApiResponseWithData<AuthResponse>>();
        Assert.NotNull(authContent?.Data);
        Assert.NotNull(authContent.Data.Token);
        Assert.Equal(authContent.Data.Name, createRequest.Username);
        Assert.Equal(authContent.Data.Email, createRequest.Email);
    }
}

public class ApiResponseWithData<T>
{
    public bool Success { get; set; }
    public string Message { get; set; } = string.Empty;
    public T Data { get; set; } = default!;
}

public class CreateUserResponse
{
    public Guid Id { get; set; }
    public string UserName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public UserRole Role { get; set; }
    public UserStatus Status { get; set; }
}

public class AuthResponse
{
    public string Token { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
} 