﻿using AutoMapper;
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
[SwaggerTag("Create, Read, Update and Delete a Table")]
public class TableController: ControllerBase
{
    private readonly ITableService _tableService;
    private readonly IMapper _mapper;

    public TableController(ITableService tableService, IMapper mapper)
    {
        _tableService = tableService;
        _mapper = mapper;
    }
    
    [HttpGet]
    [SwaggerOperation(
        Summary = "Get all Tables",
        Description = "Get all existing Tables",
        OperationId = "GetAllTables",
        Tags = new[] {"Table"})]
    public async Task<IActionResult> GetAllAsync()
    {
        var tables = await _tableService.ListAsync();
        var resources = _mapper.Map<IEnumerable<Table>, IEnumerable<TableResource>>(tables);
        return Ok(resources);
    }
    
    [HttpGet("available")]
    [SwaggerOperation(
        Summary = "Get all tables that are unoccupied",
        Description = "Get all existing tables that are unoccupied",
        OperationId = "GetAllAvailableTables",
        Tags = new[] {"Table"})]
    public async Task<IActionResult> GetAllAvailableAsync()
    {
        var tables = await _tableService.ListTableIsOccupiedAsync();
        var resources = _mapper.Map<IEnumerable<Table>, IEnumerable<TableResource>>(tables);
        return Ok(resources);
    }
   
    
    [HttpGet("{id}")]
    [SwaggerOperation(
        Summary = "Get table by id",
        Description = "Get table existing by id",
        OperationId = "GetTableById",
        Tags = new[] {"Table"})]
    public async Task<IActionResult> FindById(int id)
    {
        var table = await _tableService.FindByIdAsync(id);
        var resources = _mapper.Map<Table,TableResource>(table);
        return Ok(resources);
    }
    
    [HttpPost]
    [SwaggerOperation(
        Summary = "Create a new table",
        Description = "Create a new table",
        OperationId = "CreateTable",
        Tags = new[] { "Table" })]
    [SwaggerResponse(200, "The operation was success", typeof(RestaurantResource))]
    [SwaggerResponse(400, "The table is invalid")]
    public async Task<IActionResult> PostAsync([FromBody, SwaggerRequestBody("Table Information to Add", Required = true)] SaveTableResource resource, int restaurantId)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());

        var table = _mapper.Map<SaveTableResource, Table>(resource);
        var result = await _tableService.SaveAsync(table, restaurantId);

        if (!result.Success)
            return BadRequest(result.Message);

        var tableResource = _mapper.Map<Table, TableResource>(result.Resource);
        return Ok(tableResource);
    }
    
    [HttpPut("{id}")]
    [SwaggerOperation(
        Summary = "Update a table",
        Description = "Update a existing table",
        OperationId = "UpdateTable",
        Tags = new[] { "Table" }
    )]
    public async Task<IActionResult> PutAsync(int id, [FromBody] SaveTableResource resource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());

        var table = _mapper.Map<SaveTableResource, Table>(resource);
        var result = await _tableService.UpdateAsync(id, table);

        if (!result.Success)
            return BadRequest(result.Message);

        var tableResource = _mapper.Map<Table, TableResource>(result.Resource);
        return Ok(tableResource);
    }
    
    [HttpDelete("{id}")]
    [SwaggerOperation(
        Summary = "Delete a table",
        Description = "Delete a existing table",
        OperationId = "DeleteTable",
        Tags = new[] { "Table" }
    )]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        var result = await _tableService.DeleteAsync(id);

        if (!result.Success)
            return BadRequest(result.Message);

        var tableResource = _mapper.Map<Table, TableResource>(result.Resource);
        return Ok(tableResource);
    }
}