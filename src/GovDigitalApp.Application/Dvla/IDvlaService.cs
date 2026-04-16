using GovDigitalApp.Application.Dvla.Requests;
using GovDigitalApp.Application.Dvla.Responses;

namespace GovDigitalApp.Application.Dvla;

public interface IDvlaService
{
    Task<IEnumerable<VehicleResponse>> GetVehiclesAsync(int userId);
    Task<VehicleResponse> LookupVehicleAsync(string registrationPlate);
    Task<VehicleResponse> AddVehicleAsync(int userId, AddVehicleRequest request);
    Task<DriverLicenceStatusResponse> GetDriverLicenceStatusAsync(int userId);
    Task<DriverLicenceStatusResponse> RenewDriverLicenceAsync(int userId);
    Task<PenaltyPointsResponse> GetPenaltyPointsAsync(int userId);
}
