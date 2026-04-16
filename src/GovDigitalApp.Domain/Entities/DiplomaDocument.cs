namespace GovDigitalApp.Domain.Entities;

public class DiplomaDocument : Document
{
    public string FirstName { get; set; } = string.Empty;
    public string MiddleName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Degree { get; set; } = string.Empty;
    public string Subject { get; set; } = string.Empty;
    public string Institution { get; set; } = string.Empty;
    public string GraduationYear { get; set; } = string.Empty;
    public string Grade { get; set; } = string.Empty;
    public string CertificateNumber { get; set; } = string.Empty;
}
