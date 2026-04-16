namespace GovDigitalApp.Domain.Entities;

public class MarriageCertificateDocument : Document
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string SpouseTwoFirstName { get; set; } = string.Empty;
    public string SpouseTwoLastName { get; set; } = string.Empty;
    public string CertificateNumber { get; set; } = string.Empty;
    public string IssuedDate { get; set; } = string.Empty;
    public string PlaceOfBirth { get; set; } = string.Empty;
    public string IssuingCountry { get; set; } = string.Empty;
}
