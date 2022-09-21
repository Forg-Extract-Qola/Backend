using Qola.API.Qola.Domain.Models;
using Qola.API.Qola.Domain.Repositories;
using Qola.API.Qola.Domain.Services;
using Qola.API.Qola.Domain.Services.Communication;
using Qola.API.Shared.Domain.Repositories.Repositories;

namespace Qola.API.Qola.Services;

public class WaiterService : IWaiterService
{
    
    private readonly IWaiterRepository _waiterRepository;
    private readonly IRestaurantRepository _restaurantRepository;
    private readonly IUnitOfWork _unitOfWork;

    public WaiterService(IWaiterRepository waiterRepository, IUnitOfWork unitOfWork, IRestaurantRepository restaurantRepository)
    {
        _waiterRepository = waiterRepository;
        _unitOfWork = unitOfWork;
        _restaurantRepository = restaurantRepository;
    }

    public async Task<IEnumerable<Waiter>> ListAsync()
    {
        return await _waiterRepository.ListAsync();
    }

    public async Task<IEnumerable<Waiter>> FindWaitersByRestaurantIdAsync(int restaurantId)
    {
        return await _waiterRepository.FindWaitersByRestaurantIdAsync(restaurantId);
    }

    public async Task<Waiter> FindByIdAsync(int id)
    {
        return await _waiterRepository.FindByIdAsync(id);
    }

    public async Task<Waiter> FindByUIdAsync(string uid)
    {
        return await _waiterRepository.FindByUIdAsync(uid);
    }

    public async Task<WaiterResponse> SaveAsync(Waiter waiter, int restaurantId)
    {
        var existingRestaurant = await _restaurantRepository.FindByIdAsync(restaurantId);
        if (existingRestaurant.Equals(null))
        {
            return new WaiterResponse("Restaurant not found.");
        }
        waiter.RestaurantId = restaurantId;
        try
        {
            await _waiterRepository.AddAsync(waiter);
            await _unitOfWork.CompleteAsync();

            return new WaiterResponse(waiter);
        }
        catch (Exception ex)
        {
            // Do some logging stuff
            return new WaiterResponse($"An error occurred when saving the waiter: {ex.Message}");
        }
    }


    public async Task<WaiterResponse> UpdateAsync(int id, Waiter waiter)
    {
        var existingWaiter = await _waiterRepository.FindByIdAsync(id);
        if (existingWaiter.Equals(null))
        {
            return new WaiterResponse("Waiter not found.");
        }
        existingWaiter.FullName = waiter.FullName;
        existingWaiter.Charge = waiter.Charge;
        existingWaiter.UID = waiter.UID;
        try
        {
            _waiterRepository.Update(existingWaiter);
            await _unitOfWork.CompleteAsync();

            return new WaiterResponse(existingWaiter);
        }
        catch (Exception ex)
        {
            // Do some logging stuff
            return new WaiterResponse($"An error occurred when updating the waiter: {ex.Message}");
        }   
    }

    public async Task<WaiterResponse> DeleteAsync(int id)
    {
        var existingWaiter = await _waiterRepository.FindByIdAsync(id);
        if (existingWaiter.Equals(null))
        {
            return new WaiterResponse("Waiter not found.");
        }
        try
        {
            _waiterRepository.Remove(existingWaiter);
            await _unitOfWork.CompleteAsync();

            return new WaiterResponse(existingWaiter);
        }
        catch (Exception ex)
        {
            // Do some logging stuff
            return new WaiterResponse($"An error occurred when deleting the waiter: {ex.Message}");
        }
    }
}