using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Qola.API.Qola.Domain.Models;
using Qola.API.Qola.Domain.Repositories;
using Qola.API.Qola.Domain.Services;
using Qola.API.Qola.Resources;
using Qola.API.Security.Authorization.Attributes;
using Qola.API.Shared.Extensions;
using Swashbuckle.AspNetCore.Annotations;

namespace Qola.API.Qola.Controllers;

[Authorize]
[ApiController]
[Route("api/v1/[controller]")]
[Produces("application/json")]
[SwaggerTag("Create, Read, Update and Delete a OrderDishes")]
public class OrderDishesController : ControllerBase
{
    private readonly IOrderDishesService _orderDishesService;
    private readonly IDishRepository _dishRepository;
    private readonly IMapper _mapper;

    public OrderDishesController(IOrderDishesService orderDishesService, IMapper mapper, IDishRepository dishRepository)
    {
        _orderDishesService = orderDishesService;
        _mapper = mapper;
        _dishRepository = dishRepository;
    }
    [HttpGet("order/{orderId}")]
    [SwaggerOperation(
        Summary = "Get all Dishes for a Order id",
        Description = "Get all existing Dishes for a Order id",
        OperationId = "GetAllOrderDishesByOrderId",
        Tags = new[] {"OrderDishes"})]
    public async Task<IActionResult> FindAllByOrderIdAsync(int orderId)
    {
        var orderDishes = await _orderDishesService.FindByOrderId(orderId);
        var resources = _mapper.Map<IEnumerable<Dish>, IEnumerable<DishResource>>(orderDishes);
        return Ok(resources);
    }
    
    [HttpGet("restaurant/{restaurantId}")]
    [SwaggerOperation(
        Summary = "Get all OrderDishes",
        Description = "Get all existing OrderDishes",
        OperationId = "GetAllOrderDishes",
        Tags = new[] {"OrderDishes"})]
    public async Task<IActionResult> FindAllByOrderRestaurantIdAsync(int restaurantId)
    {
        var orderDishes = await _orderDishesService.FindAllByOrderRestaurantIdAsync(restaurantId);
        var resources = _mapper.Map<IEnumerable<OrderDishes>, IEnumerable<OrderDishesResource>>(orderDishes);
        return Ok(resources);
    }
    
    [HttpGet("{id}")]
    [SwaggerOperation(
        Summary = "Get a OrderDishes by Id",
        Description = "Get a existing OrderDishes by Id",
        OperationId = "GetOrderDishesById",
        Tags = new[] {"OrderDishes"})]
    public async Task<IActionResult> FindById(int id)
    {
        var orderDishes = await _orderDishesService.FindByIdAsync(id);
        var resources = _mapper.Map<OrderDishes,OrderDishesResource>(orderDishes);
        return Ok(resources);
    }
    
    [HttpPost]
    [SwaggerOperation(
        Summary = "Create a new OrderDishes",
        Description = "Create a new OrderDishes",
        OperationId = "CreateOrderDishes",
        Tags = new[] { "OrderDishes" })]
    [SwaggerResponse(200, "The operation was success", typeof(RestaurantResource))]
    [SwaggerResponse(400, "The OrderDishes is invalid")]
    public async Task<IActionResult> PostAsync([FromBody, SwaggerRequestBody("OrderDishes Information to Add", Required = true)] SaveOrderDishesResource resource, int orderId, int dishId)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());

        var orderDishes = _mapper.Map<SaveOrderDishesResource, OrderDishes>(resource);
        var result = await _orderDishesService.AddAsync(orderDishes, orderId, dishId);

        if (!result.Success)
            return BadRequest(result.Message);
        
        var orderDishesResource = _mapper.Map<OrderDishes, OrderDishesResource>(result.Resource);
        return Ok(orderDishesResource);
    }
    
    [HttpDelete]
    [SwaggerOperation(
        Summary = "Delete a OrderDishes",
        Description = "Delete a OrderDishes",
        OperationId = "DeleteOrderDishes",
        Tags = new[] { "OrderDishes" })]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        var result = await _orderDishesService.Delete(id);
        
        if (!result.Success)
            return BadRequest(result.Message);
        
        var orderDishesResource = _mapper.Map<OrderDishes, OrderDishesResource>(result.Resource);
        return Ok(orderDishesResource);
    }
}