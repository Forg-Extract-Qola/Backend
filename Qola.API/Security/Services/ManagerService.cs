using AutoMapper;
using Qola.API.Qola.Domain.Repositories;
using Qola.API.Security.Authorization.Handlers.Interfaces;
using Qola.API.Security.Domain.Models;
using Qola.API.Security.Domain.Repositories;
using Qola.API.Security.Domain.Services;
using Qola.API.Security.Domain.Services.Communication;
using Qola.API.Security.Exceptions;
using Qola.API.Shared.Domain.Repositories.Repositories;
using BCryptNet = BCrypt.Net.BCrypt;

namespace Qola.API.Security.Services;

public class ManagerService : IManagerService
{
    
    private readonly IManagerRepository _managerRepository;
    private readonly IRestaurantRepository _restaurantRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IJwtHandler _jwtHandler;
    private readonly IMapper _mapper;

    public ManagerService(IManagerRepository managerRepository, IUnitOfWork unitOfWork, IJwtHandler jwtHandler, IMapper mapper, IRestaurantRepository restaurantRepository)
    {
        _managerRepository = managerRepository;
        _unitOfWork = unitOfWork;
        _jwtHandler = jwtHandler;
        _mapper = mapper;
        _restaurantRepository = restaurantRepository;
    }

    public async Task<AuthenticateResponse> AuthenticateAsync(AuthenticateRequest request)
    {
        var user = await _managerRepository.FindByEmailAsync(request.Email);
        //Valite
        if (user.Equals(null) || !BCryptNet.Verify(request.Password, user.PasswordHash))
        {
            throw new AppExceptions("Invalid credentials");
        }
        var restaurant = await _restaurantRepository.FindRestaurantByManagerAsync(user.Id);
        var response = _mapper.Map<AuthenticateResponse>(user);
        response.Token = _jwtHandler.GenerateToken(user);
        if (!restaurant.Equals(null))
        {
            response.RestaurantId = restaurant.Id;
        }
        else
        {
            response.RestaurantId = 0;
        }
        return response;
    }

    public async Task<IEnumerable<Manager>> ListAsync()
    {
        return await _managerRepository.ListAsync();
    }

    public async Task<Manager> FindByIdAsync(int id)
    {
        var user = await _managerRepository.FindByIdAsync(id);
        if (user == null)
        {
            throw new KeyNotFoundException("Manager not found");
        }
        return user;
    }

    public async Task RegisterAsync(RegisterRequest request)
    {
        if(_managerRepository.ExistsByEmail(request.Email))
        {
            throw new AppExceptions($"Manager with email {request.Email} already exists");
        }
        var user = _mapper.Map<Manager>(request);
        user.PasswordHash = BCryptNet.HashPassword(request.Password);
        try
        {
            await _managerRepository.AddAsync(user);
            await _unitOfWork.CompleteAsync();
        }
        catch (Exception ex)
        {
            throw new AppExceptions($"An Error occurred while saving the manager: {ex.Message}");
        }
    }

    public async Task UpdateAsync(int id, UpdateRequest request)
    {
        var user=await _managerRepository.FindByIdAsync(id);
        var userWithSameEmail = await _managerRepository.FindByEmailAsync(request.Email);
        if (userWithSameEmail != null && userWithSameEmail.Id != id)
        {
            throw new AppExceptions($"User with email {request.Email} already exists");
        }
        if(!string.IsNullOrEmpty(request.Password)&&request.Password!="")
        {
            user.PasswordHash = BCryptNet.HashPassword(request.Password);
        }
        else
        {
            user.PasswordHash = user.PasswordHash;
        }
        _mapper.Map(request, user);
        try
        {
            _managerRepository.Update(user);
            await _unitOfWork.CompleteAsync();
        }
        catch (Exception ex)
        {
            throw new AppExceptions($"An Error occured while updating user: {ex.Message}");
        }
    }

    public async Task DeleteAsync(int id)
    {
        var user = await _managerRepository.FindByIdAsync(id);
        if (user == null)
        {
            throw new KeyNotFoundException("Manager not found");
        }

        try
        {
            _managerRepository.Delete(user);
            await _unitOfWork.CompleteAsync();
        }
        catch (Exception e)
        {
            throw new AppExceptions($"An Error occured while deleting manager: {e.Message}");
        }
    }
}