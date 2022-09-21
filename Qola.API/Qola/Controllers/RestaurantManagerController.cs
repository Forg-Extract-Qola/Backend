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
[Route("api/v1/restaurant/manager/{managerId}")]
[Produces("application/json")]
public class RestaurantManagerController: ControllerBase
{
    private readonly IRestaurantService _restaurantService;
    private readonly IMapper _mapper;

    public RestaurantManagerController( IMapper mapper, IRestaurantService restaurantService)
    {
        _mapper = mapper;
        _restaurantService = restaurantService;
    }
    
    [HttpGet]
    [SwaggerOperation(
        Summary = "Get all restaurants By Manager Id",
        Description = "Get all existing restaurants By Manager Id",
        OperationId = "GetRestaurantByManagerId",
        Tags = new[] {"Restaurant"})]
    public async Task<IActionResult> FindAllRestaurantsByManager(int managerId)
    {
        var restaurants = await _restaurantService.FindRestaurantByManagerAsync(managerId);
        var resources = _mapper.Map<Restaurant, RestaurantResource>(restaurants);
        return Ok(resources);
    }

}