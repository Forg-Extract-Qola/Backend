using Qola.API.Qola.Domain.Models;

namespace Qola.API.Qola.Domain.Repositories;

public interface IOrderDishesRepository
{
    Task<IEnumerable<OrderDishes>> FindAllByOrderRestaurantIdAsync(int restaurantId);
    Task<IEnumerable<Dish>> FindByOrderId(int orderId);
    Task<OrderDishes> FindByIdAsync(int id);
    Task AddAsync(OrderDishes orderDishes);
    void Delete(OrderDishes orderDishes);
}