using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using GovDigitalApp.Application.Dvla.Requests;
using GovDigitalApp.Application.Dvla.Responses;
using GovDigitalApp.IntegrationTests.Common;

namespace GovDigitalApp.IntegrationTests.Dvla;

public class DvlaControllerTests : IntegrationTestBase
{
    public DvlaControllerTests(TestWebAppFactory factory) : base(factory) { }

    [Fact]
    public async Task GetVehicles_WhenAuthenticated_ReturnsEmptyList()
    {
        var auth = await RegisterAndLoginAsync($"dvla_{Guid.NewGuid()}@test.com");
        AuthorizeClient(auth.Token);

        var response = await Client.GetAsync("/api/dvla/vehicles");

        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var vehicles = await response.Content.ReadFromJsonAsync<List<VehicleResponse>>();
        vehicles.Should().NotBeNull().And.BeEmpty();
    }

    [Fact]
    public async Task LookupVehicle_ReturnsMockedVehicleInfo()
    {
        var auth = await RegisterAndLoginAsync($"lookup_{Guid.NewGuid()}@test.com");
        AuthorizeClient(auth.Token);

        var response = await Client.GetAsync("/api/dvla/vehicles/lookup/AB12CDE");

        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var vehicle = await response.Content.ReadFromJsonAsync<VehicleResponse>();
        vehicle.Should().NotBeNull();
        vehicle!.RegistrationPlate.Should().Be("AB12CDE");
        vehicle.Make.Should().Be("BMW");
    }

    [Fact]
    public async Task AddVehicle_WithValidData_ReturnsSavedVehicle()
    {
        var auth = await RegisterAndLoginAsync($"addvehicle_{Guid.NewGuid()}@test.com");
        AuthorizeClient(auth.Token);
        var request = new AddVehicleRequest
        {
            RegistrationPlate = "XY22ZZZ",
            Make = "BMW",
            Model = "3 Series",
            Colour = "Black",
            EngineDescription = "2.0 Turbo",
            FuelType = "Petrol",
            MotUntil = "15/06/2026",
        };

        var response = await Client.PostAsJsonAsync("/api/dvla/vehicles", request);

        response.StatusCode.Should().Be(HttpStatusCode.Created);
        var vehicle = await response.Content.ReadFromJsonAsync<VehicleResponse>();
        vehicle!.RegistrationPlate.Should().Be("XY22ZZZ");
        vehicle.Make.Should().Be("BMW");
    }

    [Fact]
    public async Task GetPenaltyPoints_ReturnsDefaultPoints()
    {
        var auth = await RegisterAndLoginAsync($"points_{Guid.NewGuid()}@test.com");
        AuthorizeClient(auth.Token);

        var response = await Client.GetAsync("/api/dvla/penalty-points");

        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var points = await response.Content.ReadFromJsonAsync<PenaltyPointsResponse>();
        points!.MaxPoints.Should().Be(12);
    }

    [Fact]
    public async Task RenewLicence_ReturnsUpdatedValidUntil()
    {
        var auth = await RegisterAndLoginAsync($"renew_{Guid.NewGuid()}@test.com");
        AuthorizeClient(auth.Token);

        var response = await Client.PostAsync("/api/dvla/licence/renew", null);

        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var status = await response.Content.ReadFromJsonAsync<DriverLicenceStatusResponse>();
        status!.ValidUntil.Should().NotBeNullOrEmpty();
        var expectedYear = DateTime.UtcNow.AddYears(10).Year;
        status.ValidUntil.Should().EndWith(expectedYear.ToString());
    }
}
