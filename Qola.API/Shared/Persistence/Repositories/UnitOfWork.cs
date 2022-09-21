using Qola.API.Shared.Domain.Repositories.Repositories;
using Qola.API.Shared.Persistence.Contexts;

namespace Qola.API.Shared.Persistence.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;
    public UnitOfWork(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task CompleteAsync()
    {
        await _context.SaveChangesAsync();
    }
}