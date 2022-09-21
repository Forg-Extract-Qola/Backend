using Qola.API.Qola.Domain.Models;
using Qola.API.Qola.Domain.Services.Communication;

namespace Qola.API.Qola.Domain.Services;

public interface IOrderDishesService
{
    Task<IEnumerable<OrderDishes>> FindAllByOrderRestaurantIdAsync(int restaurantId);
    Task<IEnumerable<Dish>> FindByOrderId(int orderId);
    Task<OrderDishes> FindByIdAsync(int id);
    Task<OrderDishesResponse> AddAsync(OrderDishes orderDishes, int orderId, int dishId);
    Task<OrderDishesResponse> Delete(int id);
}