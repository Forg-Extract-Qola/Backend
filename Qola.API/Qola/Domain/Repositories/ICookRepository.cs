using Qola.API.Qola.Domain.Models;

namespace Qola.API.Qola.Domain.Repositories;

public interface ICookRepository
{
    Task<IEnumerable<Cook>> ListAsync();
    Task<IEnumerable<Cook>> FindCooksByRestaurantIdAsync(int restaurantId);
    Task<Cook> FindByIdAsync(int id);
    Task<Cook> FindByUIdAsync(string uid);
    Task AddAsync(Cook cook);
    void Update(Cook cook);
    void Remove(Cook cook);
}