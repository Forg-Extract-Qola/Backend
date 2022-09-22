using Qola.API.Qola.Domain.Models;
using Qola.API.Qola.Domain.Repositories;
using Qola.API.Qola.Domain.Services;
using Qola.API.Qola.Domain.Services.Communication;
using Qola.API.Shared.Domain.Repositories.Repositories;

namespace Qola.API.Qola.Services;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _orderRepository;
    private readonly  IUnitOfWork _unitOfWork;
    private readonly IWaiterRepository _waiterRepository;
    private readonly ITableRepository _tableRepository;
    private readonly IRestaurantRepository _restaurantRepository;

    public OrderService(IOrderRepository orderRepository, IUnitOfWork unitOfWork, 
        IWaiterRepository waiterRepository, ITableRepository tableRepository, 
        IRestaurantRepository restaurantRepository)
    {
        _orderRepository = orderRepository;
        _unitOfWork = unitOfWork;
        _waiterRepository = waiterRepository;
        _tableRepository = tableRepository;
        _restaurantRepository = restaurantRepository;
    }

    public async Task<IEnumerable<Order>> ListAsync()
    {
        return await _orderRepository.ListAsync();
    }

    public async Task<IEnumerable<Order>> FindByWaiterIdAsync(int waiterId)
    {
        return await _orderRepository.FindByWaiterIdAsync(waiterId);
    }

    public async Task<IEnumerable<Order>> FindByTableIdAndStatusAndRestaurantIdAsync(int tableId, string status, int restaurantId)
    {
        return await _orderRepository.FindByTableIdAndStatusAndRestaurantIdAsync(tableId, status, restaurantId);
    }

    public async Task<IEnumerable<Order>> FindByStatusAndRestaurantIdAsync(string status, int restaurantId)
    {
        return await _orderRepository.FindByStatusAndRestaurantIdAsync(status, restaurantId);
    }


    public async Task<Order> FindByIdAsync(int id)
    {
        return await _orderRepository.FindByIdAsync(id);
    }

    public async Task<OrderResponse> AddAsync(Order order, int waiterId, int tableId, int restaurantId)
    {
        var existingWaiter = await _waiterRepository.FindByIdAsync(waiterId);
        var existingTable = await _tableRepository.FindByIdAsync(tableId);
        var existingRestaurant = await _restaurantRepository.FindByIdAsync(restaurantId);
        if (existingRestaurant.Equals(null) || existingTable.Equals(null) || existingWaiter.Equals(null))
        {
            return new OrderResponse("Invalid Waiter, Table or Restaurant");
        }
        order.WaiterId = waiterId;
        order.TableId = tableId;
        order.RestaurantId = restaurantId;
        order.Total = 0;
        try
        {
            await _orderRepository.AddAsync(order);
            await _unitOfWork.CompleteAsync();

            return new OrderResponse(order);
        }
        catch (Exception ex)
        {
            return new OrderResponse($"An error occurred when saving the order: {ex.Message}");
        }
    }

    public async Task<OrderResponse> Update(int id, Order order)
    {
        var existingOrder = await _orderRepository.FindByIdAsync(id);
        if (existingOrder.Equals(null))
            return new OrderResponse("Order not found");
        
        existingOrder.Status = order.Status;
        existingOrder.Notes = order.Notes;
        try
        {
            _orderRepository.Update(existingOrder);
            await _unitOfWork.CompleteAsync();

            return new OrderResponse(existingOrder);
        }
        catch (Exception ex)
        {
            return new OrderResponse($"An error occurred when updating the order: {ex.Message}");
        }
    }

    public async Task<OrderResponse> Remove(int id)
    {
        var existingOrder = await _orderRepository.FindByIdAsync(id);
        if (existingOrder.Equals(null))
            return new OrderResponse("Order not found");
        
        try
        {
            _orderRepository.Remove(existingOrder);
            await _unitOfWork.CompleteAsync();

            return new OrderResponse(existingOrder);
        }
        catch (Exception ex)
        {
            return new OrderResponse($"An error occurred when deleting the order: {ex.Message}");
        }
    }
}