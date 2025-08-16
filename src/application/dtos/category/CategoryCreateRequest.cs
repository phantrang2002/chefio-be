using System.ComponentModel.DataAnnotations;

namespace Chefio.Application.Dtos.Category;

public class CategoryCreateRequest
{
    [Required(ErrorMessage = "Name is required.")]
    public string Name { get; set; }
}