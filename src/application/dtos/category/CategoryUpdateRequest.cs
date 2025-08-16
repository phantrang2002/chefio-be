using System.ComponentModel.DataAnnotations;

namespace Chefio.Application.Dtos.Category;

public class CategoryUpdateRequest
{
    [Required(ErrorMessage = "Name is required.")]
    public string Name { get; set; }
}