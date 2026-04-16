namespace GovDigitalApp.Application.Documents.Requests;

public class AddPassportRequest
{
    public string PassportNumber { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string MiddleName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string DateOfBirth { get; set; } = string.Empty;
    public string PlaceOfBirth { get; set; } = string.Empty;
    public string Sex { get; set; } = string.Empty;
    public string Nationality { get; set; } = string.Empty;
    public string IssuedDate { get; set; } = string.Empty;
    public string ExpiryDate { get; set; } = string.Empty;
    public string IssuingCountry { get; set; } = string.Empty;
    public string Mrz1 { get; set; } = string.Empty;
    public string Mrz2 { get; set; } = string.Empty;
}
