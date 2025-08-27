using Microsoft.AspNetCore.Http;
namespace Chefio.Application.Dtos.Dish
{
    public class DishCreateFormRequest
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; } 
        public IFormFile? Photo { get; set; }
    }
}