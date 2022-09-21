using Qola.API.Qola.Domain.Models;
using Qola.API.Qola.Domain.Services.Communication;

namespace Qola.API.Qola.Domain.Services;

public interface ICookService
{
    Task<IEnumerable<Cook>> ListAsync();
    Task<IEnumerable<Cook>> FindCooksByRestaurantIdAsync(int restaurantId);
    Task<Cook> FindByIdAsync(int id);
    Task<Cook> FindByUIdAsync(string uid);
    Task<CookResponse> SaveAsync(Cook cook, int restaurantId);
    Task<CookResponse> UpdateAsync(int id, Cook cook);
    Task<CookResponse> DeleteAsync(int id);
}