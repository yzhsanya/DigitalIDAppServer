using GovDigitalApp.Application.Auth.Requests;
using GovDigitalApp.Application.Auth.Responses;

namespace GovDigitalApp.Application.Auth;

public interface IAuthService
{
    Task<AuthResponse> RegisterAsync(RegisterRequest request);
    Task<AuthResponse> LoginAsync(LoginRequest request);
}
