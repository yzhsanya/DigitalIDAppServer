using GovDigitalApp.Domain.Entities;

namespace GovDigitalApp.Application.Common;

public interface IJwtService
{
    string GenerateToken(User user);
}
