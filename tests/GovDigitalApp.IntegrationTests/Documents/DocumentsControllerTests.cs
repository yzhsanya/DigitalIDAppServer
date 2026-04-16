using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using GovDigitalApp.Application.Documents.Requests;
using GovDigitalApp.Application.Documents.Responses;
using GovDigitalApp.Domain.Entities;
using GovDigitalApp.IntegrationTests.Common;

namespace GovDigitalApp.IntegrationTests.Documents;

public class DocumentsControllerTests : IntegrationTestBase
{
    public DocumentsControllerTests(TestWebAppFactory factory) : base(factory) { }

    [Fact]
    public async Task GetDocuments_WhenUnauthenticated_ReturnsUnauthorized()
    {
        var response = await Client.GetAsync("/api/documents");

        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task GetDocuments_WhenAuthenticated_ReturnsEmptyList()
    {
        var auth = await RegisterAndLoginAsync($"docs_{Guid.NewGuid()}@test.com");
        AuthorizeClient(auth.Token);

        var response = await Client.GetAsync("/api/documents");

        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var documents = await response.Content.ReadFromJsonAsync<List<DocumentResponse>>();
        documents.Should().NotBeNull().And.BeEmpty();
    }

    [Fact]
    public async Task AddPassport_WithValidData_ReturnsCreatedDocument()
    {
        var auth = await RegisterAndLoginAsync($"passport_{Guid.NewGuid()}@test.com");
        AuthorizeClient(auth.Token);
        var request = new AddPassportRequest
        {
            PassportNumber = "123456789",
            FirstName = "John",
            MiddleName = "Michael",
            LastName = "Smith",
            DateOfBirth = "01/01/1990",
            PlaceOfBirth = "London",
            Sex = "M",
            Nationality = "GBR",
            IssuedDate = "01/01/2020",
            ExpiryDate = "01/01/2030",
            IssuingCountry = "United Kingdom",
            Mrz1 = "P<GBRSMI<<JOHN<MICHAEL<<<<<<<<<<<<<<<<<<<<<",
            Mrz2 = "1234567892GBR9001011M3001019<<<<<<<<<<<<<<04",
        };

        var response = await Client.PostAsJsonAsync("/api/documents/passport", request);

        response.StatusCode.Should().Be(HttpStatusCode.Created);
        var doc = await response.Content.ReadFromJsonAsync<DocumentResponse>();
        doc.Should().NotBeNull();
        doc!.DocumentType.Should().Be(DocumentType.Passport);
        doc.PassportNumber.Should().Be(request.PassportNumber);
        doc.FirstName.Should().Be(request.FirstName);
    }

    [Fact]
    public async Task AddDiploma_WithValidData_ReturnsCreatedDocument()
    {
        var auth = await RegisterAndLoginAsync($"diploma_{Guid.NewGuid()}@test.com");
        AuthorizeClient(auth.Token);
        var request = new AddDiplomaRequest
        {
            FirstName = "John",
            MiddleName = "",
            LastName = "Smith",
            Degree = "Bachelor of Science",
            Subject = "Computer Science",
            Institution = "University of Chester",
            GraduationYear = "2022",
            Grade = "First Class Honours",
            CertificateNumber = "UC-2022-CS-00742",
        };

        var response = await Client.PostAsJsonAsync("/api/documents/diploma", request);

        response.StatusCode.Should().Be(HttpStatusCode.Created);
        var doc = await response.Content.ReadFromJsonAsync<DocumentResponse>();
        doc!.DocumentType.Should().Be(DocumentType.Diploma);
        doc.Degree.Should().Be("Bachelor of Science");
        doc.Institution.Should().Be("University of Chester");
    }

    [Fact]
    public async Task DeleteDocument_WithExistingId_ReturnsNoContent()
    {
        var auth = await RegisterAndLoginAsync($"delete_{Guid.NewGuid()}@test.com");
        AuthorizeClient(auth.Token);
        var addResponse = await Client.PostAsJsonAsync("/api/documents/diploma", new AddDiplomaRequest
        {
            FirstName = "A", LastName = "B", Degree = "BSc", Subject = "CS",
            Institution = "UoC", GraduationYear = "2022", Grade = "First", CertificateNumber = "X1",
        });
        var added = await addResponse.Content.ReadFromJsonAsync<DocumentResponse>();

        var deleteResponse = await Client.DeleteAsync($"/api/documents/{added!.Id}");

        deleteResponse.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }
}
