using Microsoft.EntityFrameworkCore;
using Qola.API.Qola.Domain.Models;
using Qola.API.Qola.Domain.Repositories;
using Qola.API.Shared.Persistence.Contexts;
using Qola.API.Shared.Persistence.Repositories;

namespace Qola.API.Qola.Persistence.Repositories;

public class TableRepository : BaseRepository, ITableRepository
{
    public TableRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Table>> ListAsync()
    {
        return await _context.Tables.ToListAsync();
    }

    public async Task<IEnumerable<Table>> ListTableIsOccupiedOfRestaurantAsync(int restaurantId)
    {
        return await _context.Tables.Where(t => t.RestaurantId == restaurantId && t.IsOccupied == false).ToListAsync();
    }

    public async Task<IEnumerable<Table>> ListTableIsOccupiedTrueOfRestaurantAsync(int restaurantId)
    {
        return await _context.Tables.Where(t => t.RestaurantId == restaurantId && t.IsOccupied == true).ToListAsync();
    }

    public async Task<IEnumerable<Table>> FindTablesByRestaurantIdAsync(int restaurantId)
    {
        return await _context.Tables.Where(x => x.RestaurantId == restaurantId).ToListAsync();
    }

    public async Task<Table> FindByIdAsync(int id)
    {
        return (await _context.Tables.FindAsync(id))!;
    }

    public async Task AddAsync(Table table)
    {
        await _context.Tables.AddAsync(table);
    }

    public void Update(Table table)
    {
        _context.Tables.Update(table);
    }

    public void Remove(Table table)
    {
        _context.Tables.Remove(table);
    }
}