using Qola.API.Qola.Domain.Models;
using Qola.API.Qola.Domain.Services.Communication;

namespace Qola.API.Qola.Domain.Services;

public interface ITableService
{
    Task<IEnumerable<Table>> ListAsync();
    Task<IEnumerable<Table>> FindTablesByRestaurantIdAsync(int restaurantId);
    Task<IEnumerable<Table>> ListTableIsOccupiedOfRestaurantAsync(int restaurantId);
    Task<Table> FindByIdAsync(int id);
    Task<TableResponse> SaveAsync(Table table, int restaurantId);
    Task<TableResponse> UpdateAsync(int id, Table table);
    Task<TableResponse> DeleteAsync(int id);
}