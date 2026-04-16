namespace GovDigitalApp.Application.Dvla.Responses;

public class DriverLicenceStatusResponse
{
    public string FullName { get; set; } = string.Empty;
    public string LicenceNumber { get; set; } = string.Empty;
    public string TransmissionType { get; set; } = string.Empty;
    public string ValidFrom { get; set; } = string.Empty;
    public string ValidUntil { get; set; } = string.Empty;
}
