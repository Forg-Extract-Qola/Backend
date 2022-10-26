using Qola.API.Qola.Domain.Models;

namespace Qola.API.Qola.Domain.Repositories;

public interface IRestaurantRepository
{
    Task<IEnumerable<Restaurant>> GetAll();
    Task<Restaurant> FindRestaurantByManagerAsync(int managerId);
    Task<Restaurant> FindRestaurantByManagerById(int managerId);
    Task<Restaurant> FindByIdAsync(int id);
    Task AddAsync(Restaurant restaurant);
    void Update(Restaurant restaurant);
    void Delete(Restaurant restaurant);
}