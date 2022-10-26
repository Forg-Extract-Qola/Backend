using Microsoft.EntityFrameworkCore;
using Qola.API.Qola.Domain.Models;
using Qola.API.Qola.Domain.Repositories;
using Qola.API.Shared.Persistence.Contexts;
using Qola.API.Shared.Persistence.Repositories;

namespace Qola.API.Qola.Persistence.Repositories;

public class RestaurantRepository : BaseRepository, IRestaurantRepository

{
    public RestaurantRepository(AppDbContext context) : base(context)
    {
    }
    
    public async Task<IEnumerable<Restaurant>> GetAll()
    {
        return await _context.Restaurants.ToListAsync();
    }

    public async Task<Restaurant> FindRestaurantByManagerAsync(int managerId)
    {
        return (await _context.Restaurants.Where(r => r.ManagerId == managerId).FirstOrDefaultAsync())!;
    }

    public async Task<Restaurant> FindRestaurantByManagerById(int managerId)
    {
        return (await _context.Restaurants.Where(r => r.ManagerId == managerId).FirstOrDefaultAsync())!;
    }

    public async Task<Restaurant> FindByIdAsync(int id)
    {
        return (await _context.Restaurants.FindAsync(id))!;
    }

    public async Task AddAsync(Restaurant restaurant)
    {
        await _context.Restaurants.AddAsync(restaurant);
    }

    public void Update(Restaurant restaurant)
    {
        _context.Restaurants.Update(restaurant);
    }

    public void Delete(Restaurant restaurant)
    {
        _context.Restaurants.Remove(restaurant);
    }

    
}