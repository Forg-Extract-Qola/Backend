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
[Route("api/v1/restaurant/waiter/{restaurantId}")]
[Produces("application/json")]
public class RestaurantWaiterController: ControllerBase
{
    private readonly IWaiterService _waiterService;
    private readonly IMapper _mapper;

    public RestaurantWaiterController(IWaiterService waiterService, IMapper mapper)
    {
        _waiterService = waiterService;
        _mapper = mapper;
    }
    [AllowAnonymous]
    [HttpGet]
    [SwaggerOperation(
        Summary = "Get all waiter by restaurant id",
        Description = "Get all existing waiter By restaurant Id",
        OperationId = "GetAllWaiterByRestaurantId",
        Tags = new[] {"Restaurant"})]
    public async Task<IActionResult> FindAllWaitersByRestaurant(int restaurantId)
    {
        var cooks = await _waiterService.FindWaitersByRestaurantIdAsync(restaurantId);
        var resources = _mapper.Map<IEnumerable<Waiter>, IEnumerable<WaiterResource>>(cooks);
        return Ok(resources);
    }
}