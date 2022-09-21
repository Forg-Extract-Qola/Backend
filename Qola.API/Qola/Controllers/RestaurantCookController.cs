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
[Route("api/v1/restaurant/cook/{restaurantId}")]
[Produces("application/json")]
public class RestaurantCookController : ControllerBase
{
    private readonly ICookService _cookService;
    private readonly IMapper _mapper;


    public RestaurantCookController(ICookService cookService, IMapper mapper)
    {
        _cookService = cookService;
        _mapper = mapper;
    }

    [HttpGet]
    [SwaggerOperation(
        Summary = "Get all cooks by restaurant id",
        Description = "Get all existing cooks By restaurant Id",
        OperationId = "GetAllCooksByRestaurantId",
        Tags = new[] {"Restaurant"})]
    public async Task<IActionResult> FindAllCooksByRestaurant(int restaurantId)
    {
        var cooks = await _cookService.FindCooksByRestaurantIdAsync(restaurantId);
        var resources = _mapper.Map<IEnumerable<Cook>, IEnumerable<CookResource>>(cooks);
        return Ok(resources);
    }
}