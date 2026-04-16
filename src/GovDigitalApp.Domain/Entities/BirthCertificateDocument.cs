namespace GovDigitalApp.Domain.Entities;

public class BirthCertificateDocument : Document
{
    public string FirstName { get; set; } = string.Empty;
    public string MiddleName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string DateOfBirth { get; set; } = string.Empty;
    public string PlaceOfBirth { get; set; } = string.Empty;
    public string CertificateNumber { get; set; } = string.Empty;
}
