using Microsoft.EntityFrameworkCore;
using Qola.API.Qola.Domain.Models;
using Qola.API.Qola.Domain.Repositories;
using Qola.API.Shared.Persistence.Contexts;
using Qola.API.Shared.Persistence.Repositories;

namespace Qola.API.Qola.Persistence.Repositories;

public class OrderRepository : BaseRepository, IOrderRepository
{
    public OrderRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Order>> ListAsync()
    {
        return await _context.Orders.ToListAsync();
    }

    public async Task<IEnumerable<Order>> FindByWaiterIdAsync(int waiterId)
    {
        return await _context.Orders.Where(o => o.WaiterId == waiterId).ToListAsync();
    }

    public async Task<IEnumerable<Order>> FindByTableIdAndStatusAndRestaurantIdAsync(int tableId, string status, int restaurantId)
    {
        return await _context.Orders.Where(o => o.TableId == tableId && o.Status == status && o.RestaurantId == restaurantId).ToListAsync();
    }


    public async Task<IEnumerable<Order>> FindByStatusAndRestaurantIdAsync(string status, int restaurantId)
    {
        return await _context.Orders.Where(o => o.Status == status && o.RestaurantId == restaurantId).ToListAsync();
    }

    public async Task<Order> FindByIdAsync(int id)
    {
        return (await _context.Orders.FindAsync(id))!;
    }

    public async Task AddAsync(Order order)
    {
        await _context.Orders.AddAsync(order);
    }

    public void Update(Order order)
    {
        _context.Orders.Update(order);
    }

    public void Remove(Order order)
    {
        _context.Orders.Remove(order);
    }
}