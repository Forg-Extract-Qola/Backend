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
[Route("api/v1/restaurant/table/{restaurantId}")]
[Produces("application/json")]
public class RestaurantTable: ControllerBase
{
    private readonly ITableService _tableService;
    private readonly IMapper _mapper;

    public RestaurantTable(ITableService tableService, IMapper mapper)
    {
        _tableService = tableService;
        _mapper = mapper;
    }
    [AllowAnonymous]
    [HttpGet]
    [SwaggerOperation(
        Summary = "Get all table By restaurant Id",
        Description = "Get all existing tables By restaurant Id",
        OperationId = "GetAllTableByRestaurantId",
        Tags = new[] {"Restaurant"})]
    public async Task<IActionResult> FindTableByRestaurantId(int restaurantId)
    {
        var table = await _tableService.FindTablesByRestaurantIdAsync(restaurantId);
        var tableResource = _mapper.Map<IEnumerable<Table>, IEnumerable<TableResource>>(table);
        return Ok(tableResource);
    }
}