using Microsoft.EntityFrameworkCore;
using Qola.API.Qola.Domain.Models;
using Qola.API.Qola.Domain.Repositories;
using Qola.API.Shared.Persistence.Contexts;
using Qola.API.Shared.Persistence.Repositories;

namespace Qola.API.Qola.Persistence.Repositories;

public class WaiterRepository:BaseRepository, IWaiterRepository
{
    public WaiterRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Waiter>> ListAsync()
    {
        return await _context.Waiters.ToListAsync();
    }

    public async Task<IEnumerable<Waiter>> FindWaitersByRestaurantIdAsync(int restaurantId)
    {
        return await _context.Waiters.Where(w => w.RestaurantId == restaurantId).ToListAsync();
    }

    public async Task<Waiter> FindByIdAsync(int id)
    {
        return (await _context.Waiters.FindAsync(id))!;
    }

    public async Task<Waiter> FindByUIdAsync(string uid)
    {
        return (await _context.Waiters.Where(c => c.UID == uid).FirstOrDefaultAsync())!;
    }

    public async Task AddAsync(Waiter waiter)
    {
        await _context.Waiters.AddAsync(waiter);
    }

    public void Update(Waiter waiter)
    {
        _context.Waiters.Update(waiter);
    }

    public void Remove(Waiter waiter)
    {
        _context.Waiters.Remove(waiter);
    }
}