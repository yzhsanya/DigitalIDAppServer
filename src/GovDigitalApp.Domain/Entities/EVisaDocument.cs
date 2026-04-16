namespace GovDigitalApp.Domain.Entities;

public class EVisaDocument : Document
{
    public string FirstName { get; set; } = string.Empty;
    public string MiddleName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string VisaType { get; set; } = string.Empty;
    public string ImmigrationStatus { get; set; } = string.Empty;
    public string ExpiryDate { get; set; } = string.Empty;
    public string IssuingCountry { get; set; } = string.Empty;
}
