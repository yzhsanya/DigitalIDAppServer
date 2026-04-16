namespace GovDigitalApp.Application.Documents.Requests;

public class AddResidentPermitRequest
{
    public string FirstName { get; set; } = string.Empty;
    public string MiddleName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string PermitNumber { get; set; } = string.Empty;
    public string DateOfBirth { get; set; } = string.Empty;
    public string Nationality { get; set; } = string.Empty;
    public string PermitType { get; set; } = string.Empty;
    public string IssuedDate { get; set; } = string.Empty;
    public string ExpiryDate { get; set; } = string.Empty;
    public string IssuingCountry { get; set; } = string.Empty;
}
