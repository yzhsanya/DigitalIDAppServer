using GovDigitalApp.Application.Identity.Responses;

namespace GovDigitalApp.Application.Identity;

public interface IIdentityService
{
    Task<IdentityProofResponse> GetIdentityProofAsync(int userId);
    Task<string> GenerateWorkProofShareCodeAsync(int userId);
}
