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
[Route("api/v1/restaurant/dish/{restaurantId}")]
[Produces("application/json")]
public class RestaurantDishController :ControllerBase
{
    private readonly IDishService _dishService;
    private readonly IMapper _mapper;

    public RestaurantDishController(IDishService dishService, IMapper mapper)
    {
        _dishService = dishService;
        _mapper = mapper;
    }
    
    [HttpGet]
    [SwaggerOperation(
        Summary = "Get all dish by restaurant id",
        Description = "Get all existing dish By restaurant Id",
        OperationId = "GetAllDishByRestaurantId",
        Tags = new[] {"Restaurant"})]
    public async Task<IActionResult> FindAllDishesByRestaurant(int restaurantId)
    {
        var dishes = await _dishService.FindByRestaurantIdAsync(restaurantId);
        var resources = _mapper.Map<IEnumerable<Dish>, IEnumerable<DishResource>>(dishes);
        return Ok(resources);
    }
}