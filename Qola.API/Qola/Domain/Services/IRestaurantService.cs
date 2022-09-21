using Qola.API.Qola.Domain.Models;
using Qola.API.Qola.Domain.Services.Communication;

namespace Qola.API.Qola.Domain.Services;

public interface IRestaurantService
{
    Task<IEnumerable<Restaurant>> ListAsync();
    Task<Restaurant> FindRestaurantByManagerAsync(int managerId);
    Task<Restaurant> FindByIdAsync(int id);
    Task<RestaurantResponse> SaveAsync(Restaurant restaurant, int managerId);
    Task<RestaurantResponse> UpdateAsync(int id, Restaurant restaurant);
    Task<RestaurantResponse> DeleteAsync(int id);
}