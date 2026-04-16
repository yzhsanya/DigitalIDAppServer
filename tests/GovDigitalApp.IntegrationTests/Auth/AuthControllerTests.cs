using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using GovDigitalApp.Application.Auth.Requests;
using GovDigitalApp.Application.Auth.Responses;
using GovDigitalApp.IntegrationTests.Common;

namespace GovDigitalApp.IntegrationTests.Auth;

public class AuthControllerTests : IntegrationTestBase
{
    public AuthControllerTests(TestWebAppFactory factory) : base(factory) { }

    [Fact]
    public async Task Register_WithValidData_ReturnsOkWithToken()
    {
        var request = new RegisterRequest
        {
            Email = $"newuser_{Guid.NewGuid()}@test.com",
            Password = "SecurePass@123",
            FirstName = "John",
            LastName = "Doe",
        };

        var response = await Client.PostAsJsonAsync("/api/auth/register", request);

        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var auth = await response.Content.ReadFromJsonAsync<AuthResponse>();
        auth.Should().NotBeNull();
        auth!.Token.Should().NotBeNullOrEmpty();
        auth.Email.Should().Be(request.Email);
        auth.FirstName.Should().Be(request.FirstName);
    }

    [Fact]
    public async Task Register_WithDuplicateEmail_ReturnsConflict()
    {
        var email = $"duplicate_{Guid.NewGuid()}@test.com";
        var request = new RegisterRequest { Email = email, Password = "Pass@123", FirstName = "A", LastName = "B" };
        await Client.PostAsJsonAsync("/api/auth/register", request);

        var response = await Client.PostAsJsonAsync("/api/auth/register", request);

        response.StatusCode.Should().Be(HttpStatusCode.Conflict);
    }

    [Fact]
    public async Task Login_WithValidCredentials_ReturnsToken()
    {
        var email = $"login_{Guid.NewGuid()}@test.com";
        var password = "LoginPass@123";
        await Client.PostAsJsonAsync("/api/auth/register", new RegisterRequest
        {
            Email = email, Password = password, FirstName = "Jane", LastName = "Smith",
        });

        var response = await Client.PostAsJsonAsync("/api/auth/login", new LoginRequest
        {
            Email = email, Password = password,
        });

        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var auth = await response.Content.ReadFromJsonAsync<AuthResponse>();
        auth!.Token.Should().NotBeNullOrEmpty();
    }

    [Fact]
    public async Task Login_WithWrongPassword_ReturnsUnauthorized()
    {
        var email = $"wrongpass_{Guid.NewGuid()}@test.com";
        await Client.PostAsJsonAsync("/api/auth/register", new RegisterRequest
        {
            Email = email, Password = "CorrectPass@123", FirstName = "X", LastName = "Y",
        });

        var response = await Client.PostAsJsonAsync("/api/auth/login", new LoginRequest
        {
            Email = email, Password = "WrongPass@123",
        });

        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }
}
