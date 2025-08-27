using Chefio.Application.Dtos.Dish;
using Chefio.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

[ApiController]
[Route("/dish")]
public class DishController : ControllerBase
{
    private readonly IDishService _service;

    public DishController(IDishService service)
    {
        _service = service;
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> Get(int page = 1, int pageSize = 10)
    {
        var dishes = await _service.GetAllAsync(page, pageSize);
        return Ok(new ApiResponse(ApiStatus.Success, "Lấy danh sách món ăn thành công", dishes));
    }

    [HttpGet("{id}")]
    [Authorize]
    public async Task<IActionResult> GetById(int id)
    {
        var dish = await _service.GetByIdAsync(id);
        if (dish == null)
            return NotFound(new ApiResponse(ApiStatus.Error, "Không tìm thấy món ăn"));

        return Ok(new ApiResponse(ApiStatus.Success, "Lấy thông tin món ăn thành công", new { dish }));
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> CreateDish([FromForm] DishCreateFormRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var result = await _service.CreateAsync(request);
            return Ok(new ApiResponse(ApiStatus.Success, "Thêm món ăn thành công", new { result }));
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new ApiResponse(ApiStatus.Error, ex.Message));
        }
    }

    [HttpPut("{id}")]
    [Authorize]
    public async Task<IActionResult> UpdateDish(int id, [FromForm] DishUpdateFormRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var result = await _service.UpdateAsync(id, request);
            if (result == null)
                return NotFound(new ApiResponse(ApiStatus.Error, "Không tìm thấy món ăn"));

            return Ok(new ApiResponse(ApiStatus.Success, "Cập nhật món ăn thành công", new { result }));
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new ApiResponse(ApiStatus.Error, ex.Message));
        }
    }

    [HttpDelete("{id}")]
    [Authorize]
    public async Task<IActionResult> DeleteDish(int id)
    {
        var deleted = await _service.DeleteAsync(id);
        if (!deleted)
            return NotFound(new ApiResponse(ApiStatus.Error, "Không tìm thấy món ăn"));

        return Ok(new ApiResponse(ApiStatus.Success, "Xóa món ăn thành công"));
    }
}