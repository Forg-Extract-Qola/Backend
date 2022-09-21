using Qola.API.Qola.Domain.Models;

namespace Qola.API.Qola.Domain.Repositories;

public interface IWaiterRepository
{
    Task<IEnumerable<Waiter>> ListAsync();
    Task<IEnumerable<Waiter>> FindWaitersByRestaurantIdAsync(int restaurantId);
    Task<Waiter> FindByIdAsync(int id);
    Task<Waiter> FindByUIdAsync(string uid);
    Task AddAsync(Waiter waiter);
    void Update(Waiter waiter);
    void Remove(Waiter waiter);
}