using Qola.API.Qola.Domain.Models;

namespace Qola.API.Qola.Domain.Repositories;

public interface IDishRepository
{
    Task<IEnumerable<Dish>> ListAsync();
    Task<IEnumerable<Dish>> FindByCategoryNameAndRestaurantIdAsync(string categoryName, int restaurantId);
    Task<IEnumerable<Dish>> FindByRestaurantIdAsync(int restaurantId);
    Task AddAsync(Dish dish);
    Task<Dish> FindByIdAsync(int id);
    void Update(Dish dish);
    void Remove(Dish dish);
}