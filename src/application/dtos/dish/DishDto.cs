namespace Chefio.Application.Dtos.Dish
{
    public class DishDto
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public string? Photo { get; set; }
        public decimal Price { get; set; }
        public bool IsAvailable { get; set; }
    }
}