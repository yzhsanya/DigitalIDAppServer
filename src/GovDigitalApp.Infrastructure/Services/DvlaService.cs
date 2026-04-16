using GovDigitalApp.Application.Common;
using GovDigitalApp.Application.Dvla;
using GovDigitalApp.Application.Dvla.Requests;
using GovDigitalApp.Application.Dvla.Responses;
using GovDigitalApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GovDigitalApp.Infrastructure.Services;

public class DvlaService : IDvlaService
{
    private readonly IAppDbContext _context;

    public DvlaService(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<VehicleResponse>> GetVehiclesAsync(int userId)
    {
        var vehicles = await _context.Vehicles
            .Where(v => v.UserId == userId)
            .OrderByDescending(v => v.AddedAt)
            .ToListAsync();
        return vehicles.Select(MapVehicle);
    }

    public Task<VehicleResponse> LookupVehicleAsync(string registrationPlate)
    {
        var mocked = new VehicleResponse
        {
            RegistrationPlate = registrationPlate.ToUpperInvariant(),
            Make = "BMW",
            Model = "3 Series",
            Colour = "Black",
            EngineDescription = "2.0 Turbo",
            FuelType = "Petrol",
            MotUntil = "15/06/2026",
        };
        return Task.FromResult(mocked);
    }

    public async Task<VehicleResponse> AddVehicleAsync(int userId, AddVehicleRequest request)
    {
        var vehicle = new Vehicle
        {
            UserId = userId,
            RegistrationPlate = request.RegistrationPlate.ToUpperInvariant(),
            Make = request.Make,
            Model = request.Model,
            Colour = request.Colour,
            EngineDescription = request.EngineDescription,
            FuelType = request.FuelType,
            MotUntil = request.MotUntil,
            AddedAt = DateTime.UtcNow,
        };
        _context.Vehicles.Add(vehicle);
        await _context.SaveChangesAsync();
        return MapVehicle(vehicle);
    }

    public async Task<DriverLicenceStatusResponse> GetDriverLicenceStatusAsync(int userId)
    {
        var user = await _context.Users.Include(u => u.DriverLicenceInfo).FirstOrDefaultAsync(u => u.Id == userId)
            ?? throw new KeyNotFoundException("User not found.");
        var info = user.DriverLicenceInfo ?? new DriverLicenceInfo
        {
            LicenceNumber = "SMITH901010AB9CD",
            TransmissionType = "Auto & Manual",
            ValidFrom = "01/01/2020",
            ValidUntil = "01/01/2030",
        };
        return new DriverLicenceStatusResponse
        {
            FullName = $"{user.FirstName} {user.LastName}",
            LicenceNumber = info.LicenceNumber,
            TransmissionType = info.TransmissionType,
            ValidFrom = info.ValidFrom,
            ValidUntil = info.ValidUntil,
        };
    }

    public async Task<DriverLicenceStatusResponse> RenewDriverLicenceAsync(int userId)
    {
        var user = await _context.Users.Include(u => u.DriverLicenceInfo).FirstOrDefaultAsync(u => u.Id == userId)
            ?? throw new KeyNotFoundException("User not found.");

        var newValidUntil = DateTime.UtcNow.AddYears(10).ToString("dd/MM/yyyy");

        if (user.DriverLicenceInfo == null)
        {
            user.DriverLicenceInfo = new DriverLicenceInfo
            {
                UserId = userId,
                LicenceNumber = "SMITH901010AB9CD",
                TransmissionType = "Auto & Manual",
                ValidFrom = DateTime.UtcNow.ToString("dd/MM/yyyy"),
                ValidUntil = newValidUntil,
                UpdatedAt = DateTime.UtcNow,
            };
        }
        else
        {
            user.DriverLicenceInfo.ValidFrom = DateTime.UtcNow.ToString("dd/MM/yyyy");
            user.DriverLicenceInfo.ValidUntil = newValidUntil;
            user.DriverLicenceInfo.UpdatedAt = DateTime.UtcNow;
        }

        await _context.SaveChangesAsync();

        return new DriverLicenceStatusResponse
        {
            FullName = $"{user.FirstName} {user.LastName}",
            LicenceNumber = user.DriverLicenceInfo.LicenceNumber,
            TransmissionType = user.DriverLicenceInfo.TransmissionType,
            ValidFrom = user.DriverLicenceInfo.ValidFrom,
            ValidUntil = user.DriverLicenceInfo.ValidUntil,
        };
    }

    public async Task<PenaltyPointsResponse> GetPenaltyPointsAsync(int userId)
    {
        var info = await _context.DriverLicenceInfos.FirstOrDefaultAsync(d => d.UserId == userId);
        return new PenaltyPointsResponse
        {
            Points = info?.PenaltyPoints ?? 2,
            MaxPoints = info?.MaxPenaltyPoints ?? 12,
        };
    }

    private static VehicleResponse MapVehicle(Vehicle v) => new()
    {
        Id = v.Id,
        RegistrationPlate = v.RegistrationPlate,
        Make = v.Make,
        Model = v.Model,
        Colour = v.Colour,
        EngineDescription = v.EngineDescription,
        FuelType = v.FuelType,
        MotUntil = v.MotUntil,
    };
}
