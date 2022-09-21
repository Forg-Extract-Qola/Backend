using Microsoft.EntityFrameworkCore;
using Qola.API.Security.Domain.Models;
using Qola.API.Security.Domain.Repositories;
using Qola.API.Shared.Persistence.Contexts;
using Qola.API.Shared.Persistence.Repositories;

namespace Qola.API.Security.Persistence.Repositories;

public class ManagerRepository: BaseRepository, IManagerRepository
{
    public ManagerRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Manager>> ListAsync()
    {
        return await _context.Managers.ToListAsync();
    }

    public async Task AddAsync(Manager category)
    {
        await _context.Managers.AddAsync(category);
    }

    public async Task<Manager> FindByIdAsync(int id)
    {
        return (await _context.Managers.FindAsync(id))!;
    }

    public async Task<Manager> FindByEmailAsync(string email)
    {
        return (await _context.Managers.FirstOrDefaultAsync(x => x.Email == email))!;
    }

    public bool ExistsByEmail(string email)
    {
        return _context.Managers.Any(x => x.Email == email);
    }

    public async Task<Manager> FindByIdEmailAndPasswordAsync(string email, string password)
    {
        return (await _context.Managers.FirstOrDefaultAsync(x => x.Email == email && x.PasswordHash == password))!;
    }

    public void Update(Manager manager)
    {
        _context.Managers.Update(manager);
    }

    public void Delete(Manager manager)
    {
        _context.Managers.Remove(manager);
    }
}