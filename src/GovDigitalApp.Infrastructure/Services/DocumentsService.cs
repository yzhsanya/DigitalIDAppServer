using GovDigitalApp.Application.Common;
using GovDigitalApp.Application.Documents;
using GovDigitalApp.Application.Documents.Requests;
using GovDigitalApp.Application.Documents.Responses;
using GovDigitalApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GovDigitalApp.Infrastructure.Services;

public class DocumentsService : IDocumentsService
{
    private readonly IAppDbContext _context;

    public DocumentsService(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<DocumentResponse>> GetDocumentsAsync(int userId)
    {
        var documents = await _context.Documents
            .Where(d => d.UserId == userId)
            .OrderBy(d => d.SortKey)
            .ToListAsync();
        return documents.Select(MapToResponse);
    }

    public async Task<DocumentResponse> AddPassportAsync(int userId, AddPassportRequest request)
    {
        var doc = new PassportDocument
        {
            UserId = userId,
            DocumentType = DocumentType.Passport,
            SortKey = DateTimeOffset.UtcNow.ToUnixTimeSeconds(),
            CreatedAt = DateTime.UtcNow,
            PassportNumber = request.PassportNumber,
            FirstName = request.FirstName,
            MiddleName = request.MiddleName,
            LastName = request.LastName,
            DateOfBirth = request.DateOfBirth,
            PlaceOfBirth = request.PlaceOfBirth,
            Sex = request.Sex,
            Nationality = request.Nationality,
            IssuedDate = request.IssuedDate,
            ExpiryDate = request.ExpiryDate,
            IssuingCountry = request.IssuingCountry,
            Mrz1 = request.Mrz1,
            Mrz2 = request.Mrz2,
        };
        _context.Documents.Add(doc);
        await _context.SaveChangesAsync();
        return MapToResponse(doc);
    }

    public async Task<DocumentResponse> AddDrivingLicenceAsync(int userId, AddDrivingLicenceRequest request)
    {
        var doc = new DrivingLicenceDocument
        {
            UserId = userId,
            DocumentType = DocumentType.DrivingLicence,
            SortKey = DateTimeOffset.UtcNow.ToUnixTimeSeconds(),
            CreatedAt = DateTime.UtcNow,
            LicenceNumber = request.LicenceNumber,
            FirstName = request.FirstName,
            LastName = request.LastName,
            DateOfBirth = request.DateOfBirth,
            Address = request.Address,
            IssuedDate = request.IssuedDate,
            ExpiryDate = request.ExpiryDate,
            LicenceCategory = request.LicenceCategory,
            IsAutomatic = request.IsAutomatic,
            IsManual = request.IsManual,
            IsFull = request.IsFull,
        };
        _context.Documents.Add(doc);
        await _context.SaveChangesAsync();
        return MapToResponse(doc);
    }

    public async Task<DocumentResponse> AddDiplomaAsync(int userId, AddDiplomaRequest request)
    {
        var doc = new DiplomaDocument
        {
            UserId = userId,
            DocumentType = DocumentType.Diploma,
            SortKey = DateTimeOffset.UtcNow.ToUnixTimeSeconds(),
            CreatedAt = DateTime.UtcNow,
            FirstName = request.FirstName,
            MiddleName = request.MiddleName,
            LastName = request.LastName,
            Degree = request.Degree,
            Subject = request.Subject,
            Institution = request.Institution,
            GraduationYear = request.GraduationYear,
            Grade = request.Grade,
            CertificateNumber = request.CertificateNumber,
        };
        _context.Documents.Add(doc);
        await _context.SaveChangesAsync();
        return MapToResponse(doc);
    }

    public async Task<DocumentResponse> AddBirthCertificateAsync(int userId, AddBirthCertificateRequest request)
    {
        var doc = new BirthCertificateDocument
        {
            UserId = userId,
            DocumentType = DocumentType.BirthCertificate,
            SortKey = DateTimeOffset.UtcNow.ToUnixTimeSeconds(),
            CreatedAt = DateTime.UtcNow,
            FirstName = request.FirstName,
            MiddleName = request.MiddleName,
            LastName = request.LastName,
            DateOfBirth = request.DateOfBirth,
            PlaceOfBirth = request.PlaceOfBirth,
            CertificateNumber = request.CertificateNumber,
        };
        _context.Documents.Add(doc);
        await _context.SaveChangesAsync();
        return MapToResponse(doc);
    }

    public async Task<DocumentResponse> AddEVisaAsync(int userId, AddEVisaRequest request)
    {
        var doc = new EVisaDocument
        {
            UserId = userId,
            DocumentType = DocumentType.EVisa,
            SortKey = DateTimeOffset.UtcNow.ToUnixTimeSeconds(),
            CreatedAt = DateTime.UtcNow,
            FirstName = request.FirstName,
            MiddleName = request.MiddleName,
            LastName = request.LastName,
            VisaType = request.VisaType,
            ImmigrationStatus = request.ImmigrationStatus,
            ExpiryDate = request.ExpiryDate,
            IssuingCountry = request.IssuingCountry,
        };
        _context.Documents.Add(doc);
        await _context.SaveChangesAsync();
        return MapToResponse(doc);
    }

    public async Task<DocumentResponse> AddPassportToTravelAbroadAsync(int userId, AddPassportToTravelAbroadRequest request)
    {
        var doc = new PassportToTravelAbroadDocument
        {
            UserId = userId,
            DocumentType = DocumentType.PassportToTravelAbroad,
            SortKey = DateTimeOffset.UtcNow.ToUnixTimeSeconds(),
            CreatedAt = DateTime.UtcNow,
            FirstName = request.FirstName,
            MiddleName = request.MiddleName,
            LastName = request.LastName,
            PassportNumber = request.PassportNumber,
            Nationality = request.Nationality,
            DateOfBirth = request.DateOfBirth,
            PlaceOfBirth = request.PlaceOfBirth,
            IssuedDate = request.IssuedDate,
            ExpiryDate = request.ExpiryDate,
            IssuingCountry = request.IssuingCountry,
        };
        _context.Documents.Add(doc);
        await _context.SaveChangesAsync();
        return MapToResponse(doc);
    }

    public async Task<DocumentResponse> AddResidentPermitAsync(int userId, AddResidentPermitRequest request)
    {
        var doc = new ResidentPermitDocument
        {
            UserId = userId,
            DocumentType = DocumentType.ResidentPermit,
            SortKey = DateTimeOffset.UtcNow.ToUnixTimeSeconds(),
            CreatedAt = DateTime.UtcNow,
            FirstName = request.FirstName,
            MiddleName = request.MiddleName,
            LastName = request.LastName,
            LicenceNumber = request.PermitNumber,
            DateOfBirth = request.DateOfBirth,
            Nationality = request.Nationality,
            PermitType = request.PermitType,
            IssuedDate = request.IssuedDate,
            ExpiryDate = request.ExpiryDate,
            IssuingCountry = request.IssuingCountry,
        };
        _context.Documents.Add(doc);
        await _context.SaveChangesAsync();
        return MapToResponse(doc);
    }

    public async Task<DocumentResponse> AddMarriageCertificateAsync(int userId, AddMarriageCertificateRequest request)
    {
        var doc = new MarriageCertificateDocument
        {
            UserId = userId,
            DocumentType = DocumentType.MarriageCertificate,
            SortKey = DateTimeOffset.UtcNow.ToUnixTimeSeconds(),
            CreatedAt = DateTime.UtcNow,
            FirstName = request.SpouseOneFirstName,
            LastName = request.SpouseOneLastName,
            SpouseTwoFirstName = request.SpouseTwoFirstName,
            SpouseTwoLastName = request.SpouseTwoLastName,
            CertificateNumber = request.CertificateNumber,
            IssuedDate = request.DateOfMarriage,
            PlaceOfBirth = request.PlaceOfMarriage,
            IssuingCountry = request.IssuingCountry,
        };
        _context.Documents.Add(doc);
        await _context.SaveChangesAsync();
        return MapToResponse(doc);
    }

    public async Task DeleteDocumentAsync(int userId, int documentId)
    {
        var doc = await _context.Documents.FirstOrDefaultAsync(d => d.Id == documentId && d.UserId == userId)
            ?? throw new KeyNotFoundException("Document not found.");
        _context.Documents.Remove(doc);
        await _context.SaveChangesAsync();
    }

    public async Task ReorderDocumentsAsync(int userId, IEnumerable<int> orderedDocumentIds)
    {
        var ids = orderedDocumentIds.ToList();
        var orders = await _context.DocumentOrders
            .Where(o => o.UserId == userId)
            .ToListAsync();

        foreach (var order in orders)
            _context.DocumentOrders.Remove(order);

        for (int i = 0; i < ids.Count; i++)
        {
            _context.DocumentOrders.Add(new DocumentOrder
            {
                UserId = userId,
                DocumentId = ids[i],
                Position = i,
            });
        }
        await _context.SaveChangesAsync();
    }

    private static DocumentResponse MapToResponse(Document doc)
    {
        var response = new DocumentResponse
        {
            Id = doc.Id,
            DocumentType = doc.DocumentType,
            SortKey = doc.SortKey,
            CreatedAt = doc.CreatedAt,
        };
        switch (doc)
        {
            case PassportDocument p:
                response.PassportNumber = p.PassportNumber;
                response.FirstName = p.FirstName;
                response.MiddleName = p.MiddleName;
                response.LastName = p.LastName;
                response.DateOfBirth = p.DateOfBirth;
                response.PlaceOfBirth = p.PlaceOfBirth;
                response.Sex = p.Sex;
                response.Nationality = p.Nationality;
                response.IssuedDate = p.IssuedDate;
                response.ExpiryDate = p.ExpiryDate;
                response.IssuingCountry = p.IssuingCountry;
                response.Mrz1 = p.Mrz1;
                response.Mrz2 = p.Mrz2;
                break;
            case DrivingLicenceDocument dl:
                response.FirstName = dl.FirstName;
                response.LastName = dl.LastName;
                response.LicenceNumber = dl.LicenceNumber;
                response.DateOfBirth = dl.DateOfBirth;
                response.Address = dl.Address;
                response.IssuedDate = dl.IssuedDate;
                response.ExpiryDate = dl.ExpiryDate;
                response.LicenceCategory = dl.LicenceCategory;
                response.IsAutomatic = dl.IsAutomatic;
                response.IsManual = dl.IsManual;
                response.IsFull = dl.IsFull;
                break;
            case TaxCodeDocument tc:
                response.TaxCode = tc.TaxCode;
                response.TaxYear = tc.TaxYear;
                response.FirstName = tc.FullName;
                response.NationalInsuranceNumber = tc.NationalInsuranceNumber;
                response.EmployerName = tc.EmployerName;
                break;
            case DiplomaDocument d:
                response.FirstName = d.FirstName;
                response.MiddleName = d.MiddleName;
                response.LastName = d.LastName;
                response.Degree = d.Degree;
                response.Subject = d.Subject;
                response.Institution = d.Institution;
                response.GraduationYear = d.GraduationYear;
                response.Grade = d.Grade;
                response.CertificateNumber = d.CertificateNumber;
                break;
            case BirthCertificateDocument bc:
                response.FirstName = bc.FirstName;
                response.MiddleName = bc.MiddleName;
                response.LastName = bc.LastName;
                response.DateOfBirth = bc.DateOfBirth;
                response.PlaceOfBirth = bc.PlaceOfBirth;
                response.CertificateNumber = bc.CertificateNumber;
                break;
            case EVisaDocument ev:
                response.FirstName = ev.FirstName;
                response.MiddleName = ev.MiddleName;
                response.LastName = ev.LastName;
                response.VisaType = ev.VisaType;
                response.ImmigrationStatus = ev.ImmigrationStatus;
                response.ExpiryDate = ev.ExpiryDate;
                response.IssuingCountry = ev.IssuingCountry;
                break;
            case PassportToTravelAbroadDocument pta:
                response.FirstName = pta.FirstName;
                response.MiddleName = pta.MiddleName;
                response.LastName = pta.LastName;
                response.PassportNumber = pta.PassportNumber;
                response.Nationality = pta.Nationality;
                response.DateOfBirth = pta.DateOfBirth;
                response.PlaceOfBirth = pta.PlaceOfBirth;
                response.IssuedDate = pta.IssuedDate;
                response.ExpiryDate = pta.ExpiryDate;
                response.IssuingCountry = pta.IssuingCountry;
                break;
            case ResidentPermitDocument rp:
                response.FirstName = rp.FirstName;
                response.MiddleName = rp.MiddleName;
                response.LastName = rp.LastName;
                response.LicenceNumber = rp.LicenceNumber;
                response.DateOfBirth = rp.DateOfBirth;
                response.Nationality = rp.Nationality;
                response.PermitType = rp.PermitType;
                response.IssuedDate = rp.IssuedDate;
                response.ExpiryDate = rp.ExpiryDate;
                response.IssuingCountry = rp.IssuingCountry;
                break;
            case MarriageCertificateDocument mc:
                response.FirstName = mc.FirstName;
                response.LastName = mc.LastName;
                response.SpouseTwoFirstName = mc.SpouseTwoFirstName;
                response.SpouseTwoLastName = mc.SpouseTwoLastName;
                response.CertificateNumber = mc.CertificateNumber;
                response.IssuedDate = mc.IssuedDate;
                response.PlaceOfBirth = mc.PlaceOfBirth;
                response.IssuingCountry = mc.IssuingCountry;
                break;
        }
        return response;
    }
}
