using Microsoft.EntityFrameworkCore;
using Qola.API.Qola.Domain.Models;
using Qola.API.Qola.Domain.Repositories;
using Qola.API.Shared.Persistence.Contexts;
using Qola.API.Shared.Persistence.Repositories;

namespace Qola.API.Qola.Persistence.Repositories;

public class CookRepository: BaseRepository, ICookRepository
{
    public CookRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Cook>> ListAsync()
    {
        return await _context.Cooks.ToListAsync();
    }

    public async Task<IEnumerable<Cook>> FindCooksByRestaurantIdAsync(int restaurantId)
    {
        return await _context.Cooks.Where(c => c.RestaurantId == restaurantId).ToListAsync();
    }

    public async Task<Cook> FindByUIdAsync(string uid)
    {
        return (await _context.Cooks.Where(r => r.UID == uid).FirstOrDefaultAsync())!;
    }

    public async Task AddAsync(Cook cook)
    {
        await _context.Cooks.AddAsync(cook);
    }

    public async Task<Cook> FindByIdAsync(int id)
    {
        return (await _context.Cooks.FindAsync(id))!;
    }

    public void Update(Cook cook)
    {
        _context.Cooks.Update(cook);
    }

    public void Remove(Cook cook)
    {
        _context.Cooks.Remove(cook);
    }
}