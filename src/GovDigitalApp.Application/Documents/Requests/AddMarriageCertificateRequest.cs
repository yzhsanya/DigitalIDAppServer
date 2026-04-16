namespace GovDigitalApp.Application.Documents.Requests;

public class AddMarriageCertificateRequest
{
    public string SpouseOneFirstName { get; set; } = string.Empty;
    public string SpouseOneLastName { get; set; } = string.Empty;
    public string SpouseTwoFirstName { get; set; } = string.Empty;
    public string SpouseTwoLastName { get; set; } = string.Empty;
    public string CertificateNumber { get; set; } = string.Empty;
    public string DateOfMarriage { get; set; } = string.Empty;
    public string PlaceOfMarriage { get; set; } = string.Empty;
    public string IssuingCountry { get; set; } = string.Empty;
}
