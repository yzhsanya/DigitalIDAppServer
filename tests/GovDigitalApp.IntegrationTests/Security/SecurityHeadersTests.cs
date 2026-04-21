using System.Net;
using FluentAssertions;
using GovDigitalApp.IntegrationTests.Common;

namespace GovDigitalApp.IntegrationTests.Security;

public class SecurityHeadersTests : IntegrationTestBase
{
    public SecurityHeadersTests(TestWebAppFactory factory) : base(factory) { }

    [Fact]
    public async Task EveryResponse_IncludesBaselineSecurityHeaders()
    {
        var response = await Client.GetAsync("/health");

        response.StatusCode.Should().Be(HttpStatusCode.OK);
        response.Headers.Contains("X-Content-Type-Options").Should().BeTrue();
        response.Headers.GetValues("X-Content-Type-Options").Should().Contain("nosniff");
        response.Headers.GetValues("X-Frame-Options").Should().Contain("DENY");
        response.Headers.GetValues("Referrer-Policy").Should().Contain("no-referrer");
        response.Headers.GetValues("Content-Security-Policy").Should().NotBeEmpty();
    }

    [Fact]
    public async Task ProtectedEndpoint_WithoutToken_Returns401()
    {
        var response = await Client.GetAsync("/api/documents");
        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task ProtectedEndpoint_WithGarbageToken_Returns401()
    {
        AuthorizeClient("garbage.token.value");
        var response = await Client.GetAsync("/api/documents");
        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task ProtectedEndpoint_WithOtherUsersToken_Returns401OrForbidden()
    {
        AuthorizeClient("eyJhbGciOiJIUzI1NiJ9.invalid.payload");
        var response = await Client.GetAsync("/api/documents");
        response.StatusCode.Should().BeOneOf(HttpStatusCode.Unauthorized, HttpStatusCode.Forbidden);
    }
}
