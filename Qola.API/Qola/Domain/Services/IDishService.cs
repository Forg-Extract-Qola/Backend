using Qola.API.Qola.Domain.Models;
using Qola.API.Qola.Domain.Services.Communication;

namespace Qola.API.Qola.Domain.Services;

public interface IDishService
{
    Task<IEnumerable<Dish>> ListAsync();
    Task<IEnumerable<Dish>> FindByCategoryNameAndRestaurantIdAsync(string categoryName, int restaurantId);
    Task<IEnumerable<Dish>> FindByRestaurantIdAsync(int restaurantId);
    Task<Dish> FindByIdAsync(int id);
    Task<DishResponse> AddAsync(Dish dish, int restaurantId);
    Task<DishResponse> Update(int id, Dish dish);
    Task<DishResponse> Remove(int id);
}