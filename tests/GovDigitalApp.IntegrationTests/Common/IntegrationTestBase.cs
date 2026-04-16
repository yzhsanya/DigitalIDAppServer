using System.Net.Http.Headers;
using System.Net.Http.Json;
using GovDigitalApp.Application.Auth.Requests;
using GovDigitalApp.Application.Auth.Responses;
using GovDigitalApp.Infrastructure.Persistence;
using Microsoft.Extensions.DependencyInjection;

namespace GovDigitalApp.IntegrationTests.Common;

public abstract class IntegrationTestBase : IClassFixture<TestWebAppFactory>, IDisposable
{
    protected readonly HttpClient Client;
    protected readonly TestWebAppFactory Factory;

    protected IntegrationTestBase(TestWebAppFactory factory)
    {
        Factory = factory;
        Client = factory.CreateClient();
    }

    protected async Task<AuthResponse> RegisterAndLoginAsync(
        string email = "test@test.com",
        string password = "Test@1234",
        string firstName = "Test",
        string lastName = "User")
    {
        var registerRequest = new RegisterRequest
        {
            Email = email,
            Password = password,
            FirstName = firstName,
            LastName = lastName,
        };
        var response = await Client.PostAsJsonAsync("/api/auth/register", registerRequest);
        response.EnsureSuccessStatusCode();
        var auth = await response.Content.ReadFromJsonAsync<AuthResponse>();
        return auth!;
    }

    protected void AuthorizeClient(string token)
    {
        Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
    }

    protected AppDbContext GetDbContext()
    {
        var scope = Factory.Services.CreateScope();
        return scope.ServiceProvider.GetRequiredService<AppDbContext>();
    }

    public void Dispose()
    {
        Client.DefaultRequestHeaders.Authorization = null;
    }
}
