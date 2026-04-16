namespace GovDigitalApp.Domain.Entities;

public class TaxCodeDocument : Document
{
    public string TaxCode { get; set; } = string.Empty;
    public string TaxYear { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;
    public string NationalInsuranceNumber { get; set; } = string.Empty;
    public string EmployerName { get; set; } = string.Empty;
}
