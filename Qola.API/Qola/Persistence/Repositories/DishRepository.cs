using Microsoft.EntityFrameworkCore;
using Qola.API.Qola.Domain.Models;
using Qola.API.Qola.Domain.Repositories;
using Qola.API.Shared.Persistence.Contexts;
using Qola.API.Shared.Persistence.Repositories;

namespace Qola.API.Qola.Persistence.Repositories;

public class DishRepository:BaseRepository, IDishRepository
{
    public DishRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Dish>> ListAsync()
    {
        return await _context.Dishes.ToListAsync();
    }

    public async Task<IEnumerable<Dish>> FindByCategoryNameAndRestaurantIdAsync(string categoryName, int restaurantId)
    {
        return await _context.Dishes.Where(d => d.Category_dish == categoryName && d.RestaurantId == restaurantId).ToListAsync();
    }

    public async Task<IEnumerable<Dish>> FindByRestaurantIdAsync(int restaurantId)
    {
        return await _context.Dishes.Where(d => d.RestaurantId == restaurantId).ToListAsync();
    }

    public async Task AddAsync(Dish dish)
    {
        await _context.Dishes.AddAsync(dish);
    }

    public async Task<Dish> FindByIdAsync(int id)
    {
        return (await _context.Dishes.FindAsync(id))!;
    }

    public void Update(Dish dish)
    {
        _context.Dishes.Update(dish);
    }

    public void Remove(Dish dish)
    {
        _context.Dishes.Remove(dish);
    }
}