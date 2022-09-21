using Qola.API.Qola.Domain.Models;

namespace Qola.API.Qola.Domain.Repositories;

public interface IOrderRepository
{
    Task<IEnumerable<Order>> ListAsync();
    Task<IEnumerable<Order>> FindByWaiterIdAsync(int waiterId);
    Task<IEnumerable<Order>> FindByTableIdAndStatusAsync(int tableId, string status);
    Task<IEnumerable<Order>> FindByStatusAndRestaurantIdAsync(string status, int restaurantId);
    Task<Order> FindByIdAsync(int id);
    Task AddAsync(Order order);
    void Update(Order order);
    void Remove(Order order);
}