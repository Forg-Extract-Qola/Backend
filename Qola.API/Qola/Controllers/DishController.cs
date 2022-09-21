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
[SwaggerTag("Create, Read, Update and Delete a Dish")]
public class DishController: ControllerBase
{
    private readonly IDishService _dishService;
    private readonly IMapper _mapper;


    public DishController(IDishService dishService, IMapper mapper)
    {
        _dishService = dishService;
        _mapper = mapper;
    }
    
    [HttpGet]
    [SwaggerOperation(
        Summary = "Get all Dishes",
        Description = "Get all existing Dishes",
        OperationId = "GetAllDishes",
        Tags = new[] {"Dish"})]
    public async Task<IActionResult> GetAllAsync()
    {
        var dishes = await _dishService.ListAsync();
        var resources = _mapper.Map<IEnumerable<Dish>, IEnumerable<DishResource>>(dishes);
        return Ok(resources);
    }
    
    [HttpGet ("category/{categoryName}/restaurant/{restaurantId}")]
    [SwaggerOperation(
        Summary = "Get all dish by category name and restaurant id",
        Description = "Get all existing dish By category name and restaurant id",
        OperationId = "GetAllDishByCategoryNameAndRestaurantId",
        Tags = new[] {"Dish"})]
    public async Task<IActionResult> FindAllDishesByRestaurant(string categoryName,int restaurantId)
    {
        var dishes = await _dishService.FindByCategoryNameAndRestaurantIdAsync(categoryName,restaurantId);
        var resources = _mapper.Map<IEnumerable<Dish>, IEnumerable<DishResource>>(dishes);
        return Ok(resources);
    }
    
    [HttpGet("{id}")]
    [SwaggerOperation(
        Summary = "Get dish by id",
        Description = "Get dish existing by id",
        OperationId = "GetDishById",
        Tags = new[] {"Dish"})]
    public async Task<IActionResult> FindById(int id)
    {
        var dish = await _dishService.FindByIdAsync(id);
        var resources = _mapper.Map<Dish,DishResource>(dish);
        return Ok(resources);
    }
    
    [HttpPost]
    [SwaggerOperation(
        Summary = "Create a new dish",
        Description = "Create a new dish",
        OperationId = "CreateDish",
        Tags = new[] { "Dish" })]
    [SwaggerResponse(200, "The operation was success", typeof(RestaurantResource))]
    [SwaggerResponse(400, "The dish is invalid")]
    public async Task<IActionResult> PostAsync([FromBody, SwaggerRequestBody("Dish Information to Add", Required = true)] SaveDishResource resource, int restaurantid)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());

        var dish = _mapper.Map<SaveDishResource, Dish>(resource);
        var result = await _dishService.AddAsync(dish, restaurantid);

        if (!result.Success)
            return BadRequest(result.Message);

        var dishResource = _mapper.Map<Dish, DishResource>(result.Resource);
        return Ok(dishResource);
    }
    
    [HttpPut("{id}")]
    [SwaggerOperation(
        Summary = "Update a dish",
        Description = "Update a existing dish",
        OperationId = "UpdateDish",
        Tags = new[] { "Dish" }
    )]
    public async Task<IActionResult> PutAsync(int id, [FromBody] SaveDishResource resource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());

        var dish = _mapper.Map<SaveDishResource, Dish>(resource);
        var result = await _dishService.Update(id, dish);

        if (!result.Success)
            return BadRequest(result.Message);

        var dishResource = _mapper.Map<Dish, DishResource>(result.Resource);
        return Ok(dishResource);
    }
    
    [HttpDelete("{id}")]
    [SwaggerOperation(
        Summary = "Delete a dish",
        Description = "Delete a existing dish",
        OperationId = "DeleteDish",
        Tags = new[] { "Dish" }
    )]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        var dishResponse = await _dishService.Remove(id);

        if (!dishResponse.Success)
            return BadRequest(dishResponse.Message);

        var dishResource = _mapper.Map<Dish, DishResource>(dishResponse.Resource);
        return Ok(dishResource);
    }
}