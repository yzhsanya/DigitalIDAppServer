using GovDigitalApp.Application.Common;
using GovDigitalApp.Application.Identity;
using GovDigitalApp.Application.Identity.Responses;
using GovDigitalApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GovDigitalApp.Infrastructure.Services;

public class IdentityService : IIdentityService
{
    private readonly IAppDbContext _context;

    public IdentityService(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<IdentityProofResponse> GetIdentityProofAsync(int userId)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId)
            ?? throw new KeyNotFoundException("User not found.");

        var documents = await _context.Documents.Where(d => d.UserId == userId).ToListAsync();
        var passport = documents.OfType<PassportDocument>().FirstOrDefault();
        var taxCode = documents.OfType<TaxCodeDocument>().FirstOrDefault();
        var diploma = documents.OfType<DiplomaDocument>().FirstOrDefault();
        var drivingLicence = documents.OfType<DrivingLicenceDocument>().FirstOrDefault();

        var dob = passport?.DateOfBirth ?? "";
        var age = "";
        if (!string.IsNullOrEmpty(dob) && DateTime.TryParseExact(dob, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out var dobDate))
            age = (DateTime.UtcNow.Year - dobDate.Year - (DateTime.UtcNow.DayOfYear < dobDate.DayOfYear ? 1 : 0)).ToString();

        return new IdentityProofResponse
        {
            FullName = $"{user.FirstName} {user.LastName}",
            DateOfBirth = dob,
            Age = age,
            Address = drivingLicence?.Address ?? "",
            PassportNumber = passport?.PassportNumber ?? "",
            PassportValidUntil = passport?.ExpiryDate ?? "",
            Nationality = passport?.Nationality ?? "",
            NiNumber = taxCode?.NationalInsuranceNumber ?? "",
            Degree = diploma?.Degree ?? "",
            Subject = diploma?.Subject ?? "",
            Institution = diploma?.Institution ?? "",
            GraduationYear = diploma?.GraduationYear ?? "",
            Grade = diploma?.Grade ?? "",
            QualificationCertificateNumber = diploma?.CertificateNumber ?? "",
        };
    }

    public Task<string> GenerateWorkProofShareCodeAsync(int userId)
    {
        var code = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 8).ToUpperInvariant();
        return Task.FromResult(code);
    }
}
