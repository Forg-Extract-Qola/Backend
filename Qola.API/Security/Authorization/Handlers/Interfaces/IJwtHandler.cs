using Qola.API.Security.Domain.Models;

namespace Qola.API.Security.Authorization.Handlers.Interfaces;

public interface IJwtHandler
{
    string GenerateToken(Manager manager);
    int? ValidateToken(string token);
}