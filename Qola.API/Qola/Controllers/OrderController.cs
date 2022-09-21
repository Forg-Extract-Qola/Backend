using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Qola.API.Qola.Domain.Models;
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
[SwaggerTag("Create, Read, Update and Delete a Order")]
public class OrderController : ControllerBase
{
    private readonly IOrderService _orderService;
    private readonly IMapper _mapper;

    public OrderController(IOrderService orderService, IMapper mapper)
    {
        _orderService = orderService;
        _mapper = mapper;
    }
    
    [HttpGet]
    [SwaggerOperation(
        Summary = "Get all Orders",
        Description = "Get all existing Orders",
        OperationId = "GetAllOrders",
        Tags = new[] {"Order"})]
    public async Task<IActionResult> GetAllAsync()
    {
        var orders = await _orderService.ListAsync();
        var resources = _mapper.Map<IEnumerable<Order>, IEnumerable<OrderResource>>(orders);
        return Ok(resources);
    }
    
    [HttpGet("status/{status}/restaurant/{restaurantId}")]
    [SwaggerOperation(
        Summary = "Get all Orders by Status and Restaurant id",
        Description = "Get all existing Orders by Status and Restaurant id",
        OperationId = "GetAllOrdersByStatusAndRestaurantId",
        Tags = new[] {"Order"})]
    public async Task<IActionResult> FindByStatusAndRestaurantId(string status, int restaurantId)
    {
        var orders = await _orderService.FindByStatusAndRestaurantIdAsync(status, restaurantId);
        var resources = _mapper.Map<IEnumerable<Order>, IEnumerable<OrderResource>>(orders);
        return Ok(resources);
    }

    [HttpGet("{id}")]
    [SwaggerOperation(
        Summary = "Get order by id",
        Description = "Get order existing by id",
        OperationId = "GetOrderById",
        Tags = new[] {"Order"})]
    public async Task<IActionResult> FindById(int id)
    {
        var order = await _orderService.FindByIdAsync(id);
        var resources = _mapper.Map<Order,OrderResource>(order);
        return Ok(resources);
    }
    
    [HttpPost]
    [SwaggerOperation(
        Summary = "Create a new order",
        Description = "Create a new order",
        OperationId = "CreateOrder",
        Tags = new[] { "Order" })]
    [SwaggerResponse(200, "The operation was success", typeof(RestaurantResource))]
    [SwaggerResponse(400, "The order is invalid")]
    public async Task<IActionResult> PostAsync([FromBody, SwaggerRequestBody("Order Information to Add", Required = true)] SaveOrderResource resource, 
        int waiterId, int tableId, int restaurantId)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());

        var order = _mapper.Map<SaveOrderResource, Order>(resource);
        var result = await _orderService.AddAsync(order, waiterId, tableId, restaurantId);

        if (!result.Success)
            return BadRequest(result.Message);

        var orderResource = _mapper.Map<Order, OrderResource>(result.Resource);
        return Ok(orderResource);
    }
    
    [HttpPut("{id}")]
    [SwaggerOperation(
        Summary = "Update a order",
        Description = "Update a existing order",
        OperationId = "UpdateOrder",
        Tags = new[] { "Order" }
    )]
    public async Task<IActionResult> PutAsync(int id, [FromBody] SaveOrderResource resource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());

        var order = _mapper.Map<SaveOrderResource, Order>(resource);
        var result = await _orderService.Update(id, order);

        if (!result.Success)
            return BadRequest(result.Message);

        var orderResource = _mapper.Map<Order, OrderResource>(result.Resource);
        return Ok(orderResource);
    }
    
    [HttpDelete("{id}")]
    [SwaggerOperation(
        Summary = "Delete a order",
        Description = "Delete a existing order",
        OperationId = "DeleteOrder",
        Tags = new[] { "Order" }
    )]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        var result = await _orderService.Remove(id);

        if (!result.Success)
            return BadRequest(result.Message);

        var orderResource = _mapper.Map<Order, OrderResource>(result.Resource);
        return Ok(orderResource);
    }
}