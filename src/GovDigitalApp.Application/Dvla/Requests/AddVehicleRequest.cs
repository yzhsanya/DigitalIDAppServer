namespace GovDigitalApp.Application.Dvla.Requests;

public class AddVehicleRequest
{
    public string RegistrationPlate { get; set; } = string.Empty;
    public string Make { get; set; } = string.Empty;
    public string Model { get; set; } = string.Empty;
    public string Colour { get; set; } = string.Empty;
    public string EngineDescription { get; set; } = string.Empty;
    public string FuelType { get; set; } = string.Empty;
    public string MotUntil { get; set; } = string.Empty;
}
