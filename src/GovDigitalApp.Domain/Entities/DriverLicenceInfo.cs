namespace GovDigitalApp.Domain.Entities;

public class DriverLicenceInfo
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public User User { get; set; } = null!;
    public string LicenceNumber { get; set; } = string.Empty;
    public string TransmissionType { get; set; } = string.Empty;
    public string ValidFrom { get; set; } = string.Empty;
    public string ValidUntil { get; set; } = string.Empty;
    public int PenaltyPoints { get; set; }
    public int MaxPenaltyPoints { get; set; } = 12;
    public DateTime UpdatedAt { get; set; }
}
