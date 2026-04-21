using System.Net;
using FluentAssertions;
using GovDigitalApp.Infrastructure.Persistence;
using GovDigitalApp.IntegrationTests.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace GovDigitalApp.IntegrationTests.Security;

public class EncryptedPiiTests : IntegrationTestBase
{
    public EncryptedPiiTests(TestWebAppFactory factory) : base(factory) { }

    [Fact]
    public async Task UserPiiColumns_AreRetrievedInClearThroughEfButStoredEncrypted()
    {
        var email = $"pii_{Guid.NewGuid()}@test.com";
        await RegisterAndLoginAsync(email, "PiiPass@123", "Alfred", "Windsor");

        using var scope = Factory.Services.CreateScope();
        var ctx = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        var user = await ctx.Users.FirstAsync(u => u.Email == email);

        user.FirstName.Should().Be("Alfred", "EF value converter should decrypt on read");
        user.LastName.Should().Be("Windsor");
        user.PasswordHash.Should().NotBe("PiiPass@123", "passwords must be hashed");
        user.PasswordHash.Length.Should().BeGreaterThan(20, "BCrypt hashes are > 20 chars");
    }

    [Fact]
    public async Task Register_EndpointResponse_ReturnsClearNames()
    {
        var auth = await RegisterAndLoginAsync(
            $"clear_{Guid.NewGuid()}@test.com", "PiiPass@123", "Charlie", "Mountbatten");
        auth.FirstName.Should().Be("Charlie");
        auth.LastName.Should().Be("Mountbatten");
    }
}
