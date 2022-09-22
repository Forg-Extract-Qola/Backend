

using Qola.API.Qola.Domain.Models;

namespace Qola.API.Qola.Domain.Repositories;

public interface ITableRepository
{
    Task<IEnumerable<Table>> ListAsync();
    Task<IEnumerable<Table>> ListTableIsOccupiedOfRestaurantAsync(int restaurantId);
    Task<IEnumerable<Table>> FindTablesByRestaurantIdAsync(int restaurantId);
    Task<Table> FindByIdAsync(int id);
    Task AddAsync(Table table);
    void Update(Table table);
    void Remove(Table table);
}