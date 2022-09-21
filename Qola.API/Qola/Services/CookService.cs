using Qola.API.Qola.Domain.Models;
using Qola.API.Qola.Domain.Repositories;
using Qola.API.Qola.Domain.Services;
using Qola.API.Qola.Domain.Services.Communication;
using Qola.API.Shared.Domain.Repositories.Repositories;

namespace Qola.API.Qola.Services;

public class CookService: ICookService
{
    private readonly ICookRepository _cookRepository;
    private readonly IRestaurantRepository _restaurantRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CookService(ICookRepository cookRepository, IUnitOfWork unitOfWork, IRestaurantRepository restaurantRepository)
    {
        _cookRepository = cookRepository;
        _unitOfWork = unitOfWork;
        _restaurantRepository = restaurantRepository;
    }

    public async Task<IEnumerable<Cook>> ListAsync()
    {
        return await _cookRepository.ListAsync();
    }

    public async Task<IEnumerable<Cook>> FindCooksByRestaurantIdAsync(int restaurantId)
    {
        return await _cookRepository.FindCooksByRestaurantIdAsync(restaurantId);
    }

    public async Task<Cook> FindByIdAsync(int id)
    {
        return await _cookRepository.FindByIdAsync(id);
    }

    public async Task<Cook> FindByUIdAsync(string uid)
    {
        return await _cookRepository.FindByUIdAsync(uid);
    }

    public async Task<CookResponse> SaveAsync(Cook cook, int restaurantId)
    {
        var existingRestaurant = await _restaurantRepository.FindByIdAsync(restaurantId);
        if (existingRestaurant.Equals(null))
        {
            return new CookResponse("Restaurant not found.");
        }
        cook.RestaurantId = restaurantId;
        try
        {
            await _cookRepository.AddAsync(cook);
            await _unitOfWork.CompleteAsync();

            return new CookResponse(cook);
        }
        catch (Exception ex)
        {
            // Do some logging stuff
            return new CookResponse($"An error occurred when saving the cook: {ex.Message}");
        }
    }

    public async Task<CookResponse> UpdateAsync(int id, Cook cook)
    {
        var existingCook = await _cookRepository.FindByIdAsync(id);
        if (existingCook.Equals(null))
        {
            return new CookResponse("Cook not found.");
        }
        existingCook.FullName = cook.FullName;
        existingCook.Charge = cook.Charge;
        try
        {
            _cookRepository.Update(existingCook);
            await _unitOfWork.CompleteAsync();

            return new CookResponse(existingCook);
        }
        catch (Exception ex)
        {
            // Do some logging stuff
            return new CookResponse($"An error occurred when updating the cook: {ex.Message}");
        }
    }

    public async Task<CookResponse> DeleteAsync(int id)
    {
        var existingCook = await _cookRepository.FindByIdAsync(id);
        if (existingCook.Equals(null))
        {
            return new CookResponse("Cook not found.");
        }
        try
        {
            _cookRepository.Remove(existingCook);
            await _unitOfWork.CompleteAsync();

            return new CookResponse(existingCook);
        }
        catch (Exception ex)
        {
            // Do some logging stuff
            return new CookResponse($"An error occurred when deleting the cook: {ex.Message}");
        }
    }
}