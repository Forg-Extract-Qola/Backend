using Qola.API.Security.Domain.Models;
using Qola.API.Security.Domain.Services.Communication;

namespace Qola.API.Security.Domain.Services;

public interface IManagerService
{
    Task<AuthenticateResponse> AuthenticateAsync(AuthenticateRequest request);
    Task<IEnumerable<Manager>> ListAsync();
    Task<Manager> FindByIdAsync(int id);
    Task RegisterAsync(RegisterRequest request);
    Task UpdateAsync(int id, UpdateRequest request);
    Task DeleteAsync(int id);
}