using Qola.API.Security.Domain.Models;

namespace Qola.API.Security.Domain.Repositories;

public interface IManagerRepository
{
    Task<IEnumerable<Manager>> ListAsync();
    Task AddAsync(Manager category);
    Task<Manager> FindByIdAsync(int id);
    Task<Manager> FindByEmailAsync(string email);
    bool ExistsByEmail(string email);
    void Update(Manager manager);
    void Delete(Manager manager);
}