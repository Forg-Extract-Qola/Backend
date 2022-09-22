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
[Route("api/v1/waiter/order/{waiterId}")]
[Produces("application/json")]
public class WaiterOrderController : ControllerBase
{
    private readonly IOrderService _orderService;
    private readonly IMapper _mapper;

    public WaiterOrderController(IOrderService orderService, IMapper mapper)
    {
        _orderService = orderService;
        _mapper = mapper;
    }
    [AllowAnonymous]
    [HttpGet]
    [SwaggerOperation(
        Summary = "Get all Order by Waiter id",
        Description = "Get all existing Order by Waiter id",
        OperationId = "GetAllOrdersByWaiterId",
        Tags = new[] {"Waiter"})]
    public async Task<IActionResult> FindByWaiterId(int waiterId)
    {
        var orders = await _orderService.FindByWaiterIdAsync(waiterId);
        var resources = _mapper.Map<IEnumerable<Order>, IEnumerable<OrderResource>>(orders);
        return Ok(resources);
    }
}