using Qola.API.Qola.Domain.Models;
using Qola.API.Qola.Domain.Repositories;
using Qola.API.Qola.Domain.Services;
using Qola.API.Qola.Domain.Services.Communication;
using Qola.API.Security.Domain.Repositories;
using Qola.API.Shared.Domain.Repositories.Repositories;

namespace Qola.API.Qola.Services;

public class RestaurantService : IRestaurantService
{
    private readonly IRestaurantRepository _restaurantRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IManagerRepository _managerRepository;

    public RestaurantService(IRestaurantRepository restaurantRepository, IUnitOfWork unitOfWork, IManagerRepository managerRepository)
    {
        _restaurantRepository = restaurantRepository;
        _unitOfWork = unitOfWork;
        _managerRepository = managerRepository;
    }

    public async Task<IEnumerable<Restaurant>> ListAsync()
    {
        return await _restaurantRepository.GetAll();
    }

    public async Task<Restaurant> FindRestaurantByManagerAsync(int managerId)
    {
        return await _restaurantRepository.FindRestaurantByManagerAsync(managerId);
    }

    public async Task<Restaurant> FindRestaurantByManagerById(int managerId)
    {
        var existingManager = await _managerRepository.FindByIdAsync(managerId);
        if (existingManager.Equals(null))
            return null;
        return await _restaurantRepository.FindRestaurantByManagerById(existingManager.Id);
    }

    public async Task<Restaurant> FindByIdAsync(int id)
    {
        return await _restaurantRepository.FindByIdAsync(id);
    }

    public async Task<RestaurantResponse> SaveAsync(Restaurant restaurant, int managerId)
    {
        restaurant.ManagerId = managerId;
        try
        {
            await _restaurantRepository.AddAsync(restaurant);
            await _unitOfWork.CompleteAsync();

            return new RestaurantResponse(restaurant);
        }
        catch (Exception ex)
        {
            // Do some logging stuff
            return new RestaurantResponse($"An error occurred when saving the restaurant: {ex.Message}");
        }
    }

    public async Task<RestaurantResponse> UpdateAsync(int id, Restaurant restaurant)
    {
        var existingRestaurant = await _restaurantRepository.FindByIdAsync(id);
        if (existingRestaurant.Equals(null))
            return new RestaurantResponse("Restaurant not found.");
        
        existingRestaurant.Name = restaurant.Name;
        existingRestaurant.Address = restaurant.Address;
        existingRestaurant.Logo = restaurant.Logo;
        existingRestaurant.Phone = restaurant.Phone;
        existingRestaurant.RUC = restaurant.RUC;
        try
        {
            _restaurantRepository.Update(existingRestaurant);
            await _unitOfWork.CompleteAsync();

            return new RestaurantResponse(existingRestaurant);
        }
        catch (Exception ex)
        {
            // Do some logging stuff
            return new RestaurantResponse($"An error occurred when updating the restaurant: {ex.Message}");
        }
    }

    public async Task<RestaurantResponse> DeleteAsync(int id)
    {
        var existingRestaurant = await _restaurantRepository.FindByIdAsync(id);
        if (existingRestaurant.Equals(null))
            return new RestaurantResponse("Restaurant not found.");
        try
        {
            _restaurantRepository.Delete(existingRestaurant);
            await _unitOfWork.CompleteAsync();

            return new RestaurantResponse(existingRestaurant);
        }
        catch (Exception ex)
        {
            // Do some logging stuff
            return new RestaurantResponse($"An error occurred when deleting the restaurant: {ex.Message}");
        }
    }
}