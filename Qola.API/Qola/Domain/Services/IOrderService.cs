using Qola.API.Qola.Domain.Models;
using Qola.API.Qola.Domain.Services.Communication;

namespace Qola.API.Qola.Domain.Services;

public interface IOrderService
{
    Task<IEnumerable<Order>> ListAsync();
    Task<IEnumerable<Order>> FindByWaiterIdAsync(int waiterId);
    Task<IEnumerable<Order>> FindByTableIdAndStatusAndRestaurantIdAsync(int tableId, string status, int restaurantId);
    Task<IEnumerable<Order>> FindByStatusAndRestaurantIdAsync(string status, int restaurantId);
    Task<Order> FindByIdAsync(int id);
    Task<OrderResponse> AddAsync(Order order, int waiterId, int tableId, int restaurantId);
    Task<OrderResponse> Update(int id, Order order);
    Task<OrderResponse> Remove(int id);
}