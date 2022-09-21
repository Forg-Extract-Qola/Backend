using Microsoft.EntityFrameworkCore;
using Qola.API.Qola.Domain.Models;
using Qola.API.Qola.Domain.Repositories;
using Qola.API.Shared.Persistence.Contexts;
using Qola.API.Shared.Persistence.Repositories;

namespace Qola.API.Qola.Persistence.Repositories;

public class OrderDishesRepository: BaseRepository, IOrderDishesRepository
{
    public OrderDishesRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<OrderDishes>> FindAllByOrderRestaurantIdAsync(int restaurantId)
    {
        return await _context.OrderDishes
            .Where(x => x.Order.RestaurantId == restaurantId)
            .ToListAsync();
    }

    public async Task<IEnumerable<Dish>> FindByOrderId(int orderId)
    {
        return await _context.Dishes
            .Where(x => x.OrderDishes.Any(y => y.OrderId == orderId))
            .ToListAsync();
    }

    public async Task<OrderDishes> FindByIdAsync(int id)
    {
        return (await _context.OrderDishes.FindAsync(id))!;
    }

    public async Task AddAsync(OrderDishes orderDishes)
    {
        await _context.OrderDishes.AddAsync(orderDishes);
    }
    
    public void Delete(OrderDishes orderDishes)
    {
        _context.OrderDishes.Remove(orderDishes);
    }
}