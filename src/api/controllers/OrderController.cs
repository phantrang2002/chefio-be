using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Swashbuckle.AspNetCore.Filters;
using Chefio.Api.Examples.Order;
using Chefio.Application.Dtos.Order;
using Chefio.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using System;
using Chefio.Application.Constants;

[ApiController]
[Route("/order")]
public class OrderController : ControllerBase
{
    private readonly IOrderService _service;

    public OrderController(IOrderService service)
    {
        _service = service;
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> Get(int page = 1, int pageSize = 10)
    {
        var orders = await _service.GetAllAsync(page, pageSize);
        return Ok(new ApiResponse(ApiStatus.Success, ApiMessages.CATEGORY.LIST_SUCCESS.Message, orders));
    }

    [HttpGet("{id}")]
    [Authorize]
    public async Task<IActionResult> GetById(int id)
    {
        var order = await _service.GetByIdAsync(id);
        if (order == null)
            return NotFound(new ApiResponse(ApiStatus.Error, ApiMessages.CATEGORY.NOT_FOUND.Message));

        return Ok(new ApiResponse(ApiStatus.Success, ApiMessages.CATEGORY.GET_SUCCESS.Message, new { order }));
    }

    [HttpPost]
    [Authorize]
    [SwaggerRequestExample(typeof(OrderCreateRequest), typeof(OrderCreateRequestExample))]
    public async Task<IActionResult> CreateOrder([FromBody] OrderCreateRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var result = await _service.CreateAsync(request);
            return Ok(new ApiResponse(ApiStatus.Success, ApiMessages.ORDER.CREATE_SUCCESS.Message, new { result }));
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new ApiResponse(ApiStatus.Error, ex.Message));
        }
    }

    [HttpPut("{id}")]
    [Authorize]
    public async Task<IActionResult> UpdateOrder(int id, [FromBody] OrderUpdateRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var result = await _service.UpdateAsync(id, request);
            if (result == null)
                return NotFound(new ApiResponse(ApiStatus.Error, ApiMessages.CATEGORY.NOT_FOUND.Message));

            return Ok(new ApiResponse(ApiStatus.Success, ApiMessages.CATEGORY.UPDATE_SUCCESS.Message, new { result }));
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new ApiResponse(ApiStatus.Error, ex.Message));
        }
    }
    
    [HttpDelete("{id}")]
    [Authorize]
    public async Task<IActionResult> DeleteOrder(int id)
    {
        var deleted = await _service.DeleteAsync(id);
        if (!deleted)
            return NotFound(new ApiResponse(ApiStatus.Error, ApiMessages.CATEGORY.NOT_FOUND.Message));

        return Ok(new ApiResponse(ApiStatus.Success, ApiMessages.CATEGORY.DELETE_SUCCESS.Message));
    }
    
}
