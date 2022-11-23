using Qola.API.Qola.Domain.Models;
using Qola.API.Qola.Domain.Repositories;
using Qola.API.Qola.Domain.Services;
using Qola.API.Qola.Domain.Services.Communication;
using Qola.API.Shared.Domain.Repositories.Repositories;

namespace Qola.API.Qola.Services;

public class DishService : IDishService
{
    private readonly IDishRepository _dishRepository;
    private readonly  IUnitOfWork _unitOfWork;
    private readonly IRestaurantRepository _restaurantRepository;

    public DishService(IDishRepository dishRepository, IUnitOfWork unitOfWork, IRestaurantRepository restaurantRepository)
    {
        _dishRepository = dishRepository;
        _unitOfWork = unitOfWork;
        _restaurantRepository = restaurantRepository;
    }

    public async Task<IEnumerable<Dish>> ListAsync()
    {
        return await _dishRepository.ListAsync();
    }

    public async Task<IEnumerable<Dish>> FindByCategoryNameAndRestaurantIdAsync(string categoryName, int restaurantId)
    {
        return await _dishRepository.FindByCategoryNameAndRestaurantIdAsync(categoryName, restaurantId);
    }

    public async Task<IEnumerable<Dish>> FindByRestaurantIdAsync(int restaurantId)
    {
        return await _dishRepository.FindByRestaurantIdAsync(restaurantId);
    }

    public async Task<Dish> FindByIdAsync(int id)
    {
        return await _dishRepository.FindByIdAsync(id);
    }

    public async Task<DishResponse> AddAsync(Dish dish, int restaurantId)
    {
        var existingRestaurant = await _restaurantRepository.FindByIdAsync(restaurantId);
        if (existingRestaurant.Equals(null))
        {
            return new DishResponse("Restaurant not found");
        }
        dish.RestaurantId = restaurantId;
        try
        {
            await _dishRepository.AddAsync(dish);
            await _unitOfWork.CompleteAsync();

            return new DishResponse(dish);
        }
        catch (Exception ex)
        {
            return new DishResponse($"An error occurred when saving the dish: {ex.Message}");
        }
    }

    public async Task<DishResponse> Update(int id, Dish dish)
    {
        var existingDish = await _dishRepository.FindByIdAsync(id);
        if (existingDish.Equals(null))
        {
            return new DishResponse("Dish not found");
        }
        existingDish.Name = dish.Name;
        existingDish.Description = dish.Description;
        existingDish.Image = dish.Image;
        existingDish.Price = dish.Price;
        existingDish.Category_dish = dish.Category_dish;
        try
        {
            _dishRepository.Update(existingDish);
            await _unitOfWork.CompleteAsync();

            return new DishResponse(existingDish);
        }
        catch (Exception ex)
        {
            return new DishResponse($"An error occurred when updating the dish: {ex.Message}");
        }
    }

    public async Task<DishResponse> Remove(int id)
    {
        var existingDish = await _dishRepository.FindByIdAsync(id);
        if (existingDish.Equals(null))
        {
            return new DishResponse("Dish not found");
        }
        try
        {
            _dishRepository.Remove(existingDish);
            await _unitOfWork.CompleteAsync();

            return new DishResponse(existingDish);
        }
        catch (Exception ex)
        {
            return new DishResponse($"An error occurred when deleting the dish: {ex.Message}");
        }
    }
}