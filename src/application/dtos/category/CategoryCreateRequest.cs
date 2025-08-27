using System.ComponentModel.DataAnnotations;

namespace Chefio.Application.Dtos.Category;

public class CategoryCreateRequest
{
    [Required(ErrorMessage = "Cần thêm tên danh mục")]
    public string Name { get; set; }
}