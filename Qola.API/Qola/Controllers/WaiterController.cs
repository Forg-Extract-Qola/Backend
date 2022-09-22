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
[SwaggerTag("Create, Read, Update and Delete a Waiter")]
public class WaiterController: ControllerBase
{
    private readonly IWaiterService _waiterService;
    private readonly IMapper _mapper;

    public WaiterController(IWaiterService waiterService, IMapper mapper)
    {
        _waiterService = waiterService;
        _mapper = mapper;
    }
    
      [HttpGet]
    [SwaggerOperation(
        Summary = "Get all Waiters",
        Description = "Get all existing Waiters",
        OperationId = "GetAllWaiters",
        Tags = new[] {"Waiter"})]
    public async Task<IActionResult> GetAllAsync()
    {
        var waiters = await _waiterService.ListAsync();
        var resources = _mapper.Map<IEnumerable<Waiter>, IEnumerable<WaiterResource>>(waiters);
        return Ok(resources);
    }
    [AllowAnonymous]
    [HttpGet("{id}")]
    [SwaggerOperation(
        Summary = "Get a waiter by Id",
        Description = "Get waiter existing by id",
        OperationId = "GetWaiterById",
        Tags = new[] {"Waiter"})]
    public async Task<IActionResult> FindById(int id)
    {
        var waiter = await _waiterService.FindByIdAsync(id);
        var resources = _mapper.Map<Waiter,WaiterResource>(waiter);
        return Ok(resources);
    }
    
    [AllowAnonymous]
    [HttpGet("uid/{uid}")]
    [SwaggerOperation(
        Summary = "Get waiter by uid",
        Description = "Get waiter existing by uid",
        OperationId = "GetWaiterByUid",
        Tags = new[] {"Waiter"})]
    public async Task<IActionResult> FindByUid([FromRoute]string uid)
    {
        var waiter = await _waiterService.FindByUIdAsync(uid);
        var resources = _mapper.Map<Waiter,WaiterResource>(waiter);
        return Ok(resources);
    }
    
    [HttpPost]
    [SwaggerOperation(
        Summary = "Create a new waiter",
        Description = "Create a new waiter",
        OperationId = "CreateWaiter",
        Tags = new[] { "Waiter" })]
    [SwaggerResponse(200, "The operation was success", typeof(RestaurantResource))]
    [SwaggerResponse(400, "The water is invalid")]
    public async Task<IActionResult> PostAsync([FromBody, SwaggerRequestBody("Waiter Information to Add", Required = true)] SaveWaiterResource resource, int restaurantid)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());

        var waiter = _mapper.Map<SaveWaiterResource, Waiter>(resource);
        var result = await _waiterService.SaveAsync(waiter, restaurantid);

        if (!result.Success)
            return BadRequest(result.Message);

        var waiterResource = _mapper.Map<Waiter, WaiterResource>(result.Resource);
        return Ok(waiterResource);
    }
    
    [HttpPut("{id}")]
    [SwaggerOperation(
        Summary = "Update a waiter",
        Description = "Update a existing waiter",
        OperationId = "UpdateWaiter",
        Tags = new[] { "Waiter" }
    )]
    public async Task<IActionResult> PutAsync(int id, [FromBody] SaveWaiterResource resource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());

        var waiter = _mapper.Map<SaveWaiterResource, Waiter>(resource);
        var result = await _waiterService.UpdateAsync(id, waiter);

        if (!result.Success)
            return BadRequest(result.Message);

        var waiterResource = _mapper.Map<Waiter, WaiterResource>(result.Resource);
        return Ok(waiterResource);
    }
    
    [HttpDelete("{id}")]
    [SwaggerOperation(
        Summary = "Delete a waiter",
        Description = "Delete a existing waiter",
        OperationId = "DeleteWaiter",
        Tags = new[] { "Waiter" }
    )]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        var result = await _waiterService.DeleteAsync(id);

        if (!result.Success)
            return BadRequest(result.Message);

        var waiterResource = _mapper.Map<Waiter, WaiterResource>(result.Resource);
        return Ok(waiterResource);
    }
    
}