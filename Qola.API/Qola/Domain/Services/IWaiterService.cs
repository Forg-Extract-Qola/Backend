using Qola.API.Qola.Domain.Models;
using Qola.API.Qola.Domain.Services.Communication;

namespace Qola.API.Qola.Domain.Services;

public interface IWaiterService
{
    Task<IEnumerable<Waiter>> ListAsync();
    Task<IEnumerable<Waiter>> FindWaitersByRestaurantIdAsync(int restaurantId);
    Task<Waiter> FindByIdAsync(int id);
    Task<Waiter> FindByUIdAsync(string uid);
    Task<WaiterResponse> SaveAsync(Waiter waiter, int restaurantId);
    Task<WaiterResponse> UpdateAsync(int id, Waiter waiter);
    Task<WaiterResponse> DeleteAsync(int id);
}