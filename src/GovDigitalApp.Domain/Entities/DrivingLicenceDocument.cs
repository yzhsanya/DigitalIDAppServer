namespace GovDigitalApp.Domain.Entities;

public class DrivingLicenceDocument : Document
{
    public string LicenceNumber { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string DateOfBirth { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string IssuedDate { get; set; } = string.Empty;
    public string ExpiryDate { get; set; } = string.Empty;
    public string LicenceCategory { get; set; } = string.Empty;
    public bool IsAutomatic { get; set; }
    public bool IsManual { get; set; }
    public bool IsFull { get; set; }
}
