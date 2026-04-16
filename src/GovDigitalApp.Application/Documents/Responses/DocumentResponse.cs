using GovDigitalApp.Domain.Entities;

namespace GovDigitalApp.Application.Documents.Responses;

public class DocumentResponse
{
    public int Id { get; set; }
    public DocumentType DocumentType { get; set; }
    public long SortKey { get; set; }
    public DateTime CreatedAt { get; set; }
    public string? PassportNumber { get; set; }
    public string? FirstName { get; set; }
    public string? MiddleName { get; set; }
    public string? LastName { get; set; }
    public string? DateOfBirth { get; set; }
    public string? PlaceOfBirth { get; set; }
    public string? Sex { get; set; }
    public string? Nationality { get; set; }
    public string? IssuedDate { get; set; }
    public string? ExpiryDate { get; set; }
    public string? IssuingCountry { get; set; }
    public string? Mrz1 { get; set; }
    public string? Mrz2 { get; set; }
    public string? LicenceNumber { get; set; }
    public string? Address { get; set; }
    public string? LicenceCategory { get; set; }
    public bool? IsAutomatic { get; set; }
    public bool? IsManual { get; set; }
    public bool? IsFull { get; set; }
    public string? TaxCode { get; set; }
    public string? TaxYear { get; set; }
    public string? NationalInsuranceNumber { get; set; }
    public string? EmployerName { get; set; }
    public string? Degree { get; set; }
    public string? Subject { get; set; }
    public string? Institution { get; set; }
    public string? GraduationYear { get; set; }
    public string? Grade { get; set; }
    public string? CertificateNumber { get; set; }
    public string? NhsNumber { get; set; }
    public string? RegisteredGpSurgery { get; set; }
    public string? VisaType { get; set; }
    public string? ImmigrationStatus { get; set; }
    public string? PermitType { get; set; }
    public string? SpouseTwoFirstName { get; set; }
    public string? SpouseTwoLastName { get; set; }
}
