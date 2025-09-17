using Chefio.Application.Dtos.Table;
using Chefio.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Chefio.Application.Constants;


[ApiController]
[Route("/table")]
public class TableController : ControllerBase
{
    private readonly ITableService _service;

    public TableController(ITableService service)
    {
        _service = service;
    }

    [HttpPost]
    [Authorize(Roles = "Admin,SuperAdmin")]
    public async Task<IActionResult> CreateTables([FromBody] TableCreateRequest request)
    {
        if (request.Quantity <= 0)
            return BadRequest(new ApiResponse(ApiStatus.Error, ApiMessages.TABLE.QUANTITY_INVALID.Message));

        try
        {
            var result = await _service.CreateTablesAsync(request.Quantity);
            return Ok(new ApiResponse(ApiStatus.Success, ApiMessages.TABLE.CREATE_SUCCESS.Message, result));
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new ApiResponse(ApiStatus.Error, ex.Message));
        }
    }

    [HttpPut("{id}")]
    [Authorize]
    public async Task<IActionResult> UpdateTableStatus(int id, [FromBody] TableUpdateStatusRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var result = await _service.UpdateStatusAsync(id, request.IsAvailable);
            if (result == null)
                return NotFound(new ApiResponse(ApiStatus.Error, ApiMessages.TABLE.NOT_FOUND.Message));

            return Ok(new ApiResponse(ApiStatus.Success, ApiMessages.TABLE.UPDATE_SUCCESS.Message, new { result }));
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new ApiResponse(ApiStatus.Error, ex.Message));
        }
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin,SuperAdmin")]
    public async Task<IActionResult> DeleteTable(int id)
    {
        var deleted = await _service.DeleteAsync(id);
        if (!deleted)
            return NotFound(new ApiResponse(ApiStatus.Error, ApiMessages.TABLE.NOT_FOUND.Message));

        return Ok(new ApiResponse(ApiStatus.Success, ApiMessages.TABLE.DELETE_SUCCESS.Message));
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> Get(int page = 1, int pageSize = 10)
    {
        var tables = await _service.GetAllAsync(page, pageSize);
        return Ok(new ApiResponse(ApiStatus.Success, ApiMessages.TABLE.LIST_SUCCESS.Message, tables));
    }

    [HttpGet("{id}")]
    [Authorize]
    public async Task<IActionResult> GetById(int id)
    {
        var table = await _service.GetByIdAsync(id);
        if (table == null)
            return NotFound(new ApiResponse(ApiStatus.Error, ApiMessages.TABLE.NOT_FOUND.Message));

        return Ok(new ApiResponse(ApiStatus.Success, ApiMessages.TABLE.GET_SUCCESS.Message, new { table }));
    }
}