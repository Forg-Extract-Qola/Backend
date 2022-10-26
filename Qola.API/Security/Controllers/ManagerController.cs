using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Qola.API.Security.Authorization.Attributes;
using Qola.API.Security.Domain.Models;
using Qola.API.Security.Domain.Services;
using Qola.API.Security.Domain.Services.Communication;
using Qola.API.Security.Resources;
using Swashbuckle.AspNetCore.Annotations;

namespace Qola.API.Security.Controllers;

[Authorize]
[ApiController]
[Route("api/v1/[controller]")]
[Produces("application/json")]
[SwaggerTag("Create, Read, Update and Delete a Manager")]
public class ManagerController: ControllerBase
{
    private readonly IManagerService _managerService;
    private readonly IMapper _mapper;

    public ManagerController(IManagerService managerService, IMapper mapper)
    {
        _managerService = managerService;
        _mapper = mapper;
    }
    
    [AllowAnonymous]
    [HttpPost("sign-in")]
    [SwaggerResponse(200, "The operation was success", typeof(ManagerResource))]
    [SwaggerResponse(400, "The client sent a bad request")]
    [SwaggerOperation(
        Summary = "Get a manager using their credentials",
        Description = "Get a manager using their credentials",
        OperationId = "SignIn",
        Tags = new[] { "Manager" })]
    public async Task<IActionResult> Authenticate(AuthenticateRequest request)
    {
        var response = await _managerService.AuthenticateAsync(request);
        return Ok(response);
    }
    
    [AllowAnonymous]
    [HttpPost("sign-up")]
    [SwaggerOperation(
        Summary = "Create a new manager",
        Description = "Create a new manager",
        OperationId = "SignUp",
        Tags = new[] { "Manager" })]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        await _managerService.RegisterAsync(request);
        return Ok(new { message = "Manager registered successfully" });
    }
    
    [HttpGet]
    [SwaggerOperation(
        Summary = "Get all managers",
        Description = "Get all managers",
        OperationId = "GetAll",
        Tags = new[] { "Manager" })]
    public async Task<IActionResult> GetAll()
    {
        var response = await _managerService.ListAsync();
        var resources = _mapper.Map<IEnumerable<Manager>, IEnumerable<ManagerResource>>(response);
        return Ok(resources);
    }
    
    [AllowAnonymous]
    [HttpGet("{id}")]
    [SwaggerOperation(
        Summary = "Get a manager by id",
        Description = "Get a manager by id",
        OperationId = "GetById",
        Tags = new[] { "Manager" })]
    public async Task<IActionResult> GetById(int id)
    {
        var response = await _managerService.FindByIdAsync(id);
        var resource = _mapper.Map<Manager, ManagerResource>(response);
        return Ok(resource);
    }
    
    [HttpPut("{id}")]
    [SwaggerOperation(
        Summary = "Update a manager",
        Description = "Update a manager",
        OperationId = "Update",
        Tags = new[] { "Manager" })]
    public async Task<IActionResult> Update(int id, UpdateRequest request)
    {
        await _managerService.UpdateAsync(id, request);
        return Ok(new { message = "Manager updated successfully" });
    }
    
    [HttpDelete("{id}")]
    [SwaggerOperation(
        Summary = "Delete a manager",
        Description = "Delete a manager",
        OperationId = "Delete",
        Tags = new[] { "Manager" })]
    public async Task<IActionResult> Delete(int id)
    {
        await _managerService.DeleteAsync(id);
        return Ok(new { message = "Manager deleted successfully" });
    }
}