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
[SwaggerTag("Create, Read, Update and Delete a Cook")]
public class CookController: ControllerBase
{
    private readonly ICookService _cookService;
    private readonly IMapper _mapper;

    public CookController(ICookService cookService, IMapper mapper)
    {
        _cookService = cookService;
        _mapper = mapper;
    }
    
    [HttpGet]
    [SwaggerOperation(
        Summary = "Get all Cooks",
        Description = "Get all existing Cooks",
        OperationId = "GetAllCooks",
        Tags = new[] {"Cook"})]
    public async Task<IActionResult> GetAllAsync()
    {
        var cook = await _cookService.ListAsync();
        var resources = _mapper.Map<IEnumerable<Cook>, IEnumerable<CookResource>>(cook);
        return Ok(resources);
    }
    
    [HttpGet("{id}")]
    [SwaggerOperation(
        Summary = "Get cook by id",
        Description = "Get cook existing by id",
        OperationId = "GetCookById",
        Tags = new[] {"Cook"})]
    public async Task<IActionResult> FindById(int id)
    {
        var cook = await _cookService.FindByIdAsync(id);
        var resources = _mapper.Map<Cook,CookResource>(cook);
        return Ok(resources);
    }
    [AllowAnonymous]
    [HttpGet("uid/{uid}")]
    [SwaggerOperation(
        Summary = "Get cook by uid",
        Description = "Get cook existing by uid",
        OperationId = "GetCookByUid",
        Tags = new[] {"Cook"})]
    public async Task<IActionResult> FindByUid([FromRoute]string uid)
    {
        var cook = await _cookService.FindByUIdAsync(uid);
        var resources = _mapper.Map<Cook,CookResource>(cook);
        return Ok(resources);
    }
    
    [HttpPost]
    [SwaggerOperation(
        Summary = "Create a new cook",
        Description = "Create a new cook",
        OperationId = "CreateCook",
        Tags = new[] { "Cook" })]
    [SwaggerResponse(200, "The operation was success", typeof(RestaurantResource))]
    [SwaggerResponse(400, "The cook is invalid")]
    public async Task<IActionResult> PostAsync([FromBody, SwaggerRequestBody("Cook Information to Add", Required = true)] SaveCookResource resource, int restaurantid)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());

        var cook = _mapper.Map<SaveCookResource, Cook>(resource);
        var result = await _cookService.SaveAsync(cook, restaurantid);

        if (!result.Success)
            return BadRequest(result.Message);

        var cookResource = _mapper.Map<Cook, CookResource>(result.Resource);
        return Ok(cookResource);
    }
    
    [HttpPut("{id}")]
    [SwaggerOperation(
        Summary = "Update a cook",
        Description = "Update a existing cook",
        OperationId = "UpdateCook",
        Tags = new[] { "Cook" }
    )]
    public async Task<IActionResult> PutAsync(int id, [FromBody] SaveCookResource resource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());

        var cook = _mapper.Map<SaveCookResource, Cook>(resource);
        var result = await _cookService.UpdateAsync(id, cook);

        if (!result.Success)
            return BadRequest(result.Message);

        var cookResource = _mapper.Map<Cook, CookResource>(result.Resource);
        return Ok(cookResource);
    }
    
    [HttpDelete("{id}")]
    [SwaggerOperation(
        Summary = "Delete a cook",
        Description = "Delete a existing cook",
        OperationId = "DeleteCook",
        Tags = new[] { "Cook" }
    )]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        var result = await _cookService.DeleteAsync(id);

        if (!result.Success)
            return BadRequest(result.Message);

        var cookResource = _mapper.Map<Cook, CookResource>(result.Resource);
        return Ok(cookResource);
    }
}