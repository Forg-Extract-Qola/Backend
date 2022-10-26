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
[SwaggerTag("Create, Read, Update and Delete a Restaurant")]
public class RestaurantController: ControllerBase
{
    private readonly IRestaurantService _restaurantService;
    private readonly IMapper _mapper;

    public RestaurantController( IMapper mapper, IRestaurantService restaurantService)
    {
        _mapper = mapper;
        _restaurantService = restaurantService;
    }
    [AllowAnonymous]
    [HttpGet]
    [SwaggerOperation(
        Summary = "Get all restaurant",
        Description = "Get all existing restaurant",
        OperationId = "GetRestaurant",
        Tags = new[] {"Restaurant"})]
    public async Task<IActionResult> GetAllAsync()
    {
        var restaurants = await _restaurantService.ListAsync();
        var resources = _mapper.Map<IEnumerable<Restaurant>, IEnumerable<RestaurantResource>>(restaurants);
        return Ok(resources);
    }
    [AllowAnonymous]
    [HttpGet("{id}")]
    [SwaggerOperation(
        Summary = "Get all restaurants By Id",
        Description = "Get all existing restaurants By Id",
        OperationId = "GetRestaurantById",
        Tags = new[] {"Restaurant"})]
    public async Task<IActionResult> FindById(int id)
    {
        var restaurants = await _restaurantService.FindByIdAsync(id);
        var resources = _mapper.Map<Restaurant,RestaurantResource>(restaurants);
        return Ok(resources);
    }
    
    [HttpPost]
    [SwaggerOperation(
        Summary = "Create a new restaurant",
        Description = "Create a new restaurant",
        OperationId = "CreateRestaurant",
        Tags = new[] { "Restaurant" }
    )]
    
    [SwaggerResponse(200, "The operation was success", typeof(RestaurantResource))]
    [SwaggerResponse(400, "The category is invalid")]
    public async Task<IActionResult> PostAsync([FromBody, SwaggerRequestBody("Restaurant Information to Add", Required = true)] SaveRestaurantResource resource, int managerId)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());

        var restaurant = _mapper.Map<SaveRestaurantResource, Restaurant>(resource);
        var result = await _restaurantService.SaveAsync(restaurant, managerId);

        if (!result.Success)
            return BadRequest(result.Message);

        var categoryResource = _mapper.Map<Restaurant, RestaurantResource>(result.Resource);
        return Ok(categoryResource);
    }
    
    [HttpPut("{id}")]
    [SwaggerOperation(
        Summary = "Update a restaurant",
        Description = "Update a existing restaurant",
        OperationId = "UpdateRestaurant",
        Tags = new[] { "Restaurant" }
    )]
    public async Task<IActionResult> PutAsync(int id, [FromBody] SaveRestaurantResource resource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());

        var category = _mapper.Map<SaveRestaurantResource, Restaurant>(resource);
        var result = await _restaurantService.UpdateAsync(id, category);

        if (!result.Success)
            return BadRequest(result.Message);

        var categoryResource = _mapper.Map<Restaurant, RestaurantResource>(result.Resource);
        return Ok(categoryResource);
    }
    
    [HttpDelete("{id}")]
    [SwaggerOperation(
        Summary = "Delete a restaurant",
        Description = "Delete a existing restaurant",
        OperationId = "DeleteRestaurant",
        Tags = new[] { "Restaurant" }
    )]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        var result = await _restaurantService.DeleteAsync(id);

        if (!result.Success)
            return BadRequest(result.Message);

        var categoryResource = _mapper.Map<Restaurant, RestaurantResource>(result.Resource);
        return Ok(categoryResource);
    }

}