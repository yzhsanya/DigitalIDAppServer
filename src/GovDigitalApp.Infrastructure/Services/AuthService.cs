using GovDigitalApp.Application.Auth;
using GovDigitalApp.Application.Auth.Requests;
using GovDigitalApp.Application.Auth.Responses;
using GovDigitalApp.Application.Common;
using GovDigitalApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GovDigitalApp.Infrastructure.Services;

public class AuthService : IAuthService
{
    private readonly IAppDbContext _context;
    private readonly IJwtService _jwtService;

    public AuthService(IAppDbContext context, IJwtService jwtService)
    {
        _context = context;
        _jwtService = jwtService;
    }

    public async Task<AuthResponse> RegisterAsync(RegisterRequest request)
    {
        var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == request.Email);
        if (existingUser != null)
            throw new InvalidOperationException("User with this email already exists.");

        var user = new User
        {
            Email = request.Email.ToLowerInvariant(),
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
            FirstName = request.FirstName,
            LastName = request.LastName,
            CreatedAt = DateTime.UtcNow,
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        await SeedUserDataAsync(user);

        return new AuthResponse
        {
            UserId = user.Id,
            Email = user.Email,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Token = _jwtService.GenerateToken(user),
        };
    }

    public async Task<AuthResponse> LoginAsync(LoginRequest request)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == request.Email.ToLowerInvariant())
            ?? throw new UnauthorizedAccessException("Invalid email or password.");

        if (!BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
            throw new UnauthorizedAccessException("Invalid email or password.");

        return new AuthResponse
        {
            UserId = user.Id,
            Email = user.Email,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Token = _jwtService.GenerateToken(user),
        };
    }

    private async Task SeedUserDataAsync(User user)
    {
        var now = DateTime.UtcNow;
        long sortKey = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        var niNumber = GenerateNiNumber();
        var passportNumber = GeneratePassportNumber();
        var passportAbroadNumber = GeneratePassportNumber();
        var licenceNumber = GenerateLicenceNumber(user.LastName);
        var diplomaCertNumber = $"UC-2022-CS-{Random.Shared.Next(10000, 99999)}";

        var passport = new PassportDocument
        {
            UserId = user.Id,
            DocumentType = DocumentType.Passport,
            SortKey = sortKey,
            CreatedAt = now,
            PassportNumber = passportNumber,
            FirstName = user.FirstName,
            MiddleName = string.Empty,
            LastName = user.LastName,
            DateOfBirth = "01/01/1990",
            PlaceOfBirth = "London, United Kingdom",
            Sex = "M",
            Nationality = "GBR",
            IssuedDate = "01/06/2020",
            ExpiryDate = "01/06/2030",
            IssuingCountry = "United Kingdom",
            Mrz1 = $"P<GBR{user.LastName.ToUpperInvariant()}<<{user.FirstName.ToUpperInvariant()}<<<<<<<<<<<<<<<<<<",
            Mrz2 = $"{passportNumber}2GBR9001011M3006019<<<<<<<<<<<<<<04",
        };

        var drivingLicence = new DrivingLicenceDocument
        {
            UserId = user.Id,
            DocumentType = DocumentType.DrivingLicence,
            SortKey = sortKey + 1,
            CreatedAt = now,
            LicenceNumber = licenceNumber,
            FirstName = user.FirstName,
            LastName = user.LastName,
            DateOfBirth = "01/01/1990",
            Address = "123 Example Street, London, SW1A 1AA",
            IssuedDate = "01/01/2020",
            ExpiryDate = "01/01/2030",
            LicenceCategory = "B",
            IsAutomatic = true,
            IsManual = true,
            IsFull = true,
        };

        var nationalInsurance = new TaxCodeDocument
        {
            UserId = user.Id,
            DocumentType = DocumentType.NationalInsurance,
            SortKey = sortKey + 2,
            CreatedAt = now,
            TaxCode = "1257L",
            TaxYear = "2024/2025",
            FullName = $"{user.FirstName} {user.LastName}",
            NationalInsuranceNumber = niNumber,
            EmployerName = "HM Revenue & Customs",
        };

        var diploma = new DiplomaDocument
        {
            UserId = user.Id,
            DocumentType = DocumentType.Diploma,
            SortKey = sortKey + 3,
            CreatedAt = now,
            FirstName = user.FirstName,
            MiddleName = string.Empty,
            LastName = user.LastName,
            Degree = "Bachelor of Science",
            Subject = "Computer Science",
            Institution = "University of Chester",
            GraduationYear = "2022",
            Grade = "First Class Honours",
            CertificateNumber = diplomaCertNumber,
        };

        var birthCertificate = new BirthCertificateDocument
        {
            UserId = user.Id,
            DocumentType = DocumentType.BirthCertificate,
            SortKey = sortKey + 4,
            CreatedAt = now,
            FirstName = user.FirstName,
            MiddleName = string.Empty,
            LastName = user.LastName,
            DateOfBirth = "01/01/1990",
            PlaceOfBirth = "London, United Kingdom",
            CertificateNumber = $"BXEC{Random.Shared.Next(100000, 999999)}",
        };

        var evisa = new EVisaDocument
        {
            UserId = user.Id,
            DocumentType = DocumentType.EVisa,
            SortKey = sortKey + 5,
            CreatedAt = now,
            FirstName = user.FirstName,
            MiddleName = string.Empty,
            LastName = user.LastName,
            VisaType = "Skilled Worker",
            ImmigrationStatus = "Leave to Remain",
            ExpiryDate = "01/06/2027",
            IssuingCountry = "United Kingdom",
        };

        var travelPassport = new PassportToTravelAbroadDocument
        {
            UserId = user.Id,
            DocumentType = DocumentType.PassportToTravelAbroad,
            SortKey = sortKey + 6,
            CreatedAt = now,
            FirstName = user.FirstName,
            MiddleName = string.Empty,
            LastName = user.LastName,
            PassportNumber = passportAbroadNumber,
            Nationality = "GBR",
            DateOfBirth = "01/01/1990",
            PlaceOfBirth = "London, United Kingdom",
            IssuedDate = "01/06/2020",
            ExpiryDate = "01/06/2030",
            IssuingCountry = "United Kingdom",
        };

        var residentPermit = new ResidentPermitDocument
        {
            UserId = user.Id,
            DocumentType = DocumentType.ResidentPermit,
            SortKey = sortKey + 7,
            CreatedAt = now,
            FirstName = user.FirstName,
            MiddleName = string.Empty,
            LastName = user.LastName,
            LicenceNumber = $"RP{Random.Shared.Next(100000, 999999)}",
            DateOfBirth = "01/01/1990",
            Nationality = "GBR",
            PermitType = "Indefinite Leave to Remain",
            IssuedDate = "01/01/2021",
            ExpiryDate = "01/01/2031",
            IssuingCountry = "United Kingdom",
        };

        var marriageCertificate = new MarriageCertificateDocument
        {
            UserId = user.Id,
            DocumentType = DocumentType.MarriageCertificate,
            SortKey = sortKey + 8,
            CreatedAt = now,
            FirstName = user.FirstName,
            LastName = user.LastName,
            SpouseTwoFirstName = "Jane",
            SpouseTwoLastName = "Smith",
            CertificateNumber = $"MC{Random.Shared.Next(100000, 999999)}",
            IssuedDate = "15/06/2019",
            PlaceOfBirth = "London, United Kingdom",
            IssuingCountry = "United Kingdom",
        };

        _context.Documents.Add(passport);
        _context.Documents.Add(drivingLicence);
        _context.Documents.Add(nationalInsurance);
        _context.Documents.Add(diploma);
        _context.Documents.Add(birthCertificate);
        _context.Documents.Add(evisa);
        _context.Documents.Add(travelPassport);
        _context.Documents.Add(residentPermit);
        _context.Documents.Add(marriageCertificate);

        var driverLicenceInfo = new DriverLicenceInfo
        {
            UserId = user.Id,
            LicenceNumber = licenceNumber,
            TransmissionType = "Auto & Manual",
            ValidFrom = "01/01/2020",
            ValidUntil = "01/01/2030",
            PenaltyPoints = 2,
            MaxPenaltyPoints = 12,
            UpdatedAt = now,
        };

        _context.DriverLicenceInfos.Add(driverLicenceInfo);

        var vehicle = new Vehicle
        {
            UserId = user.Id,
            RegistrationPlate = "AB" + Random.Shared.Next(10, 99) + "CDE",
            Make = "BMW",
            Model = "3 Series",
            Colour = "Black",
            EngineDescription = "2.0 Turbo",
            FuelType = "Petrol",
            MotUntil = "15/06/2026",
            AddedAt = now,
        };

        _context.Vehicles.Add(vehicle);

        await _context.SaveChangesAsync();
    }

    private static string GenerateNiNumber()
    {
        var letters = "ABCEGHJKLMNOPRSTWXYZ";
        var r = Random.Shared;
        return $"{letters[r.Next(letters.Length)]}{letters[r.Next(letters.Length)]} " +
               $"{r.Next(10, 99)} {r.Next(10, 99)} {r.Next(10, 99)} " +
               $"{(char)('A' + r.Next(4))}";
    }

    private static string GeneratePassportNumber()
    {
        return Random.Shared.Next(100000000, 999999999).ToString();
    }

    private static string GenerateLicenceNumber(string lastName)
    {
        var prefix = (lastName.Length >= 5 ? lastName[..5] : lastName.PadRight(5, '9')).ToUpperInvariant();
        var r = Random.Shared;
        return $"{prefix}{r.Next(90, 99)}{r.Next(10, 99)}{r.Next(10, 99)}AB9CD";
    }

}
