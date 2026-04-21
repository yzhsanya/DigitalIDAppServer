using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using GovDigitalApp.Application.Auth.Requests;
using GovDigitalApp.Application.Auth.Responses;
using GovDigitalApp.IntegrationTests.Common;

namespace GovDigitalApp.IntegrationTests.Auth;

public class AuthValidationTests : IntegrationTestBase
{
    public AuthValidationTests(TestWebAppFactory factory) : base(factory) { }

    [Fact]
    public async Task Register_WithMalformedEmail_ReturnsBadRequest()
    {
        var request = new RegisterRequest
        {
            Email = "not-an-email",
            Password = "ValidPass@123",
            FirstName = "Test",
            LastName = "User",
        };

        var response = await Client.PostAsJsonAsync("/api/auth/register", request);

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task Register_WithShortPassword_ReturnsBadRequest()
    {
        var request = new RegisterRequest
        {
            Email = $"shortpw_{Guid.NewGuid()}@test.com",
            Password = "short",
            FirstName = "Test",
            LastName = "User",
        };

        var response = await Client.PostAsJsonAsync("/api/auth/register", request);

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task Register_WithMissingFirstName_ReturnsBadRequest()
    {
        var request = new RegisterRequest
        {
            Email = $"nofn_{Guid.NewGuid()}@test.com",
            Password = "ValidPass@123",
            FirstName = "",
            LastName = "User",
        };

        var response = await Client.PostAsJsonAsync("/api/auth/register", request);

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task Register_WithInjectionInName_ReturnsBadRequest()
    {
        var request = new RegisterRequest
        {
            Email = $"inj_{Guid.NewGuid()}@test.com",
            Password = "ValidPass@123",
            FirstName = "Bob<script>",
            LastName = "Jones",
        };

        var response = await Client.PostAsJsonAsync("/api/auth/register", request);

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task Register_NormalisesEmailToLowercase()
    {
        var mixedCase = $"MixedCase_{Guid.NewGuid()}@Test.COM";
        var request = new RegisterRequest
        {
            Email = mixedCase,
            Password = "ValidPass@123",
            FirstName = "Case",
            LastName = "User",
        };

        var response = await Client.PostAsJsonAsync("/api/auth/register", request);

        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var auth = await response.Content.ReadFromJsonAsync<AuthResponse>();
        auth!.Email.Should().Be(mixedCase.ToLowerInvariant());
    }

    [Fact]
    public async Task Login_IsCaseInsensitiveOnEmail()
    {
        var email = $"case_{Guid.NewGuid()}@test.com";
        await RegisterAndLoginAsync(email, "ValidPass@123", "Case", "User");

        var response = await Client.PostAsJsonAsync("/api/auth/login", new LoginRequest
        {
            Email = email.ToUpperInvariant(),
            Password = "ValidPass@123",
        });

        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task Login_WithUnknownEmail_ReturnsUnauthorized()
    {
        var response = await Client.PostAsJsonAsync("/api/auth/login", new LoginRequest
        {
            Email = $"ghost_{Guid.NewGuid()}@test.com",
            Password = "DoesNotMatter@123",
        });

        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }
}
