using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Qola.API.Qola.Domain.Models;
using Qola.API.Qola.Domain.Services;
using Qola.API.Qola.Resources;
using Qola.API.Security.Authorization.Attributes;
using Swashbuckle.AspNetCore.Annotations;

namespace Qola.API.Qola.Controllers;
[Authorize]
[ApiController]
[Route("api/v1/table/order/{tableId}/status/{status}/restaurant/{restaurantId}")]
[Produces("application/json")]
public class TableOrderController : ControllerBase
{
    private readonly IOrderService _orderService;
    private readonly IMapper _mapper;

    public TableOrderController(IOrderService orderService, IMapper mapper)
    {
        _orderService = orderService;
        _mapper = mapper;
    }
    [AllowAnonymous]
    [HttpGet]
    [SwaggerOperation(
        Summary = "Get all Order by Table id and Order Status and Restaurant Id",
        Description = "Get all existing Order by Table id and Order Status and Restaurant Id",
        OperationId = "GetAllOrderByTableIdAndOrderStatusAndRestaurantId",
        Tags = new[] {"Table"})]
    public async Task<IActionResult> FindByTableId(int tableId, string status, int restaurantId)
    {
        var orders = await _orderService.FindByTableIdAndStatusAndRestaurantIdAsync(tableId, status, restaurantId);
        var resources = _mapper.Map<IEnumerable<Order>, IEnumerable<OrderResource>>(orders);
        return Ok(resources);
    }
}