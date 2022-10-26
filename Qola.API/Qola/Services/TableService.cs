using Qola.API.Qola.Domain.Models;
using Qola.API.Qola.Domain.Repositories;
using Qola.API.Qola.Domain.Services;
using Qola.API.Qola.Domain.Services.Communication;
using Qola.API.Shared.Domain.Repositories.Repositories;

namespace Qola.API.Qola.Services;

public class TableService: ITableService
{
    private readonly ITableRepository _tableRepository;
    private readonly  IUnitOfWork _unitOfWork;
    private readonly IRestaurantRepository _restaurantRepository;

    public TableService(ITableRepository tableRepository, IUnitOfWork unitOfWork, IRestaurantRepository restaurantRepository)
    {
        _tableRepository = tableRepository;
        _unitOfWork = unitOfWork;
        _restaurantRepository = restaurantRepository;
    }

    public async Task<IEnumerable<Table>> ListAsync()
    {
        return await _tableRepository.ListAsync();
    }

    public async Task<IEnumerable<Table>> FindTablesByRestaurantIdAsync(int restaurantId)
    {
        return await _tableRepository.FindTablesByRestaurantIdAsync(restaurantId);
    }

    public async Task<IEnumerable<Table>> ListTableIsOccupiedOfRestaurantAsync(int restaurantId)
    {
        return await _tableRepository.ListTableIsOccupiedOfRestaurantAsync(restaurantId);
    }

    public async Task<IEnumerable<Table>> ListTableIsOccupiedTrueOfRestaurantAsync(int restaurantId)
    {
        return await _tableRepository.ListTableIsOccupiedTrueOfRestaurantAsync(restaurantId);
    }


    public async Task<Table> FindByIdAsync(int id)
    {
        return await _tableRepository.FindByIdAsync(id);
    }

    public async Task<TableResponse> SaveAsync(Table table, int restaurantId)
    {
        var existingRestaurant = await _restaurantRepository.FindByIdAsync(restaurantId);
        if (existingRestaurant.Equals(null))
        {
            return new TableResponse("Restaurant not found.");
        }
        table.RestaurantId = restaurantId;
        try
        {
            await _tableRepository.AddAsync(table);
            await _unitOfWork.CompleteAsync();

            return new TableResponse(table);
        }
        catch (Exception ex)
        {
            // Do some logging stuff
            return new TableResponse($"An error occurred when saving the table: {ex.Message}");
        }
    }

    public async Task<TableResponse> UpdateAsync(int id, Table table)
    {
        var existingTable = await _tableRepository.FindByIdAsync(id);
        if (existingTable.Equals(null))
        {
            return  new TableResponse("Table not found.");
        }
        existingTable.Name = table.Name;
        existingTable.IsOccupied = table.IsOccupied;
        try
        {
            _tableRepository.Update(existingTable);
            await _unitOfWork.CompleteAsync();

            return new TableResponse(existingTable);
        }
        catch (Exception ex)
        {
            // Do some logging stuff
            return new TableResponse($"An error occurred when updating the table: {ex.Message}");
        }
    }

    public async Task<TableResponse> DeleteAsync(int id)
    {
        var existingTable = await _tableRepository.FindByIdAsync(id);
        if (existingTable.Equals(null))
        {
            return new TableResponse("Table not found.");
        }
        try
        {
            _tableRepository.Remove(existingTable);
            await _unitOfWork.CompleteAsync();

            return new TableResponse(existingTable);
        }
        catch (Exception ex)
        {
            // Do some logging stuff
            return new TableResponse($"An error occurred when deleting the table: {ex.Message}");
        }
    }
}