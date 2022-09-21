using Qola.API.Qola.Domain.Models;
using Qola.API.Qola.Domain.Repositories;
using Qola.API.Qola.Domain.Services;
using Qola.API.Qola.Domain.Services.Communication;
using Qola.API.Shared.Domain.Repositories.Repositories;

namespace Qola.API.Qola.Services;

public class OrderDishesService : IOrderDishesService
{
    private readonly IOrderDishesRepository _orderDishesRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IOrderRepository _orderRepository;
    private readonly IDishRepository _dishRepository;

    public OrderDishesService(IOrderDishesRepository orderDishesRepository, IUnitOfWork unitOfWork, 
        IDishRepository dishRepository, IOrderRepository orderRepository)
    {
        _orderDishesRepository = orderDishesRepository;
        _unitOfWork = unitOfWork;
        _dishRepository = dishRepository;
        _orderRepository = orderRepository;
    }

    public async Task<IEnumerable<OrderDishes>> FindAllByOrderRestaurantIdAsync(int restaurantId)
    {
        return await _orderDishesRepository.FindAllByOrderRestaurantIdAsync(restaurantId);
    }

    public async Task<IEnumerable<Dish>> FindByOrderId(int orderId)
    {
        return await _orderDishesRepository.FindByOrderId(orderId);
    }

    public async Task<OrderDishes> FindByIdAsync(int id)
    {
        return await _orderDishesRepository.FindByIdAsync(id);
    }

    public async Task<OrderDishesResponse> AddAsync(OrderDishes orderDishes, int orderId, int dishId)
    {
        var existingOrder = await _orderRepository.FindByIdAsync(orderId);
        var existingDish = await _dishRepository.FindByIdAsync(dishId);
        if(existingDish.Equals(null) || existingOrder.Equals(null))
        {
            return new OrderDishesResponse("Order or Dish not found");
        }
        orderDishes.DishId = dishId;
        orderDishes.OrderId = orderId;
        // Update the order total
        existingOrder.Total += existingDish.Price;
        try
        {
            await _orderDishesRepository.AddAsync(orderDishes);
            await _unitOfWork.CompleteAsync();

            return new OrderDishesResponse(orderDishes);
        }
        catch (Exception ex)
        {
            return new OrderDishesResponse($"An error occurred when saving the orderDishes: {ex.Message}");
        }
    }

    public async Task<OrderDishesResponse> Delete(int id)
    {
        var existingOrderDishes = await _orderDishesRepository.FindByIdAsync(id);
        if (existingOrderDishes.Equals(null))
            return new OrderDishesResponse("OrderDishes not found");
        
        try
        {
            _orderDishesRepository.Delete(existingOrderDishes);
            await _unitOfWork.CompleteAsync();

            return new OrderDishesResponse(existingOrderDishes);
        }
        catch (Exception ex)
        {
            return new OrderDishesResponse($"An error occurred when deleting the orderDishes: {ex.Message}");
        }
    }
}