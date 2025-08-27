using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Swashbuckle.AspNetCore.Filters;
using Chefio.Api.Examples.Category;
using Chefio.Application.Dtos.Category;
using Chefio.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using System;

[ApiController]
[Route("/category")]
public class CategoryController : ControllerBase
{
    private readonly ICategoryService _service;

    public CategoryController(ICategoryService service)
    {
        _service = service;
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> Get(int page = 1, int pageSize = 10)
    {
        var categories = await _service.GetAllAsync(page, pageSize);
        return Ok(new ApiResponse(ApiStatus.Success, "Lấy danh sách danh mục thành công", categories));
    }

    [HttpGet("{id}")]
    [Authorize]
    public async Task<IActionResult> GetById(int id)
    {
        var category = await _service.GetByIdAsync(id);
        if (category == null)
            return NotFound(new ApiResponse(ApiStatus.Error, "Không tìm thấy danh mục"));

        return Ok(new ApiResponse(ApiStatus.Success, "Lấy thông tin danh mục thành công", new { category }));
    }

    [HttpPost]
    [Authorize]
    [SwaggerRequestExample(typeof(CategoryCreateRequest), typeof(CategoryCreateRequestExample))]
    public async Task<IActionResult> CreateCategory([FromBody] CategoryCreateRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var result = await _service.CreateAsync(request);
            return Ok(new ApiResponse(ApiStatus.Success, "Thêm danh mục thành công", new { result }));
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new ApiResponse(ApiStatus.Error, ex.Message));
        }
    }

    [HttpPut("{id}")]
    [Authorize]
    public async Task<IActionResult> UpdateCategory(int id, [FromBody] CategoryUpdateRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var result = await _service.UpdateAsync(id, request);
            if (result == null)
                return NotFound(new ApiResponse(ApiStatus.Error, "Không tìm thấy danh mục"));

            return Ok(new ApiResponse(ApiStatus.Success, "Cập nhật danh mục thành công", new { result }));
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new ApiResponse(ApiStatus.Error, ex.Message));
        }
    }
    
    [HttpDelete("{id}")]
    [Authorize]
    public async Task<IActionResult> DeleteCategory(int id)
    {
        var deleted = await _service.DeleteAsync(id);
        if (!deleted)
            return NotFound(new ApiResponse(ApiStatus.Error, "Không tìm thấy danh mục"));

        return Ok(new ApiResponse(ApiStatus.Success, "Xóa danh mục thành công"));
    }
    
}
