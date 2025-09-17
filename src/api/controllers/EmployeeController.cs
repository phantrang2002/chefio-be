using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Swashbuckle.AspNetCore.Filters;
using Chefio.Api.Examples.Employee;
using Chefio.Application.Dtos.Employee;
using Chefio.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using System;
using Chefio.Application.Constants;


[ApiController]
[Route("/employee")]
public class EmployeeController : ControllerBase
{
    private readonly IEmployeeService _service;

    public EmployeeController(IEmployeeService service)
    {
        _service = service;
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> Get(int page = 1, int pageSize = 10)
    {
        var employees = await _service.GetAllAsync(page, pageSize);
        return Ok(new ApiResponse(ApiStatus.Success, ApiMessages.EMPLOYEE.LIST_SUCCESS.Message, employees));
    }

    [HttpGet("{id}")]
    [Authorize]
    public async Task<IActionResult> GetById(int id)
    {
        var employee = await _service.GetByIdAsync(id);
        if (employee == null)
            return NotFound(new ApiResponse(ApiStatus.Error, ApiMessages.EMPLOYEE.NOT_FOUND.Message));

        return Ok(new ApiResponse(ApiStatus.Success, ApiMessages.EMPLOYEE.GET_SUCCESS.Message, new { employee }));
    }

    [HttpPost]
    [Authorize]
    [SwaggerRequestExample(typeof(EmployeeCreateRequest), typeof(EmployeeCreateRequestExample))]
    public async Task<IActionResult> CreateEmployee([FromBody] EmployeeCreateRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var result = await _service.CreateAsync(request);
            return Ok(new ApiResponse(ApiStatus.Success, ApiMessages.EMPLOYEE.CREATE_SUCCESS.Message, new { result }));
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new ApiResponse(ApiStatus.Error, ex.Message));
        }
    }

    [HttpPut("{id}")]
    [Authorize]
    public async Task<IActionResult> UpdateEmployee(int id, [FromBody] EmployeeUpdateRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var result = await _service.UpdateAsync(id, request);
            if (result == null)
                return NotFound(new ApiResponse(ApiStatus.Error, ApiMessages.EMPLOYEE.NOT_FOUND.Message));

            return Ok(new ApiResponse(ApiStatus.Success, ApiMessages.EMPLOYEE.UPDATE_SUCCESS.Message, new { result }));
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new ApiResponse(ApiStatus.Error, ex.Message));
        }
    }
    
    [HttpDelete("{id}")]
    [Authorize]
    public async Task<IActionResult> DeleteEmployee(int id)
    {
        var deleted = await _service.DeleteAsync(id);
        if (!deleted)
            return NotFound(new ApiResponse(ApiStatus.Error, ApiMessages.EMPLOYEE.NOT_FOUND.Message));

        return Ok(new ApiResponse(ApiStatus.Success, ApiMessages.EMPLOYEE.DELETE_SUCCESS.Message));
    }
    
}
