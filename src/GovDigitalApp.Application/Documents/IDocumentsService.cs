using GovDigitalApp.Application.Documents.Requests;
using GovDigitalApp.Application.Documents.Responses;

namespace GovDigitalApp.Application.Documents;

public interface IDocumentsService
{
    Task<IEnumerable<DocumentResponse>> GetDocumentsAsync(int userId);
    Task<DocumentResponse> AddPassportAsync(int userId, AddPassportRequest request);
    Task<DocumentResponse> AddDrivingLicenceAsync(int userId, AddDrivingLicenceRequest request);
    Task<DocumentResponse> AddDiplomaAsync(int userId, AddDiplomaRequest request);
    Task<DocumentResponse> AddBirthCertificateAsync(int userId, AddBirthCertificateRequest request);
    Task<DocumentResponse> AddEVisaAsync(int userId, AddEVisaRequest request);
    Task<DocumentResponse> AddPassportToTravelAbroadAsync(int userId, AddPassportToTravelAbroadRequest request);
    Task<DocumentResponse> AddResidentPermitAsync(int userId, AddResidentPermitRequest request);
    Task<DocumentResponse> AddMarriageCertificateAsync(int userId, AddMarriageCertificateRequest request);
    Task DeleteDocumentAsync(int userId, int documentId);
    Task ReorderDocumentsAsync(int userId, IEnumerable<int> orderedDocumentIds);
}
